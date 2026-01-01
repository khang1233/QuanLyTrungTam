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

        public DataTable GetHistoryByDate(DateTime from, DateTime to)
        {
            return FinanceDAO.Instance.GetHistoryByDate(from, to);
        }

        public decimal GetRevenueByDate(DateTime from, DateTime to)
        {
            return FinanceDAO.Instance.GetRevenueByDate(from, to);
        }
    }
}
