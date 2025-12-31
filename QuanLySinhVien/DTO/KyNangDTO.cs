using System;
using System.Data;

namespace QuanLyTrungTam.DTO
{
    public class KyNangDTO
    {
        public string MaKyNang { get; set; }
        public string TenKyNang { get; set; }
        public string HinhThuc { get; set; }
        public int SoBuoi { get; set; }
        public decimal DonGia { get; set; }
        public decimal HocPhi { get; set; }
        public string MoTa { get; set; }

        public KyNangDTO(DataRow row)
        {
            this.MaKyNang = row["MaKyNang"].ToString();
            this.TenKyNang = row["TenKyNang"].ToString();
            this.HinhThuc = row["HinhThuc"].ToString();
            this.SoBuoi = (int)row["SoBuoi"];
            this.DonGia = Convert.ToDecimal(row["DonGia"]);
            this.HocPhi = Convert.ToDecimal(row["HocPhi"]);
            this.MoTa = row["MoTa"].ToString();
        }
    }
}