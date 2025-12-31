using System;
using System.Data;

namespace QuanLyTrungTam.DTO
{
    public class NhanVienDTO
    {
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public string LoaiNV { get; set; } // 'GiaoVien' hoặc 'TroGiang'
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string TrangThai { get; set; }

        public NhanVienDTO(DataRow row)
        {
            this.MaNV = row["MaNV"].ToString();
            this.HoTen = row["HoTen"].ToString();
            this.LoaiNV = row["LoaiNV"].ToString();
            this.SoDienThoai = row["SoDienThoai"].ToString();
            this.Email = row["Email"].ToString();
            this.TrangThai = row["TrangThai"].ToString();
        }
    }
}