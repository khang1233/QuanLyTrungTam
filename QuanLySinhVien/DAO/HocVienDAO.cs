using QuanLyTrungTam.DAO;
using System;
using System.Data;
namespace QuanLyTrungTam.DAO
{
    public class HocVienDAO
    {
        private static HocVienDAO instance;
        public static HocVienDAO Instance
        {
            get { if (instance == null) instance = new HocVienDAO(); return instance; }
        }
        private HocVienDAO() { }
        // --- BỔ SUNG VÀO HocVienDAO.cs ---

        // 1. Thêm Học Viên
        // File: DAO/HocVienDAO.cs

        public bool InsertHocVien(string maHV, string hoTen, DateTime ngaySinh, string sdt, string email, string diaChi)
        {
            // 1. Thêm thông tin vào bảng HocVien trước
            string queryHV = string.Format("INSERT INTO HocVien (MaHV, HoTen, NgaySinh, SoDienThoai, Email, DiaChi, NgayGiaNhap) " +
                                           "VALUES ('{0}', N'{1}', '{2}', '{3}', '{4}', N'{5}', GETDATE())",
                                           maHV, hoTen, ngaySinh.ToString("yyyy-MM-dd"), sdt, email, diaChi);

            int resultHV = DataProvider.Instance.ExecuteNonQuery(queryHV);

            // 2. Nếu thêm HV thành công -> Tự động sinh Tài khoản
            if (resultHV > 0)
            {
                try
                {
                    // Mặc định: User = Mã HV, Pass = 123, Quyen = HocVien
                    string queryTK = string.Format("INSERT INTO TaiKhoan (TenDangNhap, MatKhau, Quyen, MaNguoiDung, IsGoogleAccount) " +
                                                   "VALUES ('{0}', '123', 'HocVien', '{0}', 0)", maHV);
                    DataProvider.Instance.ExecuteNonQuery(queryTK);
                }
                catch
                {
                    // Nếu lỗi tạo TK (ví dụ trùng), có thể xử lý log hoặc bỏ qua tùy nghiệp vụ
                }
                return true;
            }
            return false;
        }
        // File: DAO/HocVienDAO.cs
        public bool UpdateEmailHocVien(string maHV, string email)
        {
            // Kiểm tra email đã có ai dùng chưa
            string check = "SELECT COUNT(*) FROM HocVien WHERE Email = '" + email + "' AND MaHV != '" + maHV + "'";
            int count = (int)DataProvider.Instance.ExecuteScalar(check);
            if (count > 0) return false; // Email đã bị người khác dùng

            // Cập nhật Email vào hồ sơ
            string query = string.Format("UPDATE HocVien SET Email = '{0}' WHERE MaHV = '{1}'", email, maHV);
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }
        // 2. Sửa Học Viên
        public bool UpdateHocVien(string maHV, string hoTen, DateTime ngaySinh, string sdt, string email, string diaChi)
        {
            string query = string.Format("UPDATE HocVien SET HoTen = N'{1}', NgaySinh = '{2}', SoDienThoai = '{3}', Email = '{4}', DiaChi = N'{5}' " +
                                         "WHERE MaHV = '{0}'",
                                         maHV, hoTen, ngaySinh.ToString("yyyy-MM-dd"), sdt, email, diaChi);
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        // 3. Xóa Học Viên
        // Trong file: QuanLyTrungTam/DAO/HocVienDAO.cs

        public bool DeleteHocVien(string maHV)
        {
            try
            {
                // BƯỚC 1: Xóa dữ liệu liên quan ở bảng ThanhToan (Tiền đã đóng)
                string queryThanhToan = string.Format("DELETE FROM ThanhToan WHERE MaHV = '{0}'", maHV);
                DataProvider.Instance.ExecuteNonQuery(queryThanhToan);

                // BƯỚC 2: Xóa dữ liệu liên quan ở bảng DangKy (Các lớp đang học)
                string queryDangKy = string.Format("DELETE FROM DangKy WHERE MaHV = '{0}'", maHV);
                DataProvider.Instance.ExecuteNonQuery(queryDangKy);

                // BƯỚC 3: Xóa Tài Khoản đăng nhập
                string queryTaiKhoan = string.Format("DELETE FROM TaiKhoan WHERE MaNguoiDung = '{0}'", maHV);
                DataProvider.Instance.ExecuteNonQuery(queryTaiKhoan);

                // BƯỚC 4: Cuối cùng mới xóa Học Viên
                string queryHocVien = string.Format("DELETE FROM HocVien WHERE MaHV = '{0}'", maHV);
                int result = DataProvider.Instance.ExecuteNonQuery(queryHocVien);

                return result > 0;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi hoặc thông báo (nếu cần)
                return false;
            }
        }
        // File: DAO/HocVienDAO.cs
        public DataRow GetInfoHocVien(string maHV)
        {
            string query = "SELECT * FROM HocVien WHERE MaHV = '" + maHV + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                return data.Rows[0];
            }
            return null;
        }
        public DataTable GetListHocVien()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT * FROM HocVien");
        }
        // --- THÊM HÀM NÀY VÀO CUỐI CLASS ---
        public string GetNewMaHV()
        {
            // 1. Lấy mã HV cuối cùng trong bảng (Sắp xếp giảm dần để lấy số lớn nhất)
            string query = "SELECT TOP 1 MaHV FROM HocVien ORDER BY MaHV DESC";
            object result = DataProvider.Instance.ExecuteScalar(query);

            // 2. Xử lý logic sinh mã
            if (result == null || result == DBNull.Value)
            {
                // Nếu bảng chưa có ai, trả về mã đầu tiên
                return "HV001";
            }
            else
            {
                string lastMa = result.ToString(); // Ví dụ: "HV009"

                // Cắt lấy phần số (bỏ 2 ký tự đầu là "HV")
                string phanSo = lastMa.Substring(2);

                int nextID = 0;
                if (int.TryParse(phanSo, out nextID))
                {
                    nextID++; // Tăng lên 1. Ví dụ: 9 -> 10
                }

                // Trả về format "HV" + 3 chữ số (ví dụ: HV010)
                return "HV" + nextID.ToString("D3");
            }
        }
    }
}