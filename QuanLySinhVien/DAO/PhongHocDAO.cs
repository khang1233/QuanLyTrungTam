using System.Data;
using QuanLyTrungTam.DAO;

namespace QuanLyTrungTam.DAO
{
    public class PhongHocDAO
    {
        private static PhongHocDAO instance;
        public static PhongHocDAO Instance
        {
            get { if (instance == null) instance = new PhongHocDAO(); return instance; }
        }
        private PhongHocDAO() { }

        public DataTable GetListPhong()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT * FROM PhongHoc");
        }
    }
}