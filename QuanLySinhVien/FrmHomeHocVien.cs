using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyTrungTam.DAO;

namespace QuanLyTrungTam
{
    public partial class FrmHomeHocVien : Form
    {
        private string maHV;
        private Label lblChaoMung;
        private Label lblInfo;
        private DataGridView dgvLichHoc;

        public FrmHomeHocVien(string ma)
        {
            this.maHV = ma;
            SetupUI();
            LoadData();
        }

        private void SetupUI()
        {
            this.BackColor = Color.White;
            this.Padding = new Padding(20);

            // 1. Phần Chào mừng & Thông tin cá nhân (Phía trên)
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 120, BackColor = ColorTranslator.FromHtml("#E0F2F1") }; // Xanh ngọc nhạt

            lblChaoMung = new Label
            {
                Text = "Xin chào...",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#00695C"),
                Location = new Point(20, 15),
                AutoSize = true
            };

            lblInfo = new Label
            {
                Text = "Đang tải...",
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.DimGray,
                Location = new Point(20, 50),
                AutoSize = true
            };

            pnlHeader.Controls.Add(lblChaoMung);
            pnlHeader.Controls.Add(lblInfo);
            this.Controls.Add(pnlHeader);

            // 2. Phần Lịch học (Phía dưới)
            GroupBox grpLich = new GroupBox
            {
                Text = " 📅 Lịch Học Của Bạn & Các Lớp Đã Đăng Ký ",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#00695C")
            };

            dgvLichHoc = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            // Style cho Grid
            dgvLichHoc.EnableHeadersVisualStyles = false;
            dgvLichHoc.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#009688");
            dgvLichHoc.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLichHoc.ColumnHeadersHeight = 40;
            dgvLichHoc.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            grpLich.Controls.Add(dgvLichHoc);

            // Panel đệm để tách header và grid
            Panel pnlSpacer = new Panel { Dock = DockStyle.Top, Height = 20 };

            this.Controls.Add(grpLich);
            this.Controls.Add(pnlSpacer);
            pnlHeader.SendToBack(); // Đẩy header lên trên cùng
        }

        private void LoadData()
        {
            // A. Tải thông tin cá nhân
            DataRow r = HocVienDAO.Instance.GetInfoHocVien(maHV);
            if (r != null)
            {
                string ten = r["HoTen"].ToString();
                string sdt = r["SoDienThoai"].ToString();
                string email = r["Email"].ToString();

                lblChaoMung.Text = $"Xin chào học viên: {ten.ToUpper()}";
                lblInfo.Text = $"Mã số: {maHV}  |  SĐT: {sdt}  |  Email: {email}\n" +
                               "Chúc bạn một ngày học tập hiệu quả!";
            }

            // B. Tải lịch học
            DataTable dt = TuitionDAO.Instance.GetListDangKy(maHV);
            dgvLichHoc.DataSource = dt;

            // Đặt tên cột tiếng Việt cho dễ hiểu
            if (dgvLichHoc.Columns.Contains("TenKyNang")) dgvLichHoc.Columns["TenKyNang"].HeaderText = "Môn Học";
            if (dgvLichHoc.Columns.Contains("TenLop")) dgvLichHoc.Columns["TenLop"].HeaderText = "Tên Lớp";
            if (dgvLichHoc.Columns.Contains("LichHoc")) dgvLichHoc.Columns["LichHoc"].HeaderText = "Lịch Học (Giờ)";
            if (dgvLichHoc.Columns.Contains("NgayBatDau")) dgvLichHoc.Columns["NgayBatDau"].HeaderText = "Ngày Bắt Đầu";
            if (dgvLichHoc.Columns.Contains("HocPhiLop"))
            {
                dgvLichHoc.Columns["HocPhiLop"].HeaderText = "Học Phí";
                dgvLichHoc.Columns["HocPhiLop"].DefaultCellStyle.Format = "N0";
            }
        }
    }
}