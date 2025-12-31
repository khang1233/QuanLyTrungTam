using QuanLyTrungTam.DAO;
using QuanLyTrungTam.DTO;
using QuanLyTrungTam.Utilities;
using System;
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
            this.Text = "Đổi Mật Khẩu Cá Nhân";
            this.StartPosition = FormStartPosition.CenterParent; // Hiện giữa form cha
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
            if (AccountDAO.Instance.UpdatePassword(userName, passwordNew))
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