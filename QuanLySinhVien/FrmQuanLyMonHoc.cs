using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using QuanLyTrungTam.BUS; // [REFACTOR]
using QuanLyTrungTam.DTO;

namespace QuanLyTrungTam
{
    public partial class FrmQuanLyMonHoc : Form
    {
        // Control
        private TextBox txbMa, txbTen, txbMoTa, txbSearch; 
        private Button btnSearch; 
        private ComboBox cbHinhThuc, cbTrangThai;
        private NumericUpDown numSoBuoi, numDonGia;
        private Label lblTongHocPhi;
        private DataGridView dgvMonHoc;

        // Biến logic
        private string curMaKN = "";

        public FrmQuanLyMonHoc()
        {
            InitializeComponent();
            SetupCustomUI();
            try { LoadData(); } catch { }
        }

        // =========================================================================
        // 1. THIẾT KẾ GIAO DIỆN (THÊM TÌM KIẾM GÓC DƯỚI)
        // =========================================================================
        private void SetupCustomUI()
        {
            this.Controls.Clear();
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Size = new Size(1280, 800);
            this.Padding = new Padding(10);

            // 1. HEADER
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = Color.White, Padding = new Padding(20, 0, 20, 0) };
            Label lblTitle = new Label { 
                Text = "QUẢN LÝ MÔN HỌC & KỸ NĂNG", 
                Font = new Font("Segoe UI", 18, FontStyle.Bold), 
                ForeColor = Color.FromArgb(33, 150, 243), 
                AutoSize = true, 
                TextAlign = ContentAlignment.MiddleLeft 
            };
            lblTitle.Location = new Point(20, (pnlHeader.Height - lblTitle.Height) / 2);
            pnlHeader.Controls.Add(lblTitle);

            // 2. INPUT PANEL
            Panel pnlInput = new Panel { Dock = DockStyle.Top, Height = 340, BackColor = Color.Transparent, Padding = new Padding(0, 15, 0, 15) }; // Increased height for better spacing

            // Init Controls
            txbMa = new TextBox { ReadOnly = false, BackColor = Color.LightYellow, BorderStyle = BorderStyle.FixedSingle };
            txbTen = new TextBox { BorderStyle = BorderStyle.FixedSingle }; 
            txbMoTa = new TextBox { BorderStyle = BorderStyle.FixedSingle, Multiline = true, Height = 60 };
            txbSearch = new TextBox { BorderStyle = BorderStyle.FixedSingle };
            SetPlaceholder(txbSearch, "Nhập tên môn cần tìm..."); 

            cbHinhThuc = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Standard };
            cbHinhThuc.Items.AddRange(new string[] { "Offline", "Online", "Video Record" });
            cbHinhThuc.SelectedIndex = 0;

            cbTrangThai = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Standard };
            cbTrangThai.Items.AddRange(new string[] { "Đang hoạt động", "Ngưng hoạt động" });
            cbTrangThai.SelectedIndex = 0;

            numSoBuoi = new NumericUpDown { Minimum = 1, Maximum = 1000, Value = 12, BorderStyle = BorderStyle.FixedSingle };
            numDonGia = new NumericUpDown { Minimum = 0, Maximum = 1000000000, Increment = 50000, Value = 100000, BorderStyle = BorderStyle.FixedSingle };

            lblTongHocPhi = new Label { Text = "TỔNG HỌC PHÍ: 1,200,000 VNĐ", Location = new Point(20, 160), AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.Red };

            numSoBuoi.ValueChanged += UpdateHocPhi;
            numDonGia.ValueChanged += UpdateHocPhi;

            // --- LAYOUT GROUPS ---
            // Group 1: Thông Tin Chung
            GroupBox gbGeneral = new GroupBox { 
                Text = "Thông Tin Môn Học", 
                Font = new Font("Segoe UI", 10, FontStyle.Bold), 
                ForeColor = Color.DimGray,
                Location = new Point(20, 10), 
                Size = new Size(450, 240),
                BackColor = Color.White 
            };
            int gy = 35; int gap = 50;
            AddInput(gbGeneral, "Mã Kỹ Năng:", txbMa, 20, gy);
            AddInput(gbGeneral, "Tên Kỹ Năng:", txbTen, 20, gy + gap);
            AddInput(gbGeneral, "Hình Thức:", cbHinhThuc, 20, gy + gap * 2);
            AddInput(gbGeneral, "Trạng Thái:", cbTrangThai, 20, gy + gap * 3);


            // Group 2: Chi Tiết & Học Phí
            GroupBox gbDetail = new GroupBox { 
                Text = "Chi Tiết & Học Phí", 
                Font = new Font("Segoe UI", 10, FontStyle.Bold), 
                ForeColor = Color.DimGray,
                Location = new Point(490, 10), 
                Size = new Size(500, 240),
                BackColor = Color.White 
            };
            AddInput(gbDetail, "Số Buổi:", numSoBuoi, 20, gy);
            AddInput(gbDetail, "Đơn Giá:", numDonGia, 20, gy + gap);
            AddInput(gbDetail, "Mô Tả:", txbMoTa, 20, gy + gap * 2);

            // Label Tong Hoc Phi - move inside Group 2 or below inputs
            lblTongHocPhi.Location = new Point(250, gy + gap + 3); // Align with Don Gia but to the right? Or below?
            // Let's put it below
            lblTongHocPhi.Location = new Point(130, gy + gap * 2 + 70);
            gbDetail.Controls.Add(lblTongHocPhi);

            pnlInput.Controls.Add(gbGeneral);
            pnlInput.Controls.Add(gbDetail);

            // --- ACTION PANEL (Right) ---
            Panel pnlActions = new Panel { Location = new Point(1010, 20), Size = new Size(180, 230) };
            Button btnThem = CreateBtn("Thêm Mới", Color.FromArgb(40, 167, 69), 0, 0); // Green
            Button btnSua = CreateBtn("Cập Nhật", Color.FromArgb(0, 123, 255), 0, 50); // Blue
            Button btnXoa = CreateBtn("Xóa Môn", Color.FromArgb(220, 53, 69), 0, 100); // Red
            Button btnLamMoi = CreateBtn("Làm Mới", Color.FromArgb(108, 117, 125), 0, 150); // Gray

            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += (s, e) => ResetForm();

            pnlActions.Controls.AddRange(new Control[] { btnThem, btnSua, btnXoa, btnLamMoi });
            pnlInput.Controls.Add(pnlActions);


            // --- SEARCH BAR (Bottom of Input) ---
             Panel pnlSearch = new Panel { Location = new Point(20, 260), Size = new Size(970, 50), BackColor = Color.White };
             Label lblSearch = new Label { Text = "Tìm kiếm:", Location = new Point(15, 15), AutoSize = true, Font = new Font("Segoe UI", 10) };
             txbSearch.Location = new Point(100, 12); txbSearch.Width = 400; txbSearch.Font = new Font("Segoe UI", 10);
            
             btnSearch = new Button { 
                Text = "🔍", 
                Location = new Point(510, 10), 
                Size = new Size(50, 28), 
                BackColor = Color.Navy, 
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat 
            };
             btnSearch.FlatAppearance.BorderSize = 0;

             btnSearch.Click += (s, e) => FilterData(txbSearch.Text); 
             txbSearch.TextChanged += (s, e) => FilterData(txbSearch.Text); 
             
             pnlSearch.Controls.AddRange(new Control[] { lblSearch, txbSearch, btnSearch });
             pnlInput.Controls.Add(pnlSearch);


            // 3. GRIDVIEW
            Panel pnlGridContainer = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 0, 20, 20) };
            
            dgvMonHoc = new DataGridView();
            StyleGrid(dgvMonHoc);
            
            dgvMonHoc.DataError += DgvMonHoc_DataError;
            dgvMonHoc.CellClick += DgvMonHoc_CellClick;
            dgvMonHoc.CellFormatting += DgvMonHoc_CellFormatting;

            pnlGridContainer.Controls.Add(dgvMonHoc);

            this.Controls.Add(pnlGridContainer);
            this.Controls.Add(pnlInput);
            this.Controls.Add(pnlHeader);
        }

        // =========================================================================
        // 2. LOGIC TÌM KIẾM & XỬ LÝ
        // =========================================================================

        private void FilterData(string keyword)
        {
            DataTable dt = dgvMonHoc.DataSource as DataTable;
            if (dt != null)
            {
                if (string.IsNullOrEmpty(keyword))
                {
                    dt.DefaultView.RowFilter = ""; // Hiện tất cả
                }
                else
                {
                    // Lọc theo Mã hoặc Tên
                    dt.DefaultView.RowFilter = string.Format("MaKyNang LIKE '%{0}%' OR TenKyNang LIKE '%{0}%'", keyword);
                }
            }
        }

        private void UpdateHocPhi(object sender, EventArgs e)
        {
            decimal tong = numSoBuoi.Value * numDonGia.Value;
            lblTongHocPhi.Text = string.Format("TỔNG HỌC PHÍ: {0:N0} VNĐ", tong);
        }

        void LoadData()
        {
            // [REFACTOR] Dùng KyNangBUS
            dgvMonHoc.DataSource = KyNangBUS.Instance.GetListKyNang();

            if (dgvMonHoc.Columns.Contains("TrangThai") && dgvMonHoc.Columns["TrangThai"] is DataGridViewCheckBoxColumn)
            {
                var oldCol = dgvMonHoc.Columns["TrangThai"];
                var newCol = new DataGridViewTextBoxColumn();
                newCol.Name = oldCol.Name;
                newCol.DataPropertyName = oldCol.DataPropertyName;
                newCol.HeaderText = "Trạng Thái";
                newCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                int idx = oldCol.Index;
                dgvMonHoc.Columns.Remove(oldCol);
                dgvMonHoc.Columns.Insert(idx, newCol);
            }

            if (dgvMonHoc.Columns.Contains("HocPhi"))
            {
                dgvMonHoc.Columns["HocPhi"].DefaultCellStyle.Format = "N0";
                dgvMonHoc.Columns["HocPhi"].HeaderText = "Tổng Học Phí";
                dgvMonHoc.Columns["HocPhi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvMonHoc.Columns["HocPhi"].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            }
            
            // Header Mapping
            if (dgvMonHoc.Columns.Contains("MaKyNang")) { dgvMonHoc.Columns["MaKyNang"].HeaderText = "Mã Môn"; }
            if (dgvMonHoc.Columns.Contains("TenKyNang")) { dgvMonHoc.Columns["TenKyNang"].HeaderText = "Tên Môn"; }
            if (dgvMonHoc.Columns.Contains("HinhThuc")) { dgvMonHoc.Columns["HinhThuc"].HeaderText = "Hình Thức"; dgvMonHoc.Columns["HinhThuc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; }
            if (dgvMonHoc.Columns.Contains("SoBuoi")) { 
                dgvMonHoc.Columns["SoBuoi"].HeaderText = "Số Buổi"; 
                dgvMonHoc.Columns["SoBuoi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; 
            }
            if (dgvMonHoc.Columns.Contains("DonGia")) { 
                dgvMonHoc.Columns["DonGia"].HeaderText = "Đơn Giá"; 
                dgvMonHoc.Columns["DonGia"].DefaultCellStyle.Format = "N0";
                dgvMonHoc.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvMonHoc.Columns.Contains("MoTa")) { dgvMonHoc.Columns["MoTa"].Visible = false; } // Hide description to save space
            if (dgvMonHoc.Columns.Contains("TrangThai")) { 
                dgvMonHoc.Columns["TrangThai"].HeaderText = "Trạng Thái";
                dgvMonHoc.Columns["TrangThai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; 
            }
            if (dgvMonHoc.Columns.Contains("ChuyenNganh")) { dgvMonHoc.Columns["ChuyenNganh"].HeaderText = "Chuyên Ngành"; }
        }

        private void DgvMonHoc_DataError(object sender, DataGridViewDataErrorEventArgs e) { e.ThrowException = false; }

        private void DgvMonHoc_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvMonHoc.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                string val = e.Value.ToString().ToLower();
                bool isActive = (val == "1" || val == "true");
                e.Value = isActive ? "Đang hoạt động" : "Ngưng hoạt động";
                e.CellStyle.ForeColor = isActive ? Color.Green : Color.Red;
            }
        }

        private void DgvMonHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                DataGridViewRow r = dgvMonHoc.Rows[e.RowIndex];

                curMaKN = r.Cells["MaKyNang"].Value.ToString();

                txbMa.Text = curMaKN;
                txbMa.ReadOnly = true;
                txbMa.BackColor = Color.LightYellow;

                txbTen.Text = r.Cells["TenKyNang"].Value.ToString();
                txbMoTa.Text = r.Cells["MoTa"].Value.ToString();
                cbHinhThuc.Text = r.Cells["HinhThuc"].Value.ToString();

                decimal soBuoi = 1;
                if (r.Cells["SoBuoi"].Value != DBNull.Value) soBuoi = Convert.ToDecimal(r.Cells["SoBuoi"].Value);
                if (soBuoi < 1) soBuoi = 1;
                if (soBuoi > numSoBuoi.Maximum) numSoBuoi.Maximum = soBuoi;
                numSoBuoi.Value = soBuoi;

                if (r.Cells["HocPhi"].Value != DBNull.Value)
                {
                    decimal tongHocPhi = Convert.ToDecimal(r.Cells["HocPhi"].Value);
                    decimal donGia = (soBuoi > 0) ? (tongHocPhi / soBuoi) : 0;
                    if (donGia > numDonGia.Maximum) numDonGia.Maximum = donGia * 2;
                    numDonGia.Value = donGia;
                }

                string statusStr = r.Cells["TrangThai"].Value.ToString().ToLower();
                cbTrangThai.SelectedIndex = (statusStr == "1" || statusStr == "true" || statusStr == "đang hoạt động") ? 0 : 1;
            }
            catch { }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbMa.Text)) { MessageBox.Show("Vui lòng nhập Mã Kỹ Năng!"); txbMa.Focus(); return; }
            if (string.IsNullOrWhiteSpace(txbTen.Text)) { MessageBox.Show("Vui lòng nhập Tên Kỹ Năng!"); txbTen.Focus(); return; }

            string status = cbTrangThai.SelectedIndex == 0 ? "1" : "0";

            // [REFACTOR] Dùng KyNangBUS
            if (KyNangBUS.Instance.InsertKyNang(
                txbMa.Text.Trim().ToUpper(),
                txbTen.Text,
                cbHinhThuc.Text,
                txbMoTa.Text,
                (int)numSoBuoi.Value,
                numDonGia.Value,
                status))
            {
                MessageBox.Show("Thêm môn thành công!");
                LoadData(); ResetForm();
            }
            else MessageBox.Show("Lỗi: Mã kỹ năng có thể đã tồn tại!");
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(curMaKN)) { MessageBox.Show("Chọn môn cần sửa!"); return; }
            string status = cbTrangThai.SelectedIndex == 0 ? "1" : "0";

            try
            {
                // [REFACTOR] Dùng KyNangBUS
                if (KyNangBUS.Instance.UpdateKyNang(
                    curMaKN,
                    txbTen.Text,
                    cbHinhThuc.Text,
                    txbMoTa.Text,
                    (int)numSoBuoi.Value,
                    numDonGia.Value,
                    status))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    LoadData();
                }
                else MessageBox.Show("Lỗi cập nhật!");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(curMaKN)) return;
            if (MessageBox.Show(string.Format("Xóa môn {0}?", txbTen.Text), "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                // [REFACTOR] Dùng KyNangBUS
                if (KyNangBUS.Instance.DeleteKyNang(curMaKN)) { MessageBox.Show("Đã xóa."); LoadData(); ResetForm(); }
                else MessageBox.Show("Không thể xóa (đang được sử dụng).");
            }
        }

        private void ResetForm()
        {
            curMaKN = "";
            txbMa.ReadOnly = false; txbMa.BackColor = Color.White; txbMa.Clear();
            txbTen.Clear(); txbMoTa.Clear(); txbSearch.Clear();
            numSoBuoi.Value = 12; numDonGia.Value = 100000;
            cbTrangThai.SelectedIndex = 0; txbMa.Focus();
            FilterData(""); // Reset bộ lọc
        }

        void AddInput(Control parent, string lb, Control c, int x, int y)
        {
            Label l = new Label { 
                Text = lb, 
                Location = new Point(x, y + 3), 
                AutoSize = true, 
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.Black
            };
            c.Location = new Point(x + 110, y); 
            c.Width = 300; 
            c.Font = new Font("Segoe UI", 10);
            parent.Controls.Add(l); parent.Controls.Add(c);
        }

        Button CreateBtn(string t, Color c, int x, int y)
        {
            Button btn = new Button { 
                Text = t, 
                Location = new Point(x, y), 
                Size = new Size(160, 45), // Big buttons
                BackColor = c, 
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat, 
                Font = new Font("Segoe UI", 10, FontStyle.Bold), 
                Cursor = Cursors.Hand 
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void StyleGrid(DataGridView dgv)
        {
            dgv.Dock = DockStyle.Fill; 
            dgv.BackgroundColor = Color.White; 
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect; 
            dgv.ReadOnly = true; 
            dgv.RowHeadersVisible = false;
            dgv.ColumnHeadersHeight = 45; 
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243); 
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold); 
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.BorderStyle = BorderStyle.None;
            dgv.GridColor = Color.WhiteSmoke;
        }
        private void SetPlaceholder(TextBox txt, string holder)
        {
            txt.Text = holder;
            txt.ForeColor = Color.Gray;

            txt.Enter += (s, e) =>
            {
                if (txt.Text == holder)
                {
                    txt.Text = "";
                    txt.ForeColor = Color.Black;
                }
            };

            txt.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    txt.Text = holder;
                    txt.ForeColor = Color.Gray;
                }
            };
        }
    }
}