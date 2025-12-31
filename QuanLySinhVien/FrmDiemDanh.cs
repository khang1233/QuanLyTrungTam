    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Data;
    using System.Windows.Forms;
    using QuanLyTrungTam.DAO;
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
                this.BackColor = Color.White;

                Panel pnlTop = new Panel { Dock = DockStyle.Top, Height = 100, BackColor = Color.WhiteSmoke };

                Label lblLop = new Label { Text = "Chọn Lớp:", Location = new Point(20, 25), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                cbLop = new ComboBox { Location = new Point(100, 22), Width = 250, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
                cbLop.SelectedIndexChanged += CbLop_SelectedIndexChanged;

                Label lblBuoi = new Label { Text = "Chọn Buổi:", Location = new Point(370, 25), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                cbBuoi = new ComboBox { Location = new Point(450, 22), Width = 250, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };

                btnLoad = new Button { Text = "TẢI DANH SÁCH", Location = new Point(720, 20), Size = new Size(140, 32), BackColor = Color.Teal, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
                btnLoad.Click += BtnLoad_Click;

                Label lblSearch = new Label { Text = "Tìm nhanh:", Location = new Point(20, 65), AutoSize = true, Font = new Font("Segoe UI", 9) };
                txbSearch = new TextBox { Location = new Point(100, 62), Width = 250, Font = new Font("Segoe UI", 9) };
                txbSearch.TextChanged += (s, e) => FilterGrid();

                btnSave = new Button { Text = "LƯU KẾT QUẢ", Location = new Point(900, 20), Size = new Size(150, 60), BackColor = Color.OrangeRed, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11, FontStyle.Bold) };
                btnSave.Click += BtnSave_Click;

                pnlTop.Controls.AddRange(new Control[] { lblLop, cbLop, lblBuoi, cbBuoi, btnLoad, lblSearch, txbSearch, btnSave });

                dgvDiemDanh = new DataGridView();
                dgvDiemDanh.Dock = DockStyle.Fill;
                dgvDiemDanh.BackgroundColor = Color.White;
                dgvDiemDanh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvDiemDanh.AllowUserToAddRows = false;
                dgvDiemDanh.RowHeadersVisible = false;
                dgvDiemDanh.ColumnHeadersHeight = 40;
                dgvDiemDanh.ColumnHeadersDefaultCellStyle.BackColor = Color.Teal;
                dgvDiemDanh.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvDiemDanh.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvDiemDanh.EnableHeadersVisualStyles = false;
                dgvDiemDanh.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

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
                        dt = DataProvider.Instance.ExecuteQuery("SELECT MaLop, TenLop FROM LopHoc");
                    }
                    else
                    {
                        string maNS = AppSession.CurrentUser.MaNguoiDung;
                        dt = LopHocDAO.Instance.GetLopByNhanSu(maNS);
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
                    if (cbLop.SelectedValue is DataRowView drv) maLop = drv["MaLop"].ToString();
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
                    if (cbLop.SelectedValue is DataRowView drv) maLop = drv["MaLop"].ToString();
                    else maLop = cbLop.SelectedValue.ToString();

                    DateTime ngayDiemDanh = (DateTime)cbBuoi.SelectedValue;

                    DataTable dt = DiemDanhDAO.Instance.GetDiemDanhList(maLop, ngayDiemDanh);
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
                    dt.DefaultView.RowFilter = $"MaHV LIKE '%{safeKey}%' OR HoTen LIKE '%{safeKey}%'";
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
                    if (cbLop.SelectedValue is DataRowView drv) maLop = drv["MaLop"].ToString();
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

                        string lyDo = row.Cells["LyDo"].Value?.ToString() ?? "";

                        if (DiemDanhDAO.Instance.SaveDiemDanh(maLop, maHV, ngay, coMat, lyDo)) count++;
                    }
                    MessageBox.Show($"Đã lưu điểm danh cho buổi ngày {ngay:dd/MM/yyyy}!", "Thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message);
                }
            }
        }
    }