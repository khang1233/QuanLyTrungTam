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
            // Business Logic: Kiểm tra tồn tại để quyết định Thêm mới hay Cập nhật
            if (DiemDAO.Instance.CheckDiemExist(maHV, maLop))
            {
                return DiemDAO.Instance.UpdateDiem(maHV, maLop, d1, d2, dGK, dCK, ghiChu);
            }
            else
            {
                return DiemDAO.Instance.InsertDiem(maHV, maLop, d1, d2, dGK, dCK, ghiChu);
            }
        }
    }
}
