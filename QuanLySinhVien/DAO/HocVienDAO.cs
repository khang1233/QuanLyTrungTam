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
            string query = "UPDATE HocVien SET TrangThai = @tt WHERE MaHV = @ma";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { trangThaiMoi, maHV });
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
            string query = "SELECT * FROM HocVien WHERE MaHV = @ma";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maHV });
            return data.Rows.Count > 0 ? data.Rows[0] : null;
        }

        public string GetNewMaHV()
        {
            // Logic sinh mã: lấy HVxxx
            string query = "SELECT TOP 1 MaHV FROM HocVien ORDER BY MaHV DESC";
            object result = DataProvider.Instance.ExecuteScalar(query);
            if (result == null || result == DBNull.Value) return "HV001";

            string lastMa = result.ToString();
            // Expected format: HVxxx
            int nextID;
            if (lastMa.Length > 2 && int.TryParse(lastMa.Substring(2), out nextID))
            {
                 // Tăng 1 và format lại thành 3 chữ số
                return "HV" + (nextID + 1).ToString("D3");
            }
            return "HV001";
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
            // Check dependency first if needed, or rely on Cascade/SP
            // Using parameterized delete
            string query = "DELETE FROM HocVien WHERE MaHV = @ma";
             // If you have a SP, use: "EXEC USP_XoaHocVien @ma"
            
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { maHV }) > 0;
        }

        public bool UpdateEmailHocVien(string maHV, string email)
        {
            string check = "SELECT COUNT(*) FROM HocVien WHERE Email = @email AND MaHV != @ma";
            int count = (int)DataProvider.Instance.ExecuteScalar(check, new object[] { email, maHV });
            if (count > 0) return false;

            string query = "UPDATE HocVien SET Email = @email WHERE MaHV = @ma";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { email, maHV }) > 0;
        }

        public DataTable GetLearningHistory(string maHV)
        {
            string query = @"SELECT k.TenKyNang, l.TenLop, d.NgayDangKy, l.TrangThai as TinhTrangLop 
                           FROM DangKy d 
                           JOIN LopHoc l ON d.MaLop = l.MaLop 
                           JOIN KyNang k ON l.MaKyNang = k.MaKyNang 
                           WHERE d.MaHV = @ma";
            
            return DataProvider.Instance.ExecuteQuery(query, new object[] { maHV });
        }
    }
}