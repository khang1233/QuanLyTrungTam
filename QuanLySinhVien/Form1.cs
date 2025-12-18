using QuanLyTrungTam.DAO;
using QuanLyTrungTam.DTO; // Bắt buộc để dùng Account
using QuanLyTrungTam.Utilities; // Bắt buộc để dùng AppSession
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyTrungTam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TaoNutGoogle();
        }

        // Nút Thoát
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát chương trình?",
                "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        // Nút Đăng nhập
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string passWord = txbPassWord.Text;

            // 1. Lấy vai trò từ RadioButton
            string role = "";

            if (rdoAdmin.Checked)
                role = "Admin";
            else if (rdoHocSinh.Checked)
                role = "HocVien";

            // Kiểm tra chọn vai trò
            if (string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Vui lòng chọn vai trò đăng nhập!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra tài khoản và mật khẩu
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. GỌI HÀM LOGIN
                if (AccountDAO.Instance.Login(userName, passWord, role))
                {
                    // 3. LẤY ĐỐI TƯỢNG ACCOUNT
                    Account loginAccount = AccountDAO.Instance.GetAccountByUserName(userName);

                    // 4. ✔ FIX QUAN TRỌNG NHẤT
                    AppSession.CurrentUser = loginAccount;

                    // 5. MỞ FORM MAIN (truyền account luôn)
                    fMain f = new fMain(loginAccount);
                    this.Hide();
                    f.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Đăng nhập thất bại!\nCó thể do:\n" +
                                    "• Sai tên đăng nhập hoặc mật khẩu.\n" +
                                    "• Bạn đã chọn sai vai trò.",
                                    "Lỗi đăng nhập",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
        }
        // Trong Form1.cs
        private async void btnGoogleLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Lấy email từ Google (Code GoogleHelper của bạn đã ổn)
                string email = await GoogleHelper.LoginGoogleAsync();

                if (string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Đăng nhập Google thất bại hoặc bị hủy.");
                    return;
                }

                // 2. Xử lý Logic Database (Login hoặc Register)
                if (AccountDAO.Instance.LoginGoogle(email))
                {
                    // 3. Lấy thông tin account sau khi xử lý xong
                    Account acc = AccountDAO.Instance.GetAccountByUserName(email);

                    // 4. Lưu Session
                    QuanLyTrungTam.Utilities.AppSession.CurrentUser = acc;

                    // 5. Chuyển Form
                    fMain f = new fMain(acc);
                    this.Hide();
                    f.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Lỗi hệ thống khi tạo tài khoản Google.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        // Trong file Form1.cs

        private async void btnGoogle_Click(object sender, EventArgs e)
        {
            // Khóa nút để tránh bấm liên tục
            Button btn = (Button)sender;
            btn.Enabled = false;
            btn.Text = "Đang kết nối...";

            try
            {
                // 1. Gọi Google lấy Email
                string email = await GoogleHelper.LoginGoogleAsync();

                if (string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Đăng nhập thất bại. Vui lòng thử lại.");
                    return;
                }

                // 2. Xử lý Logic Database (Login hoặc Register)
                if (AccountDAO.Instance.LoginGoogle(email))
                {
                    // 3. Lấy thông tin account để lưu Session
                    Account acc = AccountDAO.Instance.GetAccountByEmail(email);
                    QuanLyTrungTam.Utilities.AppSession.CurrentUser = acc;

                    // 4. Mở Form Main
                    fMain f = new fMain(acc);
                    this.Hide();
                    f.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Lỗi hệ thống khi tạo tài khoản mới.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                // Mở lại nút
                btn.Enabled = true;
                btn.Text = "Đăng nhập Google";
            }
        }
        // --- HÀM TỰ ĐỘNG TẠO NÚT GOOGLE (Dán vào Form1.cs) ---
        private void TaoNutGoogle()
        {
            // 1. Khởi tạo nút mới
            Button btnGoogle = new Button();
            btnGoogle.Name = "btnGoogleLogin";
            btnGoogle.Text = "G  Đăng nhập Google ";
            btnGoogle.Size = new Size(btnLogin.Width, btnLogin.Height); // Lấy kích thước bằng nút Đăng nhập cũ

            // 2. Căn vị trí: Nằm ngay bên dưới nút Đăng nhập cũ 15px
            btnGoogle.Location = new Point(btnLogin.Location.X, btnLogin.Location.Y + btnLogin.Height + 15);

            // 3. Trang trí cho đẹp (Giống phong cách Google)
            btnGoogle.BackColor = Color.White;
            btnGoogle.ForeColor = Color.DimGray;
            btnGoogle.FlatStyle = FlatStyle.Flat;
            btnGoogle.FlatAppearance.BorderColor = Color.Silver;
            btnGoogle.Font = new Font("Segoe UI", 7, FontStyle.Bold);
            btnGoogle.Cursor = Cursors.Hand;

            // 4. GẮN SỰ KIỆN CLICK (Kết nối với hàm xử lý bạn đã viết trước đó)
            btnGoogle.Click += new EventHandler(this.btnGoogle_Click);

            // 5. Thêm nút vào giao diện
            // (Thêm vào cùng vị trí cha với nút Login để đảm bảo nó hiện đúng chỗ)
            btnLogin.Parent.Controls.Add(btnGoogle);
            btnGoogle.BringToFront();
        }
        private void groupBoxRole_Enter(object sender, EventArgs e)
        {

        }

        private void labelAppName_Click(object sender, EventArgs e)
        {

        }
    }
}
