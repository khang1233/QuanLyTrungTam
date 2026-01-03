using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyTrungTam.DAO;
using QuanLyTrungTam.DTO;
using QuanLyTrungTam.Utilities;

namespace QuanLyTrungTam
{
    public partial class FrmThongTinCaNhan : Form
    {
        private Account loginAccount;

        public FrmThongTinCaNhan(Account acc)
        {
            InitializeComponent();
            this.loginAccount = acc;
            SetupUI();
            LoadInfo();
        }

        // Tạo giao diện bằng code để bạn đỡ phải kéo thả
        private void SetupUI()
        {
            this.Text = "Thông tin tài khoản";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            Button btnLinkGG = new Button { Text = "Liên kết Google", Location = new Point(50, 250), Size = new Size(150, 40), BackColor = Color.WhiteSmoke };
            btnLinkGG.Click += BtnLinkGG_Click; // Gắn sự kiện

            this.Controls.Add(btnLinkGG);
            Label lblTitle = new Label { Text = "THÔNG TIN CÁ NHÂN", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.FromArgb(33, 150, 243), AutoSize = true, Location = new Point(130, 20) };

            // Các Label hiển thị thông tin
            Label lblUser = new Label { Text = "Tên đăng nhập: " + loginAccount.TenDangNhap, Location = new Point(50, 70), AutoSize = true, Font = new Font("Arial", 10) };
            Label lblRole = new Label { Text = "Vai trò: " + loginAccount.Quyen, Location = new Point(50, 100), AutoSize = true, Font = new Font("Arial", 10) };

            // Panel chứa thông tin chi tiết (Họ tên, ngày sinh...)
            GroupBox grpInfo = new GroupBox { Text = "Chi tiết", Location = new Point(40, 130), Size = new Size(400, 150) };
            Label lblName = new Label { Name = "lblName", Text = "Họ tên: ...", Location = new Point(20, 30), AutoSize = true };
            Label lblExtra = new Label { Name = "lblExtra", Text = "Thông tin khác: ...", Location = new Point(20, 60), AutoSize = true };
            grpInfo.Controls.Add(lblName);
            grpInfo.Controls.Add(lblExtra);

            // Các nút chức năng
            // Các nút chức năng
            Button btnChangePass = new Button { Text = "Đổi mật khẩu", Location = new Point(50, 300), Size = new Size(120, 40), BackColor = Color.FromArgb(33, 150, 243), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            Button btnLogout = new Button { Text = "Đăng xuất", Location = new Point(180, 300), Size = new Size(120, 40), BackColor = Color.IndianRed, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            Button btnClose = new Button { Text = "Đóng", Location = new Point(310, 300), Size = new Size(100, 40), BackColor = Color.WhiteSmoke, FlatStyle = FlatStyle.Flat };

            // Gán sự kiện
            btnChangePass.Click += BtnChangePass_Click;
            btnLogout.Click += BtnLogout_Click;
            btnClose.Click += (s, e) => { this.Close(); };

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblUser);
            this.Controls.Add(lblRole);
            this.Controls.Add(grpInfo);
            this.Controls.Add(btnChangePass);
            this.Controls.Add(btnLogout);
            this.Controls.Add(btnClose);
        }
        private async void BtnLinkGG_Click(object sender, EventArgs e)
        {
            // 1. Gọi Google lấy Email
            string email = await GoogleHelper.LoginGoogleAsync();

            if (string.IsNullOrEmpty(email)) return;

            // 2. Hỏi xác nhận
            if (MessageBox.Show(string.Format("Bạn muốn liên kết tài khoản này với Google: {0}?", email), "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // 3. Cập nhật vào DB
                string maHV = loginAccount.MaNguoiDung; // Lấy mã HV của người đang đăng nhập

                if (HocVienDAO.Instance.UpdateEmailHocVien(maHV, email))
                {
                    MessageBox.Show("Liên kết thành công! Lần sau bạn có thể đăng nhập bằng nút Google.");
                }
                else
                {
                    MessageBox.Show("Email này đã được sử dụng bởi một học viên khác!");
                }
            }
        }
        void LoadInfo()
        {
            // Tìm các Label trong GroupBox để gán dữ liệu
            // Lưu ý: Chỉ số Controls[3] có thể khác tùy giao diện, nên dùng try-catch hoặc Find
            try
            {
                Label lblName = this.Controls.Find("lblName", true)[0] as Label;
                Label lblExtra = this.Controls.Find("lblExtra", true)[0] as Label;

                // CODE MỚI (Đơn giản hóa để chạy được)
                lblName.Text = "Xin chào: " + loginAccount.TenDangNhap;
                lblExtra.Text = "Vai trò: " + loginAccount.Quyen;
            }
            catch { }
        }

        private void BtnChangePass_Click(object sender, EventArgs e)
        {
            fChangePassword f = new fChangePassword(loginAccount);
            f.ShowDialog();
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Abort; // Trả về Abort để fMain biết là user muốn đăng xuất
                this.Close();
            }
        }
    }
}