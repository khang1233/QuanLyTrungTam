    using System;
    using System.Drawing;
    using System.Data;
    using System.Windows.Forms;
    using QuanLyTrungTam.DAO;

    namespace QuanLyTrungTam
    {
        public partial class FrmDangKyAdmin : Form
        {
            // --- CONTROL GIAO DIỆN ---
            private TextBox txbSearch = new TextBox();
            private DataGridView dgvHocVien = new DataGridView();

            private ComboBox cbKyNang = new ComboBox();
            private ComboBox cbLopHoc = new ComboBox();
            private Label lblHocPhi = new Label();
            private Button btnDangKy = new Button();
            private DataGridView dgvDaDangKy = new DataGridView();

            private string currentMaHV = "";
            private string currentTenHV = "";

            public FrmDangKyAdmin()
            {
                // InitializeComponent(); // Bỏ comment nếu dùng Designer, code tay thì tắt
                SetupBalancedUI();
                LoadDataHocVien("");
                LoadKyNang();
            }

            // =================================================================================
            // 1. THIẾT KẾ GIAO DIỆN (RESPONSIVE - DOCKING - AUTOSCROLL)
            // =================================================================================
            private void SetupBalancedUI()
            {
                // 1. Cấu hình Form chính
                this.Text = "Quản Lý Đăng Ký Tuyển Sinh";
                this.BackColor = Color.WhiteSmoke;
                this.WindowState = FormWindowState.Maximized;
                this.Font = new Font("Segoe UI", 10F);

                // [QUAN TRỌNG] Thiết lập kích thước tối thiểu để Form không bị kéo quá bé
                this.MinimumSize = new Size(1000, 600);

                // 2. HEADER
                Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = ColorTranslator.FromHtml("#00796B"), Padding = new Padding(20) };
                Label lblTitle = new Label { Text = "QUẢN LÝ ĐĂNG KÝ HỦY MÔN", Font = new Font("Segoe UI", 18, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(20, 20) };
                pnlHeader.Controls.Add(lblTitle);

                // 3. KHỞI TẠO SPLIT CONTAINER (Chia màn hình Trái - Phải)
                SplitContainer split = new SplitContainer { Dock = DockStyle.Fill, SplitterWidth = 8, BackColor = Color.LightGray };

                // Fix lỗi init kích thước
                split.Width = 1200;
                split.FixedPanel = FixedPanel.Panel1;
                split.Panel1MinSize = 350;
                split.SplitterDistance = 450;
                this.WindowState = FormWindowState.Maximized;
            // =========================================================================
            // A. CỘT TRÁI: DANH SÁCH HỌC VIÊN
            // =========================================================================
                 GroupBox grpLeft = new GroupBox { Text = " 1. Chọn Học Viên ", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.DimGray, BackColor = Color.White };
                grpLeft.Padding = new Padding(10);

                // Ô Tìm kiếm (Dock Top)
                txbSearch.Dock = DockStyle.Top; txbSearch.Font = new Font("Segoe UI", 12); txbSearch.Height = 35;
                SetPlaceholder(txbSearch, "🔍 Nhập tên hoặc số điện thoại...");
                txbSearch.TextChanged += (s, e) => LoadDataHocVien(txbSearch.Text);

                Panel spacerLeft = new Panel { Dock = DockStyle.Top, Height = 10, BackColor = Color.White };

                // Grid Học Viên (Dock Fill)
                StyleGrid(dgvHocVien);
                dgvHocVien.CellClick += DgvHocVien_CellClick;

                grpLeft.Controls.Add(dgvHocVien);
                grpLeft.Controls.Add(spacerLeft);
                grpLeft.Controls.Add(txbSearch);
                split.Panel1.Controls.Add(grpLeft);
                split.Panel1.Padding = new Padding(10);

                // =========================================================================
                // B. CỘT PHẢI: FORM ĐĂNG KÝ (FULL DOCKING & AUTO SCROLL)
                // =========================================================================
                Panel pnlRight = new Panel { Dock = DockStyle.Fill, BackColor = Color.WhiteSmoke };

                // [CỰC KỲ QUAN TRỌNG] Bật tính năng tự động cuộn khi thu nhỏ cửa sổ
                pnlRight.AutoScroll = true;

                // --- B1. FORM ĐĂNG KÝ (PHẦN TRÊN) ---
                GroupBox grpAction = new GroupBox
                {
                    Text = " 2. Đăng Ký Mới ",
                    Dock = DockStyle.Top,
                    Height = 260,
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    ForeColor = Color.DimGray,
                    BackColor = Color.White
                };
                // Đảm bảo GroupBox này không bao giờ bị bóp méo chiều cao khi thu nhỏ
                grpAction.MinimumSize = new Size(0, 260);

                // Sử dụng TableLayout để chia cột 60% - 40%
                TableLayoutPanel tblLayout = new TableLayoutPanel();
                tblLayout.Dock = DockStyle.Fill;
                tblLayout.ColumnCount = 2;
                tblLayout.RowCount = 1;
                tblLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F)); // Cột nhập liệu rộng hơn
                tblLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F)); // Cột nút bấm
                tblLayout.Padding = new Padding(10);

                // [[ Cột 1: Nhập liệu ]]
                Panel pnlCol1 = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, 0, 20, 0) };

                // Wrapper Môn Học (Dùng Panel Dock Top để chứa Label và Combo)
                Panel pnlMonHoc = new Panel { Dock = DockStyle.Top, Height = 70 };
                Label lblMon = new Label { Text = "Môn Học:", Dock = DockStyle.Top, Height = 25, Font = new Font("Segoe UI", 10, FontStyle.Regular), ForeColor = Color.Black };
                cbKyNang.Dock = DockStyle.Top; cbKyNang.Height = 35; cbKyNang.DropDownStyle = ComboBoxStyle.DropDownList; cbKyNang.Font = new Font("Segoe UI", 11);
                cbKyNang.SelectedIndexChanged += CbKyNang_SelectedIndexChanged;
                pnlMonHoc.Controls.Add(cbKyNang); pnlMonHoc.Controls.Add(lblMon); // Add Label trước rồi đến Combo vì Dock Top xếp chồng xuống

                // Wrapper Lớp Học
                Panel pnlLopHoc = new Panel { Dock = DockStyle.Top, Height = 70 };
                Label lblLop = new Label { Text = "Lớp Học:", Dock = DockStyle.Top, Height = 25, Font = new Font("Segoe UI", 10, FontStyle.Regular), ForeColor = Color.Black };
                cbLopHoc.Dock = DockStyle.Top; cbLopHoc.Height = 35; cbLopHoc.DropDownStyle = ComboBoxStyle.DropDownList; cbLopHoc.Font = new Font("Segoe UI", 11);
                pnlLopHoc.Controls.Add(cbLopHoc); pnlLopHoc.Controls.Add(lblLop);

                // Add các wrapper vào Cột 1
                // Mẹo: Khi dùng Dock=Top, cái nào Add SAU CÙNG sẽ nằm TRÊN CÙNG.
                // Hoặc dùng BringToFront để kiểm soát. Ở đây ta Add Môn Học trước, Lớp Học sau, rồi chỉnh thứ tự.
                pnlCol1.Controls.Add(pnlLopHoc);
                pnlCol1.Controls.Add(pnlMonHoc);
                pnlMonHoc.BringToFront(); // Đẩy Môn học lên trên cùng

                // [[ Cột 2: Giá & Nút ]]
                Panel pnlCol2 = new Panel { Dock = DockStyle.Fill };

                Label lblTieuDeGia = new Label { Text = "Học Phí:", Dock = DockStyle.Top, Height = 25, Font = new Font("Segoe UI", 10, FontStyle.Regular), ForeColor = Color.Black };
                lblHocPhi.Text = "0 VNĐ"; lblHocPhi.Dock = DockStyle.Top; lblHocPhi.Height = 45;
                lblHocPhi.Font = new Font("Segoe UI", 20, FontStyle.Bold); lblHocPhi.ForeColor = Color.Red;

                btnDangKy.Text = "XÁC NHẬN ĐĂNG KÝ";
                btnDangKy.Dock = DockStyle.Top; btnDangKy.Height = 50;
                btnDangKy.BackColor = ColorTranslator.FromHtml("#FFC107");
                btnDangKy.FlatStyle = FlatStyle.Flat; btnDangKy.FlatAppearance.BorderSize = 0;
                btnDangKy.Font = new Font("Segoe UI", 10, FontStyle.Bold); btnDangKy.ForeColor = Color.Black;
                btnDangKy.Cursor = Cursors.Hand;
                btnDangKy.Click += BtnDangKy_Click;

                Panel spacerBtn = new Panel { Dock = DockStyle.Top, Height = 10 };

                pnlCol2.Controls.Add(btnDangKy);
                pnlCol2.Controls.Add(spacerBtn);
                pnlCol2.Controls.Add(lblHocPhi);
                pnlCol2.Controls.Add(lblTieuDeGia);

                tblLayout.Controls.Add(pnlCol1, 0, 0);
                tblLayout.Controls.Add(pnlCol2, 1, 0);
                grpAction.Controls.Add(tblLayout);

                // --- B2. DANH SÁCH LỊCH SỬ (PHẦN DƯỚI) ---
                GroupBox grpHistory = new GroupBox
                {
                    Text = " Các lớp học viên này đã đăng ký (Bấm nút Đỏ để Hủy) ",
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    ForeColor = Color.DimGray,
                    BackColor = Color.White
                };
                // [QUAN TRỌNG] Đảm bảo bảng bên dưới luôn có chiều cao tối thiểu 200px
                // Nếu không đủ chỗ, thanh cuộn của pnlRight sẽ hiện ra
                grpHistory.MinimumSize = new Size(0, 200);
                grpHistory.Padding = new Padding(10);

                StyleGrid(dgvDaDangKy);
                dgvDaDangKy.CellContentClick += DgvDaDangKy_CellContentClick;
                grpHistory.Controls.Add(dgvDaDangKy);

                // --- B3. RÁP VÀO CỘT PHẢI ---
                // Add theo thứ tự để Dock hoạt động đúng:
                pnlRight.Controls.Add(grpHistory); // Dock Fill (Nằm dưới/Chiếm hết chỗ còn lại)
                pnlRight.Controls.Add(grpAction);  // Dock Top (Nằm trên)

                // Đảm bảo grpAction được ưu tiên xếp trước (Dock Top)
                grpAction.BringToFront();

                split.Panel2.Controls.Add(pnlRight);
                split.Panel2.Padding = new Padding(10);

                // 4. ADD VÀO FORM
                this.Controls.Add(split);
                this.Controls.Add(pnlHeader);

                pnlHeader.SendToBack(); // Header chìm xuống (được vẽ trước)
                split.BringToFront();   // Split nổi lên
            }

            // =================================================================================
            // CÁC HÀM HỖ TRỢ GIAO DIỆN
            // =================================================================================
            private void StyleGrid(DataGridView dgv)
            {
                dgv.Dock = DockStyle.Fill;
                dgv.BackgroundColor = Color.White;
                dgv.BorderStyle = BorderStyle.FixedSingle;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.RowHeadersVisible = false;
                dgv.AllowUserToAddRows = false;
                dgv.ReadOnly = true;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.RowTemplate.Height = 35;
                dgv.ColumnHeadersHeight = 40;
                dgv.EnableHeadersVisualStyles = false;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#00796B");
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
                dgv.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#B2DFDB");
                dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            }

            private void SetPlaceholder(TextBox txt, string holder)
            {
                txt.Text = holder; txt.ForeColor = Color.Gray;
                txt.Enter += (s, e) => { if (txt.Text == holder) { txt.Text = ""; txt.ForeColor = Color.Black; } };
                txt.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txt.Text)) { txt.Text = holder; txt.ForeColor = Color.Gray; } };
            }

            // =================================================================================
            // 2. LOGIC XỬ LÝ (GIỮ NGUYÊN)
            // =================================================================================

            void LoadDataHocVien(string keyword)
            {
                DataTable dt = HocVienDAO.Instance.GetListHocVien();
                if (dt == null) return;

                if (!string.IsNullOrEmpty(keyword) && keyword != "🔍 Nhập tên hoặc số điện thoại...")
                {
                    dt.DefaultView.RowFilter = string.Format("MaHV LIKE '%{0}%' OR HoTen LIKE '%{0}%' OR SoDienThoai LIKE '%{0}%'", keyword);
                }
                dgvHocVien.DataSource = dt;

                // Ẩn cột không cần thiết
                string[] hide = { "DiaChi", "NgaySinh", "Email", "NgayGiaNhap", "MaLop", "MaKyNang" };
                foreach (string c in hide) if (dgvHocVien.Columns.Contains(c)) dgvHocVien.Columns[c].Visible = false;

                if (dgvHocVien.Columns.Contains("MaHV")) { dgvHocVien.Columns["MaHV"].HeaderText = "Mã HV"; dgvHocVien.Columns["MaHV"].Width = 100; }
                if (dgvHocVien.Columns.Contains("HoTen")) { dgvHocVien.Columns["HoTen"].HeaderText = "Họ Tên"; dgvHocVien.Columns["HoTen"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; }
                if (dgvHocVien.Columns.Contains("SoDienThoai")) { dgvHocVien.Columns["SoDienThoai"].HeaderText = "SĐT"; dgvHocVien.Columns["SoDienThoai"].Width = 120; }

                // --- [FIX LỖI TRẮNG BẢNG] TỰ ĐỘNG CHỌN DÒNG ĐẦU TIÊN ---
                if (dgvHocVien.Rows.Count > 0)
                {
                    // 1. Chọn dòng đầu về mặt hiển thị
                    dgvHocVien.Rows[0].Selected = true;

                    // 2. [QUAN TRỌNG] Kích hoạt sự kiện CellClick bằng cơm (thủ công)
                    // Vì DataGridView không tự gọi sự kiện này khi mới load
                    DgvHocVien_CellClick(dgvHocVien, new DataGridViewCellEventArgs(0, 0));
                }
            }

            private void DgvHocVien_CellClick(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvHocVien.Rows[e.RowIndex];
                    currentMaHV = row.Cells["MaHV"].Value.ToString();
                    currentTenHV = row.Cells["HoTen"].Value.ToString();

                    // TÌM GROUPBOX CHA ĐỂ ĐỔI TÊN (Sử dụng vòng lặp an toàn)
                    Control parent = btnDangKy.Parent;
                    while (parent != null && !(parent is GroupBox))
                    {
                        parent = parent.Parent;
                    }

                    if (parent != null)
                    {
                        parent.Text = $" 2. Đăng ký cho: {currentTenHV.ToUpper()} ({currentMaHV}) ";
                        parent.ForeColor = ColorTranslator.FromHtml("#D32F2F");
                    }

                    LoadDanhSachDaDangKy();
                }
            }

            void LoadDanhSachDaDangKy()
            {
                dgvDaDangKy.DataSource = TuitionDAO.Instance.GetListDangKy(currentMaHV);

                if (dgvDaDangKy.Columns.Contains("colHuy")) dgvDaDangKy.Columns.Remove("colHuy");

                DataGridViewButtonColumn btnCancel = new DataGridViewButtonColumn();
                btnCancel.Name = "colHuy";
                btnCancel.HeaderText = "Thao tác";
                btnCancel.Text = "Hủy Đăng Ký";
                btnCancel.UseColumnTextForButtonValue = true;
                btnCancel.FlatStyle = FlatStyle.Flat;
                btnCancel.DefaultCellStyle.BackColor = Color.Red;
                btnCancel.DefaultCellStyle.ForeColor = Color.White;
                btnCancel.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);

                dgvDaDangKy.Columns.Add(btnCancel);

                if (dgvDaDangKy.Columns.Contains("TenKyNang")) dgvDaDangKy.Columns["TenKyNang"].HeaderText = "Môn Học";
                if (dgvDaDangKy.Columns.Contains("TenLop")) dgvDaDangKy.Columns["TenLop"].HeaderText = "Lớp";
                if (dgvDaDangKy.Columns.Contains("HocPhiLop"))
                {
                    dgvDaDangKy.Columns["HocPhiLop"].HeaderText = "Học Phí";
                    dgvDaDangKy.Columns["HocPhiLop"].DefaultCellStyle.Format = "N0";
                }
                if (dgvDaDangKy.Columns.Contains("NgayDangKy")) dgvDaDangKy.Columns["NgayDangKy"].Visible = false;
                if (dgvDaDangKy.Columns.Contains("MaLop")) dgvDaDangKy.Columns["MaLop"].Visible = false;
            }

            private void DgvDaDangKy_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {
                if (e.ColumnIndex == dgvDaDangKy.Columns["colHuy"].Index && e.RowIndex >= 0)
                {
                    string tenLop = dgvDaDangKy.Rows[e.RowIndex].Cells["TenLop"].Value.ToString();
                    string maLop = dgvDaDangKy.Rows[e.RowIndex].Cells["MaLop"].Value.ToString();

                    if (MessageBox.Show($"Bạn có chắc chắn muốn hủy đăng ký lớp: {tenLop}?", "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (TuitionDAO.Instance.HuyDangKy(currentMaHV, maLop))
                        {
                            MessageBox.Show("Đã hủy đăng ký thành công!");
                            LoadDanhSachDaDangKy();
                        }
                        else
                        {
                            MessageBox.Show("Lỗi khi hủy đăng ký. Vui lòng thử lại.");
                        }
                    }
                }
            }

            void LoadKyNang()
            {
                cbKyNang.DataSource = KyNangDAO.Instance.GetListKyNang();
                cbKyNang.DisplayMember = "TenKyNang";
                cbKyNang.ValueMember = "MaKyNang";
            }

            private void CbKyNang_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (cbKyNang.SelectedValue != null)
                {
                    DataRowView row = cbKyNang.SelectedItem as DataRowView;
                    if (row != null)
                    {
                        decimal hp = row["HocPhi"] != DBNull.Value ? Convert.ToDecimal(row["HocPhi"]) : 0;
                        lblHocPhi.Text = hp.ToString("N0") + " VNĐ";
                        lblHocPhi.Tag = hp;

                        string maKN = row["MaKyNang"].ToString();
                        cbLopHoc.DataSource = LopHocDAO.Instance.GetListLopByKyNang(maKN);
                        cbLopHoc.DisplayMember = "TenLop";
                        cbLopHoc.ValueMember = "MaLop";
                    }
                }
            }
        // Mở file FrmDangKyAdmin.cs, thêm method này vào
        public void AutoSelectStudent(string maHV)
        {
            // Gán vào ô tìm kiếm
            txbSearch.Text = maHV;

            // Gọi hàm load lại dữ liệu (dựa trên keyword)
            LoadDataHocVien(maHV); // Hàm này bạn đã có ở dòng 1400

            // Tự động chọn học viên đó
            if (dgvHocVien.Rows.Count > 0)
            {
                dgvHocVien.Rows[0].Selected = true;
                // Kích hoạt sự kiện click để load lịch sử đăng ký
                DgvHocVien_CellClick(dgvHocVien, new DataGridViewCellEventArgs(0, 0));
            }
        }

        private void BtnDangKy_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrEmpty(currentMaHV)) { MessageBox.Show("Vui lòng chọn học viên ở cột bên trái trước!"); return; }
                if (cbLopHoc.SelectedValue == null) { MessageBox.Show("Vui lòng chọn lớp học!"); return; }

                string maLop = cbLopHoc.SelectedValue.ToString();
                decimal hocPhi = lblHocPhi.Tag != null ? Convert.ToDecimal(lblHocPhi.Tag) : 0;

                if (TuitionDAO.Instance.DangKyLop(currentMaHV, maLop, hocPhi))
                {
                    MessageBox.Show($"Đăng ký thành công lớp {cbLopHoc.Text} cho học viên {currentTenHV}!");
                    LoadDanhSachDaDangKy();
                }
                else
                {
                    MessageBox.Show("Học viên này đã đăng ký lớp này rồi!", "Trùng đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }