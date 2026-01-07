using QuanLyTrungTam.DAO;
using QuanLyTrungTam.DTO;
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
            // Security Check
            if (UserSession.Instance.IsStudent()) 
                throw new UnauthorizedAccessException("Bạn không có quyền thêm học viên!");

            return HocVienDAO.Instance.InsertHocVien(ma, ten, ngaySinh, sdt, email, diaChi, trangThai);
        }

        public bool UpdateHocVien(string ma, string ten, DateTime ngaySinh, string sdt, string email, string diaChi, string trangThai)
        {
            // Security Check
            if (UserSession.Instance.IsStudent()) 
                throw new UnauthorizedAccessException("Bạn không có quyền chỉnh sửa thông tin học viên!");

            return HocVienDAO.Instance.UpdateHocVien(ma, ten, ngaySinh, sdt, email, diaChi, trangThai);
        }

        public bool DeleteHocVien(string maHV)
        {
            // Security Check: Chỉ Admin mới được xóa
            if (!UserSession.Instance.IsAdmin())
                throw new UnauthorizedAccessException("Chỉ Quản trị viên mới có quyền xóa học viên!");

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
