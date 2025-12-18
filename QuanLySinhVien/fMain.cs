using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyTrungTam.DTO;
using QuanLyTrungTam; // Namespace chứa các Form con

namespace QuanLyTrungTam
{
    public partial class fMain : Form
    {
        private Button currentButton;
        private Form activeForm;
        private Account loginAccount;

        public fMain(Account acc)
        {
            InitializeComponent();
            this.loginAccount = acc;

            // Cấu hình giao diện ngay khi khởi động
            SetupUI();
        }

        // =================================================================================
        // 1. CẤU HÌNH GIAO DIỆN & PHÂN QUYỀN
        // =================================================================================
        private void SetupUI()
        {
            string quyen = loginAccount.Quyen.Trim().ToLower();

            // Hiển thị tên người dùng
            lblTitle.Text = "Xin chào: " + loginAccount.TenDangNhap.ToUpper();

            // Bước 1: Ẩn hết các nút trước để reset trạng thái
            DisableAllButtons();

            // Bước 2: Hiển thị nút theo quyền
            if (quyen == "hocvien" || quyen == "hv" || quyen == "sinhvien")
            {
                // --- QUYỀN HỌC VIÊN ---
                // 1. Nút Đăng Ký (Để xem lớp và đăng ký thêm)
                SetVisibleButton("btnDangKy", true);

                // 2. Nút Học Phí (Để xem nợ và đóng tiền online/chuyển khoản)
                // Lưu ý: Form Học phí hiện tại của bạn đã có nút "Thanh toán" (InsertThanhToan)
                SetVisibleButton("btnHocPhi", true);

                // 3. Nút Tài khoản (Để đổi mật khẩu và Liên kết Google)
                SetVisibleButton("btnTaiKhoan", true);
                OpenChildForm(new FrmHomeHocVien(loginAccount.MaNguoiDung), null);

                // Đổi tiêu đề cho thân thiện
                lblTitle.Text = "TRANG CHỦ HỌC VIÊN";
                // Ẩn các nút quản lý của Admin
                SetVisibleButton("btnSinhVien", false);
                SetVisibleButton("btnSystem", false);
            }
            else // ADMIN, QUẢN LÝ
            {
                // --- QUYỀN ADMIN ---
                SetVisibleButton("btnSystem", true);    // Dashboard
                SetVisibleButton("btnSinhVien", true);  // Quản lý DS Học viên
                SetVisibleButton("btnLopHoc", true);    // Quản lý Lớp
                SetVisibleButton("btnMonHoc", true);    // Quản lý Môn/Kỹ năng

                // Hai chức năng nghiệp vụ chính:
                SetVisibleButton("btnTuyenSinh", true); // <--- [MỚI] Đăng ký tuyển sinh tập trung
                SetVisibleButton("btnTraCuu", true);    // Tra cứu & Thu ngân

                SetVisibleButton("btnTaiKhoan", true);
            }
        }

        // Hàm ẩn tất cả nút (để tránh sót quyền)
        private void DisableAllButtons()
        {
            SetVisibleButton("btnSinhVien", false);
            SetVisibleButton("btnGiangVien", false);
            SetVisibleButton("btnLopHoc", false);
            SetVisibleButton("btnMonHoc", false);
            SetVisibleButton("btnKhoa", false);
            SetVisibleButton("btnHocPhi", false);
            SetVisibleButton("btnDiem", false);
            SetVisibleButton("btnSystem", false);
            SetVisibleButton("btnDangKy", false);
            SetVisibleButton("btnTraCuu", false);
            SetVisibleButton("btnTuyenSinh", false); // Ẩn nút mới
        }

        // Hàm helper để bật/tắt nút an toàn (tránh lỗi nếu nút chưa tạo)
        private void SetVisibleButton(string btnName, bool isVisible)
        {
            if (this.panelMenu.Controls.ContainsKey(btnName))
                this.panelMenu.Controls[btnName].Visible = isVisible;
        }

        // =================================================================================
        // 2. CƠ CHẾ MỞ FORM CON (TAB)
        // =================================================================================
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null) activeForm.Close();
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktop.Controls.Add(childForm);
            this.panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null && btnSender is Button)
            {
                // Reset màu nút cũ
                foreach (Control previousBtn in panelMenu.Controls)
                {
                    if (previousBtn.GetType() == typeof(Button))
                    {
                        previousBtn.BackColor = Color.FromArgb(51, 51, 76);
                        previousBtn.ForeColor = Color.Gainsboro;
                    }
                }
                // Highlight nút mới
                currentButton = (Button)btnSender;
                currentButton.BackColor = Color.FromArgb(0, 150, 136); // Màu xanh nổi bật
                currentButton.ForeColor = Color.White;
            }
        }

        // =================================================================================
        // 3. SỰ KIỆN CLICK (MENU NAVIGATION)
        // =================================================================================

        // [ADMIN] - DASHBOARD
        private void btnSystem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmDashboard(), sender);
            lblTitle.Text = "TỔNG QUAN HỆ THỐNG";
        }

        // [ADMIN] - QUẢN LÝ HỒ SƠ HỌC VIÊN
        private void btnSinhVien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmQuanLyHocVien(), sender);
            lblTitle.Text = "QUẢN LÝ DANH SÁCH HỌC VIÊN";
        }

        // [ADMIN] - QUẢN LÝ LỚP HỌC
        private void btnLopHoc_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmLop(), sender);
            lblTitle.Text = "QUẢN LÝ LỚP HỌC";
        }

        // [ADMIN] - QUẢN LÝ MÔN HỌC (KỸ NĂNG)
        private void btnMonHoc_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmQuanLyMonHoc(), sender);
            lblTitle.Text = "QUẢN LÝ DANH MỤC MÔN HỌC";
        }

        // [ADMIN] - TRA CỨU & THU NGÂN
        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmTraCuuHocPhi(), sender);
            lblTitle.Text = "TRA CỨU HỌC VIÊN HỌC PHÍ";
        }
        
        // [ADMIN] - TUYỂN SINH (ĐĂNG KÝ TẬP TRUNG) <--- MỚI
        private void btnTuyenSinh_Click(object sender, EventArgs e)
        {
            // Mở form FrmDangKyAdmin mới tạo
            OpenChildForm(new FrmDangKyAdmin(), sender);
            lblTitle.Text = "QUẢN LÝ ĐĂNG KÝ TUYỂN SINH";
        }

        // [CHUNG] - TÀI KHOẢN
        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            FrmThongTinCaNhan f = new FrmThongTinCaNhan(loginAccount);
            if (f.ShowDialog() == DialogResult.Abort)
            {
                this.Close(); // Đăng xuất
            }
        }

        // --- CÁC NÚT DÀNH RIÊNG CHO HỌC VIÊN ---

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmDangKy(loginAccount.MaNguoiDung), sender);
            lblTitle.Text = "ĐĂNG KÝ MÔN HỌC";
        }

        private void btnHocPhi_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmHocPhi(loginAccount.MaNguoiDung), sender);
            lblTitle.Text = "THÔNG TIN HỌC PHÍ";
        }
        // Mở file fMain.cs, thêm các hàm này vào trong class fMain

        // 1. Hàm nhảy sang Tab Tra Cứu
        public void GotoTraCuuPhi(string maHV)
        {
            // Tạo form mới
            FrmTraCuuHocPhi f = new FrmTraCuuHocPhi();

            // Mở form này lên panel chính (giả lập bấm nút Tra Cứu)
            OpenChildForm(f, btnTraCuu);

            // Cập nhật tiêu đề
            lblTitle.Text = "TRA CỨU HỌC VIÊN HỌC PHÍ"; // [cite: 939]

            // Gọi hàm tự động tìm kiếm vừa viết ở Bước 1
            f.AutoSearch(maHV);
        }

        // 2. Hàm nhảy sang Tab Đăng Ký (Tuyển Sinh)
        public void GotoDangKyAdmin(string maHV)
        {
            // Tạo form đăng ký admin
            FrmDangKyAdmin f = new FrmDangKyAdmin();

            // Mở form lên (giả lập bấm nút Tuyển Sinh)
            OpenChildForm(f, btnTuyenSinh);

            // Cập nhật tiêu đề
            lblTitle.Text = "QUẢN LÝ ĐĂNG KÝ TUYỂN SINH"; // [cite: 946]

            // Gọi hàm tự động chọn sinh viên vừa viết ở Bước 1
            f.AutoSelectStudent(maHV);
        }
        // --- CÁC SỰ KIỆN CŨ (GIỮ LẠI ĐỂ TRÁNH LỖI DESIGNER) ---
        private void btnGiangVien_Click(object sender, EventArgs e) { }
        private void btnKhoa_Click(object sender, EventArgs e) { }
        private void btnDiem_Click(object sender, EventArgs e) { }
    }
}