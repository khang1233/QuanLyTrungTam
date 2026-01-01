using System.Data;
using System;

namespace QuanLyTrungTam.DAO
{
    public class DiemDAO
    {
        private static DiemDAO instance;
        public static DiemDAO Instance
        {
            get { if (instance == null) instance = new DiemDAO(); return instance; }
        }
        private DiemDAO() { }

        // =============================================================
        // 1. DATA ACCESS ONLY
        // =============================================================

        public DataTable GetBangDiemLop(string maLop)
        {
            string query = @"
                SELECT 
                    hv.MaHV, 
                    hv.HoTen, 
                    ISNULL(d.Diem15p1, 0) as Diem15p1, 
                    ISNULL(d.Diem15p2, 0) as Diem15p2,
                    ISNULL(d.DiemGiuaKy, 0) as DiemGiuaKy,
                    ISNULL(d.DiemCuoiKy, 0) as DiemCuoiKy,
                    d.DiemTongKet, 
                    ISNULL(d.GhiChu, '') as GhiChu
                FROM DangKy dk
                JOIN HocVien hv ON dk.MaHV = hv.MaHV
                LEFT JOIN Diem d ON dk.MaHV = d.MaHV AND dk.MaLop = d.MaLop
                WHERE dk.MaLop = @maLop";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { maLop });
        }

        public DataTable GetBangDiemCaNhan(string maHV)
        {
            string query = @"
                SELECT 
                    l.TenLop, 
                    k.TenKyNang AS MonHoc,
                    d.Diem15p1, d.Diem15p2, d.DiemGiuaKy, d.DiemCuoiKy, 
                    d.DiemTongKet,
                    d.GhiChu
                FROM Diem d
                JOIN LopHoc l ON d.MaLop = l.MaLop
                JOIN KyNang k ON l.MaKyNang = k.MaKyNang
                WHERE d.MaHV = @maHV";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { maHV });
        }

        public bool CheckDiemExist(string maHV, string maLop)
        {
            string check = "SELECT COUNT(*) FROM Diem WHERE MaHV = @h AND MaLop = @l";
            int count = (int)DataProvider.Instance.ExecuteScalar(check, new object[] { maHV, maLop });
            return count > 0;
        }

        public bool InsertDiem(string maHV, string maLop, double d1, double d2, double dGK, double dCK, string ghiChu)
        {
            string query = "INSERT INTO Diem (MaHV, MaLop, Diem15p1, Diem15p2, DiemGiuaKy, DiemCuoiKy, GhiChu) VALUES ( @h , @l , @d1 , @d2 , @dgk , @dck , @gc )";
            object[] paras = new object[] { maHV, maLop, d1, d2, dGK, dCK, ghiChu };
            return DataProvider.Instance.ExecuteNonQuery(query, paras) > 0;
        }
        // =============================================================
        // Hàm tổng hợp: Tự động kiểm tra để Thêm mới hoặc Cập nhật (Upsert)
        // =============================================================
        public bool LuuDiem(string maHV, string maLop, double d1, double d2, double dGK, double dCK, string ghiChu)
        {
            // 1. Kiểm tra xem điểm của học viên này trong lớp này đã có chưa
            bool daTonTai = CheckDiemExist(maHV, maLop);

            if (daTonTai)
            {
                // 2. Nếu có rồi -> Gọi hàm Update
                return UpdateDiem(maHV, maLop, d1, d2, dGK, dCK, ghiChu);
            }
            else
            {
                // 3. Nếu chưa có -> Gọi hàm Insert
                return InsertDiem(maHV, maLop, d1, d2, dGK, dCK, ghiChu);
            }
        }
        public bool UpdateDiem(string maHV, string maLop, double d1, double d2, double dGK, double dCK, string ghiChu)
        {
            string query = "UPDATE Diem SET Diem15p1 = @d1 , Diem15p2 = @d2 , DiemGiuaKy = @dgk , DiemCuoiKy = @dck , GhiChu = @gc WHERE MaHV = @h AND MaLop = @l";
            object[] paras = new object[] { d1, d2, dGK, dCK, ghiChu, maHV, maLop };
            return DataProvider.Instance.ExecuteNonQuery(query, paras) > 0;
        }
    }
}