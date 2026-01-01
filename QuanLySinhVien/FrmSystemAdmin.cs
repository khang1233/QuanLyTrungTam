using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyTrungTam.BUS;
using QuanLyTrungTam.DAO;

namespace QuanLyTrungTam
{
    public partial class FrmSystemAdmin : Form
    {
        private DataGridView dgvAccounts = new DataGridView();
        private DataGridView dgvLogs = new DataGridView();

        public FrmSystemAdmin()
        {
            InitializeComponent();
            SetupUI();
            LoadData();
        }

        // Thiết kế giao diện bằng code để đồng bộ nhanh
        private void SetupUI()
        {
            this.Text = "Hệ Thống - Bảo Mật & Nhật Ký";
            this.Size = new Size(1000, 600);
            this.BackColor = Color.White;

            TabControl tc = new TabControl { Dock = DockStyle.Fill };

            // Tab 1: Quản lý tài khoản
            TabPage tp1 = new TabPage("🛡️ Bảo Mật Tài Khoản");
            Panel pnlTools = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.WhiteSmoke };

            Button btnLock = new Button { Text = "Khóa/Mở Khóa", Location = new Point(10, 15), Size = new Size(120, 30), BackColor = Color.LightCoral, FlatStyle = FlatStyle.Flat };
            Button btnReset = new Button { Text = "Reset Mật Khẩu", Location = new Point(140, 15), Size = new Size(120, 30), BackColor = Color.LightBlue, FlatStyle = FlatStyle.Flat };
            Button btnReload = new Button { Text = "Làm mới", Location = new Point(270, 15), Size = new Size(100, 30), BackColor = Color.LightGreen, FlatStyle = FlatStyle.Flat };

            btnLock.Click += BtnLock_Click;
            btnReset.Click += BtnReset_Click;
            btnReload.Click += (s, e) => LoadData();

            pnlTools.Controls.AddRange(new Control[] { btnLock, btnReset, btnReload });

            dgvAccounts.Dock = DockStyle.Fill;
            dgvAccounts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAccounts.AllowUserToAddRows = false;
            dgvAccounts.ReadOnly = true;
            dgvAccounts.BackgroundColor = Color.White;

            tp1.Controls.Add(dgvAccounts);
            tp1.Controls.Add(pnlTools);

            // Tab 2: Nhật ký đăng nhập
            TabPage tp2 = new TabPage("📜 Nhật Ký Đăng Nhập");
            dgvLogs.Dock = DockStyle.Fill;
            dgvLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLogs.ReadOnly = true;
            dgvLogs.BackgroundColor = Color.White;
            tp2.Controls.Add(dgvLogs);

            tc.TabPages.AddRange(new TabPage[] { tp1, tp2 });
            this.Controls.Add(tc);
        }

        private void LoadData()
        {
            // [REFACTOR] Dùng AccountBUS
            dgvAccounts.DataSource = AccountBUS.Instance.GetListAccount();
            dgvLogs.DataSource = AccountBUS.Instance.GetLoginHistory();

            // Việt hóa tiêu đề cột cho đẹp
            if (dgvAccounts.Columns.Contains("TenDangNhap")) dgvAccounts.Columns["TenDangNhap"].HeaderText = "Tài khoản";
            if (dgvAccounts.Columns.Contains("MatKhau")) dgvAccounts.Columns["MatKhau"].HeaderText = "Mật khẩu";
            if (dgvAccounts.Columns.Contains("Quyen")) dgvAccounts.Columns["Quyen"].HeaderText = "Quyền";
            if (dgvAccounts.Columns.Contains("TrangThai")) dgvAccounts.Columns["TrangThai"].HeaderText = "Hoạt động";
            if (dgvAccounts.Columns.Contains("ChuSoHuu")) dgvAccounts.Columns["ChuSoHuu"].HeaderText = "Người sở hữu";
        }

        private void BtnLock_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.CurrentRow == null) return;
            string user = dgvAccounts.CurrentRow.Cells["TenDangNhap"].Value.ToString();

            if (user == "admin")
            {
                MessageBox.Show("Không thể khóa tài khoản Admin!");
                return;
            }

            // Check if column is boolean or int
            var val = dgvAccounts.CurrentRow.Cells["TrangThai"].Value;
            bool currentStatus = false;
            if (val is bool) currentStatus = (bool)val;
            else if (val is int) currentStatus = ((int)val == 1);
            else if (val.ToString() == "1" || val.ToString().ToLower() == "true" || val.ToString() == "Hoạt động") currentStatus = true;

            // [REFACTOR] Dùng AccountBUS
            AccountBUS.Instance.UpdateStatus(user, currentStatus ? 0 : 1);
            LoadData();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.CurrentRow == null) return;
            string user = dgvAccounts.CurrentRow.Cells["TenDangNhap"].Value.ToString();

            if (MessageBox.Show(string.Format("Bạn có muốn reset mật khẩu của {0} về 123?", user), "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // [REFACTOR] Dùng AccountBUS
                AccountBUS.Instance.ResetPass(user);
                MessageBox.Show("Đã Reset thành công!");
                LoadData();
            }
        }
    }
}