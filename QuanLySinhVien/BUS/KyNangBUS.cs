using QuanLyTrungTam.DAO;
using System;
using System.Data;
using System.Collections.Generic;
using QuanLyTrungTam.Utilities;
using QuanLyTrungTam.DTO;

namespace QuanLyTrungTam.BUS
{
    public class KyNangBUS
    {
        private static KyNangBUS instance;
        public static KyNangBUS Instance
        {
            get { if (instance == null) instance = new KyNangBUS(); return instance; }
        }
        private KyNangBUS() { }

        public DataTable GetListKyNang()
        {
            return KyNangDAO.Instance.GetListKyNang();
        }

        public DataTable GetListKyNangActive()
        {
            return KyNangDAO.Instance.GetListKyNangActive();
        }

        public List<string> GetListChuyenNganh()
        {
            return KyNangDAO.Instance.GetListChuyenNganh();
        }

        public bool InsertKyNang(string ma, string ten, string hinhThuc, string moTa, int soBuoi, decimal donGia, string trangThai)
        {
            if (!UserSession.Instance.IsAdmin()) throw new UnauthorizedAccessException("Chỉ Quản trị viên được phép thêm môn học!");
            if (KyNangDAO.Instance.CheckDuplicateName(ten, "")) throw new Exception("Tên môn học đã tồn tại! Vui lòng chọn tên khác.");
            
            return KyNangDAO.Instance.InsertKyNang(ma, ten, hinhThuc, moTa, soBuoi, donGia, trangThai);
        }

        public bool UpdateKyNang(string ma, string ten, string hinhthuc, string mota, int sobuoi, decimal donGia, string trangthai)
        {
            if (!UserSession.Instance.IsAdmin()) throw new UnauthorizedAccessException("Chỉ Quản trị viên được phép sửa môn học!");
            if (KyNangDAO.Instance.CheckDuplicateName(ten, ma)) throw new Exception("Tên môn học đã tồn tại! Vui lòng chọn tên khác.");

            return KyNangDAO.Instance.UpdateKyNang(ma, ten, hinhthuc, mota, sobuoi, donGia, trangthai);
        }

        public bool DeleteKyNang(string maKyNang)
        {
             if (!UserSession.Instance.IsAdmin()) throw new UnauthorizedAccessException("Chỉ Quản trị viên được phép xóa môn học!");
            return KyNangDAO.Instance.DeleteKyNang(maKyNang);
        }
    }
}
