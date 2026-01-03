using QuanLyTrungTam.BUS;
using QuanLyTrungTam.Utilities;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyTrungTam
{
    public partial class FrmLop : Form
    {
        // 1. KHAI BÁO BIẾN
        private TextBox txbMaLop, txbTenLop, txbSearch;
        private ComboBox cbThu, cbCaHoc;
        private ComboBox cbMonHoc, cbGiaoVien, cbTroGiang, cbPhongHoc, cbTrangThai;
        private NumericUpDown nmSiSo;
        private Button btnSearch;
        private DataGridView dgvMain;

        public FrmLop()
        {
            InitializeComponent();
            SetupCustomUI();
            LoadData();
        }

        // 2. THIẾT KẾ GIAO DIỆN
        private void SetupCustomUI()
        {
            this.Controls.Clear();
            this.BackColor = Color.FromArgb(240, 242, 245); // Soft gray background
            this.Size = new Size(1280, 800);
            this.Padding = new Padding(10); // Window padding

            // --- A. HEADER ---
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = Color.White, Padding = new Padding(20, 0, 20, 0) };
            Label lblTitle = new Label { 
                Text = "QUẢN LÝ LỚP HỌC & XẾP LỊCH", 
                Font = new Font("Segoe UI", 18, FontStyle.Bold), 
                ForeColor = Color.FromArgb(33, 150, 243), // Modern Blue
                AutoSize = true, 
                TextAlign = ContentAlignment.MiddleLeft 
            };
            lblTitle.Location = new Point(20, (pnlHeader.Height - lblTitle.Height) / 2);
            pnlHeader.Controls.Add(lblTitle);

            // --- B. INPUT PANEL ---
            Panel pnlInput = new Panel { Dock = DockStyle.Top, Height = 380, BackColor = Color.Transparent, Padding = new Padding(0, 15, 0, 15) };

            // Initialize controls
            txbMaLop = new TextBox { ReadOnly = true, BackColor = Color.WhiteSmoke, BorderStyle = BorderStyle.FixedSingle };
            txbTenLop = new TextBox { BorderStyle = BorderStyle.FixedSingle };
            
            cbThu = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Standard };
            cbThu.Items.AddRange(new string[] {
                "T2 (Thứ Hai)", "T3 (Thứ Ba)", "T4 (Thứ Tư)", "T5 (Thứ Năm)", "T6 (Thứ Sáu)", "T7 (Thứ Bảy)", "CN (Chủ Nhật)",
                "T2-T4", "T2-T5", "T2-T6", "T3-T5", "T3-T6", "T4-T6", "T5-T7", "T7-CN",
                "T2-T4-T6", "T3-T5-T7", "T2-T3-T4-T5-T6"
            });
            cbThu.SelectedIndex = 0;

            cbCaHoc = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Standard };
            cbCaHoc.Items.AddRange(new string[] {
                "Ca 1 (08:00 - 10:00)", "Ca 2 (10:00 - 12:00)",
                "Ca 3 (13:30 - 15:30)", "Ca 4 (15:30 - 17:30)",
                "Ca Tối 1 (17:45 - 19:15)", "Ca Tối 2 (19:30 - 21:00)"
            });
            cbCaHoc.SelectedIndex = 0;

            txbSearch = new TextBox { BorderStyle = BorderStyle.FixedSingle }; SetPlaceholder(txbSearch, "Tìm kiếm lớp học...");

            cbMonHoc = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Standard };
            cbGiaoVien = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Standard };
            cbTroGiang = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Standard };
            cbPhongHoc = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Standard };
            cbTrangThai = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Standard };
            
            cbTrangThai.Items.AddRange(new string[] { 
                Constants.STATUS_DANG_HOC, 
                Constants.STATUS_DA_KET_THUC, 
                Constants.STATUS_TAM_NGUNG, 
                Constants.STATUS_SAP_MO 
            });
            cbTrangThai.SelectedIndex = 0;

            nmSiSo = new NumericUpDown { Minimum = 1, Maximum = 100, Value = 20, BorderStyle = BorderStyle.FixedSingle };

            // Group 1: Thông Tin Chung
            GroupBox gbGeneral = new GroupBox { 
                Text = "Thông Tin Chung", 
                Font = new Font("Segoe UI", 10, FontStyle.Bold), 
                ForeColor = Color.DimGray,
                Location = new Point(20, 15), 
                Size = new Size(450, 290),
                BackColor = Color.White 
            };
            
            int g1y = 35; int gap = 50;
            AddInput(gbGeneral, "Môn Học:", cbMonHoc, 20, g1y);
            AddInput(gbGeneral, "Mã Lớp:", txbMaLop, 20, g1y + gap);
            AddInput(gbGeneral, "Tên Lớp:", txbTenLop, 20, g1y + gap * 2);
            AddInput(gbGeneral, "Trạng Thái:", cbTrangThai, 20, g1y + gap * 3);
            AddInput(gbGeneral, "Sĩ Số:", nmSiSo, 20, g1y + gap * 4);

            // Group 2: Thời Gian & Địa Điểm
            GroupBox gbTime = new GroupBox { 
                Text = "Thời Gian & Địa Điểm", 
                Font = new Font("Segoe UI", 10, FontStyle.Bold), 
                ForeColor = Color.DimGray,
                Location = new Point(480, 15), 
                Size = new Size(530, 290),
                BackColor = Color.White 
            };

            int g2y = 35;
            AddInput(gbTime, "Lịch Học (Thứ):", cbThu, 20, g2y);
            AddInput(gbTime, "Ca Học (Giờ):", cbCaHoc, 20, g2y + gap);
            AddInput(gbTime, "Phòng Học:", cbPhongHoc, 20, g2y + gap * 2);
            AddInput(gbTime, "Giáo Viên:", cbGiaoVien, 20, g2y + gap * 3);
            AddInput(gbTime, "Trợ Giảng:", cbTroGiang, 20, g2y + gap * 4);

            pnlInput.Controls.Add(gbGeneral);
            pnlInput.Controls.Add(gbTime);

            // Button Action Panel - Move to right side
            Panel pnlActions = new Panel { 
                Location = new Point(1030, 25), 
                Size = new Size(160, 280), // Aligned with group boxes
                BackColor = Color.Transparent 
            };
            
            Button btnThem = CreateBtn("Mở Lớp Mới", Color.FromArgb(40, 167, 69), 0, 0); // Green
            Button btnSua = CreateBtn("Cập Nhật", Color.FromArgb(0, 123, 255), 0, 60); // Blue
            Button btnXoa = CreateBtn("Xóa Lớp", Color.FromArgb(220, 53, 69), 0, 120); // Red
            Button btnLamMoi = CreateBtn("Làm Mới", Color.FromArgb(108, 117, 125), 0, 180); // Gray

            btnThem.Click += BtnAdd_Click;
            btnSua.Click += BtnEdit_Click;
            btnXoa.Click += BtnDel_Click;
            btnLamMoi.Click += (s, e) => ResetForm();

            pnlActions.Controls.AddRange(new Control[] { btnThem, btnSua, btnXoa, btnLamMoi });
            pnlInput.Controls.Add(pnlActions);


            // Search bar at bottom of input
            Panel pnlSearch = new Panel { Location = new Point(20, 320), Size = new Size(1070, 40), BackColor = Color.White };
            Label lblSearch = new Label { Text = "Tìm kiếm nhanh:", Location = new Point(10, 10), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Regular) };
            txbSearch.Location = new Point(130, 8); txbSearch.Width = 400; txbSearch.Font = new Font("Segoe UI", 10);
            
            btnSearch = new Button { 
                Text = "🔍", 
                Location = new Point(540, 6), 
                Size = new Size(40, 26), 
                BackColor = Color.Orange, 
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat, 
                Cursor = Cursors.Hand 
            };
            btnSearch.FlatAppearance.BorderSize = 0;

            btnSearch.Click += (s, e) => FilterData(txbSearch.Text);
            txbSearch.TextChanged += (s, e) => FilterData(txbSearch.Text);
            
            pnlSearch.Controls.Add(lblSearch); pnlSearch.Controls.Add(txbSearch); pnlSearch.Controls.Add(btnSearch);
            pnlInput.Controls.Add(pnlSearch);


            // --- C. GRIDVIEW ---
            Panel pnlGridContainer = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 10, 20, 20) };
            dgvMain = new DataGridView();
            StyleGrid(dgvMain);
            dgvMain.CellClick += DgvMain_CellClick;
            dgvMain.CellFormatting += DgvMain_CellFormatting;
            dgvMain.DataError += (s, e) => { e.ThrowException = false; };
            
            pnlGridContainer.Controls.Add(dgvMain);
            dgvMain.BringToFront();

            this.Controls.Add(pnlGridContainer);
            this.Controls.Add(pnlInput);
            this.Controls.Add(pnlHeader);
        }

        // 3. LOGIC XỬ LÝ DỮ LIỆU (LOAD DATA & CALCULATE)
        void LoadData()
        {
            try
            {
                // Load Môn Học
                DataTable dtMon = KyNangBUS.Instance.GetListKyNangActive();

                if (dtMon != null && dtMon.Rows.Count > 0)
                {
                    cbMonHoc.DataSource = dtMon;
                    cbMonHoc.DisplayMember = "TenKyNang";
                    cbMonHoc.ValueMember = "MaKyNang";

                    cbMonHoc.SelectedIndexChanged -= CbMonHoc_SelectedIndexChanged;
                    cbMonHoc.SelectedIndexChanged += CbMonHoc_SelectedIndexChanged;
                    cbMonHoc.SelectedIndex = 0;
                }

                // Load Phòng Học
                cbPhongHoc.DataSource = PhongHocBUS.Instance.GetListPhong();
                cbPhongHoc.DisplayMember = "TenPhong"; cbPhongHoc.ValueMember = "MaPhong";

                // 1. Load Giáo viên (Mặc định ban đầu load hết)
                cbGiaoVien.DataSource = NhanVienBUS.Instance.GetListGiaoVien();
                cbGiaoVien.DisplayMember = "HoTen"; cbGiaoVien.ValueMember = "MaNS";

                // 2. Load Trợ giảng
                cbTroGiang.DataSource = NhanVienBUS.Instance.GetListTroGiang();
                cbTroGiang.DisplayMember = "HoTen"; cbTroGiang.ValueMember = "MaNS";

                // Load Danh sách Lớp
                LoadListLopHoc();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.MSG_LOAD_DATA_ERROR + ex.Message);
            }
        }

        private void LoadListLopHoc()
        {
            DataTable dataLop = LopHocBUS.Instance.GetAllLop();
            if (!dataLop.Columns.Contains("NgayKetThuc"))
                dataLop.Columns.Add("NgayKetThuc", typeof(DateTime));

            foreach (DataRow row in dataLop.Rows)
            {
                try
                {
                    DateTime start = Convert.ToDateTime(row["NgayBatDau"]);
                    string thu = row["Thu"].ToString();
                    int soBuoi = row["SoBuoi"] != DBNull.Value ? Convert.ToInt32(row["SoBuoi"]) : 0;
                    row["NgayKetThuc"] = LopHocBUS.Instance.CalculateEndDate(start, thu, soBuoi);
                }
                catch { row["NgayKetThuc"] = row["NgayBatDau"]; }
            }

            dgvMain.DataSource = dataLop;
            string[] hide = { "MaKyNang", "MaGiaoVien", "MaTroGiang", "MaPhong", "SoBuoi" };
            foreach (string c in hide) if (dgvMain.Columns.Contains(c)) dgvMain.Columns[c].Visible = false;

            // Header mapping and alignment
            if (dgvMain.Columns.Contains("MaLop")) { dgvMain.Columns["MaLop"].HeaderText = "Mã Lớp"; dgvMain.Columns["MaLop"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; }
            if (dgvMain.Columns.Contains("TenLop")) { dgvMain.Columns["TenLop"].HeaderText = "Tên Lớp"; }
            if (dgvMain.Columns.Contains("TenKyNang")) { dgvMain.Columns["TenKyNang"].HeaderText = "Môn Học"; }
            if (dgvMain.Columns.Contains("Thu")) { dgvMain.Columns["Thu"].HeaderText = "Lịch Học"; dgvMain.Columns["Thu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; }
            if (dgvMain.Columns.Contains("CaHoc")) { dgvMain.Columns["CaHoc"].HeaderText = "Ca Học"; dgvMain.Columns["CaHoc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; }
            if (dgvMain.Columns.Contains("SiSoToiDa")) { dgvMain.Columns["SiSoToiDa"].HeaderText = "Sĩ Số"; dgvMain.Columns["SiSoToiDa"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; }
            if (dgvMain.Columns.Contains("TrangThai")) { dgvMain.Columns["TrangThai"].HeaderText = "Trạng Thái"; dgvMain.Columns["TrangThai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; }
            if (dgvMain.Columns.Contains("TenPhong")) { dgvMain.Columns["TenPhong"].HeaderText = "Phòng Học"; }
            if (dgvMain.Columns.Contains("TenGV")) { dgvMain.Columns["TenGV"].HeaderText = "Giáo Viên"; }
            if (dgvMain.Columns.Contains("TenTG")) { dgvMain.Columns["TenTG"].HeaderText = "Trợ Giảng"; }

            if (dgvMain.Columns.Contains("NgayBatDau")) { 
                dgvMain.Columns["NgayBatDau"].HeaderText = "Ngày Bắt Đầu"; 
                dgvMain.Columns["NgayBatDau"].DefaultCellStyle.Format = Constants.DATE_FORMAT_DISPLAY;
                dgvMain.Columns["NgayBatDau"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvMain.Columns.Contains("NgayKetThuc")) { 
                dgvMain.Columns["NgayKetThuc"].HeaderText = "Ngày Kết Thúc"; 
                dgvMain.Columns["NgayKetThuc"].DefaultCellStyle.Format = Constants.DATE_FORMAT_DISPLAY;
                dgvMain.Columns["NgayKetThuc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        // --- SỰ KIỆN CHỌN MÔN HỌC ---
        private void CbMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Reset lại ComboBox GV
                cbGiaoVien.DataSource = null;

                if (cbMonHoc.SelectedValue == null) return;

                DataRowView rowMon = cbMonHoc.SelectedItem as DataRowView;
                string maMon = "";
                string chuyenNganhMon = "";

                if (rowMon != null)
                {
                    maMon = rowMon["MaKyNang"].ToString();
                    if (rowMon.DataView.Table.Columns.Contains("ChuyenNganh") && rowMon["ChuyenNganh"] != DBNull.Value)
                    {
                        chuyenNganhMon = rowMon["ChuyenNganh"].ToString().Trim();
                    }
                }
                else
                {
                    maMon = cbMonHoc.SelectedValue.ToString();
                }

                // Tạo Mã Lớp
                txbMaLop.Text = LopHocBUS.Instance.GetNewMaLop(maMon);

                // LỌC GIÁO VIÊN QUA BUS
                cbGiaoVien.DataSource = NhanVienBUS.Instance.GetListGiaoVien(chuyenNganhMon);
                cbGiaoVien.DisplayMember = "HoTen";
                cbGiaoVien.ValueMember = "MaNS";

                if (cbGiaoVien.Items.Count > 0) cbGiaoVien.SelectedIndex = 0;
            }
            catch (Exception)
            {
                // Silent catch or simple log
            }
        }

        // 4. LOGIC SỰ KIỆN (BUTTONS & GRID)
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (cbMonHoc.SelectedValue == null || string.IsNullOrEmpty(cbMonHoc.Text))
            {
                MessageBox.Show("Vui lòng chọn Môn Học trước!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txbTenLop.Text)) { MessageBox.Show("Chưa nhập tên lớp!", "Thông báo"); return; }

            string maGVCheck = GetVal(cbGiaoVien);
            string maTGCheck = GetVal(cbTroGiang);
            string maPhong = GetVal(cbPhongHoc);
            string lichHoc = cbThu.Text;
            string caHoc = cbCaHoc.Text;

            string conflictMsg = LopHocBUS.Instance.GetConflictMessage(maPhong, maGVCheck, maTGCheck, lichHoc, caHoc, "");

            if (conflictMsg != null)
            {
                MessageBox.Show(conflictMsg, "Cảnh báo trùng lịch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool result = LopHocBUS.Instance.InsertLopFull(txbMaLop.Text, txbTenLop.Text,
                GetVal(cbMonHoc), maGVCheck, maTGCheck, maPhong,
                lichHoc, caHoc, (int)nmSiSo.Value, DateTime.Now);

            if (result)
            {
                MessageBox.Show(Constants.MSG_ADD_SUCCESS, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListLopHoc();
                ResetForm();
            }
            else MessageBox.Show("Thêm thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txbMaLop.Text)) return;

            string conflictMsg = LopHocBUS.Instance.GetConflictMessage(GetVal(cbPhongHoc), GetVal(cbGiaoVien), GetVal(cbTroGiang), cbThu.Text, cbCaHoc.Text, txbMaLop.Text);
            if (conflictMsg != null) { MessageBox.Show(conflictMsg, "Cảnh báo trùng lịch", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            bool result = LopHocBUS.Instance.UpdateLopFull(txbMaLop.Text, txbTenLop.Text,
                GetVal(cbGiaoVien), GetVal(cbTroGiang), GetVal(cbPhongHoc),
                cbThu.Text, cbCaHoc.Text, (int)nmSiSo.Value, cbTrangThai.Text);

            if (result) { MessageBox.Show(Constants.MSG_UPDATE_SUCCESS); LoadListLopHoc(); }
            else MessageBox.Show("Lỗi cập nhật!");
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txbMaLop.Text)) return;
            if (MessageBox.Show(Constants.MSG_CONFIRM_DELETE, "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (LopHocBUS.Instance.DeleteLop(txbMaLop.Text)) { MessageBox.Show(Constants.MSG_DELETE_SUCCESS); LoadListLopHoc(); ResetForm(); }
                else MessageBox.Show("Lỗi xóa lớp.");
            }
        }

        private void FilterData(string keyword)
        {
            if (dgvMain.DataSource == null) return;
            DataTable dt = dgvMain.DataSource as DataTable;
            if (string.IsNullOrWhiteSpace(keyword) || keyword == "Tìm kiếm...") dt.DefaultView.RowFilter = "";
            else dt.DefaultView.RowFilter = string.Format("MaLop LIKE '%{0}%' OR TenLop LIKE '%{0}%'", keyword);
        }

        private void DgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                DataGridViewRow r = dgvMain.Rows[e.RowIndex];
                txbMaLop.Text = r.Cells["MaLop"].Value != null ? r.Cells["MaLop"].Value.ToString() : "";
                txbTenLop.Text = r.Cells["TenLop"].Value != null ? r.Cells["TenLop"].Value.ToString() : "";
                SetCombo(cbMonHoc, r.Cells["MaKyNang"].Value);
                SetCombo(cbGiaoVien, r.Cells["MaGiaoVien"].Value);
                SetCombo(cbTroGiang, r.Cells["MaTroGiang"].Value);
                SetCombo(cbPhongHoc, r.Cells["MaPhong"].Value);

                string thu = r.Cells["Thu"].Value != null ? r.Cells["Thu"].Value.ToString() : "";
                if (cbThu.Items.Contains(thu)) cbThu.SelectedItem = thu; else cbThu.Text = thu;

                string ca = r.Cells["CaHoc"].Value != null ? r.Cells["CaHoc"].Value.ToString() : "";
                if (cbCaHoc.Items.Contains(ca)) cbCaHoc.SelectedItem = ca; else cbCaHoc.Text = ca;

                nmSiSo.Value = r.Cells["SiSoToiDa"].Value != DBNull.Value ? Convert.ToInt32(r.Cells["SiSoToiDa"].Value) : 20;
                cbTrangThai.Text = r.Cells["TrangThai"].Value != null ? r.Cells["TrangThai"].Value.ToString() : "";
            }
            catch { }
        }

        private void ResetForm()
        {
            txbMaLop.Clear(); txbTenLop.Clear(); cbThu.SelectedIndex = 0; cbCaHoc.SelectedIndex = 0;
            if (cbMonHoc.Items.Count > 0) cbMonHoc.SelectedIndex = 0;
            SetPlaceholder(txbSearch, "Tìm kiếm..."); FilterData("");
        }

        private string GetVal(ComboBox cb) 
        { 
            return cb.SelectedValue != null ? cb.SelectedValue.ToString() : ""; 
        }

        private void SetCombo(ComboBox cb, object val) 
        { 
            if (val != DBNull.Value && val != null) cb.SelectedValue = val; 
        }

        private void DgvMain_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvMain.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                string s = e.Value.ToString();
                e.CellStyle.ForeColor = s.Contains("Đang") ? Color.Green : (s.Contains("kết thúc") ? Color.Red : Color.Blue);
                e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            }
        }

        private void AddInput(Control parent, string lb, Control c, int x, int y)
        {
            Label l = new Label { 
                Text = lb, 
                Location = new Point(x, y + 3), 
                AutoSize = true, 
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.Black
            };
            c.Location = new Point(x + 110, y); 
            c.Width = 300; // Wider inputs
            c.Font = new Font("Segoe UI", 10);
            parent.Controls.Add(l); parent.Controls.Add(c);
        }

        private Button CreateBtn(string t, Color c, int x, int y) 
        { 
            Button btn = new Button { 
                Text = t, 
                Location = new Point(x, y), 
                Size = new Size(160, 45), // Bigger buttons
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
            dgv.ColumnHeadersHeight = 45; // Taller header
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243); // Modern Blue Header
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold); // Bold Header
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10); // Readable content
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254); // Soft selection
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.BorderStyle = BorderStyle.None;
            dgv.GridColor = Color.WhiteSmoke;
        }

        private void SetPlaceholder(TextBox txt, string holder)
        {
            txt.Text = holder; txt.ForeColor = Color.Gray;
            txt.Enter += (s, e) => { if (txt.Text == holder) { txt.Text = ""; txt.ForeColor = Color.Black; } };
            txt.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txt.Text)) { txt.Text = holder; txt.ForeColor = Color.Gray; } };
        }
    }
}