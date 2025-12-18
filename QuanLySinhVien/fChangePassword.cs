using System;
using System.Windows.Forms;
using QuanLyTrungTam.DAO;
using QuanLyTrungTam.DTO;

namespace QuanLyTrungTam
{
    public partial class fChangePassword : Form
    {
        // Biến để lưu tài khoản đang đăng nhập
        private Account loginAccount;

        // Constructor nhận vào Account (để fMain truyền sang)
        public fChangePassword(Account acc)
        {
            InitializeComponent();
            this.loginAccount = acc; // Lưu tài khoản lại để dùng
        }

        // 1. Sự kiện nút Cập nhật
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdatePassword();
        }

        // 2. Sự kiện nút Thoát
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // --- HÀM XỬ LÝ LOGIC ĐỔI MẬT KHẨU ---
        void UpdatePassword()
        {
            string passwordOld = txbPassOld.Text;
            string passwordNew = txbPassNew.Text;
            string reEnterPass = txbReEnter.Text;
            string userName = loginAccount.TenDangNhap;

            // 1. Kiểm tra mật khẩu cũ có đúng không?
            // Lưu ý: loginAccount.MatKhau là mật khẩu hiện tại trong bộ nhớ
            if (!passwordOld.Equals(loginAccount.MatKhau))
            {
                MessageBox.Show("Mật khẩu cũ không đúng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbPassOld.Focus();
                return;
            }

            // 2. Kiểm tra mật khẩu mới và nhập lại có khớp không?
            if (!passwordNew.Equals(reEnterPass))
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Gọi DAO để cập nhật xuống Database
            // (Đảm bảo AccountDAO đã có hàm UpdatePassword như mình hướng dẫn trước đó)
            if (AccountDAO.Instance.UpdatePassword(userName, passwordNew))
            {
                MessageBox.Show("Cập nhật mật khẩu thành công!", "Thông báo");

                // Cập nhật lại mật khẩu trong bộ nhớ để không phải đăng nhập lại
                loginAccount.MatKhau = passwordNew;

                // Xóa trắng các ô nhập
                txbPassOld.Text = "";
                txbPassNew.Text = "";
                txbReEnter.Text = "";

                // Có thể đóng form luôn nếu muốn
                // this.Close(); 
            }
            else
            {
                MessageBox.Show("Vui lòng điền đúng mật khẩu cũ!", "Thông báo");
            }
        }

        private void txbPassOld_TextChanged(object sender, EventArgs e)
        {

        }
    }
}