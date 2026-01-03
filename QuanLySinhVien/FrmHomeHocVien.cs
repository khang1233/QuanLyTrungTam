
using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using QuanLyTrungTam.DAO;
using QuanLyTrungTam.BUS;
using QuanLyTrungTam.DTO; // Thêm DTO để dùng class Account

namespace QuanLyTrungTam
{
    public partial class FrmHomeHocVien : Form
    {
        private string currentMaHV;

        // Controls
        private Label lblWelcome, lblDebtStatus;
        private DataGridView dgvSchedule, dgvGrades;
        private TabControl tabMain;
        private Button btnLogout;
        private Button btnChangePass; // [MỚI] Nút đổi mật khẩu

        public FrmHomeHocVien(string maHV)
        {
            this.currentMaHV = maHV;
            InitializeComponent();
            SetupDashboardUI();
            LoadAllData();
        }

        private void SetupDashboardUI()
        {
            this.Text = "Cổng Thông Tin Học Viên";
            this.BackColor = Color.White;
            this.Padding = new Padding(10);

            // --- 1. HEADER (Chào mừng & Đăng xuất) ---
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 80, BackColor = Color.White }; // Tăng height lên 80

            // Panel chứa nút chức năng (Neo bên phải)
            Panel pnlRight = new Panel { Dock = DockStyle.Right, Width = 150, Padding = new Padding(0, 5, 10, 0) };

            // [MỚI] Nút Đổi Mật Khẩu
            btnChangePass = new Button
            {
                Text = "Đổi mật khẩu",
                Dock = DockStyle.Top,
                Height = 30,
                BackColor = Color.FromArgb(33, 150, 243), // Blue
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnChangePass.FlatAppearance.BorderSize = 0;
            btnChangePass.Click += BtnChangePass_Click; // Gán sự kiện

            // Panel đệm giữa 2 nút
            Panel pnlSpacer = new Panel { Dock = DockStyle.Top, Height = 5 };

            // Nút Đăng Xuất
            btnLogout = new Button
            {
                Text = "Đăng xuất",
                Dock = DockStyle.Top,
                Height = 30,
                BackColor = Color.IndianRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += BtnLogout_Click;

            // Thêm nút vào Panel Phải (Thứ tự thêm ngược với hiển thị vì Dock=Top)
            pnlRight.Controls.Add(btnLogout);
            pnlRight.Controls.Add(pnlSpacer);
            pnlRight.Controls.Add(btnChangePass);

            // Label thông tin (Neo bên trái)
            lblWelcome = new Label
            {
                Text = "Xin chào, ...",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 150, 243),
                Location = new Point(10, 10),
                AutoSize = true
            };

            lblDebtStatus = new Label
            {
                Text = "Đang tải thông tin học phí...",
                Font = new Font("Segoe UI", 11, FontStyle.Italic),
                Location = new Point(10, 42),
                AutoSize = true
            };

            pnlHeader.Controls.Add(pnlRight);
            pnlHeader.Controls.Add(lblWelcome);
            pnlHeader.Controls.Add(lblDebtStatus);

            this.Controls.Add(pnlHeader);

            // --- 2. TAB CONTROL ---
            tabMain = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10) };

            TabPage tabSchedule = new TabPage("📅  Lịch Học & Lớp Đăng Ký");
            dgvSchedule = CreateStyleGrid();
            tabSchedule.Controls.Add(dgvSchedule);

            TabPage tabGrade = new TabPage("🏆  Kết Quả Học Tập Chi Tiết");
            dgvGrades = CreateStyleGrid();
            tabGrade.Controls.Add(dgvGrades);

            tabMain.TabPages.AddRange(new TabPage[] { tabSchedule, tabGrade });
            this.Controls.Add(tabMain);
            pnlHeader.SendToBack();
        }

        private DataGridView CreateStyleGrid()
        {
            DataGridView dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            dgv.BackgroundColor = Color.WhiteSmoke;
            dgv.BorderStyle = BorderStyle.None;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.ColumnHeadersHeight = 40;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            return dgv;
        }

        private void LoadAllData()
        {
            // 1. Thông tin cá nhân
            // [REFACTOR] Dùng HocVienBUS
            DataRow r = HocVienBUS.Instance.GetInfoHocVien(currentMaHV);
            if (r != null)
            {
                lblWelcome.Text = string.Format("Xin chào học viên: {0} ({1})", r["HoTen"].ToString().ToUpper(), currentMaHV);

                // [REFACTOR] Dùng TuitionBUS.GetHocPhiInfo
                HocPhiInfo info = TuitionBUS.Instance.GetHocPhiInfo(currentMaHV);
                decimal conNo = info.ConNo;

                if (conNo > 0)
                {
                    lblDebtStatus.Text = string.Format("Cảnh báo: Bạn còn nợ học phí {0:N0} VNĐ. Vui lòng đóng sớm!", conNo);
                    lblDebtStatus.ForeColor = Color.Red;
                }
                else
                {
                    lblDebtStatus.Text = "Trạng thái: Đã hoàn thành học phí.";
                    lblDebtStatus.ForeColor = Color.Green;
                }
            }

            // 2. Lịch học
            // [REFACTOR] Dùng TuitionBUS
            DataTable dtSch = TuitionBUS.Instance.GetListDangKy(currentMaHV);
            dgvSchedule.DataSource = dtSch;
            SetHeader(dgvSchedule, "TenLop", "Lớp Học");
            SetHeader(dgvSchedule, "TenKyNang", "Môn Học");
            SetHeader(dgvSchedule, "CaHoc", "Ca Học");
            SetHeader(dgvSchedule, "NgayBatDau", "Ngày Bắt Đầu");
            HideCol(dgvSchedule, "MaLop", "HocPhiLop", "NgayDangKy", "MaHV");

            // 3. Điểm số
            // [REFACTOR] Dùng DiemBUS
            DataTable dtGrade = DiemBUS.Instance.GetBangDiemCaNhan(currentMaHV);
            dgvGrades.DataSource = dtGrade;
            SetHeader(dgvGrades, "TenLop", "Lớp Học");
            SetHeader(dgvGrades, "MonHoc", "Môn Học");
            SetHeader(dgvGrades, "Diem15p1", "15 Phút (1)");
            SetHeader(dgvGrades, "Diem15p2", "15 Phút (2)");
            SetHeader(dgvGrades, "DiemGiuaKy", "Giữa Kỳ");
            SetHeader(dgvGrades, "DiemCuoiKy", "Cuối Kỳ");
            SetHeader(dgvGrades, "DiemTongKet", "Tổng Kết");
            SetHeader(dgvGrades, "GhiChu", "Nhận Xét GV");

            dgvGrades.CellFormatting += (s, e) => {
                if (dgvGrades.Columns[e.ColumnIndex].Name == "DiemTongKet" && e.Value != null && e.Value != DBNull.Value)
                {
                    double d;
                    if (double.TryParse(e.Value.ToString(), out d))
                    {
                        if (d >= 8.5) e.CellStyle.ForeColor = Color.Green;
                        else if (d < 5) e.CellStyle.ForeColor = Color.Red;
                        else e.CellStyle.ForeColor = Color.Blue;
                        e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    }
                }
            };
        }

        // --- SỰ KIỆN CLICK NÚT ---

        private void BtnChangePass_Click(object sender, EventArgs e)
        {
            // [REFACTOR] Dùng AccountBUS
            Account acc = AccountBUS.Instance.GetAccountByUserName(currentMaHV);

            if (acc == null)
            {
                // Logic giả lập nếu chưa có TK
                acc = new Account
                {
                    TenDangNhap = currentMaHV,
                    MatKhau = "123",
                    Quyen = "HocVien",
                    MaNguoiDung = currentMaHV,
                    TrangThai = "Hoạt động"
                };
            }

            if (acc != null)
            {
                fChangePassword f = new fChangePassword(acc);
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin tài khoản!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (this.ParentForm != null) this.ParentForm.Close();
                else this.Close();
            }
        }

        private void SetHeader(DataGridView dgv, string colName, string text)
        {
            if (dgv.Columns.Contains(colName)) dgv.Columns[colName].HeaderText = text;
        }
        private void HideCol(DataGridView dgv, params string[] cols)
        {
            foreach (string c in cols) if (dgv.Columns.Contains(c)) dgv.Columns[c].Visible = false;
        }
    }
}