using QuanLyTrungTam.BUS; // [REFACTOR]
using QuanLyTrungTam.DTO;
using QuanLyTrungTam.Utilities;
using System;
using System.Drawing; // Added missing namespace
using System.Windows.Forms;

namespace QuanLyTrungTam
{
    public partial class fChangePassword : Form
    {
        private Account loginAccount;

        // Constructor nhận vào Account từ Form cha
        public fChangePassword(Account acc)
        {
            InitializeComponent();
            this.loginAccount = acc;
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Đổi Mật Khẩu Cá Nhân";
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Define Blue Color
            Color blueColor = Color.FromArgb(33, 150, 243);

            // Style Labels
            // Increase height
            this.Height += 70;

            // Style Labels & Shift Down
            foreach (Control c in this.Controls)
            {
                c.Top += 60; // Shift down to avoid header overlap

                if (c is Label)
                {
                    c.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                    c.ForeColor = Color.Black;
                }
                else if (c is TextBox)
                {
                    c.Font = new Font("Segoe UI", 10);
                }
            }

            // Style Specific Controls matching Designer Names
            if (btnUpdate != null)
            {
                btnUpdate.Text = "CẬP NHẬT";
                btnUpdate.BackColor = Color.FromArgb(40, 167, 69); // Green
                btnUpdate.ForeColor = Color.White;
                btnUpdate.FlatStyle = FlatStyle.Flat;
                btnUpdate.FlatAppearance.BorderSize = 0;
                btnUpdate.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                btnUpdate.Cursor = Cursors.Hand;
            }

            if (btnExit != null)
            {
                btnExit.Text = "THOÁT";
                btnExit.BackColor = Color.IndianRed;
                btnExit.ForeColor = Color.White;
                btnExit.FlatStyle = FlatStyle.Flat;
                btnExit.FlatAppearance.BorderSize = 0;
                btnExit.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                btnExit.Cursor = Cursors.Hand;
            }

            // Add Header
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = blueColor };
            Label lblTitle = new Label { Text = "THAY ĐỔI MẬT KHẨU", AutoSize = true, Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.White, Location = new Point(100, 10) };
            
            // Center Title roughly
            lblTitle.Location = new Point((this.ClientSize.Width - lblTitle.PreferredWidth) / 2, 12);
            
            pnlHeader.Controls.Add(lblTitle);
            this.Controls.Add(pnlHeader);
        }
        private void txbPassOld_TextChanged(object sender, EventArgs e)
        {
            // Không cần viết gì ở đây cả
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdatePassword();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void UpdatePassword()
        {
            string passwordOld = txbPassOld.Text;
            string passwordNew = txbPassNew.Text;
            string reEnterPass = txbReEnter.Text;
            string userName = loginAccount.TenDangNhap;

            // 1. Kiểm tra nhập đủ thông tin
            if (string.IsNullOrEmpty(passwordOld) || string.IsNullOrEmpty(passwordNew) || string.IsNullOrEmpty(reEnterPass))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kiểm tra mật khẩu cũ (Quan trọng)
            if (!passwordOld.Equals(loginAccount.MatKhau))
            {
                MessageBox.Show("Mật khẩu cũ không đúng!", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txbPassOld.Focus();
                return;
            }

            // 3. Kiểm tra độ dài
            if (passwordNew.Length < 3)
            {
                MessageBox.Show("Mật khẩu mới phải từ 3 ký tự trở lên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 4. Kiểm tra khớp mật khẩu nhập lại
            if (!passwordNew.Equals(reEnterPass))
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 5. Cập nhật vào Database
            // [REFACTOR] Dùng AccountBUS
            if (AccountBUS.Instance.UpdatePassword(userName, passwordNew))
            {
                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cập nhật lại mật khẩu trong phiên làm việc hiện tại
                loginAccount.MatKhau = passwordNew;
                AppSession.CurrentUser.MatKhau = passwordNew;

                this.Close();
            }
            else
            {
                MessageBox.Show("Lỗi hệ thống! Không thể cập nhật mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}