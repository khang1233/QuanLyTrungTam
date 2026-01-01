using QuanLyTrungTam.DAO;
using System;
using System.Data;

namespace QuanLyTrungTam.BUS
{
    public class DiemDanhBUS
    {
        private static DiemDanhBUS instance;
        public static DiemDanhBUS Instance
        {
            get { if (instance == null) instance = new DiemDanhBUS(); return instance; }
        }
        private DiemDanhBUS() { }

        public DataTable GetDiemDanhList(string maLop, DateTime ngay)
        {
            return DiemDanhDAO.Instance.GetDiemDanhList(maLop, ngay);
        }

        public bool SaveDiemDanh(string maLop, string maHV, DateTime ngay, bool coMat, string lyDo)
        {
            return DiemDanhDAO.Instance.SaveDiemDanh(maLop, maHV, ngay, coMat, lyDo);
        }
    }
}
