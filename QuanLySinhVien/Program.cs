using System;
using System.Windows.Forms;

namespace QuanLyTrungTam // <--- ĐÃ CHUẨN HÓA
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Chạy Form1 (Đăng nhập) đầu tiên
            Application.Run(new Form1());
        }
    }
}