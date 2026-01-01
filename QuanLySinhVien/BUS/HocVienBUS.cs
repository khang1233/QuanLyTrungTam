using QuanLyTrungTam.DAO;
using System;
using System.Data;

namespace QuanLyTrungTam.BUS
{
    public class HocVienBUS
    {
        private static HocVienBUS instance;
        public static HocVienBUS Instance
        {
            get { if (instance == null) instance = new HocVienBUS(); return instance; }
        }
        private HocVienBUS() { }

        public bool CapNhatTrangThaiHocVien(string maHV, string trangThaiMoi)
        {
            return HocVienDAO.Instance.CapNhatTrangThaiHocVien(maHV, trangThaiMoi);
        }

        public DataTable GetListHocVien()
        {
            return HocVienDAO.Instance.GetListHocVien();
        }

        public DataRow GetInfoHocVien(string maHV)
        {
            return HocVienDAO.Instance.GetInfoHocVien(maHV);
        }

        public string GetNewMaHV()
        {
            return HocVienDAO.Instance.GetNewMaHV();
        }

        public bool InsertHocVien(string ma, string ten, DateTime ngaySinh, string sdt, string email, string diaChi, string trangThai)
        {
            // Có thể thêm logic kiểm tra email/sdt trùng ở đây nếu cần (nhưng hiện tại DAO không check trùng email ở hàm Insert?)
            // Giữ nguyên logic DAO
            return HocVienDAO.Instance.InsertHocVien(ma, ten, ngaySinh, sdt, email, diaChi, trangThai);
        }

        public bool UpdateHocVien(string ma, string ten, DateTime ngaySinh, string sdt, string email, string diaChi, string trangThai)
        {
            return HocVienDAO.Instance.UpdateHocVien(ma, ten, ngaySinh, sdt, email, diaChi, trangThai);
        }

        public bool DeleteHocVien(string maHV)
        {
            return HocVienDAO.Instance.DeleteHocVien(maHV);
        }

        public bool UpdateEmailHocVien(string maHV, string email)
        {
            return HocVienDAO.Instance.UpdateEmailHocVien(maHV, email);
        }

        public DataTable GetLearningHistory(string maHV)
        {
            return HocVienDAO.Instance.GetLearningHistory(maHV);
        }
    }
}
