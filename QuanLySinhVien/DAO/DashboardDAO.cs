using System;
using System.Data;
using QuanLyTrungTam.DTO;

namespace QuanLyTrungTam.DAO
{
    public class DashboardDAO
    {
        private static DashboardDAO instance;
        public static DashboardDAO Instance
        {
            get { if (instance == null) instance = new DashboardDAO(); return instance; }
        }
        private DashboardDAO() { }

        // 1. Đếm Học Viên
        public int GetSoLuongHocVien()
        {
            try { return (int)DataProvider.Instance.ExecuteScalar("SELECT COUNT(*) FROM HocVien WHERE TrangThai = N'Đang học'"); } catch { return 0; }
        }

        // 2. Đếm Lớp Học (Theo data mẫu: TrangThai = N'Đang học')
        public int GetSoLuongLopHoc()
        {
            try { return (int)DataProvider.Instance.ExecuteScalar("SELECT COUNT(*) FROM LopHoc WHERE TrangThai = N'Đang học'"); } catch { return 0; }
        }

        // 3. Đếm Môn Đào Tạo (Bảng KyNang, TrangThai = '1')
        public int GetSoLuongMon()
        {
            try
            {
                // Data của bạn lưu TrangThai là '1' cho môn đang mở
                return (int)DataProvider.Instance.ExecuteScalar("SELECT COUNT(*) FROM KyNang WHERE TrangThai = '1'");
            }
            catch { return 0; }
        }

        // 4. Tính Lợi Nhuận (Thu - Chi)
        public decimal GetLoiNhuan()
        {
            try
            {
                // Data của bạn LoaiGD có thể là 'Thu' hoặc 'Thu học phí' -> Dùng LIKE N'Thu%'
                string queryThu = "SELECT SUM(SoTien) FROM GiaoDichTaiChinh WHERE LoaiGD LIKE N'Thu%'";
                string queryChi = "SELECT SUM(SoTien) FROM GiaoDichTaiChinh WHERE LoaiGD LIKE N'Chi%'";

                object objThu = DataProvider.Instance.ExecuteScalar(queryThu);
                object objChi = DataProvider.Instance.ExecuteScalar(queryChi);

                decimal thu = (objThu == DBNull.Value || objThu == null) ? 0 : Convert.ToDecimal(objThu);
                decimal chi = (objChi == DBNull.Value || objChi == null) ? 0 : Convert.ToDecimal(objChi);

                return thu - chi;
            }
            catch { return 0; }
        }

        // 5. Đếm Giáo Viên (Bảng NhanSu, LoaiNS = N'Giáo viên')
        public int GetSoLuongGiaoVien()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("SELECT COUNT(*) FROM NhanSu WHERE LoaiNS = N'Giáo viên'");
            }
            catch { return 0; }
        }

        // 6. Đếm Trợ Giảng (Bảng NhanSu, LoaiNS = N'Trợ giảng')
        public int GetSoLuongTroGiang()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("SELECT COUNT(*) FROM NhanSu WHERE LoaiNS = N'Trợ giảng'");
            }
            catch { return 0; }
        }

        // 7. Học viên nợ phí (Logic: Tổng học phí phải đóng - Tổng đã đóng > 0)
        public int GetSoLuongNoPhi()
        {
            string query = @"
                SELECT COUNT(DISTINCT dk.MaHV)
                FROM DangKy dk
                LEFT JOIN (SELECT MaHV, SUM(SoTienDong) as TongDaDong FROM ThanhToan GROUP BY MaHV) tt 
                ON dk.MaHV = tt.MaHV
                WHERE (dk.HocPhiLop - ISNULL(tt.TongDaDong, 0)) > 0";
            try { return (int)DataProvider.Instance.ExecuteScalar(query); } catch { return 0; }
        }

        // 8. Lớp chưa đủ học viên
        public int GetSoLopChuaDu()
        {
            try { return (int)DataProvider.Instance.ExecuteScalar("SELECT COUNT(*) FROM LopHoc WHERE TrangThai = N'Đang tuyển sinh'"); } catch { return 0; }
        }

        // 9. Biểu đồ Tài chính (Group theo tháng)
        public DataTable GetFinanceChartData()
        {
            string query = @"
                SELECT 
                    FORMAT(NgayGD, 'MM/yyyy') as ThoiGian, 
                    SUM(CASE WHEN LoaiGD LIKE N'Thu%' THEN SoTien ELSE 0 END) as TongThu,
                    SUM(CASE WHEN LoaiGD LIKE N'Chi%' THEN SoTien ELSE 0 END) as TongChi
                FROM GiaoDichTaiChinh 
                GROUP BY FORMAT(NgayGD, 'MM/yyyy'), YEAR(NgayGD), MONTH(NgayGD)
                ORDER BY YEAR(NgayGD), MONTH(NgayGD)";
            try { return DataProvider.Instance.ExecuteQuery(query); } catch { return null; }
        }

        // 10. Top điểm số lớp học
        public DataTable GetTopClassScores()
        {
            // Join Diem -> LopHoc để lấy tên lớp
            try
            {
                return DataProvider.Instance.ExecuteQuery(@"
                SELECT TOP 5 l.TenLop, AVG(d.DiemTongKet) as DiemTB 
                FROM Diem d JOIN LopHoc l ON d.MaLop = l.MaLop 
                WHERE d.DiemTongKet IS NOT NULL
                GROUP BY l.TenLop ORDER BY DiemTB DESC");
            }
            catch { return null; }
        }

        // 11. Nhật ký hệ thống (Bảng LichSuDangNhap)
        public DataTable GetSystemLog()
        {
            try
            {
                return DataProvider.Instance.ExecuteQuery("SELECT TOP 10 ThoiGian, TenDangNhap, GhiChu FROM LichSuDangNhap ORDER BY ThoiGian DESC");
            }
            catch { return null; }
        }
    }
}