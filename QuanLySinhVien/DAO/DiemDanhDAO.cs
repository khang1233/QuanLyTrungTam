using QuanLyTrungTam.DTO;
using System;
using System.Data;

namespace QuanLyTrungTam.DAO
{
    public class DiemDanhDAO
    {
        private static DiemDanhDAO instance;
        public static DiemDanhDAO Instance
        {
            get { if (instance == null) instance = new DiemDanhDAO(); return instance; }
        }
        private DiemDanhDAO() { }

        // --- 1. HÀM LẤY DANH SÁCH (ĐÃ FIX LỖI LỆCH GIỜ) ---
        public DataTable GetDiemDanhList(string maLop, DateTime ngay)
        {
            // Cắt bỏ giờ phút để an toàn tuyệt đối
            DateTime safeDay = ngay.Date;

            // Câu truy vấn "Bất tử": Luôn lấy đủ danh sách học viên
            // Nếu đã điểm danh: Cột CoMat lấy giá trị thật
            // Nếu chưa điểm danh: Cột CoMat tự động là 0 (False)
            string query = @"
        SELECT 
            dk.MaHV, 
            hv.HoTen, 
            ISNULL(dd.CoMat, 0) as CoMat, 
            ISNULL(dd.LyDo, '') as LyDo
        FROM DangKy dk
        JOIN HocVien hv ON dk.MaHV = hv.MaHV
        LEFT JOIN DiemDanh dd ON dk.MaHV = dd.MaHV 
                              AND dk.MaLop = dd.MaLop 
                              AND dd.NgayDiemDanh = @ngay
        WHERE dk.MaLop = @maLop";

            return DataProvider.Instance.ExecuteQuery(query, new object[] { safeDay, maLop });
        }

        // --- 2. HÀM LƯU ĐIỂM DANH (GIỮ NGUYÊN CODE ĐÃ FIX THAM SỐ) ---
        public bool SaveDiemDanh(string maLop, string maHV, DateTime ngay, bool coMat, string lyDo)
        {
            DateTime safeDay = ngay.Date;

            // Kiểm tra xem đã có dòng này chưa
            string check = "SELECT COUNT(*) FROM DiemDanh WHERE MaLop = @ml AND MaHV = @mh AND NgayDiemDanh = @ng";
            int exist = (int)DataProvider.Instance.ExecuteScalar(check, new object[] { maLop, maHV, safeDay });

            if (exist > 0)
            {
                // UPDATE
                string query = "UPDATE DiemDanh SET CoMat = @cm , LyDo = @ld WHERE MaLop = @ml AND MaHV = @mh AND NgayDiemDanh = @ng";
                // Thứ tự tham số phải khớp với thứ tự @ trong câu lệnh
                return DataProvider.Instance.ExecuteNonQuery(query, new object[] { coMat, lyDo, maLop, maHV, safeDay }) > 0;
            }
            else
            {
                // INSERT
                string query = "INSERT INTO DiemDanh (MaLop, MaHV, NgayDiemDanh, CoMat, LyDo) VALUES ( @ml , @mh , @ng , @cm , @ld )";
                return DataProvider.Instance.ExecuteNonQuery(query, new object[] { maLop, maHV, safeDay, coMat, lyDo }) > 0;
            }
        }
    }
}