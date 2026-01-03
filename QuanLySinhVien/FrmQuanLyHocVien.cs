using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using QuanLyTrungTam.DAO;
using QuanLyTrungTam.BUS;
using QuanLyTrungTam.DTO;
using System.Linq;

namespace QuanLyTrungTam
{
    public partial class FrmQuanLyHocVien : Form
    {
        // --- 1. KHAI BÁO BIẾN UI ---
        private Panel ui_pnlTop = new Panel { Dock = DockStyle.Top, Height = 250, BackColor = Color.White };
        private DataGridView ui_dgvHocVien = new DataGridView();

        // Các control nhập liệu
        private TextBox ui_txbMa, ui_txbTen, ui_txbSDT, ui_txbEmail, ui_txbDiaChi;
        private DateTimePicker ui_dtpNgaySinh;
        private ComboBox ui_cbTrangThai;

        // Tìm kiếm
        private TextBox ui_txbSearch;
        private Button ui_btnSearch;

        // Biến Logic lưu mã học viên đang chọn
        private string currentMaHV = "";

        public FrmQuanLyHocVien()
        {
            // Không dùng InitializeComponent() vì bạn đang code giao diện động
            SetupCustomUI();
            LoadData();
        }

        // =========================================================================
        // 1. TỰ ĐỘNG VẼ GIAO DIỆN (SETUP UI)
        // =========================================================================
        private void SetupCustomUI()
        {
            this.Controls.Clear();
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Size = new Size(1280, 800);
            this.Padding = new Padding(10);

            // --- HEADER ---
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = Color.White, Padding = new Padding(20, 0, 20, 0) };
            Label lblTitle = new Label { 
                Text = "QUẢN LÝ HỌC VIÊN", 
                Font = new Font("Segoe UI", 18, FontStyle.Bold), 
                ForeColor = Color.FromArgb(33, 150, 243), 
                AutoSize = true, 
                TextAlign = ContentAlignment.MiddleLeft 
            };
            lblTitle.Location = new Point(20, (pnlHeader.Height - lblTitle.Height) / 2);
            pnlHeader.Controls.Add(lblTitle);

            // --- INPUT PANEL ---
            ui_pnlTop = new Panel { Dock = DockStyle.Top, Height = 370, BackColor = Color.Transparent, Padding = new Padding(0, 15, 0, 15) };
            
            // Init Controls
            ui_txbMa = new TextBox { ReadOnly = true, BackColor = Color.WhiteSmoke, BorderStyle = BorderStyle.FixedSingle };
            ui_txbTen = new TextBox { BorderStyle = BorderStyle.FixedSingle };
            ui_txbSDT = new TextBox { BorderStyle = BorderStyle.FixedSingle };
            ui_txbEmail = new TextBox { BorderStyle = BorderStyle.FixedSingle };
            ui_txbDiaChi = new TextBox { BorderStyle = BorderStyle.FixedSingle };
            ui_dtpNgaySinh = new DateTimePicker { Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 10) };
            ui_cbTrangThai = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Standard };
            ui_cbTrangThai.Items.AddRange(new string[] { "Nhập học", "Đang học", "Bảo lưu", "Bỏ học", "Hoàn thành" });
            ui_cbTrangThai.SelectedIndex = 0;

            // Group 1: Thông Tin Cá Nhân
            GroupBox gbInfo = new GroupBox { 
                Text = "Thông Tin Cá Nhân", 
                Font = new Font("Segoe UI", 10, FontStyle.Bold), 
                ForeColor = Color.DimGray,
                Location = new Point(20, 10), 
                Size = new Size(450, 240),
                BackColor = Color.White 
            };
            int gy = 35; int gap = 45;
            AddInput(gbInfo, "Mã HV:", ui_txbMa, 20, gy);
            AddInput(gbInfo, "Họ Tên:", ui_txbTen, 20, gy + gap);
            AddInput(gbInfo, "Ngày Sinh:", ui_dtpNgaySinh, 20, gy + gap * 2);
            AddInput(gbInfo, "Trạng Thái:", ui_cbTrangThai, 20, gy + gap * 3);

            // Group 2: Thông Tin Liên Hệ
            GroupBox gbContact = new GroupBox { 
                Text = "Thông Tin Liên Hệ", 
                Font = new Font("Segoe UI", 10, FontStyle.Bold), 
                ForeColor = Color.DimGray,
                Location = new Point(490, 10), 
                Size = new Size(450, 240),
                BackColor = Color.White 
            };
            AddInput(gbContact, "Số ĐT:", ui_txbSDT, 20, gy);
            AddInput(gbContact, "Email:", ui_txbEmail, 20, gy + gap);
            AddInput(gbContact, "Địa Chỉ:", ui_txbDiaChi, 20, gy + gap * 2);

            ui_pnlTop.Controls.Add(gbInfo);
            ui_pnlTop.Controls.Add(gbContact);

            // Button Panel (Right Side)
            Panel pnlActions = new Panel { Location = new Point(960, 20), Size = new Size(180, 340) };
            
            // Functions
            Button btnLamMoi = CreateBtn("🔄 Làm Mới", Color.FromArgb(108, 117, 125), 0, 0);
            Button btnLuu = CreateBtn("💾 Lưu Mới", Color.FromArgb(40, 167, 69), 0, 45);
            Button btnCapNhat = CreateBtn("✏️ Cập Nhật", Color.FromArgb(0, 123, 255), 0, 90);
            Button btnXoa = CreateBtn("❌ Xóa HV", Color.FromArgb(220, 53, 69), 0, 135);
            
            // Navigation
            Button btnDangKyLop = CreateBtn("📚 Đăng Ký Lớp", Color.Orange, 0, 195);
            Button btnThuPhi = CreateBtn("💰 Thu Học Phí", Color.MediumSeaGreen, 0, 240);
            Button btnCapTK = CreateBtn("🔐 Cấp Tài Khoản", Color.Purple, 0, 285);

            pnlActions.Controls.AddRange(new Control[] { btnLamMoi, btnLuu, btnCapNhat, btnXoa, btnDangKyLop, btnThuPhi, btnCapTK });
            ui_pnlTop.Controls.Add(pnlActions);

             // Search Bar
            Panel pnlSearch = new Panel { Location = new Point(20, 270), Size = new Size(920, 50), BackColor = Color.White };
            Label lblSearch = new Label { Text = "Tìm kiếm:", Location = new Point(15, 15), AutoSize = true, Font = new Font("Segoe UI", 10) };
            ui_txbSearch = new TextBox { Location = new Point(100, 12), Width = 400, Font = new Font("Segoe UI", 10), BorderStyle = BorderStyle.FixedSingle };
            SetPlaceholder(ui_txbSearch, "Nhập mã số hoặc tên học viên...");
            
            ui_btnSearch = new Button { Text = "🔍", Location = new Point(510, 10), Size = new Size(50, 28), BackColor = Color.Navy, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            ui_btnSearch.FlatAppearance.BorderSize = 0;

            ui_txbSearch.TextChanged += (s, e) => FilterData(ui_txbSearch.Text);
            ui_btnSearch.Click += (s, e) => FilterData(ui_txbSearch.Text);

            pnlSearch.Controls.AddRange(new Control[] { lblSearch, ui_txbSearch, ui_btnSearch });
            ui_pnlTop.Controls.Add(pnlSearch);


            // Events
            btnLamMoi.Click += (s, e) => ResetForm();
            btnLuu.Click += BtnThem_Click;
            btnCapNhat.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnCapTK.Click += BtnCapTK_Click;

            btnDangKyLop.Click += (s, e) => {
                if (string.IsNullOrEmpty(currentMaHV)) { MessageBox.Show("Vui lòng chọn học viên trước!"); return; }
                if (ui_cbTrangThai.Text == "Bỏ học") { MessageBox.Show("Học viên này đã bỏ học, không thể đăng ký lớp!"); return; }
                fMain main = Application.OpenForms.OfType<fMain>().FirstOrDefault();
                if (main != null) main.NavigateToDangKy(currentMaHV);
            };

            btnThuPhi.Click += (s, e) => {
                if (string.IsNullOrEmpty(currentMaHV)) { MessageBox.Show("Vui lòng chọn học viên trước!"); return; }
                fMain main = Application.OpenForms.OfType<fMain>().FirstOrDefault();
                if (main != null) main.NavigateToThuHocPhi(currentMaHV);
            };

            // Setup Grid
            Panel pnlGridContainer = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 0, 20, 20) };
            ui_dgvHocVien.Dock = DockStyle.Fill;
            ui_dgvHocVien.BackgroundColor = Color.White;
            ui_dgvHocVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ui_dgvHocVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ui_dgvHocVien.ReadOnly = true;
            ui_dgvHocVien.AllowUserToAddRows = false;
            ui_dgvHocVien.BorderStyle = BorderStyle.None;
            ui_dgvHocVien.RowHeadersVisible = false;
            ui_dgvHocVien.ColumnHeadersHeight = 45;
            ui_dgvHocVien.EnableHeadersVisualStyles = false;
            ui_dgvHocVien.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            ui_dgvHocVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            ui_dgvHocVien.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            ui_dgvHocVien.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            ui_dgvHocVien.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            ui_dgvHocVien.DefaultCellStyle.SelectionForeColor = Color.Black;

            pnlGridContainer.Controls.Add(ui_dgvHocVien);
            
            this.Controls.Add(pnlGridContainer);
            this.Controls.Add(ui_pnlTop);
            this.Controls.Add(pnlHeader);
        }

        // =========================================================================
        // 2. LOGIC XỬ LÝ DỮ LIỆU & SỰ KIỆN
        // =========================================================================

        // Tải danh sách học viên lên Grid
        // Tải danh sách học viên lên Grid
        void LoadData()
        {
            ui_dgvHocVien.DataSource = HocVienBUS.Instance.GetListHocVien();

            // Header mapping
            if (ui_dgvHocVien.Columns.Contains("MaHV")) { ui_dgvHocVien.Columns["MaHV"].HeaderText = "Mã Học Viên"; }
            if (ui_dgvHocVien.Columns.Contains("HoTen")) { ui_dgvHocVien.Columns["HoTen"].HeaderText = "Họ và Tên"; }
            if (ui_dgvHocVien.Columns.Contains("NgaySinh")) { 
                ui_dgvHocVien.Columns["NgaySinh"].HeaderText = "Ngày Sinh"; 
                ui_dgvHocVien.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
                ui_dgvHocVien.Columns["NgaySinh"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (ui_dgvHocVien.Columns.Contains("SDT")) { ui_dgvHocVien.Columns["SDT"].HeaderText = "Số Điện Thoại"; }
            if (ui_dgvHocVien.Columns.Contains("Email")) { ui_dgvHocVien.Columns["Email"].HeaderText = "Email"; }
            if (ui_dgvHocVien.Columns.Contains("DiaChi")) { ui_dgvHocVien.Columns["DiaChi"].HeaderText = "Địa Chỉ"; }
            if (ui_dgvHocVien.Columns.Contains("TrangThai")) { 
                ui_dgvHocVien.Columns["TrangThai"].HeaderText = "Trạng Thái"; 
                ui_dgvHocVien.Columns["TrangThai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (ui_dgvHocVien.Columns.Contains("NgayGiaNhap")) { 
                ui_dgvHocVien.Columns["NgayGiaNhap"].HeaderText = "Ngày Đăng Ký"; 
                ui_dgvHocVien.Columns["NgayGiaNhap"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (ui_dgvHocVien.Columns.Contains("GioiTinh")) { ui_dgvHocVien.Columns["GioiTinh"].HeaderText = "Giới Tính"; }

            // Đăng ký lại sự kiện CellClick để tránh bị double event
            ui_dgvHocVien.CellClick -= DgvHocVien_CellClick;
            ui_dgvHocVien.CellClick += DgvHocVien_CellClick;
        }

        // Lọc dữ liệu tìm kiếm
        private void FilterData(string keyword)
        {
            if (keyword == "Nhập mã số hoặc tên học viên...") keyword = "";
            DataTable dt = ui_dgvHocVien.DataSource as DataTable;
            if (dt != null)
                dt.DefaultView.RowFilter = string.IsNullOrEmpty(keyword) ? "" : string.Format("MaHV LIKE '%{0}%' OR HoTen LIKE '%{0}%'", keyword);
        }

        // Sự kiện khi click vào một dòng trong Grid
        private void DgvHocVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                DataGridViewRow r = ui_dgvHocVien.Rows[e.RowIndex];
                if (r.Cells["MaHV"].Value == null) return;

                currentMaHV = r.Cells["MaHV"].Value.ToString();

                // Đổ dữ liệu lên các ô input
                ui_txbMa.Text = currentMaHV;
                ui_txbTen.Text = r.Cells["HoTen"].Value.ToString();
                ui_txbSDT.Text = r.Cells["SDT"].Value.ToString();
                ui_txbEmail.Text = r.Cells["Email"].Value.ToString();
                ui_txbDiaChi.Text = r.Cells["DiaChi"].Value.ToString();
                ui_cbTrangThai.Text = r.Cells["TrangThai"].Value.ToString();

                if (r.Cells["NgaySinh"].Value != DBNull.Value)
                    ui_dtpNgaySinh.Value = Convert.ToDateTime(r.Cells["NgaySinh"].Value);
            }
            catch { }
        }

        // Làm mới form để nhập mới
        private void ResetForm()
        {
            currentMaHV = "";
            ui_txbMa.Text = HocVienBUS.Instance.GetNewMaHV();
            ui_txbTen.Clear();
            ui_txbSDT.Clear();
            ui_txbEmail.Clear();
            ui_txbDiaChi.Clear();
            ui_cbTrangThai.SelectedIndex = 0; // Mặc định là Nhập học

            FilterData(""); // Bỏ lọc tìm kiếm
            ui_txbTen.Focus();
        }

        // --- CÁC SỰ KIỆN NÚT BẤM ---

        // 1. Thêm Học Viên
        private void BtnThem_Click(object sender, EventArgs e)
        {
            string ma = HocVienBUS.Instance.GetNewMaHV();
            if (HocVienBUS.Instance.InsertHocVien(ma, ui_txbTen.Text, ui_dtpNgaySinh.Value, ui_txbSDT.Text, ui_txbEmail.Text, ui_txbDiaChi.Text, ui_cbTrangThai.Text))
            {
                MessageBox.Show("Thêm học viên thành công!");
                LoadData();
                ResetForm();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm học viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 2. Cập Nhật (Sửa) Học Viên
        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaHV)) return;

            // Cập nhật thông tin
            if (HocVienBUS.Instance.UpdateHocVien(currentMaHV, ui_txbTen.Text, ui_dtpNgaySinh.Value, ui_txbSDT.Text, ui_txbEmail.Text, ui_txbDiaChi.Text, ui_cbTrangThai.Text))
            {
                // Nếu trạng thái là Bỏ học -> Khóa tài khoản
                AccountBUS.Instance.LockAccountByUserID(currentMaHV, (ui_cbTrangThai.Text == "Bỏ học"));

                MessageBox.Show("Cập nhật thông tin thành công!");
                LoadData();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 3. Xóa Học Viên (MỚI THÊM)
        private void BtnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra đầu vào
            if (string.IsNullOrEmpty(currentMaHV))
            {
                MessageBox.Show("Vui lòng chọn học viên cần xóa!", "Chưa chọn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác nhận xóa (Quan trọng)
            string msg = string.Format("Bạn có chắc chắn muốn xóa học viên [{0}] (Mã: {1})?\n\n", ui_txbTen.Text, currentMaHV) +
                         "⚠️ CẢNH BÁO: Hành động này sẽ xóa vĩnh viễn:\n";


            if (MessageBox.Show(msg, "Xác nhận xóa dữ liệu", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                // Gọi BUS để xóa
                if (HocVienBUS.Instance.DeleteHocVien(currentMaHV))
                {
                    MessageBox.Show("Đã xóa học viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();   // Tải lại danh sách
                    ResetForm();  // Xóa trắng các ô nhập liệu
                }
                else
                {
                    MessageBox.Show("Xóa thất bại! Vui lòng kiểm tra lại kết nối hoặc dữ liệu.", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 4. Cấp Tài Khoản
        private void BtnCapTK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaHV))
            {
                MessageBox.Show("Vui lòng chọn học viên cần cấp tài khoản!");
                return;
            }

            // Mặc định pass là 123, Loại TK là HocVien
            if (AccountBUS.Instance.InsertAccount(currentMaHV, "123", "HocVien", currentMaHV))
            {
                MessageBox.Show("Đã cấp tài khoản thành công!\nTên đăng nhập: " + currentMaHV + "\nMật khẩu: 123");
            }
            else
            {
                MessageBox.Show("Học viên này đã có tài khoản rồi!");
            }
        }

        // =========================================================================
        // 3. CÁC HÀM HELPER (HỖ TRỢ UI)
        // =========================================================================

        // Hàm hỗ trợ vẽ Label + Control nhập liệu nhanh
        // Hàm hỗ trợ vẽ Label + Control nhập liệu nhanh
        void AddInput(Control parent, string lb, Control c, int x, int y)
        {
            Label l = new Label { 
                Text = lb, 
                Location = new Point(x, y + 3), 
                AutoSize = true, 
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.Black
            };
            c.Location = new Point(x + 100, y);
            c.Width = 300; // Wider
            c.Font = new Font("Segoe UI", 10);
            parent.Controls.Add(l);
            parent.Controls.Add(c);
        }

        // Hàm tạo nút bấm có style đồng bộ
        Button CreateBtn(string t, Color c, int x, int y)
        {
            return new Button
            {
                Text = t,
                Location = new Point(x, y),
                Size = new Size(160, 40), // Taller
                BackColor = c,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }

        // Hàm tạo placeholder text cho ô tìm kiếm
        private void SetPlaceholder(TextBox txt, string holder)
        {
            txt.Text = holder;
            txt.ForeColor = Color.Gray;
            txt.Enter += (s, e) => { if (txt.Text == holder) { txt.Text = ""; txt.ForeColor = Color.Black; } };
            txt.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txt.Text)) { txt.Text = holder; txt.ForeColor = Color.Gray; } };
        }
    }
}