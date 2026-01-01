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
            this.BackColor = Color.White;

            // 1. HEADER
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.FromArgb(0, 150, 136) };
            Label lblTitle = new Label { Text = "QUẢN LÝ MÔN HỌC & KỸ NĂNG", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(20, 15) };
            pnlHeader.Controls.Add(lblTitle);

            // 2. INPUT PANEL
            Panel pnlInput = new Panel { Dock = DockStyle.Top, Height = 260, BackColor = Color.WhiteSmoke };

            txbMa = new TextBox { ReadOnly = false, BackColor = Color.LightYellow };
            txbTen = new TextBox(); txbMoTa = new TextBox();
            txbSearch = new TextBox();
            SetPlaceholder(txbSearch, "Nhập tên môn cần tìm..."); 

            cbHinhThuc = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cbHinhThuc.Items.AddRange(new string[] { "Offline", "Online", "Video Record" });
            cbHinhThuc.SelectedIndex = 0;

            cbTrangThai = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cbTrangThai.Items.AddRange(new string[] { "Đang hoạt động", "Ngưng hoạt động" });
            cbTrangThai.SelectedIndex = 0;

            numSoBuoi = new NumericUpDown { Minimum = 1, Maximum = 1000, Value = 12 };
            numDonGia = new NumericUpDown { Minimum = 0, Maximum = 1000000000, Increment = 50000, Value = 100000 };

            lblTongHocPhi = new Label { Text = "TỔNG HỌC PHÍ: 1,200,000 VNĐ", Location = new Point(400, 110), AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.Red };

            numSoBuoi.ValueChanged += UpdateHocPhi;
            numDonGia.ValueChanged += UpdateHocPhi;

            // --- LAYOUT ---
            // Cột 1
            AddInput(pnlInput, "Mã Kỹ Năng:", txbMa, 30, 20);
            AddInput(pnlInput, "Tên Kỹ Năng:", txbTen, 30, 60);
            AddInput(pnlInput, "Hình Thức:", cbHinhThuc, 30, 100);

            // Cột 2
            AddInput(pnlInput, "Số Buổi:", numSoBuoi, 400, 20);
            AddInput(pnlInput, "Đơn Giá/Buổi:", numDonGia, 400, 60);
            pnlInput.Controls.Add(lblTongHocPhi);

            // Dòng Mô tả
            AddInput(pnlInput, "Mô Tả:", txbMoTa, 30, 140);
            txbMoTa.Width = 600;

            // [MỚI] Dòng Tìm Kiếm (Nằm dưới Mô Tả)
            AddInput(pnlInput, "Tìm Kiếm:", txbSearch, 30, 190);
            txbSearch.Width = 300; 

            // [MỚI] Nút Kính Lúp (Bên cạnh ô tìm kiếm)
            btnSearch = new Button { Text = "🔍", Location = new Point(440, 187), Size = new Size(50, 30), BackColor = Color.Orange, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Cursor = Cursors.Hand };
            btnSearch.Click += (s, e) => FilterData(txbSearch.Text); 
            txbSearch.TextChanged += (s, e) => FilterData(txbSearch.Text); 
            pnlInput.Controls.Add(btnSearch);

            // Cột 3 (Trạng Thái & Nút chức năng)
            AddInput(pnlInput, "Trạng Thái:", cbTrangThai, 770, 20);

            Button btnThem = CreateBtn("Thêm Mới", Color.LightGreen, 770, 70); btnThem.ForeColor = Color.Black;
            Button btnSua = CreateBtn("Cập Nhật", Color.LightBlue, 770, 120); btnSua.ForeColor = Color.Black;
            Button btnXoa = CreateBtn("Xóa Môn", Color.LightPink, 910, 70); btnXoa.ForeColor = Color.Black;
            Button btnLamMoi = CreateBtn("Làm Mới", Color.Gainsboro, 910, 120); btnLamMoi.ForeColor = Color.Black;

            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += (s, e) => ResetForm();

            pnlInput.Controls.AddRange(new Control[] { btnThem, btnSua, btnXoa, btnLamMoi });

            // 3. GRIDVIEW
            Panel pnlGridContainer = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };
            GroupBox grpGrid = new GroupBox { Text = " Danh sách môn học hiện có ", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

            dgvMonHoc = new DataGridView();
            StyleGrid(dgvMonHoc);

            dgvMonHoc.DataError += DgvMonHoc_DataError;
            dgvMonHoc.CellClick += DgvMonHoc_CellClick;
            dgvMonHoc.CellFormatting += DgvMonHoc_CellFormatting;

            grpGrid.Controls.Add(dgvMonHoc);
            pnlGridContainer.Controls.Add(grpGrid);

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
            }
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

        void AddInput(Panel p, string lb, Control c, int x, int y)
        {
            Label l = new Label { Text = lb, Location = new Point(x, y), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            c.Location = new Point(x + 100, y - 3); c.Width = 230; c.Font = new Font("Segoe UI", 10);
            p.Controls.Add(l); p.Controls.Add(c);
        }

        Button CreateBtn(string t, Color c, int x, int y)
        {
            return new Button { Text = t, Location = new Point(x, y), Size = new Size(130, 38), BackColor = c, ForeColor = Color.Black, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9, FontStyle.Bold), Cursor = Cursors.Hand };
        }

        private void StyleGrid(DataGridView dgv)
        {
            dgv.Dock = DockStyle.Fill; dgv.BackgroundColor = Color.White; dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dgv.ReadOnly = true; dgv.RowHeadersVisible = false;
            dgv.ColumnHeadersHeight = 35; dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 150, 136); dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold); dgv.DefaultCellStyle.ForeColor = Color.Black;
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