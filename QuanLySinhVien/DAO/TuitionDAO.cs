using QuanLyTrungTam.DAO;
using QuanLyTrungTam.DTO;
using System;
using System.Data;

namespace QuanLyTrungTam.DAO
{
    public class TuitionDAO
    {
        private static TuitionDAO instance;
        public static TuitionDAO Instance
        {
            get { if (instance == null) instance = new TuitionDAO(); return instance; }
        }
        private TuitionDAO() { }

        // Lấy danh sách chi tiết các môn học viên đã đăng ký
        // File: DAO/TuitionDAO.cs
        public DataTable GetListDangKy(string maHV)
        {
            // ĐÃ SỬA: Thêm l.LichHoc và l.NgayBatDau vào câu SELECT
            string query = "SELECT k.TenKyNang, l.TenLop, l.LichHoc, l.NgayBatDau, d.HocPhiLop " +
                           "FROM DangKy d " +
                           "JOIN LopHoc l ON d.MaLop = l.MaLop " +
                           "JOIN KyNang k ON l.MaKyNang = k.MaKyNang " +
                           "WHERE d.MaHV = N'" + maHV + "'";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public bool HuyDangKy(string maHV, string maLop)
        {
            // Xóa dòng trong bảng DangKy dựa trên Mã HV và Mã Lớp
            string query = string.Format("DELETE FROM DangKy WHERE MaHV = '{0}' AND MaLop = '{1}'", maHV, maLop);
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }
        // Tính tổng tiền PHẢI ĐÓNG
        public decimal GetTongNo(string maHV)
        {
            string query = "SELECT SUM(HocPhiLop) FROM DangKy WHERE MaHV = '" + maHV + "'";
            object result = DataProvider.Instance.ExecuteScalar(query);
            if (result != null && result != DBNull.Value)
                return Convert.ToDecimal(result);
            return 0;
        }

        // Tính tổng tiền ĐÃ ĐÓNG
        public decimal GetDaDong(string maHV)
        {
            string query = "SELECT SUM(SoTienDong) FROM ThanhToan WHERE MaHV = '" + maHV + "'";
            object result = DataProvider.Instance.ExecuteScalar(query);
            if (result != null && result != DBNull.Value)
                return Convert.ToDecimal(result);
            return 0;
        }

        // Thực hiện hành động Đóng tiền
        public bool InsertThanhToan(string maHV, decimal soTien, string ghiChu)
        {
            string query = string.Format("INSERT INTO ThanhToan (MaHV, SoTienDong, GhiChu) VALUES ('{0}', {1}, N'{2}')", maHV, soTien, ghiChu);
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }
        public bool DangKyLop(string maHV, string maLop, decimal hocPhiLop)
        {
            // 1. Kiểm tra xem đã đăng ký lớp này chưa?
            string check = "SELECT COUNT(*) FROM DangKy WHERE MaHV = '" + maHV + "' AND MaLop = '" + maLop + "'";
            int result = (int)DataProvider.Instance.ExecuteScalar(check);
            if (result > 0) return false; // Đã đăng ký rồi

            // 2. Nếu chưa thì Insert
            // Lưu ý: Lưu HocPhiLop vào thời điểm đăng ký để chốt giá
            string query = string.Format("INSERT INTO DangKy (MaHV, MaLop, HocPhiLop) VALUES ('{0}', '{1}', {2})",
                                         maHV, maLop, hocPhiLop);
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }
    }
}