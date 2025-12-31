using QuanLyTrungTam.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace QuanLyTrungTam.DAO
{
    public class NhanVienDAO
    {
        private static NhanVienDAO instance;
        public static NhanVienDAO Instance
        {
            get { if (instance == null) instance = new NhanVienDAO(); return instance; }
        }
        private NhanVienDAO() { }

        // =========================================================================
        // 1. LẤY DỮ LIỆU
        // =========================================================================

        public DataTable GetListNhanVien()
        {
            // Câu truy vấn thông minh: Tự động kiểm tra xem GV có đang đứng lớp nào không
            string query = @"
        SELECT 
            NS.MaNS, 
            NS.HoTen, 
            NS.NgaySinh, 
            NS.SDT, 
            NS.Email, 
            NS.LoaiNS, 
            NS.ChuyenNganh,
            
            -- LOGIC TRẠNG THÁI ĐỘNG --
            CASE 
                -- 1. Nếu là Giáo viên hoặc Trợ giảng
                WHEN NS.LoaiNS IN (N'Giáo viên', N'Trợ giảng') THEN
                    CASE 
                        -- Kiểm tra bảng LopHoc: Có lớp nào người này dạy mà CHƯA kết thúc không?
                        WHEN EXISTS (
                            SELECT 1 FROM LopHoc L 
                            WHERE (L.MaGiaoVien = NS.MaNS OR L.MaTroGiang = NS.MaNS) 
                            AND L.TrangThai != N'Đã kết thúc'
                        ) 
                        THEN N'Đang giảng dạy'
                        ELSE N'Đang trống lớp'
                    END
                
                -- 2. Nếu là Nhân viên hành chính -> Luôn là Đang làm việc
                ELSE N'Đang làm việc'
            END as TrangThai

        FROM NhanSu NS";

            return DataProvider.Instance.ExecuteQuery(query);
        }

        // [QUAN TRỌNG] Hàm mới: Lấy chuyên ngành của 1 giáo viên cụ thể
        // Hàm này được FrmLop sử dụng để cảnh báo nếu dạy trái ngành
        public string GetChuyenNganh(string maNS)
        {
            try
            {
                // Giả sử cột lưu chuyên ngành trong bảng NhanSu là 'ChuyenNganh'
                string query = "SELECT ChuyenNganh FROM NhanSu WHERE MaNS = @ma";
                object result = DataProvider.Instance.ExecuteScalar(query, new object[] { maNS });
                return result != null ? result.ToString() : "";
            }
            catch
            {
                return "";
            }
        }

        // =========================================================================
        // 2. XỬ LÝ NGHIỆP VỤ (THÊM, XÓA, SỬA)
        // =========================================================================

        // Sử dụng Stored Procedure để thêm nhân sự (Có xử lý chuyên ngành)
        public bool InsertNhanSu(string hoTen, DateTime ngaySinh, string sdt, string email, string loaiNS, string chuyenNganh)
        {
            // Nếu không phải Giáo viên thì chuyên ngành là null
            object cn = (loaiNS == "Giáo viên" || loaiNS == "Giáo Viên") ? (object)chuyenNganh : DBNull.Value;

            string query = "EXEC USP_ThemNhanSu @ten , @ns , @sdt , @email , @loai , @cn";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { hoTen, ngaySinh, sdt, email, loaiNS, cn }) > 0;
        }

        // Sử dụng Stored Procedure để cập nhật nhân sự
        public bool UpdateNhanSu(string maNS, string hoTen, DateTime ngaySinh, string sdt, string email, string loaiNS, string chuyenNganh)
        {
            object cn = (loaiNS == "Giáo viên" || loaiNS == "Giáo Viên") ? (object)chuyenNganh : DBNull.Value;

            string query = "EXEC USP_CapNhatNhanSu @ma , @ten , @ns , @sdt , @email , @loai , @cn";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { maNS, hoTen, ngaySinh, sdt, email, loaiNS, cn }) > 0;
        }

        public bool DeleteNhanVien(string ma)
        {
            // Xóa tài khoản trước (nếu có ràng buộc khóa ngoại)
            DataProvider.Instance.ExecuteNonQuery("DELETE FROM TaiKhoan WHERE MaNguoiDung = '" + ma + "'");
            // Sau đó xóa nhân sự
            return DataProvider.Instance.ExecuteNonQuery("DELETE FROM NhanSu WHERE MaNS = '" + ma + "'") > 0;
        }

        // =========================================================================
        // 3. TIỆN ÍCH (SINH MÃ TỰ ĐỘNG)
        // =========================================================================

        public string GetNewMaNS(string loaiNS)
        {
            // Xác định tiền tố (Prefix)
            string prefix = "NV"; // Mặc định nhân viên
            if (loaiNS.Contains("Giáo")) prefix = "GV";
            else if (loaiNS.Contains("Trợ")) prefix = "TG";

            // Lấy mã lớn nhất hiện tại của loại đó
            string query = "SELECT TOP 1 MaNS FROM NhanSu WHERE MaNS LIKE '" + prefix + "%' ORDER BY MaNS DESC";
            object result = DataProvider.Instance.ExecuteScalar(query);

            if (result == null || result == DBNull.Value) return prefix + "01";

            string lastMa = result.ToString(); // Ví dụ: GV15

            // Cắt bỏ prefix, lấy số và cộng 1
            if (lastMa.Length > 2 && int.TryParse(lastMa.Substring(2), out int nextID))
            {
                return prefix + (nextID + 1).ToString("D2"); // D2 để ra số 01, 02...
            }
            return prefix + "01";
        }

        // =========================================================================
        // 4. CÁC HÀM CŨ / LEGACY (GIỮ LẠI ĐỂ TRÁNH LỖI FORM CŨ)
        // =========================================================================

        public bool InsertNhanVien(string ma, string ten, string loai, string sdt, string email)
        {
            string query = string.Format("INSERT INTO NhanSu (MaNS, HoTen, LoaiNS, SDT, Email, TrangThai) VALUES ('{0}', N'{1}', N'{2}', '{3}', '{4}', N'Đang làm việc')", ma, ten, loai, sdt, email);
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }
    }
}