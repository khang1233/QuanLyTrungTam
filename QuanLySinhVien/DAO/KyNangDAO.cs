using QuanLyTrungTam.DAO;
using System.Data;

namespace QuanLyTrungTam.DAO
{
    public class KyNangDAO
    {
        private static KyNangDAO instance;
        public static KyNangDAO Instance
        {
            get { if (instance == null) instance = new KyNangDAO(); return instance; }
        }
        private KyNangDAO() { }
        // --- BỔ SUNG VÀO KyNangDAO.cs ---

        // 1. Thêm Môn học (Kỹ năng)
        public bool InsertKyNang(string maKyNang, string tenKyNang, decimal hocPhi, string moTa)
        {
            string query = string.Format("INSERT INTO KyNang (MaKyNang, TenKyNang, HocPhi, MoTa) VALUES ('{0}', N'{1}', {2}, N'{3}')",
                                         maKyNang, tenKyNang, hocPhi, moTa);
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        // 2. Sửa Môn học
        public bool UpdateKyNang(string maKyNang, string tenKyNang, decimal hocPhi, string moTa)
        {
            string query = string.Format("UPDATE KyNang SET TenKyNang = N'{1}', HocPhi = {2}, MoTa = N'{3}' WHERE MaKyNang = '{0}'",
                                         maKyNang, tenKyNang, hocPhi, moTa);
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        // 3. Xóa Môn học
        // Trong file: QuanLyTrungTam/DAO/KyNangDAO.cs [Thay thế đoạn code cũ từ dòng 410]

        public bool DeleteKyNang(string maKyNang)
        {
            // Kiểm tra xem có lớp nào đang mở môn này không?
            string checkQuery = "SELECT COUNT(*) FROM LopHoc WHERE MaKyNang = '" + maKyNang + "'";
            int count = (int)DataProvider.Instance.ExecuteScalar(checkQuery);

            if (count > 0)
            {
                // Nếu còn lớp học thì KHÔNG CHO XÓA (trả về false)
                return false;
            }

            // Nếu không có lớp nào, tiến hành xóa
            string query = string.Format("DELETE FROM KyNang WHERE MaKyNang = '{0}'", maKyNang);
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }
        public DataTable GetListKyNang()
        {
            // Lấy tất cả kỹ năng đang mở (TrangThai = 1
            string query = "SELECT * FROM KyNang WHERE TrangThai = 1";
            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}