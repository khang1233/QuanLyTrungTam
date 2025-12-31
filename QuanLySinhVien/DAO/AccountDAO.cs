using QuanLyTrungTam.DTO;
using System;
using System.Data;

namespace QuanLyTrungTam.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;
        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
        }
        private AccountDAO() { }

        // =================================================================================
        // 1. XỬ LÝ ĐĂNG NHẬP (HỖ TRỢ CẢ NHÂN SỰ)
        // =================================================================================
        // File: DAO/AccountDAO.cs
        public bool Login(string userName, string passWord, string role)
        {
            string query = "";
            object[] parameters = null;

            // [FIX LỖI QUAN TRỌNG]: Thêm điều kiện kiểm tra trạng thái linh hoạt hơn
            // Chấp nhận: 1, Hoạt động, Active, Chờ xếp lớp (cho học viên mới)
            string conditionStatus = " AND (TrangThai = '1' OR TrangThai = N'Hoạt động' OR TrangThai = N'Active' OR TrangThai = N'Chờ xếp lớp' OR TrangThai = N'Đang học')";

            if (role == "NhanSu")
            {
                // Nếu là Nhân sự: Kiểm tra Giáo viên HOẶC Trợ giảng
                query = "SELECT * FROM TaiKhoan WHERE TenDangNhap = @u AND MatKhau = @p AND (Quyen = 'GiaoVien' OR Quyen = 'TroGiang')" + conditionStatus;
                parameters = new object[] { userName, passWord }; // Chỉ cần 2 tham số
            }
            else
            {
                // Nếu là Admin hoặc Học viên: Kiểm tra chính xác quyền
                query = "SELECT * FROM TaiKhoan WHERE TenDangNhap = @u AND MatKhau = @p AND Quyen = @r" + conditionStatus;
                parameters = new object[] { userName, passWord, role }; // Cần 3 tham số
            }

            DataTable result = DataProvider.Instance.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                LogLogin(userName, "Thành công", "Đăng nhập bằng mật khẩu");
                return true;
            }
            else
            {
                // LogLogin(userName, "Thất bại", "Sai mật khẩu hoặc vai trò");
                return false;
            }
        }

        public bool LoginGoogle(string email)
        {
            try
            {
                string queryFind = "SELECT t.* FROM TaiKhoan t JOIN HocVien h ON t.MaNguoiDung = h.MaHV WHERE h.Email = @email";
                DataTable result = DataProvider.Instance.ExecuteQuery(queryFind, new object[] { email });

                if (result.Rows.Count > 0)
                {
                    LogLogin(email, "Thành công", "Đăng nhập Google");
                    return true;
                }
                else
                {
                    string maHV = "HV" + DateTime.Now.Ticks.ToString().Substring(10);
                    // Tạo tài khoản mới tự động
                    DataProvider.Instance.ExecuteNonQuery("INSERT INTO HocVien (MaHV, Email, HoTen) VALUES ( @id , @email , N'Học viên Google' )", new object[] { maHV, email });
                    DataProvider.Instance.ExecuteNonQuery("INSERT INTO TaiKhoan (TenDangNhap, MatKhau, Quyen, MaNguoiDung, TrangThai) VALUES ( @email , 'google' , 'HocVien' , @maND , 1 )", new object[] { email, maHV });

                    LogLogin(email, "Thành công", "Tạo mới qua Google");
                    return true;
                }
            }
            catch { return false; }
        }

        // =================================================================================
        // 2. CÁC HÀM QUẢN LÝ (SỬA ĐỂ HẾT LỖI Ở FORM ADMIN)
        // =================================================================================

        // Hàm chuẩn mới (dùng bool)
        public bool UpdateStatus(string user, bool isActive)
        {
            // Sửa lại lưu trạng thái là chuỗi 'Hoạt động' cho đồng bộ
            string status = isActive ? "Hoạt động" : "Bị khóa";
            string query = "UPDATE TaiKhoan SET TrangThai = @stt WHERE TenDangNhap = @user";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { status, user }) > 0;
        }

        // [FIX LỖI 1] Thêm hàm này để tương thích với FrmSystemAdmin cũ (đang truyền int)
        public bool UpdateStatus(string user, int status)
        {
            return UpdateStatus(user, status == 1);
        }

        public bool ResetPassword(string userName)
        {
            string query = "UPDATE TaiKhoan SET MatKhau = '123' WHERE TenDangNhap = @user";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { userName }) > 0;
        }

        // [FIX LỖI 2] Thêm hàm tên cũ "ResetPass" trỏ về hàm mới
        public bool ResetPass(string user)
        {
            return ResetPassword(user);
        }

        // =================================================================================
        // 3. CÁC HÀM HỖ TRỢ KHÁC
        // =================================================================================
        public Account GetAccountByUserName(string userName)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM TaiKhoan WHERE TenDangNhap = @user", new object[] { userName });
            foreach (DataRow item in data.Rows) return new Account(item);
            return null;
        }

        public Account GetAccountByEmail(string email)
        {
            string query = "SELECT t.* FROM TaiKhoan t JOIN HocVien h ON t.MaNguoiDung = h.MaHV WHERE h.Email = @email";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { email });
            if (data.Rows.Count == 0)
            {
                query = "SELECT * FROM TaiKhoan WHERE TenDangNhap = @email"; // Fallback
                data = DataProvider.Instance.ExecuteQuery(query, new object[] { email });
            }
            foreach (DataRow item in data.Rows) return new Account(item);
            return null;
        }

        public void LogLogin(string user, string status, string note = "")
        {
            string query = "INSERT INTO LichSuDangNhap (TenDangNhap, ThoiGian, TrangThai, GhiChu) VALUES ( @u , GETDATE() , @s , @n )";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { user, status, note });
        }

        public bool InsertAccount(string user, string pass, string quyen, string maNguoiDung)
        {
            string check = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @user";
            int count = (int)DataProvider.Instance.ExecuteScalar(check, new object[] { user });
            if (count > 0) return false;

            // Mặc định tạo mới là "Hoạt động"
            string query = "INSERT INTO TaiKhoan (TenDangNhap, MatKhau, Quyen, MaNguoiDung, TrangThai) VALUES ( @user , @pass , @quyen , @maND , N'Hoạt động' )";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { user, pass, quyen, maNguoiDung }) > 0;
        }

        public bool UpdatePassword(string userName, string passMoi)
        {
            string query = "UPDATE TaiKhoan SET MatKhau = @pass WHERE TenDangNhap = @user";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { passMoi, userName }) > 0;
        }

        public void LockAccountByUserID(string maNguoiDung, bool khoa)
        {
            // Sửa lại lưu trạng thái chuỗi
            string status = khoa ? "Bị khóa" : "Hoạt động";
            string query = "UPDATE TaiKhoan SET TrangThai = @stt WHERE MaNguoiDung = @maND";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { status, maNguoiDung });
        }

        public DataTable GetListAccount()
        {
            string query = @"SELECT t.TenDangNhap, t.MatKhau, t.Quyen, t.MaNguoiDung, t.TrangThai, 
                             ISNULL(n.HoTen, h.HoTen) as ChuSoHuu 
                             FROM TaiKhoan t
                             LEFT JOIN NhanSu n ON t.MaNguoiDung = n.MaNS
                             LEFT JOIN HocVien h ON t.MaNguoiDung = h.MaHV";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable GetLoginHistory()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT TOP 100 * FROM LichSuDangNhap ORDER BY ThoiGian DESC");
        }
    }
}