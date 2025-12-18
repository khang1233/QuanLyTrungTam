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
            private set { instance = value; }
        }
        private AccountDAO() { }

        // --- HÀM LOGIN ĐÃ SỬA ---
        // Thêm tham số 'role' vào hàm
        // --- SỬA LẠI TÊN CỘT TRONG CÂU LỆNH SQL ---
        public bool Login(string userName, string passWord, string role)
        {
            // Thay 'LoaiTaiKhoan' thành 'Quyen' (hoặc tên cột thật của bạn)
            string query = "SELECT * FROM TaiKhoan WHERE TenDangNhap = '" + userName + "' AND MatKhau = '" + passWord + "' AND Quyen = N'" + role + "'";

            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            return result.Rows.Count > 0;
        }

        // 2. Lấy thông tin Account
        public Account GetAccountByUserName(string userName)
        {
            string query = "SELECT * FROM TaiKhoan WHERE TenDangNhap = '" + userName + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }

        // 3. Cập nhật mật khẩu
        public bool UpdatePassword(string userName, string passMoi)
        {
            string query = "UPDATE TaiKhoan SET MatKhau = '" + passMoi + "' WHERE TenDangNhap = '" + userName + "'";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        // Trong AccountDAO.cs
        public bool InsertAccount(string user, string pass, string quyen, string maNguoiDung)
        {
            string query = "INSERT INTO TaiKhoan (TenDangNhap, MatKhau, Quyen, MaNguoiDung) VALUES ( @user , @pass , @quyen , @maND )";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { user, pass, quyen, maNguoiDung }) > 0;
        }
        // Trong class AccountDAO
        // Trong file AccountDAO.cs

        // File: DAO/AccountDAO.cs

        public bool LoginGoogle(string email)
        {
            // CÁCH 1: Tìm xem Email này đã được liên kết với Học Viên nào chưa?
            // Join bảng TaiKhoan và HocVien để tìm
            string queryFind = @"
        SELECT t.* FROM TaiKhoan t
        JOIN HocVien h ON t.MaNguoiDung = h.MaHV
        WHERE h.Email = '" + email + "'";

            DataTable result = DataProvider.Instance.ExecuteQuery(queryFind);

            if (result.Rows.Count > 0)
            {
                // TRƯỜNG HỢP 1: Đã tìm thấy tài khoản liên kết với Email này
                // Lấy thông tin tài khoản đó ra để đăng nhập
                DataRow row = result.Rows[0];
                string tenDangNhap = row["TenDangNhap"].ToString();

                // Cập nhật lại Session (Quan trọng: Đăng nhập dưới danh nghĩa HV đó)
                // Lưu ý: Hàm này chỉ trả về bool, việc lưu Session sẽ làm ở Form1
                return true;
            }
            else
            {
                // TRƯỜNG HỢP 2: Email này hoàn toàn mới -> Tự động đăng ký mới (như cũ)
                // (Giữ nguyên logic tạo mới nếu bạn muốn cho người lạ đăng ký)
                // ... Code tạo mới HV và TK ...
                // Để ngắn gọn, bạn có thể copy lại phần logic Insert cũ vào đây

                // Ví dụ logic tạo mới rút gọn:
                string maHV = "HV" + DateTime.Now.Ticks.ToString().Substring(12);
                DataProvider.Instance.ExecuteNonQuery($"INSERT INTO HocVien (MaHV, Email) VALUES ('{maHV}', '{email}')");
                DataProvider.Instance.ExecuteNonQuery($"INSERT INTO TaiKhoan (TenDangNhap, Quyen, MaNguoiDung) VALUES ('{email}', 'HocVien', '{maHV}')");
                return true;
            }
        }

        // BỔ SUNG: Hàm lấy Account bằng Email (Dùng cho Form1 sau khi LoginGoogle thành công)
        public Account GetAccountByEmail(string email)
        {
            string query = @"
        SELECT t.* FROM TaiKhoan t
        JOIN HocVien h ON t.MaNguoiDung = h.MaHV
        WHERE h.Email = '" + email + "'"; // Tìm tài khoản khớp email trong bảng HocVien (hoặc TaiKhoan nếu là tạo tự động)

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            // Nếu không tìm thấy trong bảng HocVien, tìm trong bảng TaiKhoan (cho trường hợp đăng ký tự động cũ)
            if (data.Rows.Count == 0)
            {
                query = "SELECT * FROM TaiKhoan WHERE TenDangNhap = '" + email + "'";
                data = DataProvider.Instance.ExecuteQuery(query);
            }

            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }
    }
}