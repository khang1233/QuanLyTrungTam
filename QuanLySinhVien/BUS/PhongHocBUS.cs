using QuanLyTrungTam.DAO;
using System.Data;

namespace QuanLyTrungTam.BUS
{
    public class PhongHocBUS
    {
        private static PhongHocBUS instance;
        public static PhongHocBUS Instance
        {
            get { if (instance == null) instance = new PhongHocBUS(); return instance; }
        }
        private PhongHocBUS() { }

        public DataTable GetListPhong()
        {
            return PhongHocDAO.Instance.GetListPhong();
        }
    }
}
