using System;
using System.Data;

namespace QuanLyTrungTam.DTO
{
    public class KyNangDTO
    {
        public string MaKyNang { get; set; }
        public string TenKyNang { get; set; }
        public decimal HocPhi { get; set; }

        public KyNangDTO(DataRow row)
        {
            this.MaKyNang = row["MaKyNang"].ToString();
            this.TenKyNang = row["TenKyNang"].ToString();
            // Xử lý an toàn nếu null
            this.HocPhi = row["HocPhi"] != DBNull.Value ? (decimal)row["HocPhi"] : 0;
        }
    }
}