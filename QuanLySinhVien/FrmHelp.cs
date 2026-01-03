
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyTrungTam
{
    public class FrmHelp : Form
    {
        public FrmHelp()
        {
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Trợ Giúp & Giới Thiệu";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 10);
            this.ShowIcon = false;
            this.BackColor = Color.White;

            // Header
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.FromArgb(33, 150, 243) }; // Blue
            Label lblTitle = new Label { Text = "THÔNG TIN & HƯỚNG DẪN SỬ DỤNG", Location = new Point(20, 15), AutoSize = true, Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.White };
            pnlHeader.Controls.Add(lblTitle);
            this.Controls.Add(pnlHeader);

            // Tab Control
            TabControl tab = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 11) };
            this.Controls.Add(tab);
            tab.BringToFront();

            // TAB 1: GIỚI THIỆU
            TabPage tabIntro = new TabPage("Giới Thiệu Chung");
            tabIntro.Padding = new Padding(20);
            tabIntro.AutoScroll = true;
            tabIntro.BackColor = Color.White;

            Label lblAppName = new Label { Text = "HỆ THỐNG QUẢN LÝ TRUNG TÂM ĐÀO TẠO", Font = new Font("Segoe UI", 18, FontStyle.Bold), ForeColor = Color.FromArgb(33, 150, 243), AutoSize = true, Location = new Point(30, 30) };
            Label lblVer = new Label { Text = "Cập nhật ngày: 01/01/2026", AutoSize = true, Location = new Point(30, 70) };
            Label lblDesc = new Label 
            { 
                Text = "Phần mềm hỗ trợ quản lý toàn diện cho trung tâm đào tạo ngắn hạn, bao gồm:\n\n" +
                       "   ● Quản lý hồ sơ học viên, nhân sự, giáo viên.\n" +
                       "   ● Xếp lịch học, quản lý lớp học, điểm danh.\n" +
                       "   ● Quản lý học phí, lợi nhuận.\n" +
                       "   ● Báo cáo thống kê trực quan (Dashboard).\n\n" +
                       "Mục tiêu: Tối ưu hóa quy trình vận hành, giảm tải áp lực quản lý thủ công và nâng cao hiệu quả đào tạo.",
                AutoSize = true, Location = new Point(30, 110), MaximumSize = new Size(900, 0)
            };
            
            tabIntro.Controls.AddRange(new Control[] { lblAppName, lblVer, lblDesc });
            tab.TabPages.Add(tabIntro);

            // TAB 2: HƯỚNG DẪN
            TabPage tabGuide = new TabPage("Hướng Dẫn Nhanh");
            tabGuide.Padding = new Padding(20);
            tabGuide.BackColor = Color.White;
            tabGuide.AutoScroll = true;

            string guideText = 
                "1. QUẢN LÝ HỌC VIÊN\n" +
                "- Menu Học Viên > Thông tin học viên: Xem, thêm, sửa, xóa học viên.\n" +
                "- Menu Học Viên > Đăng ký lớp: Đăng ký môn học mới cho học viên.\n\n" +
                
                "2. QUẢN LÝ ĐÀO TẠO\n" +
                "- Menu Đào Tạo > Môn học: Quản lý danh mục khóa học/kỹ năng.\n" +
                "- Menu Đào Tạo > Lớp học: Mở lớp mới, xếp lịch, gán giáo viên/trợ giảng.\n" +
                "  *Lưu ý: Hệ thống tự động cảnh báo nếu chọn giáo viên trái chuyên ngành.\n" +
                "- Menu Đào Tạo > Nhân sự: Quản lý hồ sơ giáo viên.\n\n" +
                
                "3. VẬN HÀNH\n" +
                "- Menu Vận Hành > Điểm danh: Điểm danh từng buổi học.\n" +
                "- Menu Vận Hành > Điểm số: Nhập điểm cuối kỳ.\n\n" +

                "4. TÀI CHÍNH\n" +
                "- Menu Tài Chính > Thu học phí: Tra cứu công nợ và in phiếu thu.\n" +
                "- Menu Tài Chính > Báo cáo: Xem doanh thu theo tháng/quý.\n\n" +

                "5. HỆ THỐNG\n" +
                "- Đổi mật khẩu: Cập nhật mật khẩu cá nhân.\n" +
                "- Nhật ký: (Dành cho Admin) Xem lịch sử hoạt động.";

            Label lblGuide = new Label { Text = guideText, AutoSize = true, Location = new Point(20, 20), MaximumSize = new Size(900, 0) };
            tabGuide.Controls.Add(lblGuide);
            tab.TabPages.Add(tabGuide);

            // TAB 3: LIÊN HỆ
            TabPage tabContact = new TabPage("Trợ Giúp & Liên Hệ");
            tabContact.Padding = new Padding(40);
            tabContact.BackColor = Color.White;

            Label lblContactTitle = new Label { Text = "CẦN HỖ TRỢ KỸ THUẬT?", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.OrangeRed, AutoSize = true, Location = new Point(40, 40) };
            Label lblContactInfo = new Label 
            { 
                Text = "Vui lòng liên hệ bộ phận IT:\n\n" +
                       "📧 Email: viethuy@gmail.com\n" +
                       "☎ Hotline: 0938775898\n" +
                       "🏢 Địa chỉ: Phòng Kỹ Thuật, Tầng 3, Tòa nhà Trung Tâm.",
                AutoSize = true, Location = new Point(40, 80)
            };
            
            tabContact.Controls.AddRange(new Control[] { lblContactTitle, lblContactInfo });
            tab.TabPages.Add(tabContact);
        }
    }
}
