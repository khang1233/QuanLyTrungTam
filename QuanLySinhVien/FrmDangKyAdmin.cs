using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using QuanLyTrungTam.DAO;

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
            SetupUI();            // 1. Vẽ giao diện chuẩn
            LoadDataHocVien(""); // 2. Load dữ liệu ban đầu
            LoadKyNang();        // 3. Load môn học
        }

        // =========================================================================
        // 2. THIẾT KẾ GIAO DIỆN (ĐÃ FIX LỖI BỊ CHE VÀ LỆCH)
        // =========================================================================
        private void SetupUI()
        {
            // --- CẤU HÌNH FORM ---
            this.Controls.Clear();
            this.FormBorderStyle = FormBorderStyle.None; // Tắt viền
            this.Dock = DockStyle.Fill;                  // Fill đầy Dashboard
            this.BackColor = Color.White;

            // --- A. HEADER (THANH TIÊU ĐỀ) ---
            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(0, 121, 107),
                Padding = new Padding(20, 15, 0, 0) // Căn lề chữ
            };
            Label lblTitle = new Label
            {
                Text = "QUẢN LÝ ĐĂNG KÝ & HỦY MÔN",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblTitle);

            // --- B. NỘI DUNG CHÍNH (SPLIT CONTAINER) ---
            SplitContainer split = new SplitContainer
            {
                Dock = DockStyle.Fill, // Tự động lấp đầy phần còn lại
                BackColor = Color.WhiteSmoke,
                SplitterWidth = 10,      // Khoảng cách giữa 2 panel
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

            // 2. Panel chứa ô tìm kiếm
            Panel pnlSearch = new Panel
            {
                Dock = DockStyle.Top,
                Height = 55,
                Padding = new Padding(5, 10, 5, 10),
                BackColor = Color.White // Đảm bảo nền trắng
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

            // 3. Grid Học viên
            dgvHocVien = new DataGridView();
            StyleGrid(dgvHocVien);
            dgvHocVien.Dock = DockStyle.Fill;
            dgvHocVien.CellClick += DgvHocVien_CellClick;

            // --- [FIX QUAN TRỌNG Ở ĐÂY] ---
            // Add thằng Grid vào TRƯỚC (Nó sẽ nằm ở Index 0 - Lớp trên cùng tạm thời)
            grpLeft.Controls.Add(dgvHocVien);
            // Add thằng Search vào SAU (Nó sẽ nằm ở Index 0, đẩy Grid xuống Index 1)
            grpLeft.Controls.Add(pnlSearch);

            // --- CHỐT HẠ LAYOUT ---
            // Trong WinForms: Thằng nào nằm dưới cùng (SendToBack) sẽ được Dock tính toán TRƯỚC.
            // 1. Ta muốn pnlSearch chiếm chỗ Dock.Top TRƯỚC.
            pnlSearch.SendToBack();
            // 2. Sau đó dgvHocVien mới Fill vào phần còn lại.
            dgvHocVien.BringToFront();

            // Add Group vào Split Panel 1
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
                ForeColor = Color.FromArgb(0, 121, 107), // Màu xanh cho đẹp
                BackColor = Color.White
            };

            Label lblMon = new Label { Text = "Môn Học:", Location = new Point(20, 40), AutoSize = true, Font = new Font("Segoe UI", 9) };
            Label lblLop = new Label { Text = "Lớp Học:", Location = new Point(300, 40), AutoSize = true, Font = new Font("Segoe UI", 9) };
            Label lblTien = new Label { Text = "Học Phí:", Location = new Point(580, 40), AutoSize = true, Font = new Font("Segoe UI", 9) };

            cbKyNang = new ComboBox { Location = new Point(20, 65), Width = 250, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 11), ItemHeight = 30 };
            cbLopHoc = new ComboBox { Location = new Point(300, 65), Width = 250, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 11) };
            lblHocPhi = new Label { Text = "0 VNĐ", Location = new Point(580, 65), AutoSize = true, Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.Red };

            btnDangKy = new Button { Text = "XÁC NHẬN ĐĂNG KÝ", Location = new Point(580, 120), Size = new Size(220, 45), BackColor = Color.Orange, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11, FontStyle.Bold), Cursor = Cursors.Hand };
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
            pnlRightContent.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 10 }); // Khoảng cách giữa 2 group
            pnlRightContent.Controls.Add(grpReg);

            split.Panel2.Padding = new Padding(5, 10, 10, 10);
            split.Panel2.Controls.Add(pnlRightContent);


            // ========================================================
            // [QUAN TRỌNG] THỨ TỰ ADD VÀO FORM ĐỂ KHÔNG BỊ LỆCH
            // ========================================================
            // Add SplitContainer trước (Nó sẽ nằm đè lên Header nếu không chỉnh)
            this.Controls.Add(split);
            // Add Header sau
            this.Controls.Add(pnlHeader);

            // --- MẤU CHỐT SỬA LỖI: ---
            // Đẩy Header xuống dưới cùng của ngăn xếp Z-Order -> Nó sẽ được Dock Top ĐẦU TIÊN
            pnlHeader.SendToBack();
            // Đẩy Split lên trên cùng Z-Order -> Nó sẽ Fill vào PHẦN CÒN LẠI (Nằm dưới Header)
            split.BringToFront();

            split.SplitterDistance = 420; // Chỉnh độ rộng cột trái

            // Gán sự kiện
            cbKyNang.SelectedIndexChanged += CbKyNang_SelectedIndexChanged;
            btnDangKy.Click += BtnDangKy_Click;
        }

        // =========================================================================
        // 3. LOGIC XỬ LÝ (ĐÃ FIX LỖI HIỂN THỊ LỚP VÀ CẬP NHẬT TRẠNG THÁI)
        // =========================================================================

        void LoadDataHocVien(string keyword)
        {
            if (keyword == "Nhập tên hoặc SĐT...") keyword = "";
            DataTable dt = HocVienDAO.Instance.GetListHocVien();
            if (!string.IsNullOrEmpty(keyword))
            {
                dt.DefaultView.RowFilter = string.Format("MaHV LIKE '%{0}%' OR HoTen LIKE '%{0}%' OR SDT LIKE '%{0}%'", keyword);
                dt = dt.DefaultView.ToTable();
            }
            dgvHocVien.DataSource = dt;
            string[] hide = { "NgaySinh", "Email", "DiaChi", "NgayGiaNhap", "MaLop", "MaKyNang" };
            foreach (string c in hide) if (dgvHocVien.Columns.Contains(c)) dgvHocVien.Columns[c].Visible = false;
        }

        private void DgvHocVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow r = dgvHocVien.Rows[e.RowIndex];
                currentMaHV = r.Cells["MaHV"].Value.ToString();
                GroupBox grp = (GroupBox)btnDangKy.Parent;
                grp.Text = $" 2. Đăng Ký Cho: {r.Cells["HoTen"].Value.ToString().ToUpper()}";
                LoadDanhSachDaDangKy();
            }
        }

        void LoadDanhSachDaDangKy()
        {
            dgvDaDangKy.DataSource = TuitionDAO.Instance.GetListDangKy(currentMaHV);
            if (!dgvDaDangKy.Columns.Contains("btnHuy"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Name = "btnHuy"; btn.HeaderText = ""; btn.Text = "Hủy";
                btn.UseColumnTextForButtonValue = true; btn.FlatStyle = FlatStyle.Flat;
                btn.DefaultCellStyle.BackColor = Color.IndianRed; btn.DefaultCellStyle.ForeColor = Color.White;
                dgvDaDangKy.Columns.Add(btn);
            }
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
                if (MessageBox.Show($"Hủy lớp {dgvDaDangKy.Rows[e.RowIndex].Cells["TenLop"].Value}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (TuitionDAO.Instance.HuyDangKy(currentMaHV, maLop)) { MessageBox.Show("Đã hủy."); LoadDanhSachDaDangKy(); }
                    else MessageBox.Show("Lỗi hủy lớp.");
                }
            }
        }

        void LoadKyNang()
        {
            cbKyNang.DataSource = KyNangDAO.Instance.GetListKyNang();
            cbKyNang.DisplayMember = "TenKyNang"; cbKyNang.ValueMember = "MaKyNang";
        }

        private void CbKyNang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbKyNang.SelectedValue != null && cbKyNang.SelectedItem is DataRowView row)
            {
                decimal hp = row["HocPhi"] != DBNull.Value ? Convert.ToDecimal(row["HocPhi"]) : 0;
                lblHocPhi.Text = hp.ToString("N0") + " VNĐ"; lblHocPhi.Tag = hp;

                string maKN = row["MaKyNang"].ToString();
                DataTable dtLop = LopHocDAO.Instance.GetListLopByKyNang(maKN);
                DataView dvLop = new DataView(dtLop);

                // [FIX LỖI 1]: Thêm 'Đang tuyển sinh' vào bộ lọc để hiện lớp mới tạo
                dvLop.RowFilter = "TrangThai = 'Sắp mở' OR TrangThai = 'Đang học' OR TrangThai = 'Đang tuyển sinh'";

                cbLopHoc.DataSource = dvLop; cbLopHoc.DisplayMember = "TenLop"; cbLopHoc.ValueMember = "MaLop";
            }
        }

        private void BtnDangKy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaHV)) { MessageBox.Show("Chưa chọn học viên!"); return; }
            if (cbLopHoc.SelectedValue == null) { MessageBox.Show("Chưa chọn lớp!"); return; }
            if (lblHocPhi.Tag == null) return;

            string maLop = cbLopHoc.SelectedValue.ToString();
            decimal hp = 0; decimal.TryParse(lblHocPhi.Tag.ToString(), out hp);

            // 1. Thực hiện đăng ký
            if (TuitionDAO.Instance.DangKyLop(currentMaHV, maLop, hp))
            {
                // [FIX LỖI 2]: Cập nhật trạng thái lớp thành 'Đang học'
                LopHocDAO.Instance.UpdateTrangThaiLop(maLop, "Đang học");

                MessageBox.Show("Đăng ký thành công!\nLớp học đã được chuyển trạng thái sang 'Đang học'.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachDaDangKy();
            }
            else
            {
                MessageBox.Show("Học viên đã có trong lớp này!", "Trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // --- STYLE & HELPER ---
        public void AutoSelectStudent(string maHV) { txbSearch.Text = maHV; txbSearch.ForeColor = Color.Black; }

        private void StyleGrid(DataGridView dgv)
        {
            dgv.Dock = DockStyle.Fill; dgv.BackgroundColor = Color.White; dgv.BorderStyle = BorderStyle.None;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true; dgv.AllowUserToAddRows = false; dgv.RowHeadersVisible = false;
            dgv.ColumnHeadersHeight = 40; dgv.EnableHeadersVisualStyles = false;
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