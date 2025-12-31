using System;
using System.Data;
using System.Collections.Generic; // Cần cho List
using System.Linq; // Cần cho Intersect (so sánh mảng)
using QuanLyTrungTam.Utilities;

namespace QuanLyTrungTam.DAO
{
    public class LopHocDAO
    {
        // --- SINGLETON PATTERN ---
        private static LopHocDAO instance;
        public static LopHocDAO Instance
        {
            get { if (instance == null) instance = new LopHocDAO(); return instance; }
        }
        private LopHocDAO() { }

        // =============================================================
        // 1. LOGIC XỬ LÝ CHUỖI NGÀY & KIỂM TRA TRÙNG (CORE LOGIC)
        // =============================================================

        /// <summary>
        /// Hàm tách chuỗi ngày phức tạp thành danh sách đơn giản.
        /// VD: "T3 (Thứ Ba)" -> {"T3"}
        /// VD: "T3-T5-T7" -> {"T3", "T5", "T7"}
        /// </summary>
        private List<string> PhanTichNgayHoc(string chuoiThu)
        {
            List<string> result = new List<string>();
            if (string.IsNullOrEmpty(chuoiThu)) return result;

            // 1. Loại bỏ phần chú thích trong ngoặc đơn (nếu có)
            string raw = chuoiThu;
            if (raw.Contains("(")) raw = raw.Split('(')[0];

            // 2. Tách theo dấu gạch ngang hoặc dấu phẩy
            string[] parts = raw.Split(new char[] { '-', ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in parts)
            {
                string cleanPart = part.Trim().ToUpper();
                if (!string.IsNullOrEmpty(cleanPart)) result.Add(cleanPart);
            }
            return result;
        }

        /// <summary>
        /// Kiểm tra xem 2 chuỗi thứ có ngày nào trùng nhau không.
        /// </summary>
        private bool CheckTrungThu(string thuInput, string thuDb)
        {
            var listInput = PhanTichNgayHoc(thuInput);
            var listDb = PhanTichNgayHoc(thuDb);
            // Nếu có ít nhất 1 phần tử chung -> Trùng
            return listInput.Intersect(listDb).Any();
        }

        /// <summary>
        /// [QUAN TRỌNG] Hàm trả về THÔNG BÁO LỖI CỤ THỂ (String) thay vì True/False
        /// Trả về null nếu không có lỗi.
        /// </summary>
        public string GetConflictMessage(string maPhong, string maGV, string maTG, string thuInput, string caInput, string maLopHienTai = "")
        {
            // Bước 1: Lấy tất cả các lớp đang hoạt động trong cùng Ca học
            string query = @"SELECT MaLop, TenLop, Thu, MaPhong, MaGiaoVien, MaTroGiang 
                             FROM LopHoc 
                             WHERE CaHoc = @ca 
                             AND TrangThai != N'Đã kết thúc' 
                             AND MaLop != @maLop";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { caInput, maLopHienTai });

            // Bước 2: Duyệt từng lớp để so sánh logic
            foreach (DataRow row in dt.Rows)
            {
                string dbThu = row["Thu"].ToString();

                // Nếu ngày không trùng thì bỏ qua (VD: T2 so với T3-T5)
                if (!CheckTrungThu(thuInput, dbThu)) continue;

                // --- NẾU ĐÃ TRÙNG NGÀY + TRÙNG CA THÌ KIỂM TRA ĐỐI TƯỢNG ---
                string lopGayTrung = $"{row["TenLop"]} ({row["MaLop"]})";
                string dbPhong = row["MaPhong"].ToString();
                string dbGV = row["MaGiaoVien"].ToString();
                string dbTG = row["MaTroGiang"].ToString();

                // 1. Check Trùng Phòng
                if (!string.IsNullOrEmpty(maPhong) && maPhong == dbPhong)
                {
                    return $"Phòng '{maPhong}' bị trùng lịch với lớp: {lopGayTrung}.";
                }

                // 2. Check Trùng Giáo Viên (So với cả GV và TG của lớp kia)
                if (!string.IsNullOrEmpty(maGV))
                {
                    if (maGV == dbGV) return $"Giảng viên đã có lịch dạy lớp: {lopGayTrung}.";
                    if (maGV == dbTG) return $"Giảng viên đang làm trợ giảng cho lớp: {lopGayTrung}.";
                }

                // 3. Check Trùng Trợ Giảng
                if (!string.IsNullOrEmpty(maTG))
                {
                    if (maTG == dbGV) return $"Trợ giảng đang là GV chính của lớp: {lopGayTrung}.";
                    if (maTG == dbTG) return $"Trợ giảng đã có lịch tại lớp: {lopGayTrung}.";
                }
            }

            // Kiểm tra nội bộ: GV và TG không được là 1 người
            if (!string.IsNullOrEmpty(maGV) && !string.IsNullOrEmpty(maTG) && maGV == maTG)
            {
                return "Giáo viên và Trợ giảng không thể là cùng một người!";
            }

            return null; // Không có lỗi
        }

        // =============================================================
        // 2. CÁC HÀM CRUD (THÊM, SỬA, XÓA, LẤY DỮ LIỆU)
        // =============================================================

        public DataTable GetAllLop()
        {
            string query = @"
                SELECT 
                    L.MaLop, L.TenLop, L.Thu, L.CaHoc, L.SiSoToiDa, L.TrangThai, 
                    L.MaGiaoVien, L.MaTroGiang, L.MaPhong, L.MaKyNang,
                    L.NgayBatDau,
                    ISNULL(L.NgayKetThuc, DATEADD(month, 3, L.NgayBatDau)) as NgayKetThuc,
                    K.TenKyNang, K.SoBuoi,
                    P.TenPhong,
                    NS1.HoTen as TenGV,
                    NS2.HoTen as TenTG
                FROM LopHoc L 
                JOIN KyNang K ON L.MaKyNang = K.MaKyNang
                LEFT JOIN PhongHoc P ON L.MaPhong = P.MaPhong
                LEFT JOIN NhanSu NS1 ON L.MaGiaoVien = NS1.MaNS
                LEFT JOIN NhanSu NS2 ON L.MaTroGiang = NS2.MaNS";

            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable GetListLopByKyNang(string maKyNang)
        {
            string query = "SELECT * FROM LopHoc WHERE MaKyNang = @maKN";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { maKyNang });
        }

        public string GetNewMaLop(string maKyNang)
        {
            string query = "SELECT COUNT(*) FROM LopHoc WHERE MaKyNang = @maKN";
            int count = (int)DataProvider.Instance.ExecuteScalar(query, new object[] { maKyNang });
            return "L" + maKyNang + (count + 1).ToString("D2");
        }

        public bool InsertLopFull(string maLop, string tenLop, string maKN, string maGV, string maTG, string maPhong, string thu, string ca, int siSo, DateTime ngayBD)
        {
            // Lưu ý: Việc check trùng đã được thực hiện ở Form trước khi gọi hàm này
            string query = @"INSERT INTO LopHoc (MaLop, TenLop, MaKyNang, MaGiaoVien, MaTroGiang, MaPhong, Thu, CaHoc, SiSoToiDa, NgayBatDau, TrangThai)
                             VALUES ( @maL , @tenL , @maKN , @maGV , @tg , @ph , @thu , @ca , @si , @ngay , N'Đang tuyển sinh')";

            object pTG = string.IsNullOrEmpty(maTG) ? DBNull.Value : (object)maTG;
            object pPhong = string.IsNullOrEmpty(maPhong) ? DBNull.Value : (object)maPhong;

            object[] para = { maLop, tenLop, maKN, maGV, pTG, pPhong, thu, ca, siSo, ngayBD };
            return DataProvider.Instance.ExecuteNonQuery(query, para) > 0;
        }

        public bool UpdateLopFull(string maLop, string tenLop, string maGV, string maTG, string maPhong, string thu, string ca, int siSo, string trangThai)
        {
            // Lưu ý: Việc check trùng đã được thực hiện ở Form trước khi gọi hàm này
            string query = @"UPDATE LopHoc SET 
                                TenLop = @t, 
                                MaGiaoVien = @g, 
                                MaTroGiang = @tg, 
                                MaPhong = @p, 
                                Thu = @thu, 
                                CaHoc = @ca, 
                                SiSoToiDa = @si, 
                                TrangThai = @tt 
                             WHERE MaLop = @id";

            object pTG = string.IsNullOrEmpty(maTG) ? DBNull.Value : (object)maTG;
            object pPhong = string.IsNullOrEmpty(maPhong) ? DBNull.Value : (object)maPhong;

            object[] para = { tenLop, maGV, pTG, pPhong, thu, ca, siSo, trangThai, maLop };
            return DataProvider.Instance.ExecuteNonQuery(query, para) > 0;
        }

        public DataTable GetLopByNhanSu(string maNS)
        {
            string query = @"SELECT l.MaLop, l.TenLop FROM LopHoc l WHERE l.MaGiaoVien = @m1 OR l.MaTroGiang = @m2";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { maNS, maNS });
        }

        public bool DeleteLop(string maLop)
        {
            DataProvider.Instance.ExecuteNonQuery("DELETE FROM DangKy WHERE MaLop = @id", new object[] { maLop });
            return DataProvider.Instance.ExecuteNonQuery("DELETE FROM LopHoc WHERE MaLop = @id", new object[] { maLop }) > 0;
        }

        public DataTable GetScheduleByHocVien(string maHV)
        {
            string query = @"SELECT l.TenLop, l.Thu, l.CaHoc, p.TenPhong, ns.HoTen as TenGV
                             FROM DangKy dk
                             JOIN LopHoc l ON dk.MaLop = l.MaLop
                             LEFT JOIN PhongHoc p ON l.MaPhong = p.MaPhong
                             LEFT JOIN NhanSu ns ON l.MaGiaoVien = ns.MaNS
                             WHERE dk.MaHV = @maHV AND l.TrangThai != N'Đã kết thúc'";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { maHV });
        }

        public DataTable GetScheduleByGiaoVien(string maGV)
        {
            string query = @"SELECT l.TenLop, l.Thu, l.CaHoc, p.TenPhong, l.TrangThai
                             FROM LopHoc l
                             LEFT JOIN PhongHoc p ON l.MaPhong = p.MaPhong
                             WHERE l.MaGiaoVien = @maGV
                             ORDER BY CASE WHEN l.TrangThai = N'Đang học' THEN 1 ELSE 2 END";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { maGV });
        }

        public DataRow GetClassScheduleInfo(string maLop)
        {
            string query = @"SELECT l.NgayBatDau, l.Thu, k.SoBuoi 
                             FROM LopHoc l 
                             JOIN KyNang k ON l.MaKyNang = k.MaKyNang 
                             WHERE l.MaLop = @id";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { maLop });
            if (dt.Rows.Count > 0) return dt.Rows[0];
            return null;
        }
    }
}