using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using QuanLyTrungTam.DAO;
using QuanLyTrungTam.BUS;
using QuanLyTrungTam.Utilities;

namespace QuanLyTrungTam
{
    public partial class FrmDiemDanh : Form
    {
        private ComboBox cbLop;
        private ComboBox cbBuoi;
        private DataGridView dgvDiemDanh;
        private TextBox txbSearch;
        private Button btnLoad, btnSave;

        public FrmDiemDanh()
        {
            InitializeComponent();
            SetupUI();
            LoadClasses();
        }

        private void SetupUI()
        {
            this.Controls.Clear();
            this.Text = "Điểm Danh Lớp Học Theo Lịch";
            this.Size = new Size(1100, 650);
            this.BackColor = Color.FromArgb(240, 242, 245); // Soft Gray Background

            // --- HEADER PANEL ---
            Panel pnlTop = new Panel { Dock = DockStyle.Top, Height = 90, BackColor = Color.White };
            
            // Labels
            Label lblLop = new Label { Text = "Lớp học:", Location = new Point(20, 25), AutoSize = true, Font = new Font("Segoe UI", 10) };
            Label lblBuoi = new Label { Text = "Buổi học:", Location = new Point(320, 25), AutoSize = true, Font = new Font("Segoe UI", 10) };
            
            // Comboboxes
            cbLop = new ComboBox { Location = new Point(85, 22), Width = 220, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
            cbLop.SelectedIndexChanged += CbLop_SelectedIndexChanged;
            
            cbBuoi = new ComboBox { Location = new Point(390, 22), Width = 220, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };

            // Buttons
            btnLoad = new Button { 
                Text = "Tải Danh Sách", 
                Location = new Point(630, 20), 
                Size = new Size(130, 32), 
                BackColor = Color.FromArgb(33, 150, 243), // Blue
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat, 
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLoad.FlatAppearance.BorderSize = 0;
            btnLoad.Click += BtnLoad_Click;

            // Search
            Label lblSearch = new Label { Text = "🔍", Location = new Point(20, 62), AutoSize = true, Font = new Font("Segoe UI", 10) };
            txbSearch = new TextBox { Location = new Point(85, 60), Width = 525, Font = new Font("Segoe UI", 10), BorderStyle = BorderStyle.FixedSingle };
            txbSearch.Tag = "Tìm kiếm học viên...";
            txbSearch.Text = (string)txbSearch.Tag;
            txbSearch.ForeColor = Color.Gray;
            
            txbSearch.Enter += (s, e) => { if (txbSearch.Text == (string)txbSearch.Tag) { txbSearch.Text = ""; txbSearch.ForeColor = Color.Black; } };
            txbSearch.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txbSearch.Text)) { txbSearch.Text = (string)txbSearch.Tag; txbSearch.ForeColor = Color.Gray; } };
            txbSearch.TextChanged += (s, e) => { if(txbSearch.ForeColor == Color.Black) FilterGrid(); };

            // Save Button (Big)
            btnSave = new Button { 
                Text = "💾 LƯU ĐIỂM DANH", 
                Location = new Point(800, 20), 
                Size = new Size(180, 50), 
                BackColor = Color.FromArgb(40, 167, 69), // Green
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat, 
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            pnlTop.Controls.AddRange(new Control[] { lblLop, cbLop, lblBuoi, cbBuoi, btnLoad, lblSearch, txbSearch, btnSave });

            // --- GRID ---
            dgvDiemDanh = new DataGridView();
            dgvDiemDanh.Dock = DockStyle.Fill;
            dgvDiemDanh.BackgroundColor = Color.White;
            dgvDiemDanh.BorderStyle = BorderStyle.None;
            dgvDiemDanh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDiemDanh.AllowUserToAddRows = false;
            dgvDiemDanh.RowHeadersVisible = false;
            dgvDiemDanh.ColumnHeadersHeight = 45;
            
            dgvDiemDanh.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243); // Blue Header
            dgvDiemDanh.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDiemDanh.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvDiemDanh.EnableHeadersVisualStyles = false;
            
            dgvDiemDanh.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvDiemDanh.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dgvDiemDanh.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvDiemDanh.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDiemDanh.RowTemplate.Height = 35;

            // Layout
            this.Controls.Add(dgvDiemDanh);
            this.Controls.Add(pnlTop);
        }

        private void LoadClasses()
        {
            DataTable dt;
            try
            {
                if (AppSession.CurrentUser.Quyen.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    // [REFACTOR] Dùng LopHocBUS. Get all lop.
                    dt = LopHocBUS.Instance.GetAllLop(); 
                }
                else
                {
                    string maNS = AppSession.CurrentUser.MaNguoiDung;
                    // [REFACTOR] Dùng LopHocBUS
                    dt = LopHocDAO.Instance.GetLopByNhanSu(maNS); // Temporary fallback to DAO if BUS missing, 
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    cbLop.DisplayMember = "TenLop";
                    cbLop.ValueMember = "MaLop";
                    cbLop.DataSource = dt;
                }
                else
                {
                    cbLop.DataSource = null;
                }
            }
            catch { }
        }

        private void CbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbLop.SelectedValue == null) return;

                string maLop = "";
                DataRowView drv = cbLop.SelectedValue as DataRowView;
                if (drv != null) maLop = drv["MaLop"].ToString();
                else maLop = cbLop.SelectedValue.ToString();

                DataRow info = LopHocDAO.Instance.GetClassScheduleInfo(maLop);

                if (info != null)
                {
                    DateTime startDate = Convert.ToDateTime(info["NgayBatDau"]);
                    string scheduleStr = info["Thu"].ToString();

                    int totalSessions = 12;
                    if (info.Table.Columns.Contains("SoBuoi") && info["SoBuoi"] != DBNull.Value)
                    {
                        totalSessions = Convert.ToInt32(info["SoBuoi"]);
                    }

                    List<BuoiHocDTO> listBuoi = ScheduleHelper.GenerateSchedule(startDate, scheduleStr, totalSessions);

                    cbBuoi.DataSource = listBuoi;
                    cbBuoi.DisplayMember = "HienThi";
                    cbBuoi.ValueMember = "Ngay";
                }
                else
                {
                    cbBuoi.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch học: " + ex.Message);
            }
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            if (cbLop.SelectedValue == null || cbBuoi.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Lớp và Buổi học!");
                return;
            }

            try
            {
                string maLop = "";
                DataRowView drv = cbLop.SelectedValue as DataRowView;
                if (drv != null) maLop = drv["MaLop"].ToString();
                else maLop = cbLop.SelectedValue.ToString();

                DateTime ngayDiemDanh = (DateTime)cbBuoi.SelectedValue;

                // [REFACTOR] Dùng DiemDanhBUS
                DataTable dt = DiemDanhBUS.Instance.GetDiemDanhList(maLop, ngayDiemDanh);
                dgvDiemDanh.DataSource = dt;

                // Format columns
                if (dgvDiemDanh.Columns.Contains("MaHV"))
                {
                    dgvDiemDanh.Columns["MaHV"].ReadOnly = true;
                    dgvDiemDanh.Columns["MaHV"].HeaderText = "Mã HV";
                }
                if (dgvDiemDanh.Columns.Contains("HoTen"))
                {
                    dgvDiemDanh.Columns["HoTen"].ReadOnly = true;
                    dgvDiemDanh.Columns["HoTen"].HeaderText = "Họ Tên Học Viên";
                }

                if (dgvDiemDanh.Columns.Contains("CoMat"))
                {
                    dgvDiemDanh.Columns["CoMat"].HeaderText = "Có Mặt";
                    dgvDiemDanh.Columns["CoMat"].Width = 100;
                }
                if (dgvDiemDanh.Columns.Contains("LyDo")) dgvDiemDanh.Columns["LyDo"].HeaderText = "Ghi Chú Vắng";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message);
            }
        }

        private void FilterGrid()
        {
            DataTable dt = dgvDiemDanh.DataSource as DataTable;
            if (dt != null)
            {
                string k = txbSearch.Text;
                string safeKey = k.Replace("'", "''");
                dt.DefaultView.RowFilter = string.Format("MaHV LIKE '%{0}%' OR HoTen LIKE '%{0}%'", safeKey);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (dgvDiemDanh.Rows.Count == 0) return;

            // --- QUAN TRỌNG: Kết thúc chỉnh sửa để lưu giá trị checkbox ---
            dgvDiemDanh.EndEdit();

            try
            {
                string maLop = "";
                DataRowView drv = cbLop.SelectedValue as DataRowView;
                if (drv != null) maLop = drv["MaLop"].ToString();
                else maLop = cbLop.SelectedValue.ToString();

                DateTime ngay = (DateTime)cbBuoi.SelectedValue;

                int count = 0;
                foreach (DataGridViewRow row in dgvDiemDanh.Rows)
                {
                    string maHV = row.Cells["MaHV"].Value.ToString();

                    bool coMat = false;
                    // Xử lý giá trị checkbox an toàn
                    if (row.Cells["CoMat"].Value != null && row.Cells["CoMat"].Value != DBNull.Value)
                    {
                        var val = row.Cells["CoMat"].Value;
                        if (val is bool) coMat = (bool)val;
                        else if (val.ToString() == "1" || val.ToString().ToLower() == "true") coMat = true;
                    }

                    string lyDo = "";
                    if (row.Cells["LyDo"].Value != null) lyDo = row.Cells["LyDo"].Value.ToString();

                    // [REFACTOR] Dùng DiemDanhBUS
                    if (DiemDanhBUS.Instance.SaveDiemDanh(maLop, maHV, ngay, coMat, lyDo)) count++;
                }
                MessageBox.Show(string.Format("Đã lưu điểm danh cho buổi ngày {0:dd/MM/yyyy}!", ngay), "Thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message);
            }
        }
    }
}