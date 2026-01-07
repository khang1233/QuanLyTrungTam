using System;

namespace QuanLyTrungTam.DTO
{
    // Class quản lý phiá người dùng (Client-Side Session)
    public class UserSession
    {
        private static UserSession instance;
        public static UserSession Instance
        {
            get { if (instance == null) instance = new UserSession(); return instance; }
        }

        private UserSession() { }

        // Tài khoản hiện tại
        public Account CurrentUser { get; private set; }

        // Đăng nhập (Lưu Session)
        public void SetSession(Account acc)
        {
            this.CurrentUser = acc;
        }

        // Đăng xuất (Xóa Session)
        public void ClearSession()
        {
            this.CurrentUser = null;
        }

        // --- CHECK QUYỀN (AUTHORIZATION) ---
        
        public bool IsAdmin()
        {
            return CurrentUser != null && CurrentUser.Quyen == "Admin";
        }

        public bool IsTeacher()
        {
            return CurrentUser != null && (CurrentUser.Quyen == "GiaoVien" || CurrentUser.Quyen == "TroGiang");
        }

        public bool IsStudent()
        {
            return CurrentUser != null && CurrentUser.Quyen == "HocVien";
        }

        public bool IsStaff()
        {
            return CurrentUser != null && (CurrentUser.Quyen == "NhanSu" || CurrentUser.Quyen == "GiaoVien" || CurrentUser.Quyen == "TroGiang");
        }
    }
}
