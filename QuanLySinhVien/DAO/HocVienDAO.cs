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

        // =================================================================
        // [QUAN TRỌNG] HÀM FIX LỖI TRẠNG THÁI (MỚI THÊM)
        // =================================================================
        public bool CapNhatTrangThaiHocVien(string maHV, string trangThaiMoi)
        {
            // Dùng string.Format để điền trực tiếp giá trị vào câu lệnh
            // Lưu ý: N'...' để hỗ trợ tiếng Việt
            string query = string.Format("UPDATE HocVien SET TrangThai = N'{0}' WHERE MaHV = '{1}'", trangThaiMoi, maHV);

            // Gọi hàm thực thi (không cần truyền mảng object nữa)
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        // =================================================================
        // CÁC HÀM CƠ BẢN KHÁC (ĐÃ DỌN DẸP)
        // =================================================================

        public DataTable GetListHocVien()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT * FROM HocVien");
        }

        public DataRow GetInfoHocVien(string maHV)
        {
            string query = "SELECT * FROM HocVien WHERE MaHV = '" + maHV + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data.Rows.Count > 0 ? data.Rows[0] : null;
        }

        public string GetNewMaHV()
        {
            string query = "SELECT TOP 1 MaHV FROM HocVien ORDER BY MaHV DESC";
            object result = DataProvider.Instance.ExecuteScalar(query);
            if (result == null || result == DBNull.Value) return "HV001";

            string lastMa = result.ToString();
            // Giả sử mã dạng HV001, lấy số từ vị trí index 2
            int nextID = int.Parse(lastMa.Substring(2)) + 1;
            return "HV" + nextID.ToString("D3");
        }

        public bool InsertHocVien(string ma, string ten, DateTime ngaySinh, string sdt, string email, string diaChi, string trangThai)
        {
            string query = "INSERT INTO HocVien (MaHV, HoTen, NgaySinh, SDT, Email, DiaChi, TrangThai) " +
                           "VALUES ( @ma , @ten , @ns , @sdt , @mail , @dc , @tt )";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { ma, ten, ngaySinh, sdt, email, diaChi, trangThai }) > 0;
        }

        public bool UpdateHocVien(string ma, string ten, DateTime ngaySinh, string sdt, string email, string diaChi, string trangThai)
        {
            string query = "UPDATE HocVien SET HoTen = @ten , NgaySinh = @ns , SDT = @sdt , " +
                           "Email = @mail , DiaChi = @dc , TrangThai = @tt WHERE MaHV = @ma ";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { ten, ngaySinh, sdt, email, diaChi, trangThai, ma }) > 0;
        }

        public bool DeleteHocVien(string maHV)
        {
            // Gọi Stored Procedure xóa (nếu bạn đã tạo SP này trong SQL)
            // Nếu chưa có SP, bạn có thể đổi thành: "DELETE FROM HocVien WHERE MaHV = @ma" (nhưng coi chừng ràng buộc khóa ngoại)
            string query = "EXEC USP_XoaHocVien @MaHV";
            object result = DataProvider.Instance.ExecuteScalar(query, new object[] { maHV });

            if (result != null && int.TryParse(result.ToString(), out int res))
            {
                return res == 1;
            }
            return false;
        }

        public bool UpdateEmailHocVien(string maHV, string email)
        {
            string check = "SELECT COUNT(*) FROM HocVien WHERE Email = '" + email + "' AND MaHV != '" + maHV + "'";
            int count = (int)DataProvider.Instance.ExecuteScalar(check);
            if (count > 0) return false;

            string query = string.Format("UPDATE HocVien SET Email = '{0}' WHERE MaHV = '{1}'", email, maHV);
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        public DataTable GetLearningHistory(string maHV)
        {
            string query = "SELECT k.TenKyNang, l.TenLop, d.NgayDangKy, l.TrangThai as TinhTrangLop " +
                           "FROM DangKy d " +
                           "JOIN LopHoc l ON d.MaLop = l.MaLop " +
                           "JOIN KyNang k ON l.MaKyNang = k.MaKyNang " +
                           "WHERE d.MaHV = '" + maHV + "'";
            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}