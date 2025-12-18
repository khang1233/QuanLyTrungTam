using QuanLyTrungTam.DAO;
using System.Data;

namespace QuanLyTrungTam.DAO
{
    public class StatsDAO
    {
        private static StatsDAO instance;
        public static StatsDAO Instance
        {
            get { if (instance == null) instance = new StatsDAO(); return instance; }
        }
        private StatsDAO() { }

        public DataTable GetRevenueBySkill()
        {
            // Query lấy doanh thu theo từng kỹ năng
            string query = "SELECT k.TenKyNang, COUNT(d.MaDangKy) AS SoLuotDK, SUM(d.HocPhiLop) AS DoanhThu " +
                           "FROM KyNang k " +
                           "LEFT JOIN LopHoc l ON k.MaKyNang = l.MaKyNang " +
                           "LEFT JOIN DangKy d ON l.MaLop = d.MaLop " +
                           "GROUP BY k.TenKyNang";
            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}