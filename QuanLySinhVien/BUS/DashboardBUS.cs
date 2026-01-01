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
        public int GetSoLuongHocVien() { return DashboardDAO.Instance.GetSoLuongHocVien(); }
        public int GetSoLuongLopHoc() { return DashboardDAO.Instance.GetSoLuongLopHoc(); }
        public int GetSoLuongMon() { return DashboardDAO.Instance.GetSoLuongMon(); }
        public decimal GetLoiNhuan() { return DashboardDAO.Instance.GetLoiNhuan(); }
        public int GetSoLuongGiaoVien() { return DashboardDAO.Instance.GetSoLuongGiaoVien(); }
        public int GetSoLuongTroGiang() { return DashboardDAO.Instance.GetSoLuongTroGiang(); }
        public int GetSoLuongNoPhi() { return DashboardDAO.Instance.GetSoLuongNoPhi(); }
        public int GetSoLopChuaDu() { return DashboardDAO.Instance.GetSoLopChuaDu(); }
        public DataTable GetFinanceChartData() { return DashboardDAO.Instance.GetFinanceChartData(); }
        public DataTable GetTopClassScores() { return DashboardDAO.Instance.GetTopClassScores(); }
        public DataTable GetSystemLog() { return DashboardDAO.Instance.GetSystemLog(); }

        // Wrapper for StatsDAO
        public DataTable GetRevenueBySkill() { return StatsDAO.Instance.GetRevenueBySkill(); }
    }
}
