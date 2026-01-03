
using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using QuanLyTrungTam.BUS; // [REFACTOR] Sử dụng BUS
// using QuanLyTrungTam.DAO; // [REFACTOR] Loại bỏ direct DAO access

namespace QuanLyTrungTam
{
    public partial class FrmDangKyAdmin : Form
    {
        // =========================================================================
        // 1. KHAI BÁO BIẾN
        // =========================================================================
        private TextBox txbSearch;
        private DataGridView dgvHocVien;
        private DataGridView dgvDaDangKy;

        private ComboBox cbKyNang;
        private ComboBox cbLopHoc;
        private Label lblHocPhi;
        private Button btnDangKy;

        // Biến lưu trạng thái
        private string currentMaHV = "";

        public FrmDangKyAdmin()
        {
            // Nếu bạn có file Designer thì giữ dòng này, nếu code tay 100% thì xóa đi cũng được
            try { InitializeComponent(); } catch { }

            SetupUI();           // 1. Vẽ giao diện chuẩn
            LoadDataHocVien(""); // 2. Load dữ liệu ban đầu
            LoadKyNang();        // 3. Load môn học
        }

        // =========================================================================
        // 2. THIẾT KẾ GIAO DIỆN (ĐÃ FIX LỖI BỊ CHE VÀ LỆCH SPLITTER)
        // =========================================================================
        private void SetupUI()
        {
            // --- CẤU HÌNH FORM ---
            this.Controls.Clear();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            // --- A. HEADER (THANH TIÊU ĐỀ) ---
            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White,
                Padding = new Padding(20, 15, 0, 0)
            };
            Label lblTitle = new Label
            {
                Text = "QUẢN LÝ ĐĂNG KÝ & HỦY MÔN",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 150, 243),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblTitle);

            // --- B. NỘI DUNG CHÍNH (SPLIT CONTAINER) ---
            SplitContainer split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke,
                SplitterWidth = 10,
                FixedPanel = FixedPanel.Panel1
            };

            // ========================================================
            // PANEL TRÁI: DANH SÁCH HỌC VIÊN + TÌM KIẾM
            // ========================================================
            GroupBox grpLeft = new GroupBox
            {
                Text = " 1. Chọn Học Viên ",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.DimGray,
                BackColor = Color.White
            };
            grpLeft.Padding = new Padding(5);

            // Panel chứa ô tìm kiếm
            Panel pnlSearch = new Panel
            {
                Dock = DockStyle.Top,
                Height = 55,
                Padding = new Padding(5, 10, 5, 10),
                BackColor = Color.White
            };

            txbSearch = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 12),
                BorderStyle = BorderStyle.FixedSingle
            };
            SetPlaceholder(txbSearch, "Nhập tên hoặc SĐT...");
            txbSearch.TextChanged += (s, e) => LoadDataHocVien(txbSearch.Text);

            pnlSearch.Controls.Add(txbSearch);

            // Grid Học viên
            dgvHocVien = new DataGridView();
            StyleGrid(dgvHocVien);
            dgvHocVien.Dock = DockStyle.Fill;
            dgvHocVien.CellClick += DgvHocVien_CellClick;

            // Add vào GroupBox Trái (Grid Add trước, Search Add sau để Search nằm trên)
            grpLeft.Controls.Add(dgvHocVien);
            grpLeft.Controls.Add(pnlSearch);
            // Đảm bảo thứ tự hiển thị đúng
            pnlSearch.SendToBack();
            dgvHocVien.BringToFront();

            split.Panel1.Padding = new Padding(10, 10, 5, 10);
            split.Panel1.Controls.Add(grpLeft);

            // ========================================================
            // PANEL PHẢI: FORM ĐĂNG KÝ + LỊCH SỬ
            // ========================================================
            Panel pnlRightContent = new Panel { Dock = DockStyle.Fill };

            // 1. Group Đăng Ký (Phía trên)
            GroupBox grpReg = new GroupBox
            {
                Text = " 2. Thông Tin Đăng Ký ",
                Dock = DockStyle.Top,
                Height = 220,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 150, 243),
                BackColor = Color.White
            };

            Label lblMon = new Label { Text = "Môn Học:", Location = new Point(20, 40), AutoSize = true, Font = new Font("Segoe UI", 9) };
            Label lblLop = new Label { Text = "Lớp Học:", Location = new Point(260, 40), AutoSize = true, Font = new Font("Segoe UI", 9) };
            Label lblTien = new Label { Text = "Học Phí:", Location = new Point(500, 40), AutoSize = true, Font = new Font("Segoe UI", 9) };

            cbKyNang = new ComboBox { Location = new Point(20, 65), Width = 220, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 11), ItemHeight = 30 };
            cbLopHoc = new ComboBox { Location = new Point(260, 65), Width = 220, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 11) };
            lblHocPhi = new Label { Text = "0 VNĐ", Location = new Point(500, 65), AutoSize = true, Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.Red };

            btnDangKy = new Button { Text = "XÁC NHẬN ĐĂNG KÝ", Location = new Point(500, 120), Size = new Size(220, 45), BackColor = Color.FromArgb(40, 167, 69), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11, FontStyle.Bold), Cursor = Cursors.Hand };
            btnDangKy.FlatAppearance.BorderSize = 0;

            grpReg.Controls.AddRange(new Control[] { lblMon, lblLop, lblTien, cbKyNang, cbLopHoc, lblHocPhi, btnDangKy });

            // 2. Group Lịch Sử (Phía dưới)
            GroupBox grpList = new GroupBox
            {
                Text = " 3. Danh sách môn đã đăng ký ",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.DimGray,
                BackColor = Color.White
            };
            grpList.Padding = new Padding(10);

            dgvDaDangKy = new DataGridView();
            StyleGrid(dgvDaDangKy);
            dgvDaDangKy.CellContentClick += DgvDaDangKy_CellContentClick;
            grpList.Controls.Add(dgvDaDangKy);

            // Add vào Panel Phải
            pnlRightContent.Controls.Add(grpList);
            pnlRightContent.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 10 });
            pnlRightContent.Controls.Add(grpReg);

            split.Panel2.Padding = new Padding(5, 10, 10, 10);
            split.Panel2.Controls.Add(pnlRightContent);

            // ========================================================
            // ADD VÀO FORM & XỬ LÝ LOAD
            // ========================================================
            this.Controls.Add(split);
            this.Controls.Add(pnlHeader);

            pnlHeader.SendToBack();
            split.BringToFront();

            // SỰ KIỆN LOAD ĐỂ CHỈNH KÍCH THƯỚC CỘT (FIX LỖI KHÔNG ĐỔI KÍCH THƯỚC)
            this.Load += (s, e) =>
            {
                split.Panel1MinSize = 0;
                split.Panel2MinSize = 0;
                split.SplitterDistance = 450; // Chỉnh độ rộng cột trái tại đây (450 là vừa đẹp)
            };

            // Gán sự kiện
            cbKyNang.SelectedIndexChanged += CbKyNang_SelectedIndexChanged;
            btnDangKy.Click += BtnDangKy_Click;
        }

        // =========================================================================
        // 3. LOGIC XỬ LÝ (ĐÃ CẬP NHẬT TRẠNG THÁI TỰ ĐỘNG)
        // =========================================================================

        void LoadDataHocVien(string keyword)
        {
            if (keyword == "Nhập tên hoặc SĐT...") keyword = "";
            // [REFACTOR] Use HocVienBUS
            DataTable dt = HocVienBUS.Instance.GetListHocVien();
            if (!string.IsNullOrEmpty(keyword))
            {
                dt.DefaultView.RowFilter = string.Format("MaHV LIKE '%{0}%' OR HoTen LIKE '%{0}%' OR SDT LIKE '%{0}%'", keyword);
                dt = dt.DefaultView.ToTable();
            }
            dgvHocVien.DataSource = dt;
            
            // Header Mapping
            if (dgvHocVien.Columns.Contains("MaHV")) dgvHocVien.Columns["MaHV"].HeaderText = "Mã HV";
            if (dgvHocVien.Columns.Contains("HoTen")) dgvHocVien.Columns["HoTen"].HeaderText = "Họ Tên";
            if (dgvHocVien.Columns.Contains("SDT")) dgvHocVien.Columns["SDT"].HeaderText = "Số ĐT";
            if (dgvHocVien.Columns.Contains("GioiTinh")) dgvHocVien.Columns["GioiTinh"].HeaderText = "Giới Tính";
            if (dgvHocVien.Columns.Contains("TrangThai")) dgvHocVien.Columns["TrangThai"].HeaderText = "Trạng Thái";

            string[] hide = { "NgaySinh", "Email", "DiaChi", "NgayGiaNhap", "MaLop", "MaKyNang" };
            foreach (string c in hide) if (dgvHocVien.Columns.Contains(c)) dgvHocVien.Columns[c].Visible = false;
        }

        private void DgvHocVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow r = dgvHocVien.Rows[e.RowIndex];
                currentMaHV = r.Cells["MaHV"].Value.ToString();

                // Cập nhật tiêu đề GroupBox
                GroupBox grp = (GroupBox)btnDangKy.Parent;
                grp.Text = string.Format(" 2. Đăng Ký Cho: {0} ({1})", r.Cells["HoTen"].Value.ToString().ToUpper(), currentMaHV);

                LoadDanhSachDaDangKy();
            }
        }

        void LoadDanhSachDaDangKy()
        {
            // [REFACTOR] Use TuitionBUS
            dgvDaDangKy.DataSource = TuitionBUS.Instance.GetListDangKy(currentMaHV);

            // Thêm nút Hủy nếu chưa có
            if (!dgvDaDangKy.Columns.Contains("btnHuy"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Name = "btnHuy"; btn.HeaderText = ""; btn.Text = "Hủy";
                btn.UseColumnTextForButtonValue = true; btn.FlatStyle = FlatStyle.Flat;
                btn.DefaultCellStyle.BackColor = Color.IndianRed; btn.DefaultCellStyle.ForeColor = Color.White;
                dgvDaDangKy.Columns.Add(btn);
            }

            // Format tiền
            if (dgvDaDangKy.Columns.Contains("HocPhiLop"))
            {
                dgvDaDangKy.Columns["HocPhiLop"].DefaultCellStyle.Format = "N0";
                dgvDaDangKy.Columns["HocPhiLop"].HeaderText = "Học Phí";
            }

            string[] hide = { "MaLop", "NgayDangKy", "MaHV" };
            foreach (string c in hide) if (dgvDaDangKy.Columns.Contains(c)) dgvDaDangKy.Columns[c].Visible = false;
        }

        private void DgvDaDangKy_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvDaDangKy.Columns[e.ColumnIndex].Name == "btnHuy")
            {
                string maLop = dgvDaDangKy.Rows[e.RowIndex].Cells["MaLop"].Value.ToString();
                string tenLop = dgvDaDangKy.Rows[e.RowIndex].Cells["TenLop"].Value.ToString();

                // [REFACTOR] Use TuitionBUS
                if (TuitionBUS.Instance.HuyDangKy(currentMaHV, maLop))
                {
                    // Đã sửa lỗi trong DAO (gọi EXEC params đúng), không cần gọi thủ công ở đây nữa.
                    MessageBox.Show("Đã hủy thành công.");
                    LoadDanhSachDaDangKy();
                    LoadDataHocVien(txbSearch.Text); // Refresh lại danh sách
                }
                else MessageBox.Show("Lỗi: Không thể hủy lớp này.");
            }
        }

        void LoadKyNang()
        {
            // [REFACTOR] Use KyNangBUS
            cbKyNang.DataSource = KyNangBUS.Instance.GetListKyNang();
            cbKyNang.DisplayMember = "TenKyNang"; cbKyNang.ValueMember = "MaKyNang";
        }

        private void CbKyNang_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView row = cbKyNang.SelectedItem as DataRowView;
            if (cbKyNang.SelectedValue != null && row != null)
            {
                decimal hp = row["HocPhi"] != DBNull.Value ? Convert.ToDecimal(row["HocPhi"]) : 0;
                lblHocPhi.Text = hp.ToString("N0") + " VNĐ"; lblHocPhi.Tag = hp;

                string maKN = row["MaKyNang"].ToString();

                // [REFACTOR] Use LopHocBUS instead of DAO
                DataTable dtLop = LopHocBUS.Instance.GetListLopByKyNang(maKN);
                DataView dvLop = new DataView(dtLop);
                dvLop.RowFilter = "TrangThai = 'Sắp mở' OR TrangThai = 'Đang học'";
                cbLopHoc.DataSource = dvLop; cbLopHoc.DisplayMember = "TenLop"; cbLopHoc.ValueMember = "MaLop";
            }
        }

        private void BtnDangKy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaHV)) { MessageBox.Show("Vui lòng chọn học viên trước!"); return; }
            if (cbLopHoc.SelectedValue == null) { MessageBox.Show("Vui lòng chọn lớp học!"); return; }
            if (lblHocPhi.Tag == null) return;

            decimal hp = 0; decimal.TryParse(lblHocPhi.Tag.ToString(), out hp);

            // [REFACTOR] Use TuitionBUS instead of DAO
            if (TuitionBUS.Instance.DangKyLop(currentMaHV, cbLopHoc.SelectedValue.ToString(), hp))
            {
                // Đã sửa lỗi trong DAO (gọi EXEC params đúng), không cần gọi thủ công ở đây nữa.
                MessageBox.Show("Đăng ký thành công!");
                LoadDanhSachDaDangKy();
                LoadDataHocVien(txbSearch.Text); // Refresh lại danh sách
            }
            else
            {
                MessageBox.Show("Học viên đã có trong lớp này rồi!");
            }
        }

        // --- STYLE & HELPER ---
        public void AutoSelectStudent(string maHV)
        {
            txbSearch.Text = maHV;
            txbSearch.ForeColor = Color.Black;
        }

        private void StyleGrid(DataGridView dgv)
        {
            dgv.Dock = DockStyle.Fill; dgv.BackgroundColor = Color.White; dgv.BorderStyle = BorderStyle.None;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true; dgv.AllowUserToAddRows = false; dgv.RowHeadersVisible = false;
            dgv.ColumnHeadersHeight = 40; dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void SetPlaceholder(TextBox txt, string holder)
        {
            txt.Text = holder; txt.ForeColor = Color.Gray;
            txt.Enter += (s, e) => { if (txt.Text == holder) { txt.Text = ""; txt.ForeColor = Color.Black; } };
            txt.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txt.Text)) { txt.Text = holder; txt.ForeColor = Color.Gray; } };
        }
    }
}