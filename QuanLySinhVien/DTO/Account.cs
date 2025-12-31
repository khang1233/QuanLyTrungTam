using System;
using System.Data;

namespace QuanLyTrungTam.DTO
{
    public class Account
    {
        // =========================================================================
        // 1. CÁC PROPERTY CHUẨN (TIẾNG ANH)
        // =========================================================================
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Type { get; set; }        // Quyen (Admin, GiaoVien...)
        public string DisplayName { get; set; } // MaNguoiDung
        public string TrangThai { get; set; }

        // =========================================================================
        // 2. CÁC PROPERTY CẦU NỐI (GIÚP CODE CŨ CHẠY ĐƯỢC VỚI CODE MỚI)
        // =========================================================================

        // Code cũ gọi 'TenDangNhap' -> Trả về UserName
        public string TenDangNhap
        {
            get { return UserName; }
            set { UserName = value; }
        }

        // Code cũ gọi 'Quyen' -> Trả về Type
        public string Quyen
        {
            get { return Type; }
            set { Type = value; }
        }

        // Code cũ gọi 'MatKhau' -> Trả về PassWord
        public string MatKhau
        {
            get { return PassWord; }
            set { PassWord = value; }
        }

        // Code cũ gọi 'MaNguoiDung' -> Trả về DisplayName
        public string MaNguoiDung
        {
            get { return DisplayName; }
            set { DisplayName = value; }
        }

        // =========================================================================
        // 3. CONSTRUCTOR (HÀM TẠO)
        // =========================================================================

        // [MỚI THÊM] Hàm tạo rỗng này BẮT BUỘC phải có để dùng cú pháp "new Account { ... }"
        public Account() { }

        // Hàm tạo đầy đủ tham số
        public Account(string userName, string displayName, string type, string password = null, string trangThai = "Hoạt động")
        {
            this.UserName = userName;
            this.DisplayName = displayName;
            this.Type = type;
            this.PassWord = password;
            this.TrangThai = trangThai;
        }

        // Hàm tạo từ Database (DataRow)
        public Account(DataRow row)
        {
            // Sử dụng .ToString() để tránh lỗi InvalidCastException
            this.UserName = row["TenDangNhap"].ToString();
            this.PassWord = row["MatKhau"].ToString();
            this.Type = row["Quyen"].ToString();

            // Xử lý MaNguoiDung (có thể null)
            if (row["MaNguoiDung"] != DBNull.Value)
                this.DisplayName = row["MaNguoiDung"].ToString();
            else
                this.DisplayName = "";

            // Xử lý TrangThai (đề phòng chưa có cột này)
            if (row.Table.Columns.Contains("TrangThai") && row["TrangThai"] != DBNull.Value)
                this.TrangThai = row["TrangThai"].ToString();
            else
                this.TrangThai = "Hoạt động";
        }
    }
}