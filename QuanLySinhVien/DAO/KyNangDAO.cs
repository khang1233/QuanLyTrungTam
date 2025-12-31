using System;
using System.Collections.Generic;
using System.Data;

namespace QuanLyTrungTam.DAO
{
    public class KyNangDAO
    {
        private static KyNangDAO instance;
        public static KyNangDAO Instance
        {
            get { if (instance == null) instance = new KyNangDAO(); return instance; }
        }
        private KyNangDAO() { }

        // =========================================================================
        // 1. LẤY DỮ LIỆU
        // =========================================================================

        // Lấy danh sách đầy đủ (Dùng cho Form quản lý môn học để xem cả môn đã đóng)
        public DataTable GetListKyNang()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT * FROM KyNang");
        }

        // [MỚI - QUAN TRỌNG] Chỉ lấy môn ĐANG HOẠT ĐỘNG
        // Dùng cho FrmLop để tránh chọn nhầm môn đã ngưng
        public DataTable GetListKyNangActive()
        {
            // Lọc theo các trạng thái 'active' phổ biến
            string query = "SELECT * FROM KyNang WHERE TrangThai = N'Đang hoạt động' OR TrangThai = '1' OR TrangThai = N'Active'";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        // Lấy danh sách Chuyên Ngành (để fill vào combo hoặc gợi ý)
        public List<string> GetListChuyenNganh()
        {
            List<string> list = new List<string>();
            string query = "SELECT DISTINCT ChuyenNganh FROM KyNang WHERE ChuyenNganh IS NOT NULL AND ChuyenNganh <> ''";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["ChuyenNganh"].ToString());
            }
            return list;
        }

        // =========================================================================
        // 2. THÊM - XÓA - SỬA
        // =========================================================================

        public bool InsertKyNang(string ma, string ten, string hinhThuc, string moTa, int soBuoi, decimal donGia, string trangThai)
        {
            // Lưu ý: Nếu bạn muốn lưu Chuyên Ngành khi thêm mới, cần sửa thêm Stored Proc hoặc câu lệnh SQL ở đây
            string query = "INSERT INTO KyNang (MaKyNang, TenKyNang, HinhThuc, MoTa, SoBuoi, DonGia, TrangThai) " +
                           "VALUES ( @ma , @ten , @ht , @mt , @sb , @dg , @tt )";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { ma, ten, hinhThuc, moTa, soBuoi, donGia, trangThai }) > 0;
        }

        public bool UpdateKyNang(string ma, string ten, string hinhthuc, string mota, int sobuoi, decimal donGia, string trangthai)
        {
            string query = "UPDATE KyNang SET TenKyNang = @ten , HinhThuc = @hinh , MoTa = @mota , SoBuoi = @so , DonGia = @dg , TrangThai = @tt WHERE MaKyNang = @ma";
            object[] param = new object[] { ten, hinhthuc, mota, sobuoi, donGia, trangthai, ma };
            return DataProvider.Instance.ExecuteNonQuery(query, param) > 0;
        }

        public bool DeleteKyNang(string maKyNang)
        {
            // Kiểm tra ràng buộc trước khi xóa: Nếu môn này đã có lớp học thì không cho xóa
            string check = "SELECT COUNT(*) FROM LopHoc WHERE MaKyNang = @ma";
            int count = (int)DataProvider.Instance.ExecuteScalar(check, new object[] { maKyNang });

            if (count > 0) return false; // Trả về false nếu đang được sử dụng

            return DataProvider.Instance.ExecuteNonQuery("DELETE FROM KyNang WHERE MaKyNang = @ma", new object[] { maKyNang }) > 0;
        }
    }
}