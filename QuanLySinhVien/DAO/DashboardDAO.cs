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

            // 1. Tổng doanh thu dự kiến (Tổng tiền đăng ký)
            public decimal GetTongDoanhThuDuKien()
            {
                string query = "SELECT SUM(HocPhiLop) FROM DangKy";
                return GetDecimalFromQuery(query);
            }

            // 2. Tổng thực thu (Tiền đã vào túi)
            public decimal GetTongThucThu()
            {
                string query = "SELECT SUM(SoTienDong) FROM ThanhToan";
                return GetDecimalFromQuery(query);
            }

            // 3. Tổng nợ hiện tại
            public decimal GetTongNo()
            {
                return GetTongDoanhThuDuKien() - GetTongThucThu();
            }

            // 4. Số lượng học viên
            public int GetSoLuongHocVien()
            {
                try
                {
                    return (int)DataProvider.Instance.ExecuteScalar("SELECT COUNT(*) FROM HocVien");
                }
                catch { return 0; }
            }

            // 5. Thống kê doanh thu theo từng Kỹ Năng (Để vẽ biểu đồ)
            public DataTable GetRevenueBySkill()
            {
                string query = @"
                    SELECT k.TenKyNang, SUM(d.HocPhiLop) as TongTien
                    FROM KyNang k
                    JOIN LopHoc l ON k.MaKyNang = l.MaKyNang
                    JOIN DangKy d ON l.MaLop = d.MaLop
                    GROUP BY k.TenKyNang";
                return DataProvider.Instance.ExecuteQuery(query);
            }

            // Helper function
            private decimal GetDecimalFromQuery(string query)
            {
                try
                {
                    object result = DataProvider.Instance.ExecuteScalar(query);
                    if (result != null && result != DBNull.Value)
                        return Convert.ToDecimal(result);
                    return 0;
                }
                catch { return 0; }
            }
            public int GetSoLuongLopHoc()
            {
                try { return (int)DataProvider.Instance.ExecuteScalar("SELECT COUNT(*) FROM LopHoc"); }
                catch { return 0; }
            }

            // 7. Đếm tổng số môn học (Kỹ năng)
            public int GetSoLuongMonHoc()
            {
                try { return (int)DataProvider.Instance.ExecuteScalar("SELECT COUNT(*) FROM KyNang"); }
                catch { return 0; }
            }
            public DataTable GetStudentCountBySkill()
            {
                // Chúng ta dùng COUNT(d.MaHV) để đếm số học viên đã đăng ký
                string query = @"
            SELECT k.TenKyNang, COUNT(d.MaHV) as SoLuong
            FROM KyNang k
            LEFT JOIN LopHoc l ON k.MaKyNang = l.MaKyNang
            LEFT JOIN DangKy d ON l.MaLop = d.MaLop
            GROUP BY k.TenKyNang";

                return DataProvider.Instance.ExecuteQuery(query);
            }
        }
    }