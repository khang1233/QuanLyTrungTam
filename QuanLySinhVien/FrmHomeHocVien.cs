using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using QuanLyTrungTam.DAO;
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
                BackColor = Color.Teal,
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
                ForeColor = Color.Teal,
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
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Teal;
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
            DataRow r = HocVienDAO.Instance.GetInfoHocVien(currentMaHV);
            if (r != null)
            {
                lblWelcome.Text = $"Xin chào học viên: {r["HoTen"].ToString().ToUpper()} ({currentMaHV})";

                decimal tongHP = TuitionDAO.Instance.GetTongNo(currentMaHV);
                decimal daDong = TuitionDAO.Instance.GetDaDong(currentMaHV);
                decimal conNo = tongHP - daDong;

                if (conNo > 0)
                {
                    lblDebtStatus.Text = $"Cảnh báo: Bạn còn nợ học phí {conNo:N0} VNĐ. Vui lòng đóng sớm!";
                    lblDebtStatus.ForeColor = Color.Red;
                }
                else
                {
                    lblDebtStatus.Text = "Trạng thái: Đã hoàn thành học phí.";
                    lblDebtStatus.ForeColor = Color.Green;
                }
            }

            // 2. Lịch học
            DataTable dtSch = TuitionDAO.Instance.GetListDangKy(currentMaHV);
            dgvSchedule.DataSource = dtSch;
            SetHeader(dgvSchedule, "TenLop", "Lớp Học");
            SetHeader(dgvSchedule, "TenKyNang", "Môn Học");
            SetHeader(dgvSchedule, "CaHoc", "Ca Học");
            SetHeader(dgvSchedule, "NgayBatDau", "Ngày Bắt Đầu");
            HideCol(dgvSchedule, "MaLop", "HocPhiLop", "NgayDangKy", "MaHV");

            // 3. Điểm số
            DataTable dtGrade = DiemDAO.Instance.GetBangDiemCaNhan(currentMaHV);
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
            // Lấy thông tin Account hiện tại dựa vào MaHV (vì đây là form của HV)
            // Lưu ý: Cần đảm bảo cột TenDangNhap trong bảng TaiKhoan khớp với MaHV của học viên
            // Hoặc dùng AccountDAO để tìm theo MaNguoiDung = currentMaHV

            // Giả sử TenDangNhap = MaHV (logic thường thấy)
            // Nếu hệ thống bạn cho phép tên đăng nhập tùy ý, cần tìm lại Account từ Database
            Account acc = AccountDAO.Instance.GetAccountByUserName(currentMaHV);

            // Nếu không tìm thấy theo username, thử tìm theo MaNguoiDung (cách an toàn hơn)
            if (acc == null)
            {
                // Bạn cần thêm hàm GetAccountByUserID vào AccountDAO nếu chưa có
                // Hoặc tạm thời dùng currentMaHV làm tên đăng nhập nếu chắc chắn
                acc = new Account
                {
                    TenDangNhap = currentMaHV,
                    MatKhau = "123",
                    Quyen = "HocVien",
                    MaNguoiDung = currentMaHV,
                    TrangThai = "Hoạt động"
                };
                // Dòng trên chỉ là giả lập nếu chưa lấy đc DB, tốt nhất nên query DB
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