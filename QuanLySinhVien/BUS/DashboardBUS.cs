using QuanLyTrungTam.DAO;
using System.Data;

namespace QuanLyTrungTam.BUS
{
    public class DashboardBUS
    {
        private static DashboardBUS instance;
        public static DashboardBUS Instance
        {
            get { if (instance == null) instance = new DashboardBUS(); return instance; }
        }
        private DashboardBUS() { }

        // Wrapper for DashboardDAO
        public int GetSoLuongHocVien() => DashboardDAO.Instance.GetSoLuongHocVien();
        public int GetSoLuongLopHoc() => DashboardDAO.Instance.GetSoLuongLopHoc();
        public int GetSoLuongMon() => DashboardDAO.Instance.GetSoLuongMon();
        public decimal GetLoiNhuan() => DashboardDAO.Instance.GetLoiNhuan();
        public int GetSoLuongGiaoVien() => DashboardDAO.Instance.GetSoLuongGiaoVien();
        public int GetSoLuongTroGiang() => DashboardDAO.Instance.GetSoLuongTroGiang();
        public int GetSoLuongNoPhi() => DashboardDAO.Instance.GetSoLuongNoPhi();
        public int GetSoLopChuaDu() => DashboardDAO.Instance.GetSoLopChuaDu();
        public DataTable GetFinanceChartData() => DashboardDAO.Instance.GetFinanceChartData();
        public DataTable GetTopClassScores() => DashboardDAO.Instance.GetTopClassScores();
        public DataTable GetSystemLog() => DashboardDAO.Instance.GetSystemLog();

        // Wrapper for StatsDAO
        public DataTable GetRevenueBySkill() => StatsDAO.Instance.GetRevenueBySkill();
    }
}
