using QuanLyTrungTam.DAO;
using System.Data;

namespace QuanLyTrungTam.BUS
{
    public class DiemBUS
    {
        private static DiemBUS instance;
        public static DiemBUS Instance
        {
            get { if (instance == null) instance = new DiemBUS(); return instance; }
        }
        private DiemBUS() { }

        public DataTable GetBangDiemLop(string maLop)
        {
            return DiemDAO.Instance.GetBangDiemLop(maLop);
        }

        public DataTable GetBangDiemCaNhan(string maHV)
        {
            return DiemDAO.Instance.GetBangDiemCaNhan(maHV);
        }

        public bool LuuDiem(string maHV, string maLop, double d1, double d2, double dGK, double dCK, string ghiChu)
        {
            return DiemDAO.Instance.LuuDiem(maHV, maLop, d1, d2, dGK, dCK, ghiChu);
        }
    }
}
