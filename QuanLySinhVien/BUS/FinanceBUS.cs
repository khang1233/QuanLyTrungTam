using QuanLyTrungTam.DAO;
using System;
using System.Data;

namespace QuanLyTrungTam.BUS
{
    public class FinanceBUS
    {
        private static FinanceBUS instance;
        public static FinanceBUS Instance
        {
            get { if (instance == null) instance = new FinanceBUS(); return instance; }
        }
        private FinanceBUS() { }

        public DataTable GetFullHistory() => FinanceDAO.Instance.GetFullHistory();
        public bool InsertChi(string maDoiTuong, decimal soTien, string noiDung) => FinanceDAO.Instance.InsertChi(maDoiTuong, soTien, noiDung);
        public decimal GetTotalByRole(string type) => FinanceDAO.Instance.GetTotalByRole(type);
        public DataTable GetHistoryByDate(DateTime from, DateTime to) => FinanceDAO.Instance.GetHistoryByDate(from, to);
        public decimal GetRevenueByDate(DateTime from, DateTime to) => FinanceDAO.Instance.GetRevenueByDate(from, to);
    }
}
