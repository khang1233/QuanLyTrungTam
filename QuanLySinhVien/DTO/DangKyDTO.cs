using System.Data;
using System;

namespace QuanLyTrungTam.DTO
{
    public class DangKyDTO
    {
        public string TenKyNang { get; set; }
        public string TenLop { get; set; }
        public decimal HocPhiLop { get; set; }
        public DateTime NgayDangKy { get; set; }

        public DangKyDTO(DataRow row)
        {
            this.TenKyNang = row["TenKyNang"].ToString();
            this.TenLop = row["TenLop"].ToString();
            this.HocPhiLop = (decimal)row["HocPhiLop"];
            this.NgayDangKy = (DateTime)row["NgayDangKy"];
        }
    }
}