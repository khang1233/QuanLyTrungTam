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
            LoadInfo();
        }

        private async void BtnLinkGG_Click(object sender, EventArgs e)
        {
            // 1. Gọi Google lấy Email
            string email = await GoogleHelper.LoginGoogleAsync();

            if (string.IsNullOrEmpty(email)) return;

            // 2. Hỏi xác nhận
            if (MessageBox.Show($"Bạn muốn liên kết tài khoản này với Google: {email}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // 3. Cập nhật vào DB
                string maHV = loginAccount.MaNguoiDung; 

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
            if (loginAccount == null) return;
            
            lblUser.Text = "Tên đăng nhập: " + loginAccount.TenDangNhap;
            lblRole.Text = "Vai trò: " + loginAccount.Quyen;
            lblExtra.Text = "Vai trò: " + loginAccount.Quyen;

            // Fetch Real Name
            string realName = loginAccount.TenDangNhap; // Default
            string id = loginAccount.MaNguoiDung;

            try
            {
                if (loginAccount.Quyen == "HocVien")
                {
                    string nm = HocVienDAO.Instance.GetTenHocVien(id);
                    if (!string.IsNullOrEmpty(nm)) realName = nm;
                }
                else if (loginAccount.Quyen == "GiaoVien" || loginAccount.Quyen == "NhanSu" || loginAccount.Quyen == "TroGiang")
                {
                    string nm = NhanVienDAO.Instance.GetTenNhanVien(id);
                    if (!string.IsNullOrEmpty(nm)) realName = nm;
                }
            }
            catch {}

            lblName.Text = "Xin chào: " + realName;
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

        private void BtnClose_Click(object sender, EventArgs e)
        {
             this.Close();
        }
    }
}