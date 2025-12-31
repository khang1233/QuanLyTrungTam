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

        // Lấy bảng điểm đầy đủ
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
            // Lấy điểm tất cả các lớp mà học viên này đã học
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
        // Lưu điểm (ĐÃ SỬA LỖI CÚ PHÁP SQL)
        public bool LuuDiem(string maHV, string maLop, double d1, double d2, double dGK, double dCK, string ghiChu)
        {
            // 1. Kiểm tra xem đã có điểm chưa
            // Lưu ý: Cần khoảng trắng quanh dấu = để DataProvider nhận diện đúng tham số
            string check = "SELECT COUNT(*) FROM Diem WHERE MaHV = @h AND MaLop = @l";
            int count = (int)DataProvider.Instance.ExecuteScalar(check, new object[] { maHV, maLop });

            string query;
            object[] paras;

            if (count > 0)
            {
                // TRƯỜNG HỢP UPDATE:
                // Lưu ý thứ tự tham số phải khớp với thứ tự @ trong câu lệnh SQL
                // Thêm khoảng trắng xung quanh các dấu , và = để DataProvider cắt chuỗi chính xác
                query = "UPDATE Diem SET Diem15p1 = @d1 , Diem15p2 = @d2 , DiemGiuaKy = @dgk , DiemCuoiKy = @dck , GhiChu = @gc WHERE MaHV = @h AND MaLop = @l";

                // Thứ tự: d1 -> d2 -> dgk -> dck -> gc -> h -> l
                paras = new object[] { d1, d2, dGK, dCK, ghiChu, maHV, maLop };
            }
            else
            {
                // TRƯỜNG HỢP INSERT:
                // Thêm khoảng trắng trong VALUES ( ... )
                query = "INSERT INTO Diem (MaHV, MaLop, Diem15p1, Diem15p2, DiemGiuaKy, DiemCuoiKy, GhiChu) VALUES ( @h , @l , @d1 , @d2 , @dgk , @dck , @gc )";

                // Thứ tự: h -> l -> d1 -> d2 -> dgk -> dck -> gc
                paras = new object[] { maHV, maLop, d1, d2, dGK, dCK, ghiChu };
            }

            return DataProvider.Instance.ExecuteNonQuery(query, paras) > 0;
        }
    }
}