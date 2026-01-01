using QuanLyTrungTam.DAO;
using System;
using System.Data;

namespace QuanLyTrungTam.BUS
{
    public class HocPhiInfo
    {
        public decimal TongNo { get; set; }
        public decimal DaDong { get; set; }
        public decimal ConNo { get; set; }
    }

    public class TuitionBUS
    {
        private static TuitionBUS instance;
        public static TuitionBUS Instance
        {
            get { if (instance == null) instance = new TuitionBUS(); return instance; }
        }
        private TuitionBUS() { }

        public DataTable GetListDangKy(string maHV)
        {
            return TuitionDAO.Instance.GetListDangKy(maHV);
        }

        // Logic nghiệp vụ tính toán nợ
        public HocPhiInfo GetHocPhiInfo(string maHV)
        {
            decimal tong = TuitionDAO.Instance.GetTongNo(maHV);
            decimal daDong = TuitionDAO.Instance.GetDaDong(maHV);
            return new HocPhiInfo
            {
                TongNo = tong,
                DaDong = daDong,
                ConNo = tong - daDong
            };
        }

        // Các hàm DAO wrapper
        public decimal GetTongNo(string maHV)
        {
            return TuitionDAO.Instance.GetTongNo(maHV);
        }

        public decimal GetDaDong(string maHV)
        {
            return TuitionDAO.Instance.GetDaDong(maHV);
        }

        public bool InsertThanhToan(string maHV, decimal soTien, string ghiChu)
        {
            return TuitionDAO.Instance.InsertThanhToan(maHV, soTien, ghiChu);
        }

        public bool DangKyLop(string maHV, string maLop, decimal hocPhiLop)
        {
            return TuitionDAO.Instance.DangKyLop(maHV, maLop, hocPhiLop);
        }

        public bool HuyDangKy(string maHV, string maLop)
        {
            return TuitionDAO.Instance.HuyDangKy(maHV, maLop);
        }
    }
}
