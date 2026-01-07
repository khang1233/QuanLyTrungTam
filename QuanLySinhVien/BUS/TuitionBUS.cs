using QuanLyTrungTam.DAO;
using System.Data;

namespace QuanLyTrungTam.BUS
{
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

        public DTO.HocPhiInfo GetHocPhiInfo(string maHV)
        {
            decimal tong = GetTongNo(maHV);
            decimal daDong = GetDaDong(maHV);
            return new DTO.HocPhiInfo
            {
                TongHocPhi = tong,
                DaDong = daDong,
                ConNo = tong - daDong
            };
        }
    }
}
