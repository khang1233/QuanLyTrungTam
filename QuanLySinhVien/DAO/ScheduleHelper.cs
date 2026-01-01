using System;
using System.Collections.Generic;

namespace QuanLyTrungTam.Utilities
{
    public class BuoiHocDTO
    {
        public int BuoiSo { get; set; }
        public DateTime Ngay { get; set; }
        // Hiển thị lên ComboBox: "Buổi 1 - 22/12/2025 (Thứ Hai)"
        public string HienThi
        {
            get
            {
                return string.Format("Buổi {0} - {1:dd/MM/yyyy} ({2})", BuoiSo, Ngay, GetThuTiengViet(Ngay.DayOfWeek));
            }
        }

        private string GetThuTiengViet(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday: return "T2";
                case DayOfWeek.Tuesday: return "T3";
                case DayOfWeek.Wednesday: return "T4";
                case DayOfWeek.Thursday: return "T5";
                case DayOfWeek.Friday: return "T6";
                case DayOfWeek.Saturday: return "T7";
                case DayOfWeek.Sunday: return "CN";
                default: return "";
            }
        }
    }

    public static class ScheduleHelper
    {
        public static List<BuoiHocDTO> GenerateSchedule(DateTime startDay, string scheduleStr, int totalSessions)
        {
            List<BuoiHocDTO> list = new List<BuoiHocDTO>();
            if (string.IsNullOrEmpty(scheduleStr)) return list;

            // 1. Phân tích chuỗi lịch học (Ví dụ: "T2-T4-T6" -> [Monday, Wednesday, Friday])
            List<DayOfWeek> daysOfWeek = ParseScheduleString(scheduleStr);
            if (daysOfWeek.Count == 0) return list;

            // 2. Thuật toán nhảy cóc ngày
            DateTime currentDay = startDay;
            int count = 0;

            // Lặp cho đến khi đủ số buổi
            while (count < totalSessions)
            {
                // Nếu ngày hiện tại trùng với một trong các thứ trong lịch -> Ghi nhận là 1 buổi
                if (daysOfWeek.Contains(currentDay.DayOfWeek))
                {
                    count++;
                    list.Add(new BuoiHocDTO { BuoiSo = count, Ngay = currentDay });
                }
                // Tăng ngày lên 1 để kiểm tra tiếp
                currentDay = currentDay.AddDays(1);
            }

            return list;
        }

        private static List<DayOfWeek> ParseScheduleString(string str)
        {
            List<DayOfWeek> result = new List<DayOfWeek>();
            str = str.ToUpper(); // Chuẩn hóa chữ hoa

            if (str.Contains("T2")) result.Add(DayOfWeek.Monday);
            if (str.Contains("T3")) result.Add(DayOfWeek.Tuesday);
            if (str.Contains("T4")) result.Add(DayOfWeek.Wednesday);
            if (str.Contains("T5")) result.Add(DayOfWeek.Thursday);
            if (str.Contains("T6")) result.Add(DayOfWeek.Friday);
            if (str.Contains("T7")) result.Add(DayOfWeek.Saturday);
            if (str.Contains("CN")) result.Add(DayOfWeek.Sunday);

            return result;
        }
    }
}