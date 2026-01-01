using QuanLyTrungTam.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace QuanLyTrungTam.BUS
{
    public class LopHocBUS
    {
        private static LopHocBUS instance;
        public static LopHocBUS Instance
        {
            get { if (instance == null) instance = new LopHocBUS(); return instance; }
        }
        private LopHocBUS() { }

        // =============================================================
        // 1. BUSINESS LOGIC (MOVED FROM DAO & FORM)
        // =============================================================

        /// <summary>
        /// Logic tách chuỗi ngày: "T3 (Thứ Ba)" -> {"T3"}
        /// </summary>
        private List<string> PhanTichNgayHoc(string chuoiThu)
        {
            List<string> result = new List<string>();
            if (string.IsNullOrEmpty(chuoiThu)) return result;

            string raw = chuoiThu;
            if (raw.Contains("(")) raw = raw.Split('(')[0];

            string[] parts = raw.Split(new char[] { '-', ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in parts)
            {
                string cleanPart = part.Trim().ToUpper();
                if (!string.IsNullOrEmpty(cleanPart)) result.Add(cleanPart);
            }
            return result;
        }

        private bool CheckTrungThu(string thuInput, string thuDb)
        {
            var listInput = PhanTichNgayHoc(thuInput);
            var listDb = PhanTichNgayHoc(thuDb);
            return listInput.Intersect(listDb).Any();
        }

        public string GetConflictMessage(string maPhong, string maGV, string maTG, string thuInput, string caInput, string maLopHienTai = "")
        {
            // 1. Lấy tất cả các lớp trong cùng Ca đó từ DAO
            DataTable dt = LopHocDAO.Instance.GetClassesByCa(caInput, maLopHienTai);

            // 2. Logic so sánh trong bộ nhớ (BUS)
            foreach (DataRow row in dt.Rows)
            {
                string dbThu = row["Thu"].ToString();
                if (!CheckTrungThu(thuInput, dbThu)) continue;

                // Trùng lịch rồi -> Check đối tượng
                string lopGayTrung = string.Format("{0} ({1})", row["TenLop"], row["MaLop"]);
                string dbPhong = row["MaPhong"].ToString();
                string dbGV = row["MaGiaoVien"].ToString();
                string dbTG = row["MaTroGiang"].ToString();

                if (!string.IsNullOrEmpty(maPhong) && maPhong == dbPhong)
                    return string.Format("Phòng '{0}' bị trùng lịch với lớp: {1}.", maPhong, lopGayTrung);

                if (!string.IsNullOrEmpty(maGV))
                {
                    if (maGV == dbGV) return string.Format("Giảng viên đã có lịch dạy lớp: {0}.", lopGayTrung);
                    if (maGV == dbTG) return string.Format("Giảng viên đang làm trợ giảng cho lớp: {0}.", lopGayTrung);
                }

                if (!string.IsNullOrEmpty(maTG))
                {
                    if (maTG == dbGV) return string.Format("Trợ giảng đang là GV chính của lớp: {0}.", lopGayTrung);
                    if (maTG == dbTG) return string.Format("Trợ giảng đã có lịch tại lớp: {0}.", lopGayTrung);
                }
            }

            if (!string.IsNullOrEmpty(maGV) && !string.IsNullOrEmpty(maTG) && maGV == maTG)
            {
                return "Giáo viên và Trợ giảng không thể là cùng một người!";
            }

            return null;
        }

        // Logic từ Form chuyển xuống: Tính ngày kết thúc
        public DateTime CalculateEndDate(DateTime startDate, string scheduleStr, int totalSessions)
        {
            if (totalSessions <= 0) return startDate;
            List<DayOfWeek> days = new List<DayOfWeek>();
            if (scheduleStr.Contains("T2")) days.Add(DayOfWeek.Monday);
            if (scheduleStr.Contains("T3")) days.Add(DayOfWeek.Tuesday);
            if (scheduleStr.Contains("T4")) days.Add(DayOfWeek.Wednesday);
            if (scheduleStr.Contains("T5")) days.Add(DayOfWeek.Thursday);
            if (scheduleStr.Contains("T6")) days.Add(DayOfWeek.Friday);
            if (scheduleStr.Contains("T7")) days.Add(DayOfWeek.Saturday);
            if (scheduleStr.Contains("CN")) days.Add(DayOfWeek.Sunday);

            if (days.Count == 0) return startDate;
            DateTime currentDate = startDate;
            int sessionsCount = 0;
            for (int i = 0; i < 365; i++)
            {
                if (days.Contains(currentDate.DayOfWeek))
                {
                    sessionsCount++;
                    if (sessionsCount >= totalSessions) return currentDate;
                }
                currentDate = currentDate.AddDays(1);
            }
            return currentDate;
        }
        
        // =============================================================
        // 2. CRUD WRAPPERS
        // =============================================================

        public DataTable GetAllLop()
        {
            return LopHocDAO.Instance.GetAllLop();
        }

        public string GetNewMaLop(string maKyNang)
        {
            return LopHocDAO.Instance.GetNewMaLop(maKyNang);
        }

        public bool InsertLopFull(string maLop, string tenLop, string maKN, string maGV, string maTG, string maPhong, string thu, string ca, int siSo, DateTime ngayBD)
        {
            return LopHocDAO.Instance.InsertLopFull(maLop, tenLop, maKN, maGV, maTG, maPhong, thu, ca, siSo, ngayBD);
        }

        public bool UpdateLopFull(string maLop, string tenLop, string maGV, string maTG, string maPhong, string thu, string ca, int siSo, string trangThai)
        {
            return LopHocDAO.Instance.UpdateLopFull(maLop, tenLop, maGV, maTG, maPhong, thu, ca, siSo, trangThai);
        }

        public bool DeleteLop(string maLop)
        {
            return LopHocDAO.Instance.DeleteLop(maLop);
        }

        public DataTable GetScheduleByHocVien(string maHV)
        {
            return LopHocDAO.Instance.GetScheduleByHocVien(maHV);
        }

        public DataTable GetListLopByKyNang(string maKyNang)
        {
            return LopHocDAO.Instance.GetListLopByKyNang(maKyNang);
        }
    }
}
