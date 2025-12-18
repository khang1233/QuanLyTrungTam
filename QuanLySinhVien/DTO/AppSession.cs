using System;
using QuanLyTrungTam.DTO; // Đảm bảo bạn đã có class Account trong DTO

// QUAN TRỌNG: Namespace phải trùng với Project của bạn
namespace QuanLyTrungTam.Utilities
{
    public static class AppSession
    {
        // Lưu trữ tài khoản đang đăng nhập
        public static Account CurrentUser { get; set; }
    }
}