using QuanLyTrungTam.DAO;
using System;
using System.Data;

namespace QuanLyTrungTam.BUS
{
    public class NhanVienBUS
    {
        private static NhanVienBUS instance;
        public static NhanVienBUS Instance
        {
            get { if (instance == null) instance = new NhanVienBUS(); return instance; }
        }
        private NhanVienBUS() { }

        public DataTable GetListNhanVien()
        {
            return NhanVienDAO.Instance.GetListNhanVien();
        }

        public string GetChuyenNganh(string maNS)
        {
            return NhanVienDAO.Instance.GetChuyenNganh(maNS);
        }

        public bool InsertNhanSu(string hoTen, DateTime ngaySinh, string sdt, string email, string loaiNS, string chuyenNganh)
        {
            // Có thể thêm Validation logic ở đây
            return NhanVienDAO.Instance.InsertNhanSu(hoTen, ngaySinh, sdt, email, loaiNS, chuyenNganh);
        }

        public bool UpdateNhanSu(string maNS, string hoTen, DateTime ngaySinh, string sdt, string email, string loaiNS, string chuyenNganh)
        {
            return NhanVienDAO.Instance.UpdateNhanSu(maNS, hoTen, ngaySinh, sdt, email, loaiNS, chuyenNganh);
        }

        public bool DeleteNhanVien(string ma)
        {
            return NhanVienDAO.Instance.DeleteNhanVien(ma);
        }

        public string GetNewMaNS(string loaiNS)
        {
            return NhanVienDAO.Instance.GetNewMaNS(loaiNS);
        }

        // Logic cũ nếu cần
        public bool InsertNhanVien(string ma, string ten, string loai, string sdt, string email)
        {
            return NhanVienDAO.Instance.InsertNhanVien(ma, ten, loai, sdt, email);
        }
    }
}
