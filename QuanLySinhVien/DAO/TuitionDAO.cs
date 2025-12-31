using QuanLyTrungTam.DAO;
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

        public DataTable GetListDangKy(string maHV)
        {
            string query = "SELECT k.TenKyNang, l.MaLop, l.TenLop, l.CaHoc, l.NgayBatDau, d.HocPhiLop " +
                           "FROM DangKy d " +
                           "JOIN LopHoc l ON d.MaLop = l.MaLop " +
                           "JOIN KyNang k ON l.MaKyNang = k.MaKyNang " +
                           "WHERE d.MaHV = @maHV";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { maHV });
        }

        public decimal GetTongNo(string maHV)
        {
            string query = "SELECT SUM(HocPhiLop) FROM DangKy WHERE MaHV = @maHV";
            object result = DataProvider.Instance.ExecuteScalar(query, new object[] { maHV });
            return (result != null && result != DBNull.Value) ? Convert.ToDecimal(result) : 0;
        }

        public decimal GetDaDong(string maHV)
        {
            string query = "SELECT SUM(SoTienDong) FROM ThanhToan WHERE MaHV = @maHV";
            object result = DataProvider.Instance.ExecuteScalar(query, new object[] { maHV });
            return (result != null && result != DBNull.Value) ? Convert.ToDecimal(result) : 0;
        }

        // --- CẬP NHẬT HÀM THANH TOÁN: GHI NHẬN VÀO SỔ CÁI TỔNG ---
        public bool InsertThanhToan(string maHV, decimal soTien, string ghiChu)
        {
            // 1. Ghi vào bảng ThanhToan (Chi tiết học viên)
            string queryTT = "INSERT INTO ThanhToan (MaHV, SoTienDong, GhiChu) VALUES ( @maHV , @soTien , @ghiChu )";
            bool checkTT = DataProvider.Instance.ExecuteNonQuery(queryTT, new object[] { maHV, soTien, ghiChu }) > 0;

            if (checkTT)
            {
                // 2. Ghi vào bảng GiaoDichTaiChinh (Sổ cái trung tâm)
                string noiDung = "Học phí: " + (string.IsNullOrEmpty(ghiChu) ? "" : ghiChu);
                string queryGD = "INSERT INTO GiaoDichTaiChinh (LoaiGD, MaDoiTuong, SoTien, NoiDung) VALUES ( N'Thu' , @maHV , @soTien , @noiDung )";
                DataProvider.Instance.ExecuteNonQuery(queryGD, new object[] { maHV, soTien, noiDung });
            }
            return checkTT;
        }

        public bool DangKyLop(string maHV, string maLop, decimal hocPhiLop)
        {
            // 1. Kiểm tra trạng thái học viên
            string queryStatus = "SELECT TrangThai FROM HocVien WHERE MaHV = @maHV";
            object res = DataProvider.Instance.ExecuteScalar(queryStatus, new object[] { maHV });
            string status = res != null ? res.ToString() : "";

            // Nếu đã Bỏ học hoặc Bảo lưu thì không cho đăng ký
            if (status == "Bỏ học" || status == "Bảo lưu")
            {
                return false;
            }

            // 2. Kiểm tra trùng môn/lớp (Học viên đã học lớp này chưa)
            string check = "SELECT COUNT(*) FROM DangKy WHERE MaHV = @maHV AND MaLop = @maLop";
            int result = (int)DataProvider.Instance.ExecuteScalar(check, new object[] { maHV, maLop });
            if (result > 0) return false;

            // 3. Thực hiện Insert
            string query = "INSERT INTO DangKy (MaHV, MaLop, HocPhiLop) VALUES ( @maHV , @maLop , @hocPhiLop )";
            bool success = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maHV, maLop, hocPhiLop }) > 0;

            // 4. CẬP NHẬT TRẠNG THÁI (Quan trọng)
            if (success)
            {
                // Gọi thủ tục SQL vừa tạo ở Bước 1
                try
                {
                    DataProvider.Instance.ExecuteNonQuery("EXEC USP_TuDongCapNhatTrangThai");
                }
                catch
                {
                    // Nếu lỗi SQL (quên tạo proc) thì bỏ qua, không làm crash app
                }
            }

            return success;
        }

        public bool HuyDangKy(string maHV, string maLop)
        {
            string query = "DELETE FROM DangKy WHERE MaHV = @maHV AND MaLop = @maLop";
            bool success = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maHV, maLop }) > 0;

            // [UPDATE MỚI] GỌI THỦ TỤC TỰ ĐỘNG CẬP NHẬT TRẠNG THÁI (Nếu hủy hết lớp sẽ về Nhập học)
            if (success)
            {
                DataProvider.Instance.ExecuteNonQuery("EXEC USP_TuDongCapNhatTrangThai");
            }

            return success;
        }
    }
}