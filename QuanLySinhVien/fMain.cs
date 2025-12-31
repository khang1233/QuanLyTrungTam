using QuanLyTrungTam.DAO;
using QuanLyTrungTam.DTO;
using QuanLyTrungTam.Utilities;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyTrungTam
{
    public partial class fMain : Form
    {
        // --- 1. KHAI BÁO BIẾN ---
        private Button btnNavDashboard;
        private FlowLayoutPanel pnlSidebar;
        private Panel pnlHeader;
        private Panel pnlBody;
        private Label lblHeaderTitle;
        private Button currentButton; // Nút đang chọn

        // Các nút Menu chính
        private Button btnNavHocVien, btnNavTaiChinh, btnNavDaoTao, btnNavHeThong;
        private Button btnNavDoiMatKhau; // [MỚI] Nút Đổi Mật Khẩu

        // Các nút Menu con (để gọi từ bên ngoài nếu cần)
        private Button btnMonHoc, btnLopHoc, btnGiangVien, btnTKB;

        // Các Panel menu con
        private Panel pnlSubHocVien, pnlSubDaoTao, pnlSubVanHanh, pnlSubTaiChinh, pnlSubHeThong;

        private Account loginAccount;
        private Form activeChildForm;

        public fMain(Account acc)
        {
            this.loginAccount = acc;
            AppSession.CurrentUser = acc;
            BuildProfessionalUI();
            ApplyUserPermissions();

            // Tự động click Dashboard nếu không phải Học viên
            if (!loginAccount.Quyen.ToLower().Contains("hocvien"))
            {
                this.Load += (s, e) =>
                {
                    if (btnNavDashboard != null && btnNavDashboard.Visible)
                        btnNavDashboard.PerformClick();
                };
            }
        }

        // =========================================================================
        // 2. DỰNG GIAO DIỆN (MENU SIDEBAR)
        // =========================================================================
        private void BuildProfessionalUI()
        {
            this.Controls.Clear();
            this.Size = new Size(1300, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Trung Tâm Đào Tạo - Professional UI";

            // A. SIDEBAR
            pnlSidebar = new FlowLayoutPanel
            {
                Dock = DockStyle.Left,
                Width = 260,
                BackColor = Color.FromArgb(31, 30, 68),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true
            };
            this.Controls.Add(pnlSidebar);

            // B. HEADER
            pnlHeader = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = Color.FromArgb(0, 150, 136) };
            this.Controls.Add(pnlHeader);
            lblHeaderTitle = new Label { Text = "TRANG CHỦ", ForeColor = Color.White, Font = new Font("Segoe UI", 15, FontStyle.Bold), AutoSize = true, Location = new Point(25, 20) };
            pnlHeader.Controls.Add(lblHeaderTitle);

            // C. BODY
            pnlBody = new Panel { Dock = DockStyle.Fill, BackColor = Color.WhiteSmoke, Padding = new Padding(10) };
            this.Controls.Add(pnlBody);
            pnlBody.BringToFront();

            // --- TẠO MENU ---

            // 1. Dashboard
            btnNavDashboard = CreateMenuButton("  📊   DASHBOARD", btnDashboard_Click);

            // 2. HỌC VIÊN (Menu Xổ Xuống)
            btnNavHocVien = CreateMenuButton("  👥   HỌC VIÊN", (s, e) => ShowSubMenu(pnlSubHocVien, s));
            pnlSubHocVien = CreateSubPanel(
                new string[] { "Thông tin học viên", "Đăng ký lớp" },
                new EventHandler[] { btnSinhVien_Click, btnDangKyLop_Click }
            );

            // 3. ĐÀO TẠO
            CreateMenuButton("  📚   ĐÀO TẠO", (s, e) => ShowSubMenu(pnlSubDaoTao, s));
            pnlSubDaoTao = CreateSubPanel(
                new string[] { "Môn học", "Lớp học", "Nhân sự", "Thời khóa biểu" },
                new EventHandler[] { btnMonHoc_Click, btnLopHoc_Click, btnGiangVien_Click, btnTKB_Click }
            );

            // 4. VẬN HÀNH
            CreateMenuButton("  📋   VẬN HÀNH", (s, e) => ShowSubMenu(pnlSubVanHanh, s));
            pnlSubVanHanh = CreateSubPanel(
                new string[] { "Điểm danh", "Điểm số" },
                new EventHandler[] { btnDiemDanh_Click, btnDiem_Click }
            );

            // 5. TÀI CHÍNH
            btnNavTaiChinh = CreateMenuButton("  💰   TÀI CHÍNH", (s, e) => ShowSubMenu(pnlSubTaiChinh, s));
            pnlSubTaiChinh = CreateSubPanel(
                new string[] { "Thu học phí", "Báo Cáo Tài Chính" },
                new EventHandler[] { btnTraCuuPhi_Click, btnThuChi_Click }
            );

            // 6. HỆ THỐNG
            CreateMenuButton("  ⚙️   HỆ THỐNG", (s, e) => ShowSubMenu(pnlSubHeThong, s));
            pnlSubHeThong = CreateSubPanel(
                new string[] { "Tài khoản", "Nhật ký" },
                new EventHandler[] { btnTaiKhoan_Click, btnNhatKy_Click }
            );

            // [MỚI] 7. ĐỔI MẬT KHẨU (Nằm riêng cho dễ thấy)
            btnNavDoiMatKhau = CreateMenuButton("  🔐   ĐỔI MẬT KHẨU", btnDoiMatKhau_Click);

            // 8. Đăng xuất
            CreateMenuButton("  🚪   ĐĂNG XUẤT", (s, e) => this.Close());
        }

        // --- CÁC HÀM HELPER ---
        private Button CreateMenuButton(string text, EventHandler click)
        {
            Button btn = new Button
            {
                Text = "  " + text,
                Height = 55,
                Width = 260,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.Gainsboro,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor = Cursors.Hand,
                Margin = new Padding(0),
                Tag = Color.FromArgb(31, 30, 68)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (s, e) => { ActivateButton(s); click?.Invoke(s, e); };
            pnlSidebar.Controls.Add(btn);
            return btn;
        }

        private Panel CreateSubPanel(string[] items, EventHandler[] events)
        {
            Panel p = new Panel { Height = items.Length * 45, Width = 260, Visible = false, BackColor = Color.FromArgb(45, 45, 72), Margin = new Padding(0) };
            for (int i = 0; i < items.Length; i++)
            {
                Button b = new Button { Text = "    ● " + items[i], Dock = DockStyle.Top, Height = 45, FlatStyle = FlatStyle.Flat, ForeColor = Color.Silver, Font = new Font("Segoe UI", 9), TextAlign = ContentAlignment.MiddleLeft, Cursor = Cursors.Hand };
                b.FlatAppearance.BorderSize = 0;
                int idx = i;
                b.Click += (s, e) => { events[idx]?.Invoke(s, e); };
                p.Controls.Add(b);
                b.BringToFront();
            }
            pnlSidebar.Controls.Add(p);
            return p;
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null && currentButton != (Button)btnSender)
            {
                DisableButton();
                currentButton = (Button)btnSender;
                currentButton.BackColor = Color.FromArgb(0, 150, 136);
                currentButton.ForeColor = Color.White;
            }
        }

        private void DisableButton()
        {
            if (currentButton != null)
            {
                currentButton.BackColor = Color.FromArgb(31, 30, 68);
                currentButton.ForeColor = Color.Gainsboro;
            }
        }

        private void ShowSubMenu(Panel sub, object btnSender)
        {
            if (sub.Visible) sub.Visible = false;
            else
            {
                if (pnlSubHocVien != null) pnlSubHocVien.Visible = false;
                if (pnlSubDaoTao != null) pnlSubDaoTao.Visible = false;
                if (pnlSubVanHanh != null) pnlSubVanHanh.Visible = false;
                if (pnlSubTaiChinh != null) pnlSubTaiChinh.Visible = false;
                if (pnlSubHeThong != null) pnlSubHeThong.Visible = false;
                sub.Visible = true;
            }
        }

        private void ActivateChildForm(Form child)
        {
            if (activeChildForm != null) activeChildForm.Close();
            activeChildForm = child;
            child.TopLevel = false;
            child.FormBorderStyle = FormBorderStyle.None;
            child.Dock = DockStyle.Fill;
            pnlBody.Controls.Add(child);
            child.BringToFront();
            child.Show();
        }

        // --- PHÂN QUYỀN NGƯỜI DÙNG ---
        private void ApplyUserPermissions()
        {
            string role = loginAccount.Quyen;

            // 1. ADMIN: Hiện tất cả
            if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase)) return;

            // 2. HỌC VIÊN: Ẩn sidebar, chuyển sang Form riêng
            if (role.Equals("HocVien", StringComparison.OrdinalIgnoreCase))
            {
                pnlSidebar.Visible = false;
                ActivateChildForm(new FrmHomeHocVien(loginAccount.MaNguoiDung));
                return;
            }

            // 3. NHÂN SỰ (Giáo viên / Trợ giảng)
            if (role.Equals("GiaoVien", StringComparison.OrdinalIgnoreCase) ||
                role.Equals("TroGiang", StringComparison.OrdinalIgnoreCase) ||
                role.Equals("NhanSu", StringComparison.OrdinalIgnoreCase))
            {
                // Ẩn Dashboard & Tài Chính & Học Viên
                if (btnNavDashboard != null) btnNavDashboard.Visible = false;
                if (btnNavHocVien != null) btnNavHocVien.Visible = false;
                if (btnNavTaiChinh != null) btnNavTaiChinh.Visible = false;

                // Ẩn menu Hệ thống (Nhật ký, Tài khoản) - Chỉ giữ nút Đổi Mật Khẩu riêng
                foreach (Control c in pnlSidebar.Controls)
                {
                    if (c is Button btn && btn.Text.Contains("HỆ THỐNG")) btn.Visible = false;
                }

                // Xử lý menu Đào tạo: Chỉ hiện TKB
                foreach (Control c in pnlSubDaoTao.Controls)
                {
                    if (!c.Text.Contains("Thời khóa biểu")) c.Visible = false;
                }

                // Mặc định mở TKB
                lblHeaderTitle.Text = "LỊCH DẠY CỦA TÔI";
                ActivateChildForm(new FrmSchedule());
            }
        }

        // --- SỰ KIỆN CLICK MENU ---
        private void btnDashboard_Click(object sender, EventArgs e) { lblHeaderTitle.Text = "DASHBOARD QUẢN TRỊ"; ActivateChildForm(new FrmDashboard()); }
        private void btnSinhVien_Click(object sender, EventArgs e) { lblHeaderTitle.Text = "HỒ SƠ HỌC VIÊN"; ActivateChildForm(new FrmQuanLyHocVien()); }
        private void btnDangKyLop_Click(object sender, EventArgs e) { lblHeaderTitle.Text = "ĐĂNG KÝ LỚP HỌC"; ActivateChildForm(new FrmDangKyAdmin()); }
        private void btnMonHoc_Click(object sender, EventArgs e) { lblHeaderTitle.Text = "MÔN HỌC"; ActivateChildForm(new FrmQuanLyMonHoc()); }
        private void btnLopHoc_Click(object sender, EventArgs e) { lblHeaderTitle.Text = "LỚP HỌC"; ActivateChildForm(new FrmLop()); }
        private void btnGiangVien_Click(object sender, EventArgs e) { lblHeaderTitle.Text = "NHÂN SỰ"; ActivateChildForm(new FrmQuanLyNhanSu()); }
        private void btnTKB_Click(object sender, EventArgs e) { lblHeaderTitle.Text = "TRA CỨU THỜI KHÓA BIỂU"; ActivateChildForm(new FrmSchedule()); }
        private void btnDiemDanh_Click(object sender, EventArgs e) { lblHeaderTitle.Text = "ĐIỂM DANH"; ActivateChildForm(new FrmDiemDanh()); }
        private void btnDiem_Click(object sender, EventArgs e) { lblHeaderTitle.Text = "ĐIỂM SỐ"; ActivateChildForm(new FrmDiem()); }
        private void btnTraCuuPhi_Click(object sender, EventArgs e) { lblHeaderTitle.Text = "THU HỌC PHÍ"; ActivateChildForm(new FrmTraCuuHocPhi()); }
        private void btnThuChi_Click(object sender, EventArgs e) { lblHeaderTitle.Text = "QUẢN LÝ THU CHI"; ActivateChildForm(new FrmTaiChinh()); }
        private void btnTaiKhoan_Click(object sender, EventArgs e) { new FrmThongTinCaNhan(loginAccount).ShowDialog(); }
        private void btnNhatKy_Click(object sender, EventArgs e) { lblHeaderTitle.Text = "NHẬT KÝ HỆ THỐNG"; ActivateChildForm(new FrmSystemAdmin()); }

        // [MỚI] SỰ KIỆN NÚT ĐỔI MẬT KHẨU
        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            // Mở form fChangePassword dưới dạng Dialog (Cửa sổ con)
            fChangePassword f = new fChangePassword(loginAccount);
            f.ShowDialog();
        }

        // --- HÀM CHUYỂN TAB TỪ FORM CON ---
        public void NavigateToThuHocPhi(string maHV)
        {
            if (pnlSubTaiChinh.Visible == false) ShowSubMenu(pnlSubTaiChinh, null);
            ActivateButton(btnNavTaiChinh);
            lblHeaderTitle.Text = "THU PHÍ HỌC VIÊN";
            FrmTraCuuHocPhi f = new FrmTraCuuHocPhi();
            ActivateChildForm(f);
            f.AutoSearch(maHV);
        }
        public void NavigateToDangKy(string maHV)
        {
            if (pnlSubHocVien.Visible == false) ShowSubMenu(pnlSubHocVien, null);
            lblHeaderTitle.Text = "ĐĂNG KÝ LỚP HỌC";
            FrmDangKyAdmin f = new FrmDangKyAdmin();
            ActivateChildForm(f);
            if (!string.IsNullOrEmpty(maHV)) f.AutoSelectStudent(maHV);
        }
    }
}