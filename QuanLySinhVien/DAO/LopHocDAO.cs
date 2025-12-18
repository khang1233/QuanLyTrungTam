using QuanLyTrungTam.DAO;
using System;
using System.Data;

namespace QuanLyTrungTam.DAO // Nhớ check namespace cho trùng project
{
    public class LopHocDAO
    {
        private static LopHocDAO instance;
        public static LopHocDAO Instance
        {
            get { if (instance == null) instance = new LopHocDAO(); return instance; }
        }
        private LopHocDAO() { }

        public DataTable GetListLopByKyNang(string maKyNang)
        {
            string query = "SELECT * FROM LopHoc WHERE MaKyNang = '" + maKyNang + "'";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        // --- CÁC HÀM MỚI THÊM VÀO ---

        // 1. Thêm Lớp
        public bool InsertLop(string maLop, string tenLop, string maKyNang, DateTime ngayMo)
        {
            string query = string.Format("INSERT INTO LopHoc (MaLop, TenLop, MaKyNang, NgayBatDau) VALUES ('{0}', N'{1}', '{2}', '{3}')",
                                         maLop, tenLop, maKyNang, ngayMo.ToString("yyyy-MM-dd"));
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        // 2. Sửa Lớp
        public bool UpdateLop(string maLop, string tenLop, string maKyNang, DateTime ngayMo)
        {
            string query = string.Format("UPDATE LopHoc SET TenLop = N'{1}', MaKyNang = '{2}', NgayBatDau = '{3}' WHERE MaLop = '{0}'",
                                         maLop, tenLop, maKyNang, ngayMo.ToString("yyyy-MM-dd"));
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        // 3. Xóa Lớp
        // Trong file: QuanLyTrungTam/DAO/LopHocDAO.cs [Thay thế đoạn code cũ từ dòng 457]

        public bool DeleteLop(string maLop)
        {
            // 1. Xóa các đăng ký của học viên vào lớp này trước
            string queryDelDK = string.Format("DELETE FROM DangKy WHERE MaLop = '{0}'", maLop);
            DataProvider.Instance.ExecuteNonQuery(queryDelDK);

            // 2. Sau đó mới xóa Lớp
            string query = string.Format("DELETE FROM LopHoc WHERE MaLop = '{0}'", maLop);
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }
        public DataTable GetAllLop()
        {
            // Lấy thêm TenKyNang để hiển thị cho rõ
            string query = "SELECT l.*, k.TenKyNang " +
                           "FROM LopHoc l " +
                           "LEFT JOIN KyNang k ON l.MaKyNang = k.MaKyNang";
            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}