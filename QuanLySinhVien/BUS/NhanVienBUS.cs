using QuanLyTrungTam.DAO;
using QuanLyTrungTam.Utilities;
using System;
using System.Data;
using System.Linq;

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
            // Validation
            if (string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(loaiNS)) return false;

            return NhanVienDAO.Instance.InsertNhanSu(hoTen, ngaySinh, sdt, email, loaiNS, chuyenNganh);
        }

        public bool UpdateNhanSu(string maNS, string hoTen, DateTime ngaySinh, string sdt, string email, string loaiNS, string chuyenNganh)
        {
             if (string.IsNullOrWhiteSpace(maNS)) return false;

            return NhanVienDAO.Instance.UpdateNhanSu(maNS, hoTen, ngaySinh, sdt, email, loaiNS, chuyenNganh);
        }

        public bool DeleteNhanVien(string ma)
        {
            if (string.IsNullOrWhiteSpace(ma)) return false;
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

        // --- NEW BUSINESS LOGIC ---

        /// <summary>
        /// Lấy danh sách Giáo viên (có thể lọc theo chuyên ngành)
        /// </summary>
        public DataView GetListGiaoVien(string chuyenNganh = "")
        {
            DataTable dt = GetListNhanVien();
            DataView dv = new DataView(dt);
            
            // Note: Database might store "Giáo viên" or "Giáo Viên", so we use LIKE
            // But ideally we should standardize DB data.
            string filter = "LoaiNS LIKE 'Giáo%'"; 

            if (!string.IsNullOrEmpty(chuyenNganh))
            {
                // Escape special chars for RowFilter just in case
                string safeCN = chuyenNganh.Replace("'", "''").Replace("[", "[[]").Replace("%", "[%]");
                // Fixed: Downgraded to C# 5.0 (No string interpolation)
                filter += string.Format(" AND ChuyenNganh LIKE '%{0}%'", safeCN);
            }

            dv.RowFilter = filter;
            return dv;
        }

        /// <summary>
        /// Lấy danh sách Trợ giảng
        /// </summary>
        public DataView GetListTroGiang()
        {
            DataTable dt = GetListNhanVien();
            DataView dv = new DataView(dt);
            dv.RowFilter = "LoaiNS LIKE 'Trợ%'";
            return dv;
        }
    }
}
