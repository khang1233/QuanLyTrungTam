using QuanLyTrungTam.BUS;
using QuanLyTrungTam.DTO;
using System;
using System.Windows.Forms;

namespace QuanLyTrungTam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btnLogin.Click += BtnLogin_Click;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text.Trim();
            string pass = txtPass.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Account acc = AccountBUS.Instance.Login(user, pass);
            if (acc != null)
            {
                UserSession.Instance.SetSession(acc);
                
                this.Hide();
                new fMain(acc).ShowDialog();
                this.Show();
                
                txtPass.Text = "";
                UserSession.Instance.ClearSession();
                QuanLyTrungTam.Utilities.AppSession.CurrentUser = null; // Important: Clear legacy session
            }
            else
            {
                MessageBox.Show("Sai thông tin đăng nhập!", "Lỗi Đăng Nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}