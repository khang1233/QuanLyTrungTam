using System;

namespace QuanLyTrungTam.Utilities
{
    public static class Constants
    {
        // --- ROLES ---
        public const string ROLE_GIAO_VIEN = "Giáo viên";
        public const string ROLE_TRO_GIANG = "Trợ giảng";
        public const string ROLE_HOC_VIEN = "HocVien"; // Keep as key used in logic
        public const string ROLE_ADMIN = "Admin";
        public const string ROLE_NHAN_SU = "NhanSu";

        // --- STATUS ---
        public const string STATUS_DANG_HOC = "Đang học";
        public const string STATUS_DA_KET_THUC = "Đã kết thúc";
        public const string STATUS_TAM_NGUNG = "Tạm ngưng";
        public const string STATUS_SAP_MO = "Sắp mở";
        
        public const string STATUS_DANG_LAM_VIEC = "Đang làm việc";
        public const string STATUS_DANG_GIANG_DAY = "Đang giảng dạy";
        public const string STATUS_DA_NGHI = "Đã nghỉ";

        // --- MESSAGES ---
        public const string MSG_LOAD_DATA_ERROR = "Lỗi tải dữ liệu: ";
        public const string MSG_UPDATE_SUCCESS = "Cập nhật thành công!";
        public const string MSG_ADD_SUCCESS = "Thêm mới thành công!";
        public const string MSG_DELETE_SUCCESS = "Xóa thành công!";
        public const string MSG_CONFIRM_DELETE = "Bạn có chắc chắn muốn xóa không?";
        
        // --- FORMATS ---
        public const string DATE_FORMAT_DISPLAY = "dd/MM/yyyy";
    }
}
