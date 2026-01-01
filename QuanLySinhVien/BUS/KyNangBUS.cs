using QuanLyTrungTam.DAO;
using System;
using System.Collections.Generic;
using System.Data;

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
            return KyNangDAO.Instance.InsertKyNang(ma, ten, hinhThuc, moTa, soBuoi, donGia, trangThai);
        }

        public bool UpdateKyNang(string ma, string ten, string hinhthuc, string mota, int sobuoi, decimal donGia, string trangthai)
        {
            return KyNangDAO.Instance.UpdateKyNang(ma, ten, hinhthuc, mota, sobuoi, donGia, trangthai);
        }

        public bool DeleteKyNang(string maKyNang)
        {
            return KyNangDAO.Instance.DeleteKyNang(maKyNang);
        }
    }
}
