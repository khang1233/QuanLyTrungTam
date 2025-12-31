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
        // 1. KHAI BÁO CÁC CONTROL & BIẾN
        // =========================================================================
        private TextBox txbSearch;
        private DataGridView dgvHocVien;

        private ComboBox cbKyNang;
        private ComboBox cbLopHoc;
        private Label lblHocPhi;
        private Button btnDangKy;
        private DataGridView dgvDaDangKy;

        private string currentMaHV = ""; // Lưu mã học viên đang được chọn

        public FrmDangKyAdmin()
        {
            SetupUI(); // Vẽ giao diện
            LoadDataHocVien(""); // Load danh sách học viên
            LoadKyNang(); // Load danh sách môn học vào ComboBox
        }

        // =========================================================================
        // 2. THIẾT KẾ GIAO DIỆN (Code tay - Không dùng Designer)
        // =========================================================================
        private void SetupUI()
        {
            this.Text = "Quản Lý Đăng Ký & Hủy Môn";
            this.BackColor = Color.White;
            this.Size = new Size(1250, 750);

            // --- A. HEADER ---
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.FromArgb(0, 121, 107), Padding = new Padding(15) };
            Label lblTitle = new Label { Text = "QUẢN LÝ ĐĂNG KÝ HỦY MÔN", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(20, 15) };
            pnlHeader.Controls.Add(lblTitle);
            this.Controls.Add(pnlHeader);

            // --- B. SPLIT CONTAINER (Chia màn hình làm 2 phần) ---
            SplitContainer split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke,
                SplitterWidth = 8,
                FixedPanel = FixedPanel.Panel1
            };

            // >>> PANEL TRÁI: DANH SÁCH HỌC VIÊN
            GroupBox grpLeft = new GroupBox { Text = " 1. Chọn Học Viên ", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DimGray, BackColor = Color.White };
            grpLeft.Padding = new Padding(10);

            txbSearch = new TextBox { Dock = DockStyle.Top, Font = new Font("Segoe UI", 11), Height = 35 };
            SetPlaceholder(txbSearch, "Nhập tên hoặc số điện thoại...");
            txbSearch.TextChanged += (s, e) => LoadDataHocVien(txbSearch.Text); // Tìm kiếm ngay khi gõ

            dgvHocVien = new DataGridView();
            StyleGrid(dgvHocVien);
            dgvHocVien.CellClick += DgvHocVien_CellClick; // Sự kiện chọn học viên

            grpLeft.Controls.Add(dgvHocVien);
            grpLeft.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 10 }); // Khoảng cách
            grpLeft.Controls.Add(txbSearch);

            split.Panel1.Padding = new Padding(10);
            split.Panel1.Controls.Add(grpLeft);

            // >>> PANEL PHẢI: FORM ĐĂNG KÝ
            Panel pnlRight = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20, 20, 10, 10) // Căn lề cho thoáng
            };

            // 1. Group Đăng Ký (Phần trên)
            GroupBox grpReg = new GroupBox
            {
                Text = " 2. Đăng Ký Mới ",
                Dock = DockStyle.Top,
                Height = 220, // Chiều cao cố định cho khu vực nhập liệu
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.Red,
                BackColor = Color.White
            };

            // Label
            Label lblMon = new Label { Text = "Môn Học:", Location = new Point(20, 40), AutoSize = true, Font = new Font("Segoe UI", 9) };
            Label lblLop = new Label { Text = "Lớp Học:", Location = new Point(300, 40), AutoSize = true, Font = new Font("Segoe UI", 9) };
            Label lblTien = new Label { Text = "Học Phí:", Location = new Point(580, 40), AutoSize = true, Font = new Font("Segoe UI", 9) };

            // Control
            cbKyNang = new ComboBox { Location = new Point(20, 65), Width = 250, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
            cbLopHoc = new ComboBox { Location = new Point(300, 65), Width = 250, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
            lblHocPhi = new Label { Text = "0 VNĐ", Location = new Point(580, 65), AutoSize = true, Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.Red };

            // Nút Đăng Ký
            btnDangKy = new Button { Text = "XÁC NHẬN ĐĂNG KÝ", Location = new Point(580, 120), Size = new Size(200, 45), BackColor = Color.Orange, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Cursor = Cursors.Hand };
            btnDangKy.FlatAppearance.BorderSize = 0;

            grpReg.Controls.AddRange(new Control[] { lblMon, lblLop, lblTien, cbKyNang, cbLopHoc, lblHocPhi, btnDangKy });

            // 2. Group Lịch Sử (Phần dưới)
            GroupBox grpList = new GroupBox { Text = " 3. Danh sách môn đã đăng ký (Nhấn nút Đỏ để Hủy) ", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DimGray, BackColor = Color.White };
            grpList.Padding = new Padding(10);

            dgvDaDangKy = new DataGridView();
            StyleGrid(dgvDaDangKy);
            dgvDaDangKy.CellContentClick += DgvDaDangKy_CellContentClick; // Sự kiện nút Hủy
            grpList.Controls.Add(dgvDaDangKy);

            // Add vào Panel phải
            pnlRight.Controls.Add(grpList);
            pnlRight.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 10 });
            pnlRight.Controls.Add(grpReg);

            split.Panel2.Controls.Add(pnlRight);
            this.Controls.Add(split);
            pnlHeader.BringToFront();

            split.SplitterDistance = 400; // Độ rộng cột danh sách học viên

            // Gán sự kiện logic
            cbKyNang.SelectedIndexChanged += CbKyNang_SelectedIndexChanged;
            btnDangKy.Click += BtnDangKy_Click;
        }

        // =========================================================================
        // 3. LOGIC XỬ LÝ (Load Data, Click, Register, Cancel)
        // =========================================================================

        // Load danh sách học viên
        void LoadDataHocVien(string keyword)
        {
            if (keyword == "Nhập tên hoặc số điện thoại...") keyword = "";

            DataTable dt = HocVienDAO.Instance.GetListHocVien();
            if (!string.IsNullOrEmpty(keyword))
            {
                dt.DefaultView.RowFilter = string.Format("MaHV LIKE '%{0}%' OR HoTen LIKE '%{0}%' OR SDT LIKE '%{0}%'", keyword);
                dt = dt.DefaultView.ToTable();
            }
            dgvHocVien.DataSource = dt;

            // Ẩn các cột không cần thiết cho gọn
            string[] hide = { "NgaySinh", "Email", "DiaChi", "NgayGiaNhap", "MaLop", "MaKyNang" };
            foreach (string c in hide) if (dgvHocVien.Columns.Contains(c)) dgvHocVien.Columns[c].Visible = false;
        }

        // Khi click chọn học viên
        private void DgvHocVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow r = dgvHocVien.Rows[e.RowIndex];
                currentMaHV = r.Cells["MaHV"].Value.ToString();

                // Cập nhật giao diện tiêu đề
                GroupBox grp = (GroupBox)btnDangKy.Parent;
                grp.Text = $" 2. Đăng Ký Cho: {r.Cells["HoTen"].Value.ToString().ToUpper()} ({currentMaHV})";

                LoadDanhSachDaDangKy(); // Load lịch sử đăng ký của người này
            }
        }

        // Load danh sách các môn ĐÃ đăng ký
        void LoadDanhSachDaDangKy()
        {
            dgvDaDangKy.DataSource = TuitionDAO.Instance.GetListDangKy(currentMaHV);

            // Thêm nút Hủy nếu chưa có
            if (!dgvDaDangKy.Columns.Contains("btnHuy"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Name = "btnHuy"; btn.HeaderText = ""; btn.Text = "Hủy Lớp";
                btn.UseColumnTextForButtonValue = true; btn.FlatStyle = FlatStyle.Flat;
                btn.DefaultCellStyle.BackColor = Color.IndianRed; btn.DefaultCellStyle.ForeColor = Color.White;
                dgvDaDangKy.Columns.Add(btn);
            }

            // Định dạng tiền tệ
            if (dgvDaDangKy.Columns.Contains("HocPhiLop"))
            {
                dgvDaDangKy.Columns["HocPhiLop"].DefaultCellStyle.Format = "N0";
                dgvDaDangKy.Columns["HocPhiLop"].HeaderText = "Học Phí";
            }

            // Ẩn cột thừa
            string[] hide = { "MaLop", "NgayDangKy", "MaHV" };
            foreach (string c in hide) if (dgvDaDangKy.Columns.Contains(c)) dgvDaDangKy.Columns[c].Visible = false;
        }

        // Xử lý sự kiện HỦY LỚP (Click nút đỏ trong Grid)
        private void DgvDaDangKy_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvDaDangKy.Columns[e.ColumnIndex].Name == "btnHuy")
            {
                string tenLop = dgvDaDangKy.Rows[e.RowIndex].Cells["TenLop"].Value.ToString();
                string maLop = dgvDaDangKy.Rows[e.RowIndex].Cells["MaLop"].Value.ToString();

                if (MessageBox.Show($"Hủy lớp {tenLop}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (TuitionDAO.Instance.HuyDangKy(currentMaHV, maLop))
                    {
                        MessageBox.Show("Đã hủy thành công.");
                        LoadDanhSachDaDangKy(); // Refresh lại lưới
                    }
                    else MessageBox.Show("Lỗi: Không thể hủy lớp này!");
                }
            }
        }

        // Load danh sách kỹ năng (Môn học)
        void LoadKyNang()
        {
            cbKyNang.DataSource = KyNangDAO.Instance.GetListKyNang();
            cbKyNang.DisplayMember = "TenKyNang";
            cbKyNang.ValueMember = "MaKyNang";
        }

        // Khi chọn Môn -> Tự động load Lớp tương ứng & Hiển thị học phí
        private void CbKyNang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbKyNang.SelectedValue != null)
            {
                DataRowView row = cbKyNang.SelectedItem as DataRowView;
                if (row != null)
                {
                    // 1. Hiển thị học phí
                    decimal hp = row["HocPhi"] != DBNull.Value ? Convert.ToDecimal(row["HocPhi"]) : 0;
                    lblHocPhi.Text = hp.ToString("N0") + " VNĐ";
                    lblHocPhi.Tag = hp; // Lưu giá trị gốc để tính toán

                    // 2. Load lớp học, CHỈ LẤY LỚP SẮP MỞ hoặc ĐANG HỌC
                    string maKN = row["MaKyNang"].ToString();
                    DataTable dtLop = LopHocDAO.Instance.GetListLopByKyNang(maKN);

                    DataView dvLop = new DataView(dtLop);
                    dvLop.RowFilter = "TrangThai = 'Sắp mở' OR TrangThai = 'Đang học'";

                    cbLopHoc.DataSource = dvLop;
                    cbLopHoc.DisplayMember = "TenLop";
                    cbLopHoc.ValueMember = "MaLop";
                }
            }
        }

        // Xử lý sự kiện ĐĂNG KÝ
        private void BtnDangKy_Click(object sender, EventArgs e)
        {
            // 1. Validate
            if (string.IsNullOrEmpty(currentMaHV)) { MessageBox.Show("Vui lòng chọn học viên trước!"); return; }
            if (cbLopHoc.SelectedValue == null) { MessageBox.Show("Vui lòng chọn lớp học!"); return; }

            // 2. Lấy dữ liệu
            string maLop = cbLopHoc.SelectedValue.ToString();
            decimal hp = Convert.ToDecimal(lblHocPhi.Tag);

            // 3. Gọi DAO thực hiện
            if (TuitionDAO.Instance.DangKyLop(currentMaHV, maLop, hp))
            {
                MessageBox.Show("Đăng ký thành công!");
                LoadDanhSachDaDangKy();
            }
            else MessageBox.Show("Học viên đã có trong lớp này rồi!");
        }

        // --- HÀM HỖ TRỢ ---

        // Hàm gọi từ Form khác để tự động chọn học viên (Ví dụ từ Dashboard chuyển sang)
        public void AutoSelectStudent(string maHV)
        {
            txbSearch.Text = maHV;
            txbSearch.ForeColor = Color.Black; // Reset placeholder color
            LoadDataHocVien(maHV);

            if (dgvHocVien.Rows.Count > 0)
            {
                dgvHocVien.Rows[0].Selected = true;
                DgvHocVien_CellClick(null, new DataGridViewCellEventArgs(0, 0));
            }
        }

        private void StyleGrid(DataGridView dgv)
        {
            dgv.Dock = DockStyle.Fill;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false;
            dgv.ColumnHeadersHeight = 35;
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