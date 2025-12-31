using System;
using System.Data;

namespace QuanLyTrungTam.DTO
{
    public class HocVienDTO
    {
        public string MaHV { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgayGiaNhap { get; set; }
        public string TrangThai { get; set; }

        public HocVienDTO(DataRow row)
        {
            this.MaHV = row["MaHV"].ToString();
            this.HoTen = row["HoTen"].ToString();
            this.NgaySinh = row["NgaySinh"] != DBNull.Value ? (DateTime?)row["NgaySinh"] : null;
            this.SDT = row["SDT"].ToString();
            this.Email = row["Email"].ToString();
            this.DiaChi = row["DiaChi"].ToString();
            this.NgayGiaNhap = (DateTime)row["NgayGiaNhap"];
            this.TrangThai = row["TrangThai"].ToString();
        }
    }
}