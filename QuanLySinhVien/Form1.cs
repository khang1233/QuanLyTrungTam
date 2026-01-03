using QuanLyTrungTam.DAO; // Giữ lại nếu cần, hoặc xóa nếu không dùng
using QuanLyTrungTam.BUS;
using QuanLyTrungTam.DTO;
using QuanLyTrungTam.Utilities;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace QuanLyTrungTam
{
    public partial class Form1 : Form
    {
        // ===== CONTROLS =====
        private TextBox txtUser, txtPass;
        private RadioButton rdoHocVien, rdoNhanSu, rdoAdmin;
        private Button btnLogin, btnGoogle;

        // ===== COLORS =====
        private readonly Color colorPrimary = Color.FromArgb(33, 150, 243); // Blue #2196F3
        private readonly Color colorSecondary = ColorTranslator.FromHtml("#2c3e50"); // Xanh Đen

        public Form1()
        {
            InitializeComponent();
            BuildUI();
        }

        // ====================================================================================
        // 1. DỰNG GIAO DIỆN
        // ====================================================================================
        private void BuildUI()
        {
            SuspendLayout();

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1000, 600);
            this.BackColor = Color.White;
            this.Controls.Clear();

            // 1. THANH KÉO (DRAG BAR) - CHỨA LUÔN NÚT X
            Panel drag = new Panel { Dock = DockStyle.Top, Height = 40, BackColor = Color.Transparent, Padding = new Padding(0, 0, 10, 0) };
            drag.MouseDown += ReleaseCapture_MouseDown;
            this.Controls.Add(drag);

            // --- TẠO NÚT X (CLOSE) ---
            Label lblClose = new Label
            {
                Text = "✕",
                Font = new Font("Arial", 18, FontStyle.Bold),
                ForeColor = Color.DimGray,
                Dock = DockStyle.Right,
                AutoSize = true,
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
            lblClose.Click += (s, e) => Application.Exit();
            lblClose.MouseEnter += (s, e) => lblClose.ForeColor = Color.Red;
            lblClose.MouseLeave += (s, e) => lblClose.ForeColor = Color.DimGray;
            drag.Controls.Add(lblClose);

            // 2. CHIA CỘT (TABLE LAYOUT)
            TableLayoutPanel main = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F)); // Trái 45%
            main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F)); // Phải 55%

            this.Controls.Add(main);
            main.SendToBack();

            // 3. THÊM NỘI DUNG VÀO 2 CỘT
            main.Controls.Add(BuildLeftPanel(), 0, 0);  // Cột trái
            main.Controls.Add(BuildRightPanel(), 1, 0); // Cột phải

            ResumeLayout();
            this.AcceptButton = btnLogin; // Enter để đăng nhập
        }

        // ====================================================================================
        // 2. PANEL TRÁI (LOGO & TITLE)
        // ====================================================================================
        private Control BuildLeftPanel()
        {
            Panel p = new Panel { Dock = DockStyle.Fill, BackColor = colorPrimary };
            Panel centerContent = new Panel { Size = new Size(350, 300), BackColor = Color.Transparent };

            // Logo Tròn
            PictureBox logo = new PictureBox { Size = new Size(120, 120), Location = new Point(115, 0) };
            logo.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillEllipse(Brushes.White, 0, 0, 119, 119);
                e.Graphics.DrawString("EDU", new Font("Segoe UI", 24, FontStyle.Bold), new SolidBrush(colorPrimary), 22, 38);
            };

            // Tiêu đề
            Label title = new Label
            {
                Text = "QUẢN LÝ\nTRUNG TÂM",
                Font = new Font("Segoe UI", 26, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter
            };
            title.Location = new Point((350 - title.PreferredWidth) / 2, 140);

            // Slogan
            Label sub = new Label
            {
                Text = "Hệ thống đào tạo kỹ năng\nchuyên nghiệp",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.WhiteSmoke,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter
            };
            sub.Location = new Point((350 - sub.PreferredWidth) / 2, title.Bottom + 15);

            centerContent.Controls.AddRange(new Control[] { logo, title, sub });
            p.Controls.Add(centerContent);

            p.Resize += (s, e) =>
            {
                centerContent.Location = new Point((p.Width - centerContent.Width) / 2, (p.Height - centerContent.Height) / 2);
            };

            return p;
        }

        // ====================================================================================
        // 3. PANEL PHẢI (FORM NHẬP LIỆU)
        // ====================================================================================
        private Control BuildRightPanel()
        {
            Panel p = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };

            FlowLayoutPanel stack = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(10)
            };

            Label lblTitle = new Label { Text = "ĐĂNG NHẬP", Font = new Font("Segoe UI", 24, FontStyle.Bold), ForeColor = colorSecondary, AutoSize = true, Margin = new Padding(0, 0, 0, 30) };

            Control boxUser = CreateInput("Tên đăng nhập", false, tb => txtUser = tb);
            Control boxPass = CreateInput("Mật khẩu", true, tb => txtPass = tb);
            Control boxRole = CreateRoleBox();
            Control btnLog = CreateLoginButton();
            // Control btnGg = CreateGoogleButton(); // Tạm comment nếu chưa có GoogleHelper

            stack.Controls.AddRange(new Control[] { lblTitle, boxUser, boxPass, boxRole, btnLog }); // Thêm btnGg vào đây nếu muốn dùng
            p.Controls.Add(stack);

            p.Resize += (s, e) =>
            {
                stack.Location = new Point((p.Width - stack.Width) / 2, (p.Height - stack.Height) / 2);
            };

            return p;
        }

        // ====================================================================================
        // 4. HELPER COMPONENTS
        // ====================================================================================
        private Control CreateInput(string placeholder, bool isPass, Action<TextBox> assign)
        {
            Panel p = new Panel { Width = 380, Height = 55, Margin = new Padding(0, 0, 0, 15) };
            TextBox tb = new TextBox
            {
                Text = placeholder,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 12),
                BorderStyle = BorderStyle.None,
                Location = new Point(5, 15),
                Width = 370
            };
            Panel line = new Panel { Height = 2, Dock = DockStyle.Bottom, BackColor = Color.LightGray };

            tb.Enter += (s, e) => {
                if (tb.Text == placeholder) { tb.Text = ""; tb.ForeColor = Color.Black; if (isPass) tb.UseSystemPasswordChar = true; }
                line.BackColor = colorPrimary;
            };
            tb.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(tb.Text)) { tb.UseSystemPasswordChar = false; tb.Text = placeholder; tb.ForeColor = Color.Gray; }
                line.BackColor = Color.LightGray;
            };

            assign(tb);
            p.Controls.Add(tb); p.Controls.Add(line);
            return p;
        }

        private Control CreateRoleBox()
        {
            FlowLayoutPanel p = new FlowLayoutPanel { Width = 380, Height = 40, Margin = new Padding(0, 10, 0, 25) };
            rdoHocVien = CreateRadio("Học viên");
            rdoNhanSu = CreateRadio("Nhân sự");
            rdoAdmin = CreateRadio("Admin");
            rdoHocVien.Checked = true;
            p.Controls.AddRange(new Control[] { rdoHocVien, rdoNhanSu, rdoAdmin });
            return p;
        }

        private RadioButton CreateRadio(string text)
        {
            return new RadioButton { Text = text, Font = new Font("Segoe UI", 11), AutoSize = true, Margin = new Padding(0, 0, 25, 0), Cursor = Cursors.Hand };
        }

        private Control CreateLoginButton()
        {
            btnLogin = new Button
            {
                Text = "ĐĂNG NHẬP",
                Width = 380,
                Height = 50,
                BackColor = colorPrimary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click;
            return btnLogin;
        }

        private Control CreateGoogleButton()
        {
            btnGoogle = new Button
            {
                Text = "G  Đăng nhập bằng Google",
                Width = 380,
                Height = 45,
                BackColor = Color.White,
                ForeColor = Color.DimGray,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11),
                Margin = new Padding(0, 15, 0, 0),
                Cursor = Cursors.Hand
            };
            btnGoogle.FlatAppearance.BorderColor = Color.LightGray;
            // btnGoogle.Click += BtnGoogle_Click;
            return btnGoogle;
        }

        // ====================================================================================
        // 5. LOGIC XỬ LÝ (ĐÃ SỬA LỖI STRING)
        // ====================================================================================
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string role = rdoAdmin.Checked ? "Admin" : rdoNhanSu.Checked ? "NhanSu" : "HocVien";
            string user = txtUser.Text.Trim();
            string pass = txtPass.Text.Trim();

            if (string.IsNullOrEmpty(user) || user == "Tên đăng nhập" || string.IsNullOrEmpty(pass) || pass == "Mật khẩu")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Account acc = AccountBUS.Instance.Login(user, pass, role);
                if (acc != null)
                {
                    AppSession.CurrentUser = acc;
                    this.Hide();
                    new fMain(acc).ShowDialog();
                    this.Show();
                    txtPass.Text = "";
                }
                else
                {
                    MessageBox.Show("Sai thông tin đăng nhập hoặc vai trò!", "Lỗi Đăng Nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /* // Nếu bạn chưa có GoogleHelper thì comment đoạn này lại để tránh lỗi
        private async void BtnGoogle_Click(object sender, EventArgs e)
        {
            try
            {
                string email = await GoogleHelper.LoginGoogleAsync();
                if (!string.IsNullOrEmpty(email) && AccountDAO.Instance.LoginGoogle(email))
                {
                    Account acc = AccountDAO.Instance.GetAccountByEmail(email);
                    AppSession.CurrentUser = acc;
                    this.Hide();
                    new fMain(acc).ShowDialog();
                    this.Show();
                }
                else MessageBox.Show("Đăng nhập Google thất bại!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        */

        // --- DRAG FORM ---
        [DllImport("user32.dll")] private static extern void ReleaseCapture();
        [DllImport("user32.dll")] private static extern void SendMessage(IntPtr hWnd, int msg, int w, int l);
        private void ReleaseCapture_MouseDown(object s, MouseEventArgs e) { ReleaseCapture(); SendMessage(Handle, 0x112, 0xf012, 0); }
    }
}