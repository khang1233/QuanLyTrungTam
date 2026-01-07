using QuanLyTrungTam.BUS;
using QuanLyTrungTam.DTO;
using QuanLyTrungTam.Utilities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyTrungTam
{
    public partial class FrmSystemAdmin : Form
    {
        public FrmSystemAdmin()
        {
            InitializeComponent();
            
            // Shortcuts
            this.KeyPreview = true;
            
            // Security Check
            if (!UserSession.Instance.IsAdmin())
            {
                MessageBox.Show("Truy cập bị từ chối! Chỉ Admin mới được vào trang này.", "Cảnh cáo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.BeginInvoke(new MethodInvoker(Close));
                return;
            }
            
            LoadData();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                BtnReload_Click(null, null);
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }

        private void LoadData()
        {
            dgvAccounts.DataSource = AccountBUS.Instance.GetListAccount();
            dgvLogs.DataSource = AccountBUS.Instance.GetLoginHistory();
            StyleGrids();

            // Headers
            if (dgvAccounts.Columns.Contains("TenDangNhap")) dgvAccounts.Columns["TenDangNhap"].HeaderText = "Tài khoản";
            if (dgvAccounts.Columns.Contains("MatKhau")) dgvAccounts.Columns["MatKhau"].HeaderText = "Mật khẩu";
            if (dgvAccounts.Columns.Contains("Quyen")) dgvAccounts.Columns["Quyen"].HeaderText = "Quyền";
            if (dgvAccounts.Columns.Contains("TrangThai")) dgvAccounts.Columns["TrangThai"].HeaderText = "Hoạt động";
            if (dgvAccounts.Columns.Contains("MaNguoiDung")) dgvAccounts.Columns["MaNguoiDung"].HeaderText = "Mã Người Dùng";
            if (dgvAccounts.Columns.Contains("ChuSoHuu")) dgvAccounts.Columns["ChuSoHuu"].HeaderText = "Người sở hữu";
        }
        
        private void StyleGrids()
        {
             StyleGrid(dgvAccounts);
             StyleGrid(dgvLogs);
        }

        private void StyleGrid(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.ColumnHeadersHeight = 40;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.RowTemplate.Height = 35;
        }

        private void BtnLock_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.CurrentRow == null) return;
            string user = dgvAccounts.CurrentRow.Cells["TenDangNhap"].Value.ToString();

            if (user.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Không thể khóa tài khoản Admin!");
                return;
            }

            var val = dgvAccounts.CurrentRow.Cells["TrangThai"].Value;
            bool currentStatus = false;
            if (val is bool) currentStatus = (bool)val;
            else if (val is int) currentStatus = ((int)val == 1);
            else if (val.ToString() == "1" || val.ToString().ToLower() == "true" || val.ToString() == "Hoạt động") currentStatus = true;

            AccountBUS.Instance.UpdateStatus(user, currentStatus ? 0 : 1);
            LoadData();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.CurrentRow == null) return;
            string user = dgvAccounts.CurrentRow.Cells["TenDangNhap"].Value.ToString();

            if (MessageBox.Show(string.Format("Bạn có muốn reset mật khẩu của {0} về 123?", user), "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AccountBUS.Instance.ResetPass(user);
                MessageBox.Show("Đã Reset thành công!");
                LoadData();
            }
        }
        
        private void BtnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}