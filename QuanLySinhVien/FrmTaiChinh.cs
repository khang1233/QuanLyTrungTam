using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using QuanLyTrungTam.DAO;

namespace QuanLyTrungTam
{
    public partial class FrmTaiChinh : Form
    {
        // --- KHAI BÁO CONTROLS ---
        private DataGridView dgvHistory;
        private DateTimePicker dtpFrom, dtpTo;
        private Label lblTongTien;
        private Button btnXem;

        public FrmTaiChinh()
        {
            // Khởi tạo giao diện code thuần (Bỏ qua Designer cũ)
            InitializeCustomUI();

            // Mặc định load dữ liệu từ đầu tháng đến hiện tại
            LoadData();
        }

        // =========================================================
        // 1. THIẾT KẾ GIAO DIỆN MỚI
        // =========================================================
        private void InitializeCustomUI()
        {
            this.Controls.Clear();
            this.Text = "Lịch Sử Giao Dịch & Doanh Thu";
            this.Size = new Size(1100, 650);
            this.BackColor = Color.WhiteSmoke;

            // --- PANEL HEADER (CHỨA BỘ LỌC) ---
            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            // Tiêu đề
            Label lblTitle = new Label
            {
                Text = "BÁO CÁO TÀI CHÍNH",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.Navy,
                AutoSize = true,
                Location = new Point(20, 15)
            };

            // Bộ lọc ngày
            Label lblTu = new Label { Text = "Từ ngày:", Location = new Point(25, 60), AutoSize = true, Font = new Font("Segoe UI", 10) };
            dtpFrom = new DateTimePicker { Location = new Point(90, 57), Width = 120, Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 10) };
            // Set ngày về mùng 1 đầu tháng
            dtpFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            Label lblDen = new Label { Text = "Đến ngày:", Location = new Point(230, 60), AutoSize = true, Font = new Font("Segoe UI", 10) };
            dtpTo = new DateTimePicker { Location = new Point(300, 57), Width = 120, Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 10) };

            // Nút Xem
            btnXem = new Button
            {
                Text = "🔍 Xem Kết Quả",
                Location = new Point(440, 55),
                Size = new Size(140, 32),
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnXem.Click += (s, e) => LoadData();

            // Khung hiển thị Tổng tiền (Góc phải)
            Panel pnlTotal = new Panel
            {
                Location = new Point(700, 15),
                Size = new Size(350, 70),
                BackColor = Color.MintCream,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblLabelTien = new Label
            {
                Text = "TỔNG DOANH THU (THỰC THU):",
                Location = new Point(10, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.Gray
            };

            lblTongTien = new Label
            {
                Text = "0 VNĐ",
                Location = new Point(10, 30),
                AutoSize = false,
                Width = 330,
                Height = 35,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.SeaGreen,
                TextAlign = ContentAlignment.MiddleRight
            };

            pnlTotal.Controls.AddRange(new Control[] { lblLabelTien, lblTongTien });

            // Add control vào Header
            pnlHeader.Controls.AddRange(new Control[] { lblTitle, lblTu, dtpFrom, lblDen, dtpTo, btnXem, pnlTotal });
            this.Controls.Add(pnlHeader);

            // --- GRID VIEW (DANH SÁCH) ---
            Panel pnlBody = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };

            dgvHistory = new DataGridView();
            dgvHistory.Dock = DockStyle.Fill;
            dgvHistory.BackgroundColor = Color.White;
            dgvHistory.BorderStyle = BorderStyle.None;
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistory.ReadOnly = true;
            dgvHistory.RowHeadersVisible = false;
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Style Grid
            dgvHistory.ColumnHeadersHeight = 40;
            dgvHistory.EnableHeadersVisualStyles = false;
            dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 150, 136);
            dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvHistory.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvHistory.RowTemplate.Height = 35;

            pnlBody.Controls.Add(dgvHistory);
            this.Controls.Add(pnlBody);
            pnlHeader.SendToBack(); // Đẩy header xuống dưới trong stack control để dock fill hoạt động đúng
            pnlBody.BringToFront();
        }

        // =========================================================
        // 2. LOGIC TẢI DỮ LIỆU
        // =========================================================
        private void LoadData()
        {
            DateTime from = dtpFrom.Value;
            DateTime to = dtpTo.Value;

            if (from > to)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Lỗi ngày tháng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Lấy danh sách giao dịch từ FinanceDAO
            DataTable dt = FinanceDAO.Instance.GetHistoryByDate(from, to);
            dgvHistory.DataSource = dt;
            FormatGrid();

            // 2. Tính tổng doanh thu từ FinanceDAO
            decimal totalRevenue = FinanceDAO.Instance.GetRevenueByDate(from, to);
            lblTongTien.Text = string.Format("{0:N0} VNĐ", totalRevenue);
        }

        private void FormatGrid()
        {
            if (dgvHistory.Columns.Count == 0) return;

            // Đổi tên cột hiển thị tiếng Việt
            SetHeader("IdGD", "Mã GD");
            SetHeader("NgayGD", "Ngày Giao Dịch");
            SetHeader("LoaiGD", "Loại");
            SetHeader("SoTien", "Số Tiền");
            SetHeader("NoiDung", "Nội Dung");
            SetHeader("MaDoiTuong", "Đối Tượng");

            // Định dạng cột
            if (dgvHistory.Columns.Contains("NgayGD"))
                dgvHistory.Columns["NgayGD"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            if (dgvHistory.Columns.Contains("SoTien"))
            {
                dgvHistory.Columns["SoTien"].DefaultCellStyle.Format = "N0";
                dgvHistory.Columns["SoTien"].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvHistory.Columns["SoTien"].DefaultCellStyle.ForeColor = Color.DarkGreen;
            }

            // Ẩn cột IdGD nếu thấy không cần thiết
            if (dgvHistory.Columns.Contains("IdGD")) dgvHistory.Columns["IdGD"].Width = 80;
        }

        private void SetHeader(string colName, string text)
        {
            if (dgvHistory.Columns.Contains(colName)) dgvHistory.Columns[colName].HeaderText = text;
        }
    }
}