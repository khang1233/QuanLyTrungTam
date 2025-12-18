using System.Data;

namespace QuanLyTrungTam.DTO
{
    public class Account
    {
        // Các thuộc tính cơ bản
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; } // (Lưu ý: Mật khẩu nên bảo mật, nhưng ở đây ta giữ nguyên để test)
        public string Quyen { get; set; }

        // [QUAN TRỌNG] Thêm thuộc tính này để lưu mã SV/GV
        public string MaNguoiDung { get; set; }

        public Account(string userName, string passWord, string quyen, string maNguoiDung)
        {
            this.TenDangNhap = userName;
            this.MatKhau = passWord;
            this.Quyen = quyen;
            this.MaNguoiDung = maNguoiDung;
        }

        public Account(DataRow row)
        {
            this.TenDangNhap = row["TenDangNhap"].ToString();
            this.MatKhau = row["MatKhau"].ToString();
            this.Quyen = row["Quyen"].ToString();

            // [SỬA LỖI TẠI ĐÂY]
            // Kiểm tra xem trong SQL cột MaNguoiDung có null không trước khi lấy
            if (row["MaNguoiDung"] != System.DBNull.Value)
            {
                this.MaNguoiDung = row["MaNguoiDung"].ToString();
            }
            else
            {
                this.MaNguoiDung = ""; // Hoặc null tùy bạn xử lý
            }
        }
    }
}