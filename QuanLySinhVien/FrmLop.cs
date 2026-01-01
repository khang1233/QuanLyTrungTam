using QuanLyTrungTam.BUS;
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
            this.BackColor = Color.White;
            this.Size = new Size(1250, 780);

            // --- A. HEADER ---
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.FromArgb(0, 150, 136) };
            Label lblTitle = new Label { Text = "QUẢN LÝ LỚP HỌC & XẾP LỊCH", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(20, 15) };
            pnlHeader.Controls.Add(lblTitle);

            // --- B. INPUT PANEL ---
            Panel pnlInput = new Panel { Dock = DockStyle.Top, Height = 360, BackColor = Color.WhiteSmoke };

            txbMaLop = new TextBox { ReadOnly = true, BackColor = Color.LightYellow };
            txbTenLop = new TextBox();

            cbThu = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cbThu.Items.AddRange(new string[] {
                "T2 (Thứ Hai)", "T3 (Thứ Ba)", "T4 (Thứ Tư)", "T5 (Thứ Năm)", "T6 (Thứ Sáu)", "T7 (Thứ Bảy)", "CN (Chủ Nhật)",
                "T2-T4", "T2-T5", "T2-T6", "T3-T5", "T3-T6", "T4-T6", "T5-T7", "T7-CN",
                "T2-T4-T6", "T3-T5-T7", "T2-T3-T4-T5-T6"
            });
            cbThu.SelectedIndex = 0;

            cbCaHoc = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cbCaHoc.Items.AddRange(new string[] {
                "Ca 1 (08:00 - 10:00)", "Ca 2 (10:00 - 12:00)",
                "Ca 3 (13:30 - 15:30)", "Ca 4 (15:30 - 17:30)",
                "Ca Tối 1 (17:45 - 19:15)", "Ca Tối 2 (19:30 - 21:00)"
            });
            cbCaHoc.SelectedIndex = 0;

            txbSearch = new TextBox(); SetPlaceholder(txbSearch, "Tìm kiếm...");

            cbMonHoc = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cbGiaoVien = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cbTroGiang = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cbPhongHoc = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };

            cbTrangThai = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cbTrangThai.Items.AddRange(new string[] { "Đang học", "Đã kết thúc", "Tạm ngưng", "Sắp mở" });
            cbTrangThai.SelectedIndex = 0;

            nmSiSo = new NumericUpDown { Minimum = 1, Maximum = 100, Value = 20 };

            // Bố cục
            int col1 = 30; int col2 = 450; int col3 = 850; int rowHeight = 50;

            AddInput(pnlInput, "Môn Học:", cbMonHoc, col1, 20);
            AddInput(pnlInput, "Mã Lớp (Auto):", txbMaLop, col1, 20 + rowHeight);
            AddInput(pnlInput, "Tên Lớp:", txbTenLop, col1, 20 + rowHeight * 2);
            AddInput(pnlInput, "Phòng Học:", cbPhongHoc, col1, 20 + rowHeight * 3);

            AddInput(pnlInput, "Lịch Học (Thứ):", cbThu, col2, 20);
            AddInput(pnlInput, "Ca Học (Giờ):", cbCaHoc, col2, 20 + rowHeight);
            AddInput(pnlInput, "Giáo Viên:", cbGiaoVien, col2, 20 + rowHeight * 2);
            AddInput(pnlInput, "Trợ Giảng:", cbTroGiang, col2, 20 + rowHeight * 3);

            AddInput(pnlInput, "Sĩ Số Tối Đa:", nmSiSo, col3, 20);
            AddInput(pnlInput, "Trạng Thái:", cbTrangThai, col3, 20 + rowHeight);

            AddInput(pnlInput, "Tìm nhanh:", txbSearch, col1, 260);
            txbSearch.Width = 350;
            btnSearch = new Button { Text = "🔍", Location = new Point(550, 257), Size = new Size(50, 30), BackColor = Color.Orange, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Cursor = Cursors.Hand };
            btnSearch.Click += (s, e) => FilterData(txbSearch.Text);
            txbSearch.TextChanged += (s, e) => FilterData(txbSearch.Text);
            pnlInput.Controls.Add(btnSearch);

            // Buttons
            Button btnThem = CreateBtn("Mở Lớp Mới", Color.Teal, col3, 140);
            Button btnSua = CreateBtn("Cập Nhật", Color.DodgerBlue, col3, 190);
            Button btnXoa = CreateBtn("Xóa Lớp", Color.IndianRed, col3 + 140, 140);
            Button btnLamMoi = CreateBtn("Làm Mới", Color.Gray, col3 + 140, 190);

            btnThem.Click += BtnAdd_Click;
            btnSua.Click += BtnEdit_Click;
            btnXoa.Click += BtnDel_Click;
            btnLamMoi.Click += (s, e) => ResetForm();

            pnlInput.Controls.AddRange(new Control[] { btnThem, btnSua, btnXoa, btnLamMoi });

            // --- C. GRIDVIEW ---
            Panel pnlGridContainer = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0), BackColor = Color.WhiteSmoke };
            Panel pnlHeaderGrid = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.FromArgb(0, 150, 136) };
            Label lblTitleGrid = new Label { Text = "DANH SÁCH LỚP HỌC", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(15, 10) };
            pnlHeaderGrid.Controls.Add(lblTitleGrid);

            dgvMain = new DataGridView();
            StyleGrid(dgvMain);
            dgvMain.CellClick += DgvMain_CellClick;
            dgvMain.CellFormatting += DgvMain_CellFormatting;
            dgvMain.DataError += (s, e) => { e.ThrowException = false; };

            pnlGridContainer.Controls.Add(dgvMain);
            pnlGridContainer.Controls.Add(pnlHeaderGrid);
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
                // [REFACTOR] Dùng KyNangBUS
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
                else
                {
                    cbMonHoc.DataSource = null; // Clear nếu không có môn nào
                }

                // [REFACTOR] Dùng PhongHocBUS
                cbPhongHoc.DataSource = PhongHocBUS.Instance.GetListPhong();
                cbPhongHoc.DisplayMember = "TenPhong"; cbPhongHoc.ValueMember = "MaPhong";

                // [REFACTOR] Dùng NhanVienBUS
                dtNS = NhanVienBUS.Instance.GetListNhanVien();

                // 1. GIÁO VIÊN
                DataView dvGV = new DataView(dtNS);
                dvGV.RowFilter = "LoaiNS LIKE '%Giáo%' OR LoaiNS LIKE '%Giao%'";
                cbGiaoVien.DataSource = dvGV;
                cbGiaoVien.DisplayMember = "HoTen"; cbGiaoVien.ValueMember = "MaNS";

                // 2. TRỢ GIẢNG
                DataView dvTG = new DataView(dtNS);
                dvTG.RowFilter = "LoaiNS LIKE '%Trợ%' OR LoaiNS LIKE '%Tro%'";
                if (dvTG.Count > 0)
                {
                    cbTroGiang.DataSource = dvTG;
                    cbTroGiang.DisplayMember = "HoTen"; cbTroGiang.ValueMember = "MaNS";
                }

                // Load Danh sách Lớp
                LoadListLopHoc();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void LoadListLopHoc()
        {
            // [REFACTOR] Dùng LopHocBUS
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

                    // [REFACTOR] Logic tính ngày chuyển sang BUS
                    row["NgayKetThuc"] = LopHocBUS.Instance.CalculateEndDate(start, thu, soBuoi);
                }
                catch { row["NgayKetThuc"] = row["NgayBatDau"]; }
            }

            dgvMain.DataSource = dataLop;
            string[] hide = { "MaKyNang", "MaGiaoVien", "MaTroGiang", "MaPhong", "SoBuoi" };
            foreach (string c in hide) if (dgvMain.Columns.Contains(c)) dgvMain.Columns[c].Visible = false;

            if (dgvMain.Columns.Contains("NgayBatDau")) dgvMain.Columns["NgayBatDau"].DefaultCellStyle.Format = "dd/MM/yyyy";
            if (dgvMain.Columns.Contains("NgayKetThuc")) dgvMain.Columns["NgayKetThuc"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        // [NEW] Lưu danh sách nhân sự để lọc động
        private DataTable dtNS;

        private void CbMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbMonHoc.SelectedValue != null)
                {
                    DataRowView rowMon = cbMonHoc.SelectedItem as DataRowView;
                    string maMon = "";
                    string chuyenNganhMon = "";

                    if (rowMon != null)
                    {
                        maMon = rowMon["MaKyNang"].ToString();
                        // Kiểm tra cột và giá trị DBNull
                        if (rowMon.DataView.Table.Columns.Contains("ChuyenNganh") && rowMon["ChuyenNganh"] != DBNull.Value)
                        {
                            chuyenNganhMon = rowMon["ChuyenNganh"].ToString().Trim();
                        }
                    }
                    else
                    {
                        maMon = cbMonHoc.SelectedValue.ToString();
                    }

                    // [REFACTOR] Dùng LopHocBUS lấy mã lớp mới
                    txbMaLop.Text = LopHocBUS.Instance.GetNewMaLop(maMon);

                    // [NEW FEATURE] Lọc Giáo viên theo Chuyên Ngành
                    if (dtNS != null)
                    {
                        DataView dvGV = new DataView(dtNS);

                        // Chỉ lấy Giáo viên
                        string filter = "LoaiNS LIKE '%Giáo%' OR LoaiNS LIKE '%Giao%'";

                        // Nếu môn học có chuyên ngành -> Lọc thêm chuyên ngành
                        if (!string.IsNullOrEmpty(chuyenNganhMon))
                        {
                            // Escape dấu '
                            string safeCN = chuyenNganhMon.Replace("'", "''");

                            // Sử dụng LIKE để mềm dẻo hơn (ví dụ: "Tiếng Anh" match "Tiếng Anh giao tiếp")
                            // Hoặc dùng = nếu muốn chính xác. Ở đây dùng LIKE cho an toàn.
                            filter += $" AND (ChuyenNganh = '{safeCN}' OR ChuyenNganh LIKE '%{safeCN}%')";
                        }

                        dvGV.RowFilter = filter;
                        cbGiaoVien.DataSource = dvGV;
                        cbGiaoVien.DisplayMember = "HoTen"; cbGiaoVien.ValueMember = "MaNS";

                        // Debug để tìm nguyên nhân lỗi (Sẽ xóa sau khi OK)
                        // MessageBox.Show($"Debug: Môn học '{cbMonHoc.Text}'\nChuyên ngành tìm được: '{chuyenNganhMon}'\nSố GV tìm thấy: {dvGV.Count}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lọc giáo viên: " + ex.Message);
            }
        }

        // 4. LOGIC SỰ KIỆN (BUTTONS & GRID)
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // Kiểm tra chọn môn học
            if (cbMonHoc.SelectedValue == null || string.IsNullOrEmpty(cbMonHoc.Text))
            {
                MessageBox.Show("Vui lòng chọn Môn Học trước!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txbTenLop.Text)) { MessageBox.Show("Chưa nhập tên lớp!", "Thông báo"); return; }

            // [FIX LỖI 2]: KIỂM TRA CHUYÊN NGÀNH GIÁO VIÊN
            if (cbGiaoVien.SelectedValue != null)
            {
                DataRowView rowMon = cbMonHoc.SelectedItem as DataRowView;
                string chuyenNganhMon = "";
                if (rowMon != null && rowMon.DataView.Table.Columns.Contains("ChuyenNganh"))
                {
                    chuyenNganhMon = rowMon["ChuyenNganh"].ToString();
                }

                string maGV = GetVal(cbGiaoVien);
                // [REFACTOR] Dùng NhanVienBUS
                string chuyenNganhGV = NhanVienBUS.Instance.GetChuyenNganh(maGV);

                if (!string.IsNullOrEmpty(chuyenNganhMon) && !string.IsNullOrEmpty(chuyenNganhGV))
                {
                    // So sánh không phân biệt hoa thường
                    if (!chuyenNganhMon.Equals(chuyenNganhGV, StringComparison.OrdinalIgnoreCase))
                    {
                        DialogResult confirm = MessageBox.Show(
                            $"Cảnh báo chuyên môn:\nGiáo viên này có chuyên ngành '{chuyenNganhGV}', khác với môn học '{chuyenNganhMon}'.\n\nBạn có chắc chắn muốn tiếp tục mở lớp không?",
                            "Cảnh báo lệch chuyên ngành",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning
                        );

                        if (confirm == DialogResult.No) return; // Hủy bỏ thao tác
                    }
                }
            }

            // --- KIỂM TRA LOGIC TRÙNG LỊCH (Dùng BUS Logic) ---
            string caHoc = cbCaHoc.Text;
            string lichHoc = cbThu.Text;
            string maPhong = GetVal(cbPhongHoc);
            string maGVCheck = GetVal(cbGiaoVien);
            string maTGCheck = GetVal(cbTroGiang);

            // [REFACTOR] Move logic check sang BUS
            string conflictMsg = LopHocBUS.Instance.GetConflictMessage(maPhong, maGVCheck, maTGCheck, lichHoc, caHoc, "");

            if (conflictMsg != null)
            {
                MessageBox.Show(conflictMsg, "Cảnh báo trùng lịch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Insert
            // [REFACTOR] Dùng LopHocBUS
            bool result = LopHocBUS.Instance.InsertLopFull(txbMaLop.Text, txbTenLop.Text,
                GetVal(cbMonHoc), maGVCheck, maTGCheck, maPhong,
                lichHoc, caHoc, (int)nmSiSo.Value, DateTime.Now);

            if (result)
            {
                MessageBox.Show("Mở lớp mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListLopHoc();
                ResetForm();
            }
            else MessageBox.Show("Thêm thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txbMaLop.Text)) return;

            // [REFACTOR] Dùng LopHocBUS
            string conflictMsg = LopHocBUS.Instance.GetConflictMessage(GetVal(cbPhongHoc), GetVal(cbGiaoVien), GetVal(cbTroGiang), cbThu.Text, cbCaHoc.Text, txbMaLop.Text);
            if (conflictMsg != null) { MessageBox.Show(conflictMsg, "Cảnh báo trùng lịch", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            bool result = LopHocBUS.Instance.UpdateLopFull(txbMaLop.Text, txbTenLop.Text,
                GetVal(cbGiaoVien), GetVal(cbTroGiang), GetVal(cbPhongHoc),
                cbThu.Text, cbCaHoc.Text, (int)nmSiSo.Value, cbTrangThai.Text);

            if (result) { MessageBox.Show("Cập nhật thành công!"); LoadListLopHoc(); }
            else MessageBox.Show("Lỗi cập nhật!");
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txbMaLop.Text)) return;
            if (MessageBox.Show("Xóa lớp sẽ xóa hết đăng ký. Tiếp tục?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                // [REFACTOR] Dùng LopHocBUS
                if (LopHocBUS.Instance.DeleteLop(txbMaLop.Text)) { MessageBox.Show("Đã xóa."); LoadListLopHoc(); ResetForm(); }
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
                txbMaLop.Text = r.Cells["MaLop"].Value?.ToString();
                txbTenLop.Text = r.Cells["TenLop"].Value?.ToString();
                SetCombo(cbMonHoc, r.Cells["MaKyNang"].Value);
                SetCombo(cbGiaoVien, r.Cells["MaGiaoVien"].Value);
                SetCombo(cbTroGiang, r.Cells["MaTroGiang"].Value);
                SetCombo(cbPhongHoc, r.Cells["MaPhong"].Value);

                string thu = r.Cells["Thu"].Value?.ToString();
                if (cbThu.Items.Contains(thu)) cbThu.SelectedItem = thu; else cbThu.Text = thu;

                string ca = r.Cells["CaHoc"].Value?.ToString();
                if (cbCaHoc.Items.Contains(ca)) cbCaHoc.SelectedItem = ca; else cbCaHoc.Text = ca;

                nmSiSo.Value = r.Cells["SiSoToiDa"].Value != DBNull.Value ? Convert.ToInt32(r.Cells["SiSoToiDa"].Value) : 20;
                cbTrangThai.Text = r.Cells["TrangThai"].Value?.ToString();
            }
            catch { }
        }

        private void ResetForm()
        {
            txbMaLop.Clear(); txbTenLop.Clear(); cbThu.SelectedIndex = 0; cbCaHoc.SelectedIndex = 0;
            if (cbMonHoc.Items.Count > 0) cbMonHoc.SelectedIndex = 0;
            SetPlaceholder(txbSearch, "Tìm kiếm..."); FilterData("");
        }

        private string GetVal(ComboBox cb) => cb.SelectedValue != null ? cb.SelectedValue.ToString() : "";
        private void SetCombo(ComboBox cb, object val) { if (val != DBNull.Value && val != null) cb.SelectedValue = val; }

        private void DgvMain_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvMain.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                string s = e.Value.ToString();
                e.CellStyle.ForeColor = s.Contains("Đang") ? Color.Green : (s.Contains("kết thúc") ? Color.Red : Color.Blue);
                e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            }
        }

        private void AddInput(Panel p, string lb, Control c, int x, int y)
        {
            Label l = new Label { Text = lb, Location = new Point(x, y), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            c.Location = new Point(x + 130, y - 3); c.Width = 220; c.Font = new Font("Segoe UI", 10);
            p.Controls.Add(l); p.Controls.Add(c);
        }

        private Button CreateBtn(string t, Color c, int x, int y) => new Button { Text = t, Location = new Point(x, y), Size = new Size(130, 38), BackColor = c, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9, FontStyle.Bold), Cursor = Cursors.Hand };

        private void StyleGrid(DataGridView dgv)
        {
            dgv.Dock = DockStyle.Fill; dgv.BackgroundColor = Color.White;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false; dgv.ColumnHeadersHeight = 35;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 150, 136);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        }

        private void SetPlaceholder(TextBox txt, string holder)
        {
            txt.Text = holder; txt.ForeColor = Color.Gray;
            txt.Enter += (s, e) => { if (txt.Text == holder) { txt.Text = ""; txt.ForeColor = Color.Black; } };
            txt.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txt.Text)) { txt.Text = holder; txt.ForeColor = Color.Gray; } };
        }
    }
}