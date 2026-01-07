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
        private Button currentButton; // Nút đang chọn

        // Các nút Menu chính
        private Button btnNavHocVien, btnNavTaiChinh, btnNavDaoTao, btnNavHeThong;
        private Button btnNavDoiMatKhau; 

        // Các Panel menu con
        private Panel pnlSubHocVien, pnlSubDaoTao, pnlSubVanHanh, pnlSubTaiChinh, pnlSubHeThong;

        private Form activeChildForm;

        // Keep constructor compatible if other forms call it with Account
        public fMain(Account acc)
        {
            InitializeComponent();
            
            // Sync Session if not already set
            if (AppSession.CurrentUser == null) AppSession.CurrentUser = acc;
            if (UserSession.Instance.CurrentUser == null) UserSession.Instance.SetSession(acc);

            BuildMenu();
            ApplyUserPermissions();

            // Tự động click Dashboard nếu không phải Học viên
            if (!UserSession.Instance.IsStudent())
            {
                this.Load += (s, e) =>
                {
                    if (btnNavDashboard != null && btnNavDashboard.Visible)
                        btnNavDashboard.PerformClick();
                };
            }
        }

        private void BuildMenu()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Trung Tâm Đào Tạo - Professional UI";

            // Sidebar and Header are already executed in InitializeComponent()
            // Ensure pnlBody reference is correct if needed, or rely on Designer's pnlBody
            
            // Re-assign references if they were manually created in Designer but variables are here
             // (Assuming pnlSidebar, pnlHeader, pnlBody are in Designer now)

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
                new string[] { "Tài khoản", "Nhật ký", "Cài đặt phím tắt" },
                new EventHandler[] { btnTaiKhoan_Click, btnNhatKy_Click, btnShortcutConfig_Click }
            );

            // 7. ĐỔI MẬT KHẨU
            btnNavDoiMatKhau = CreateMenuButton("  🔐   ĐỔI MẬT KHẨU", btnDoiMatKhau_Click);

            // 8. TRỢ GIÚP
            CreateMenuButton("  ❓   TRỢ GIÚP", btnHelp_Click);

            // 9. Đăng xuất
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
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor = Cursors.Hand,
                Margin = new Padding(0),
                Tag = Color.FromArgb(0, 0, 64) // Default Dark Navy
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (s, e) => { ActivateButton(s); click?.Invoke(s, e); };
            pnlSidebar.Controls.Add(btn);
            return btn;
        }

        private Panel CreateSubPanel(string[] items, EventHandler[] events)
        {
            Panel p = new Panel { Height = items.Length * 45, Width = 260, Visible = false, BackColor = Color.FromArgb(0, 0, 64), Margin = new Padding(0) };
            for (int i = 0; i < items.Length; i++)
            {
                Button b = new Button { 
                    Text = "    ● " + items[i], 
                    Dock = DockStyle.Top, 
                    Height = 45, 
                    FlatStyle = FlatStyle.Flat, 
                    ForeColor = Color.White, 
                    Font = new Font("Segoe UI", 9), 
                    TextAlign = ContentAlignment.MiddleLeft, 
                    Cursor = Cursors.Hand,
                    Tag = Color.FromArgb(0, 0, 64) // Default Dark Navy
                };
                b.FlatAppearance.BorderSize = 0;
                int idx = i;
                b.Click += (s, e) => { ActivateButton(s); events[idx]?.Invoke(s, e); };
                p.Controls.Add(b);
                b.BringToFront();
            }
            pnlSidebar.Controls.Add(p);
            return p;
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = Color.FromArgb(33, 150, 243); // Bright Blue Active
                    currentButton.ForeColor = Color.White;
                }
            }
        }

        private void DisableButton()
        {
            if (currentButton != null)
            {
                if (currentButton.Tag != null) currentButton.BackColor = (Color)currentButton.Tag;
                else currentButton.BackColor = Color.FromArgb(0, 0, 64);
                currentButton.ForeColor = Color.White;
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
            if (UserSession.Instance.IsAdmin()) return;

            // 2. HỌC VIÊN: Ẩn sidebar, chuyển sang Form riêng
            if (UserSession.Instance.IsStudent())
            {
                pnlSidebar.Visible = false;
                ActivateChildForm(new FrmHomeHocVien(AppSession.CurrentUser.MaNguoiDung));
                return;
            }

            // 3. NHÂN SỰ (Giáo viên / Trợ giảng)
            if (UserSession.Instance.IsTeacher() || UserSession.Instance.IsStaff())
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
        private void btnTaiKhoan_Click(object sender, EventArgs e) { new FrmThongTinCaNhan(AppSession.CurrentUser).ShowDialog(); }
        private void btnNhatKy_Click(object sender, EventArgs e) { lblHeaderTitle.Text = "NHẬT KÝ HỆ THỐNG"; ActivateChildForm(new FrmSystemAdmin()); }
        private void btnHelp_Click(object sender, EventArgs e) { lblHeaderTitle.Text = "HƯỚNG DẪN SỬ DỤNG"; ActivateChildForm(new FrmHelp()); }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            fChangePassword f = new fChangePassword(AppSession.CurrentUser);
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
        private void btnShortcutConfig_Click(object sender, EventArgs e)
        {
            new FrmShortcutConfig().ShowDialog();
        }
    }
}