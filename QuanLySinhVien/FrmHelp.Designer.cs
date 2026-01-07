
namespace QuanLyTrungTam
{
    partial class FrmHelp
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tab = new System.Windows.Forms.TabControl();
            this.tabIntro = new System.Windows.Forms.TabPage();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lblVer = new System.Windows.Forms.Label();
            this.lblAppName = new System.Windows.Forms.Label();
            this.tabGuide = new System.Windows.Forms.TabPage();
            this.lblGuide = new System.Windows.Forms.Label();
            this.tabContact = new System.Windows.Forms.TabPage();
            this.lblContactInfo = new System.Windows.Forms.Label();
            this.lblContactTitle = new System.Windows.Forms.Label();
            this.tabShortcuts = new System.Windows.Forms.TabPage();
            this.dgvShortcuts = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            this.tab.SuspendLayout();
            this.tabIntro.SuspendLayout();
            this.tabGuide.SuspendLayout();
            this.tabContact.SuspendLayout();
            this.tabShortcuts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShortcuts)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 60;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1000, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(437, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "THÔNG TIN & HƯỚNG DẪN SỬ DỤNG";
            // 
            // tab
            // 
            this.tab.Controls.Add(this.tabIntro);
            this.tab.Controls.Add(this.tabGuide);
            this.tab.Controls.Add(this.tabShortcuts);
            this.tab.Controls.Add(this.tabContact);
            this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.tab.Location = new System.Drawing.Point(0, 60);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(1000, 640);
            this.tab.TabIndex = 1;
            // 
            // tabIntro
            // 
            this.tabIntro.AutoScroll = true;
            this.tabIntro.BackColor = System.Drawing.Color.White;
            this.tabIntro.Controls.Add(this.lblDesc);
            this.tabIntro.Controls.Add(this.lblVer);
            this.tabIntro.Controls.Add(this.lblAppName);
            this.tabIntro.Location = new System.Drawing.Point(4, 29);
            this.tabIntro.Name = "tabIntro";
            this.tabIntro.Padding = new System.Windows.Forms.Padding(20);
            this.tabIntro.Size = new System.Drawing.Size(992, 607);
            this.tabIntro.TabIndex = 0;
            this.tabIntro.Text = "Giới Thiệu Chung";
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(30, 110);
            this.lblDesc.MaximumSize = new System.Drawing.Size(900, 0);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(842, 140);
            this.lblDesc.TabIndex = 2;
            this.lblDesc.Text = "Phần mềm hỗ trợ quản lý toàn diện cho trung tâm đào tạo ngắn hạn, bao gồm:\n\n   ● Quản lý hồ sơ học viên, nhân sự, giáo viên.\n   ● Xếp lịch học, quản lý lớp học, điểm danh.\n   ● Quản lý học phí, lợi nhuận.\n   ● Báo cáo thống kê trực quan (Dashboard).\n\nMục tiêu: Tối ưu hóa quy trình vận hành, giảm tải áp lực quản lý thủ công và nâng cao hiệu quả đào tạo.";
            // 
            // lblVer
            // 
            this.lblVer.AutoSize = true;
            this.lblVer.Location = new System.Drawing.Point(30, 70);
            this.lblVer.Name = "lblVer";
            this.lblVer.Size = new System.Drawing.Size(193, 20);
            this.lblVer.TabIndex = 1;
            this.lblVer.Text = "Cập nhật ngày: 01/01/2026";
            // 
            // lblAppName
            // 
            this.lblAppName.AutoSize = true;
            this.lblAppName.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblAppName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblAppName.Location = new System.Drawing.Point(30, 30);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(519, 32);
            this.lblAppName.TabIndex = 0;
            this.lblAppName.Text = "HỆ THỐNG QUẢN LÝ TRUNG TÂM ĐÀO TẠO";
            // 
            // tabGuide
            // 
            this.tabGuide.AutoScroll = true;
            this.tabGuide.BackColor = System.Drawing.Color.White;
            this.tabGuide.Controls.Add(this.lblGuide);
            this.tabGuide.Location = new System.Drawing.Point(4, 29);
            this.tabGuide.Name = "tabGuide";
            this.tabGuide.Padding = new System.Windows.Forms.Padding(20);
            this.tabGuide.Size = new System.Drawing.Size(992, 607);
            this.tabGuide.TabIndex = 1;
            this.tabGuide.Text = "Hướng Dẫn Nhanh";
            // 
            // lblGuide
            // 
            this.lblGuide.AutoSize = true;
            this.lblGuide.Location = new System.Drawing.Point(20, 20);
            this.lblGuide.MaximumSize = new System.Drawing.Size(900, 0);
            this.lblGuide.Name = "lblGuide";
            this.lblGuide.Size = new System.Drawing.Size(847, 360);
            this.lblGuide.TabIndex = 0;
            this.lblGuide.Text = "1. QUẢN LÝ HỌC VIÊN\n- Menu Học Viên > Thông tin học viên: Xem, thêm, sửa, xóa học viên.\n- Menu Học Viên > Đăng ký lớp: Đăng ký môn học mới cho học viên.\n\n2. QUẢN LÝ ĐÀO TẠO\n- Menu Đào Tạo > Môn học: Quản lý danh mục khóa học/kỹ năng.\n- Menu Đào Tạo > Lớp học: Mở lớp mới, xếp lịch, gán giáo viên/trợ giảng.\n  *Lưu ý: Hệ thống tự động cảnh báo nếu chọn giáo viên trái chuyên ngành.\n- Menu Đào Tạo > Nhân sự: Quản lý hồ sơ giáo viên.\n\n3. VẬN HÀNH\n- Menu Vận Hành > Điểm danh: Điểm danh từng buổi học.\n- Menu Vận Hành > Điểm số: Nhập điểm cuối kỳ.\n\n4. TÀI CHÍNH\n- Menu Tài Chính > Thu học phí: Tra cứu công nợ và in phiếu thu.\n- Menu Tài Chính > Báo cáo: Xem doanh thu theo tháng/quý.\n\n5. HỆ THỐNG\n- Đổi mật khẩu: Cập nhật mật khẩu cá nhân.\n- Nhật ký: (Dành cho Admin) Xem lịch sử hoạt động.";
            // 
            // tabShortcuts
            // 
            this.tabShortcuts.BackColor = System.Drawing.Color.White;
            this.tabShortcuts.Controls.Add(this.dgvShortcuts);
            this.tabShortcuts.Location = new System.Drawing.Point(4, 29);
            this.tabShortcuts.Name = "tabShortcuts";
            this.tabShortcuts.Padding = new System.Windows.Forms.Padding(0);
            this.tabShortcuts.Size = new System.Drawing.Size(992, 607);
            this.tabShortcuts.TabIndex = 3;
            this.tabShortcuts.Text = "Danh Sách Phím Tắt";
            // 
            // dgvShortcuts
            // 
            this.dgvShortcuts.AllowUserToAddRows = false;
            this.dgvShortcuts.AllowUserToDeleteRows = false;
            this.dgvShortcuts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvShortcuts.BackgroundColor = System.Drawing.Color.White;
            this.dgvShortcuts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvShortcuts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShortcuts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvShortcuts.Location = new System.Drawing.Point(0, 0);
            this.dgvShortcuts.Name = "dgvShortcuts";
            this.dgvShortcuts.ReadOnly = true;
            this.dgvShortcuts.RowHeadersVisible = false;
            this.dgvShortcuts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvShortcuts.Size = new System.Drawing.Size(992, 607);
            this.dgvShortcuts.TabIndex = 0;
            // 
            // tabContact
            // 
            this.tabContact.BackColor = System.Drawing.Color.White;
            this.tabContact.Controls.Add(this.lblContactInfo);
            this.tabContact.Controls.Add(this.lblContactTitle);
            this.tabContact.Location = new System.Drawing.Point(4, 29);
            this.tabContact.Name = "tabContact";
            this.tabContact.Padding = new System.Windows.Forms.Padding(40);
            this.tabContact.Size = new System.Drawing.Size(992, 607);
            this.tabContact.TabIndex = 2;
            this.tabContact.Text = "Trợ Giúp & Liên Hệ";
            // 
            // lblContactInfo
            // 
            this.lblContactInfo.AutoSize = true;
            this.lblContactInfo.Location = new System.Drawing.Point(40, 80);
            this.lblContactInfo.Name = "lblContactInfo";
            this.lblContactInfo.Size = new System.Drawing.Size(359, 80);
            this.lblContactInfo.TabIndex = 1;
            this.lblContactInfo.Text = "Vui lòng liên hệ bộ phận IT:\r\n\r\n📧 Email: viethuy@gmail.com\r\n☎ Hotline: 0938775898\r\n🏢 Địa chỉ: Phòng Kỹ Thuật, Tầng 3, Tòa nhà Trung Tâm.";
            // 
            // lblContactTitle
            // 
            this.lblContactTitle.AutoSize = true;
            this.lblContactTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblContactTitle.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblContactTitle.Location = new System.Drawing.Point(40, 40);
            this.lblContactTitle.Name = "lblContactTitle";
            this.lblContactTitle.Size = new System.Drawing.Size(236, 25);
            this.lblContactTitle.TabIndex = 0;
            this.lblContactTitle.Text = "CẦN HỖ TRỢ KỸ THUẬT?";
            // 
            // FrmHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.tab);
            this.Controls.Add(this.pnlHeader);
            this.Name = "FrmHelp";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trợ Giúp & Giới Thiệu";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tab.ResumeLayout(false);
            this.tabIntro.ResumeLayout(false);
            this.tabIntro.PerformLayout();
            this.tabGuide.ResumeLayout(false);
            this.tabGuide.PerformLayout();
            this.tabContact.ResumeLayout(false);
            this.tabContact.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage tabIntro;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Label lblVer;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TabPage tabGuide;
        private System.Windows.Forms.Label lblGuide;
        private System.Windows.Forms.TabPage tabContact;
        private System.Windows.Forms.Label lblContactTitle;
        private System.Windows.Forms.Label lblContactInfo;
        private System.Windows.Forms.TabPage tabShortcuts;
        private System.Windows.Forms.DataGridView dgvShortcuts;
    }
}
