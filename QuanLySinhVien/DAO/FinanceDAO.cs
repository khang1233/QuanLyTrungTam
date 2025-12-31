using System;
using System.Data;

namespace QuanLyTrungTam.DAO
{
    public class FinanceDAO
    {
        private static FinanceDAO instance;

        public static FinanceDAO Instance
        {
            get { if (instance == null) instance = new FinanceDAO(); return instance; }
        }

        private FinanceDAO() { }

        // --- CÁC HÀM CŨ (GIỮ NGUYÊN ĐỂ KHÔNG LỖI CODE CŨ NẾU CÒN DÙNG) ---
        public DataTable GetFullHistory()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT * FROM GiaoDichTaiChinh ORDER BY NgayGD DESC");
        }

        public bool InsertChi(string maDoiTuong, decimal soTien, string noiDung)
        {
            string query = "INSERT INTO GiaoDichTaiChinh (LoaiGD, MaDoiTuong, SoTien, NoiDung) VALUES (N'Chi', @ma , @tien , @nd )";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { maDoiTuong, soTien, noiDung }) > 0;
        }

        public decimal GetTotalByRole(string type)
        {
            string query = "SELECT SUM(SoTien) FROM GiaoDichTaiChinh WHERE LoaiGD = @type";
            object res = DataProvider.Instance.ExecuteScalar(query, new object[] { type });
            return (res != null && res != DBNull.Value) ? Convert.ToDecimal(res) : 0;
        }

        // --- CÁC HÀM MỚI (PHỤC VỤ TRA CỨU THEO NGÀY) ---

        // 1. Lấy lịch sử giao dịch trong khoảng thời gian (Chỉ lấy THU hoặc Tất cả)
        public DataTable GetHistoryByDate(DateTime from, DateTime to)
        {
            // Lấy hết giao dịch trong khoảng ngày chọn
            string query = "SELECT IdGD, NgayGD, LoaiGD, SoTien, NoiDung, MaDoiTuong " +
                           "FROM GiaoDichTaiChinh " +
                           "WHERE NgayGD >= @tu AND NgayGD <= @den " +
                           "ORDER BY NgayGD DESC";

            // Chỉnh lại giờ của ngày kết thúc thành 23:59:59 để lấy trọn ngày
            DateTime toDate = new DateTime(to.Year, to.Month, to.Day, 23, 59, 59);

            return DataProvider.Instance.ExecuteQuery(query, new object[] { from, toDate });
        }

        // 2. Tính tổng doanh thu (Chỉ tính tiền THU) trong khoảng thời gian
        public decimal GetRevenueByDate(DateTime from, DateTime to)
        {
            string query = "SELECT SUM(SoTien) FROM GiaoDichTaiChinh WHERE LoaiGD = N'Thu' AND NgayGD >= @tu AND NgayGD <= @den";

            DateTime toDate = new DateTime(to.Year, to.Month, to.Day, 23, 59, 59);

            object result = DataProvider.Instance.ExecuteScalar(query, new object[] { from, toDate });
            return (result != null && result != DBNull.Value) ? Convert.ToDecimal(result) : 0;
        }
    }
}