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
            this.Controls.Add(ui_dgvNhanSu);
            this.Controls.Add(ui_pnlTop);

            // --- Cấu hình GridView ---
            ui_dgvNhanSu.Dock = DockStyle.Fill;
            ui_dgvNhanSu.BackgroundColor = Color.WhiteSmoke;
            ui_dgvNhanSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ui_dgvNhanSu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ui_dgvNhanSu.ReadOnly = true;
            ui_dgvNhanSu.AllowUserToAddRows = false;
            ui_dgvNhanSu.RowHeadersVisible = false;
            ui_dgvNhanSu.ColumnHeadersHeight = 40;
            ui_dgvNhanSu.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // --- Khởi tạo Controls ---
            ui_txbMa = new TextBox { ReadOnly = true, BackColor = Color.LightYellow };
            ui_txbTen = new TextBox();
            ui_txbSDT = new TextBox();
            ui_txbEmail = new TextBox();
            ui_dtpNgaySinh = new DateTimePicker { Format = DateTimePickerFormat.Short };

            ui_cbLoaiNS = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            ui_cbLoaiNS.Items.AddRange(new string[] { "Giáo viên", "Trợ giảng", "Nhân viên" });
            ui_cbLoaiNS.SelectedIndex = 0;
            ui_cbLoaiNS.SelectedIndexChanged += Ui_cbLoaiNS_SelectedIndexChanged;

            ui_cbChuyenNganh = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            ui_lblChuyenNganh = new Label { Text = "Chuyên Ngành:", AutoSize = true, Font = new Font("Segoe UI", 9) };

            // --- Bố cục Panel ---
            AddInput(ui_pnlTop, "Mã NS (Auto):", ui_txbMa, 20, 20);
            AddInput(ui_pnlTop, "Họ và Tên:", ui_txbTen, 20, 60);
            AddInput(ui_pnlTop, "Ngày Sinh:", ui_dtpNgaySinh, 20, 100);

            AddInput(ui_pnlTop, "Số Điện Thoại:", ui_txbSDT, 400, 20);
            AddInput(ui_pnlTop, "Email:", ui_txbEmail, 400, 60);
            AddInput(ui_pnlTop, "Chức Vụ:", ui_cbLoaiNS, 400, 100);

            // Vị trí Chuyên ngành
            ui_lblChuyenNganh.Location = new Point(780, 20);
            ui_cbChuyenNganh.Location = new Point(890, 17);
            ui_cbChuyenNganh.Width = 200;
            ui_cbChuyenNganh.Font = new Font("Segoe UI", 10);
            ui_pnlTop.Controls.Add(ui_lblChuyenNganh);
            ui_pnlTop.Controls.Add(ui_cbChuyenNganh);

            // Dàn Nút Bấm
            Button btnLamMoi = CreateBtn("🔄 Làm Mới", Color.Gray, 20, 160);
            Button btnThem = CreateBtn("➕ Thêm NS", Color.Teal, 160, 160);
            Button btnSua = CreateBtn("✏️ Cập Nhật", Color.DodgerBlue, 300, 160);
            Button btnXoa = CreateBtn("❌ Xóa NS", Color.Crimson, 440, 160);
            Button btnCapTK = CreateBtn("🔐 Cấp Tài Khoản", Color.Purple, 580, 160);

            // Tìm kiếm
            Label lblSearch = new Label { Text = "Tìm kiếm:", Location = new Point(20, 212), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            ui_txbSearch = new TextBox { Location = new Point(100, 210), Width = 350, Font = new Font("Segoe UI", 10) };
            ui_btnSearch = new Button { Text = "🔍", Location = new Point(460, 209), Size = new Size(50, 29), BackColor = Color.Navy, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };

            ui_txbSearch.TextChanged += (s, e) => FilterData(ui_txbSearch.Text);

            ui_pnlTop.Controls.AddRange(new Control[] { lblSearch, ui_txbSearch, ui_btnSearch });
            ui_pnlTop.Controls.AddRange(new Control[] { btnLamMoi, btnThem, btnSua, btnXoa, btnCapTK });

            // Gắn sự kiện nút
            btnLamMoi.Click += (s, e) => ResetForm();
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnCapTK.Click += BtnCapTK_Click;
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
        void AddInput(Panel p, string lb, Control c, int x, int y)
        {
            Label l = new Label { Text = lb, Location = new Point(x, y), AutoSize = true, Font = new Font("Segoe UI", 9) };
            c.Location = new Point(x + 100, y - 3); c.Width = 220; c.Font = new Font("Segoe UI", 10);
            p.Controls.Add(l); p.Controls.Add(c);
        }

        Button CreateBtn(string t, Color c, int x, int y)
        {
            return new Button { Text = t, Location = new Point(x, y), Size = new Size(130, 35), BackColor = c, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
        }
    }
}