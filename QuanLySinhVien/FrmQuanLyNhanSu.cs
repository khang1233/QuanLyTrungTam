using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using QuanLyTrungTam.DAO;
using QuanLyTrungTam.BUS;

namespace QuanLyTrungTam
{
    public partial class FrmQuanLyNhanSu : Form
    {
        // =========================================================
        // 1. KHAI BÁO UI CONTROLS
        // =========================================================
        private Panel ui_pnlTop = new Panel { Dock = DockStyle.Top, Height = 250, BackColor = Color.White };
        private DataGridView ui_dgvNhanSu = new DataGridView();

        private TextBox ui_txbMa, ui_txbTen, ui_txbSDT, ui_txbEmail;
        private DateTimePicker ui_dtpNgaySinh;
        private ComboBox ui_cbLoaiNS;
        private ComboBox ui_cbChuyenNganh;
        private Label ui_lblChuyenNganh;

        // Tìm kiếm
        private TextBox ui_txbSearch;
        private Button ui_btnSearch;

        // Biến logic
        private string currentMaNS = "";

        public FrmQuanLyNhanSu()
        {
            InitializeComponent(); // Giữ lại nếu bạn có Designer, nếu không có thể bỏ
            SetupCustomUI();
            LoadComboBoxData(); // Load danh sách chuyên ngành trước
            LoadData();         // Load dữ liệu nhân sự sau
        }

        // =========================================================
        // 2. SETUP GIAO DIỆN (UI)
        // =========================================================
        private void SetupCustomUI()
        {
            this.Controls.Clear();
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Size = new Size(1280, 800);
            this.Padding = new Padding(10);

            // 1. HEADER
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = Color.White, Padding = new Padding(20, 0, 20, 0) };
            Label lblTitle = new Label { 
                Text = "QUẢN LÝ NHÂN SỰ", 
                Font = new Font("Segoe UI", 18, FontStyle.Bold), 
                ForeColor = Color.FromArgb(33, 150, 243), 
                AutoSize = true, 
                TextAlign = ContentAlignment.MiddleLeft 
            };
            lblTitle.Location = new Point(20, (pnlHeader.Height - lblTitle.Height) / 2);
            pnlHeader.Controls.Add(lblTitle);

            // 2. INPUT PANEL
            Panel pnlInput = new Panel { Dock = DockStyle.Top, Height = 340, BackColor = Color.Transparent, Padding = new Padding(0, 15, 0, 15) };

            // --- Init Controls ---
            ui_txbMa = new TextBox { ReadOnly = true, BackColor = Color.LightYellow, BorderStyle = BorderStyle.FixedSingle };
            ui_txbTen = new TextBox { BorderStyle = BorderStyle.FixedSingle };
            ui_txbSDT = new TextBox { BorderStyle = BorderStyle.FixedSingle };
            ui_txbEmail = new TextBox { BorderStyle = BorderStyle.FixedSingle };
            ui_dtpNgaySinh = new DateTimePicker { Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 10) };

            ui_cbLoaiNS = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Standard };
            ui_cbLoaiNS.Items.AddRange(new string[] { "Giáo viên", "Trợ giảng", "Nhân viên" });
            ui_cbLoaiNS.SelectedIndex = 0;
            ui_cbLoaiNS.SelectedIndexChanged += Ui_cbLoaiNS_SelectedIndexChanged;

            ui_cbChuyenNganh = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Standard };

            // --- GROUP BOXES ---
            
            // Group 1: Thông Tin Cá Nhân
            GroupBox gbInfo = new GroupBox { 
                Text = "Thông Tin Cá Nhân", 
                Font = new Font("Segoe UI", 10, FontStyle.Bold), 
                ForeColor = Color.DimGray,
                Location = new Point(20, 10), 
                Size = new Size(450, 240),
                BackColor = Color.White 
            };
            int gy = 40; int gap = 50;
            AddInput(gbInfo, "Mã Nhân Sự:", ui_txbMa, 20, gy);
            AddInput(gbInfo, "Họ Tên:", ui_txbTen, 20, gy + gap);
            AddInput(gbInfo, "Ngày Sinh:", ui_dtpNgaySinh, 20, gy + gap * 2);
            AddInput(gbInfo, "Chức Vụ:", ui_cbLoaiNS, 20, gy + gap * 3);


            // Group 2: Thông Tin Liên Hệ
            GroupBox gbContact = new GroupBox { 
                Text = "Thông Tin Liên Hệ", 
                Font = new Font("Segoe UI", 10, FontStyle.Bold), 
                ForeColor = Color.DimGray,
                Location = new Point(490, 10), 
                Size = new Size(450, 240),
                BackColor = Color.White 
            };
            AddInput(gbContact, "Số Điện Thoại:", ui_txbSDT, 20, gy);
            AddInput(gbContact, "Email:", ui_txbEmail, 20, gy + gap);
            
            // Chuyên Ngành (Label + Combo) - Add manually to handle visibility logic easily
            ui_lblChuyenNganh = new Label { Text = "Chuyên Ngành:", Location = new Point(20, gy + gap * 2 + 3), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Regular), ForeColor = Color.Black };
            ui_cbChuyenNganh.Location = new Point(130, gy + gap * 2);
            ui_cbChuyenNganh.Width = 300; ui_cbChuyenNganh.Font = new Font("Segoe UI", 10);
            gbContact.Controls.Add(ui_lblChuyenNganh);
            gbContact.Controls.Add(ui_cbChuyenNganh);

            pnlInput.Controls.Add(gbInfo);
            pnlInput.Controls.Add(gbContact);


            // --- ACTIONS PANEL (Right) ---
            Panel pnlActions = new Panel { Location = new Point(960, 20), Size = new Size(200, 310) };
            
            Button btnThem = CreateBtn("➕ Thêm NS", Color.FromArgb(40, 167, 69), 0, 0); // Green
            Button btnSua = CreateBtn("✏️ Cập Nhật", Color.FromArgb(0, 123, 255), 0, 50); // Blue
            Button btnXoa = CreateBtn("❌ Xóa NS", Color.FromArgb(220, 53, 69), 0, 100); // Red
            Button btnLamMoi = CreateBtn("🔄 Làm Mới", Color.FromArgb(108, 117, 125), 0, 150); // Gray
            Button btnCapTK = CreateBtn("🔐 Cấp Tài Khoản", Color.FromArgb(111, 66, 193), 0, 200); // Purple

            btnLamMoi.Click += (s, e) => ResetForm();
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnCapTK.Click += BtnCapTK_Click;

            pnlActions.Controls.AddRange(new Control[] { btnThem, btnSua, btnXoa, btnLamMoi, btnCapTK });
            pnlInput.Controls.Add(pnlActions);


            // --- SEARCH BAR (Bottom of Input) ---
            Panel pnlSearch = new Panel { Location = new Point(20, 260), Size = new Size(920, 60), BackColor = Color.White };
            Label lblSearch = new Label { Text = "Tìm kiếm:", Location = new Point(15, 18), AutoSize = true, Font = new Font("Segoe UI", 10) };
            ui_txbSearch = new TextBox { Location = new Point(100, 15), Width = 400, Font = new Font("Segoe UI", 11), BorderStyle = BorderStyle.FixedSingle };
            
            ui_btnSearch = new Button { 
                Text = "🔍", 
                Location = new Point(510, 13), 
                Size = new Size(50, 31), 
                BackColor = Color.Navy, 
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat 
            };
            ui_btnSearch.FlatAppearance.BorderSize = 0;

            ui_txbSearch.TextChanged += (s, e) => FilterData(ui_txbSearch.Text);
            
            pnlSearch.Controls.AddRange(new Control[] { lblSearch, ui_txbSearch, ui_btnSearch });
            pnlInput.Controls.Add(pnlSearch);


            // 3. GRIDVIEW
            Panel pnlGridContainer = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 0, 20, 20) };
            
            ui_dgvNhanSu = new DataGridView();
            // Style Grid
            ui_dgvNhanSu.Dock = DockStyle.Fill;
            ui_dgvNhanSu.BackgroundColor = Color.White;
            ui_dgvNhanSu.BorderStyle = BorderStyle.None;
            ui_dgvNhanSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ui_dgvNhanSu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ui_dgvNhanSu.ReadOnly = true;
            ui_dgvNhanSu.AllowUserToAddRows = false;
            ui_dgvNhanSu.RowHeadersVisible = false;
            ui_dgvNhanSu.ColumnHeadersHeight = 45;
            ui_dgvNhanSu.EnableHeadersVisualStyles = false;
            ui_dgvNhanSu.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            ui_dgvNhanSu.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            ui_dgvNhanSu.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            ui_dgvNhanSu.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            ui_dgvNhanSu.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            ui_dgvNhanSu.DefaultCellStyle.SelectionForeColor = Color.Black;
            ui_dgvNhanSu.GridColor = Color.WhiteSmoke;

            pnlGridContainer.Controls.Add(ui_dgvNhanSu);

            this.Controls.Add(pnlGridContainer);
            this.Controls.Add(pnlInput);
            this.Controls.Add(pnlHeader);
        }

        // =========================================================
        // 3. LOGIC LOAD DATA & XỬ LÝ MÀU SẮC
        // =========================================================

        void LoadData()
        {
            // [REFACTOR] NhanVienBUS
            ui_dgvNhanSu.DataSource = NhanVienBUS.Instance.GetListNhanVien();

            // Gắn sự kiện Click để binding dữ liệu
            ui_dgvNhanSu.CellClick -= DgvNhanSu_CellClick;
            ui_dgvNhanSu.CellClick += DgvNhanSu_CellClick;

            // [QUAN TRỌNG] Gắn sự kiện Tô màu trạng thái
            ui_dgvNhanSu.CellFormatting -= DgvNhanSu_CellFormatting;
            ui_dgvNhanSu.CellFormatting += DgvNhanSu_CellFormatting;

            // Header Mapping
            if (ui_dgvNhanSu.Columns.Contains("MaNS")) { ui_dgvNhanSu.Columns["MaNS"].HeaderText = "Mã Nhân Sự"; }
            if (ui_dgvNhanSu.Columns.Contains("HoTen")) { ui_dgvNhanSu.Columns["HoTen"].HeaderText = "Họ Tên"; }
            if (ui_dgvNhanSu.Columns.Contains("NgaySinh")) { 
                ui_dgvNhanSu.Columns["NgaySinh"].HeaderText = "Ngày Sinh"; 
                ui_dgvNhanSu.Columns["NgaySinh"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (ui_dgvNhanSu.Columns.Contains("SDT")) { 
                ui_dgvNhanSu.Columns["SDT"].HeaderText = "Số Điện Thoại"; 
                ui_dgvNhanSu.Columns["SDT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (ui_dgvNhanSu.Columns.Contains("Email")) { ui_dgvNhanSu.Columns["Email"].HeaderText = "Email"; }
            if (ui_dgvNhanSu.Columns.Contains("LoaiNS")) { 
                ui_dgvNhanSu.Columns["LoaiNS"].HeaderText = "Chức Vụ"; 
                ui_dgvNhanSu.Columns["LoaiNS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (ui_dgvNhanSu.Columns.Contains("TrangThai")) { ui_dgvNhanSu.Columns["TrangThai"].HeaderText = "Trạng Thái"; }
            if (ui_dgvNhanSu.Columns.Contains("ChuyenNganh")) { ui_dgvNhanSu.Columns["ChuyenNganh"].HeaderText = "Chuyên Ngành"; }
        }

        // Hàm tô màu chữ trên GridView
        private void DgvNhanSu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (ui_dgvNhanSu.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                string status = e.Value.ToString();

                if (status == "Đang giảng dạy")
                {
                    e.CellStyle.ForeColor = Color.Green;
                    e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                }
                else if (status == "Đang trống lớp")
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Italic);
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Blue; // Cho nhân viên văn phòng
                }
            }
        }

        void LoadComboBoxData()
        {
            try
            {
                // [REFACTOR] KyNangBUS
                List<string> listCN = KyNangBUS.Instance.GetListChuyenNganh();
                ui_cbChuyenNganh.Items.Clear();
                foreach (string cn in listCN)
                {
                    ui_cbChuyenNganh.Items.Add(cn);
                }
                if (ui_cbChuyenNganh.Items.Count > 0) ui_cbChuyenNganh.SelectedIndex = 0;
            }
            catch { }
        }

        // Logic ẩn/hiện ComboBox Chuyên Ngành
        private void Ui_cbLoaiNS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ui_cbLoaiNS.Text == "Giáo viên")
            {
                ui_cbChuyenNganh.Enabled = true;
                ui_cbChuyenNganh.Visible = true;
                ui_lblChuyenNganh.Visible = true;
            }
            else
            {
                ui_cbChuyenNganh.Enabled = false;
                ui_cbChuyenNganh.SelectedIndex = -1;
                ui_cbChuyenNganh.Visible = false;
                ui_lblChuyenNganh.Visible = false;
            }
        }

        private void DgvNhanSu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                DataGridViewRow r = ui_dgvNhanSu.Rows[e.RowIndex];
                currentMaNS = r.Cells["MaNS"].Value.ToString();

                ui_txbMa.Text = currentMaNS;
                ui_txbTen.Text = r.Cells["HoTen"].Value.ToString();
                ui_txbSDT.Text = r.Cells["SDT"].Value.ToString();
                ui_txbEmail.Text = r.Cells["Email"].Value.ToString();

                ui_cbLoaiNS.Text = r.Cells["LoaiNS"].Value.ToString();

                if (r.Cells["NgaySinh"].Value != DBNull.Value)
                    ui_dtpNgaySinh.Value = Convert.ToDateTime(r.Cells["NgaySinh"].Value);

                if (ui_dgvNhanSu.Columns.Contains("ChuyenNganh") && r.Cells["ChuyenNganh"].Value != DBNull.Value)
                {
                    string cn = r.Cells["ChuyenNganh"].Value.ToString();
                    if (!string.IsNullOrEmpty(cn)) ui_cbChuyenNganh.Text = cn;
                }
            }
            catch { }
        }

        private void FilterData(string key)
        {
            DataTable dt = ui_dgvNhanSu.DataSource as DataTable;
            if (dt != null) dt.DefaultView.RowFilter = string.Format("HoTen LIKE '%{0}%' OR MaNS LIKE '%{0}%'", key);
        }

        private void ResetForm()
        {
            currentMaNS = "";
            ui_txbMa.Clear(); ui_txbTen.Clear(); ui_txbSDT.Clear(); ui_txbEmail.Clear();
            ui_cbLoaiNS.SelectedIndex = 0;
            LoadComboBoxData();
            LoadData(); // Load lại để cập nhật trạng thái mới nhất
            ui_txbTen.Focus();
        }

        // =========================================================
        // 4. CÁC HÀM XỬ LÝ SỰ KIỆN (ACTIONS)
        // =========================================================

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (ui_cbLoaiNS.Text == "Giáo viên" && string.IsNullOrEmpty(ui_cbChuyenNganh.Text))
            {
                MessageBox.Show("Giáo viên bắt buộc phải có Chuyên ngành!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // [REFACTOR] NhanVienBUS
            if (NhanVienBUS.Instance.InsertNhanSu(ui_txbTen.Text, ui_dtpNgaySinh.Value, ui_txbSDT.Text, ui_txbEmail.Text, ui_cbLoaiNS.Text, ui_cbChuyenNganh.Text))
            {
                MessageBox.Show("Thêm nhân sự thành công!");
                LoadData();
                ResetForm();
            }
            else
            {
                MessageBox.Show("Thêm thất bại! Vui lòng kiểm tra lại thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaNS)) return;

            if (ui_cbLoaiNS.Text == "Giáo viên" && string.IsNullOrEmpty(ui_cbChuyenNganh.Text))
            {
                MessageBox.Show("Giáo viên bắt buộc phải có Chuyên ngành!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // [REFACTOR] NhanVienBUS
            if (NhanVienBUS.Instance.UpdateNhanSu(currentMaNS, ui_txbTen.Text, ui_dtpNgaySinh.Value, ui_txbSDT.Text, ui_txbEmail.Text, ui_cbLoaiNS.Text, ui_cbChuyenNganh.Text))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadData();
            }
            else MessageBox.Show("Lỗi cập nhật!");
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaNS)) return;
            if (MessageBox.Show(string.Format("Bạn có chắc muốn xóa nhân sự {0}?", currentMaNS), "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                // [REFACTOR] NhanVienBUS
                if (NhanVienBUS.Instance.DeleteNhanVien(currentMaNS))
                {
                    MessageBox.Show("Đã xóa!");
                    ResetForm();
                }
            }
        }

        private void BtnCapTK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaNS)) return;

            // Logic cấp quyền: Nhân viên -> Admin, còn lại là user thường
            string quyen = (ui_cbLoaiNS.Text == "Nhân viên") ? "Admin" : (ui_cbLoaiNS.Text == "Trợ giảng" ? "TroGiang" : "GiaoVien");

            // [REFACTOR] AccountBUS
            if (AccountBUS.Instance.InsertAccount(currentMaNS, "123", quyen, currentMaNS))
                MessageBox.Show(string.Format("Cấp TK thành công!\nUser: {0}\nPass: 123", currentMaNS));
            else
                MessageBox.Show("Nhân sự này đã có tài khoản rồi!");
        }

        // --- Helpers UI ---
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
            if (c is DateTimePicker) c.Width = 300;
            c.Font = new Font("Segoe UI", 10);
            parent.Controls.Add(l); parent.Controls.Add(c);
        }

        Button CreateBtn(string t, Color c, int x, int y)
        {
            Button btn = new Button { 
                Text = t, 
                Location = new Point(x, y), 
                Size = new Size(180, 45), // Bigger buttons
                BackColor = c, 
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat, 
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }
    }
}