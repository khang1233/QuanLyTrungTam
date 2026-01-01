using QuanLyTrungTam.DAO;
using QuanLyTrungTam.Utilities;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyTrungTam
{
    public partial class FrmSchedule : Form
    {
        private ComboBox cbFilterType;
        private ComboBox cbFilterValue;
        private TextBox txbSearch; // <--- MỚI: Ô tìm kiếm mã lớp
        private DataGridView dgvSchedule;
        private Button btnSearch;

        public FrmSchedule()
        {
            InitializeComponent();
            SetupBetterUI();
            LoadInitData();
        }

        // =========================================================================
        // 1. THIẾT KẾ GIAO DIỆN (ĐÃ CĂN CHỈNH KHOẢNG CÁCH & THÊM TÌM KIẾM)
        // =========================================================================
        private void SetupBetterUI()
        {
            this.Controls.Clear();
            this.BackColor = Color.White;
            this.Size = new Size(1100, 700);

            // --- A. HEADER ---
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.FromArgb(0, 150, 136) };
            Label lblTitle = new Label { Text = "TRA CỨU THỜI KHÓA BIỂU", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(20, 15) };
            pnlHeader.Controls.Add(lblTitle);

            // --- B. TOOLBAR (Tăng chiều cao lên 100 để chia 2 dòng cho thoáng) ---
            Panel pnlTool = new Panel { Dock = DockStyle.Top, Height = 100, BackColor = Color.WhiteSmoke };

            // Dòng 1: Bộ lọc chính (Y = 15)
            Label lblType = new Label { Text = "Xem theo:", Location = new Point(20, 18), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            cbFilterType = new ComboBox { Location = new Point(100, 15), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
            cbFilterType.Items.AddRange(new string[] { "Tất cả lịch học", "Theo Giáo Viên", "Theo Phòng Học", "Theo Lớp Học" });
            cbFilterType.SelectedIndex = 0;
            cbFilterType.SelectedIndexChanged += CbFilterType_SelectedIndexChanged;

            // Đẩy tọa độ X của "Chọn đối tượng" ra xa hơn (320 -> 350) để tránh bị dính
            Label lblValue = new Label { Text = "Chọn đối tượng:", Location = new Point(350, 18), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            cbFilterValue = new ComboBox { Location = new Point(470, 15), Width = 250, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
            cbFilterValue.Enabled = false;

            btnSearch = new Button { Text = "TẢI DỮ LIỆU", Location = new Point(750, 12), Size = new Size(120, 35), BackColor = Color.Orange, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Cursor = Cursors.Hand };
            btnSearch.Click += BtnSearch_Click;

            // Dòng 2: Tìm kiếm nhanh (MỚI - Y = 60)
            Label lblSearch = new Label { Text = "Tìm nhanh (Mã/Tên):", Location = new Point(20, 63), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DarkBlue };
            txbSearch = new TextBox { Location = new Point(180, 60), Width = 400, Font = new Font("Segoe UI", 10) };
            txbSearch.TextChanged += TxbSearch_TextChanged; // Sự kiện gõ phím

            pnlTool.Controls.AddRange(new Control[] { lblType, cbFilterType, lblValue, cbFilterValue, btnSearch, lblSearch, txbSearch });

            // --- C. GRIDVIEW ---
            Panel pnlGridContainer = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15) };
            GroupBox grpGrid = new GroupBox { Text = " Chi tiết lịch học ", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

            dgvSchedule = new DataGridView();
            StyleGrid(dgvSchedule);
            grpGrid.Controls.Add(dgvSchedule);
            pnlGridContainer.Controls.Add(grpGrid);

            // Thứ tự Add (Quan trọng để không bị che)
            this.Controls.Add(pnlGridContainer);
            this.Controls.Add(pnlTool);
            this.Controls.Add(pnlHeader);
        }

        // =========================================================================
        // 2. LOGIC XỬ LÝ
        // =========================================================================
        // File: FrmSchedule.cs

        // 1. Sửa lại hàm LoadInitData
        private void LoadInitData()
        {
            // Nếu là Admin thì load hết như cũ
            if (AppSession.CurrentUser.Quyen.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                BtnSearch_Click(null, null);
            }
            else
            {
                // NẾU LÀ GIÁO VIÊN: Khóa giao diện filter, chỉ load lịch của chính mình
                LockUIForTeacher();
                LoadTeacherSchedule();
            }
        }

        // 2. Thêm hàm Khóa giao diện (Ẩn các ô chọn Giáo viên khác đi)
        private void LockUIForTeacher()
        {
            // Ẩn hoặc Disable các combobox chọn lọc
            cbFilterType.Visible = false;
            cbFilterValue.Visible = false;
            // Đổi tiêu đề label
            foreach (Control c in this.Controls)
            {
                Panel pnl = c as Panel;
                if (pnl != null)
                {
                    foreach (Control child in pnl.Controls)
                    {
                        if (child.Text == "Xem theo:") child.Visible = false;
                        if (child.Text == "Chọn đối tượng:") child.Visible = false;
                    }
                }
            }
        }

        // 3. Hàm load riêng cho Giáo viên
        // File: FrmSchedule.cs

        // File: FrmSchedule.cs

        private void LoadTeacherSchedule()
        {
            string maNS = AppSession.CurrentUser.MaNguoiDung;

            // Sửa lại câu truy vấn: Đổi @ma thành @ma1 và @ma2 để không bị trùng tên biến
            string query = @"
        SELECT l.MaLop, l.TenLop, k.TenKyNang, l.Thu, l.CaHoc, p.TenPhong, l.TrangThai,
               ns1.HoTen as TenGV, ns2.HoTen as TenTG
        FROM LopHoc l
        JOIN KyNang k ON l.MaKyNang = k.MaKyNang
        LEFT JOIN PhongHoc p ON l.MaPhong = p.MaPhong
        LEFT JOIN NhanSu ns1 ON l.MaGiaoVien = ns1.MaNS
        LEFT JOIN NhanSu ns2 ON l.MaTroGiang = ns2.MaNS
        WHERE l.MaGiaoVien = @ma1 OR l.MaTroGiang = @ma2";

            // Truyền tham số: Vẫn truyền maNS 2 lần, nhưng lần này SQL sẽ hiểu là cho @ma1 và @ma2
            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { maNS, maNS });

            dgvSchedule.DataSource = dt;
        }

        private void CbFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbFilterValue.DataSource = null;
            cbFilterValue.Items.Clear();
            int type = cbFilterType.SelectedIndex;

            if (type == 0) // Tất cả
            {
                cbFilterValue.Enabled = false;
            }
            else
            {
                cbFilterValue.Enabled = true;
                if (type == 1) // GV
                {
                    DataTable dt = NhanVienDAO.Instance.GetListNhanVien();
                    DataView dv = new DataView(dt);
                    dv.RowFilter = "LoaiNS = 'GiaoVien'";
                    cbFilterValue.DataSource = dv;
                    cbFilterValue.DisplayMember = "HoTen"; cbFilterValue.ValueMember = "MaNS";
                }
                else if (type == 2) // Phòng
                {
                    cbFilterValue.DataSource = PhongHocDAO.Instance.GetListPhong();
                    cbFilterValue.DisplayMember = "TenPhong"; cbFilterValue.ValueMember = "MaPhong";
                }
                else if (type == 3) // Lớp
                {
                    cbFilterValue.DataSource = LopHocDAO.Instance.GetAllLop();
                    cbFilterValue.DisplayMember = "TenLop"; cbFilterValue.ValueMember = "MaLop";
                }
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            DataTable allLop = LopHocDAO.Instance.GetAllLop();

            if (cbFilterType.SelectedIndex == 0) // Tất cả
            {
                dtResult = allLop;
            }
            else
            {
                if (cbFilterValue.SelectedValue == null) { MessageBox.Show("Vui lòng chọn đối tượng!"); return; }

                string id = cbFilterValue.SelectedValue.ToString();
                DataView dv = new DataView(allLop);

                if (cbFilterType.SelectedIndex == 1) dv.RowFilter = string.Format("MaGiaoVien = '{0}'", id);
                else if (cbFilterType.SelectedIndex == 2) dv.RowFilter = string.Format("MaPhong = '{0}'", id);
                else if (cbFilterType.SelectedIndex == 3) dv.RowFilter = string.Format("MaLop = '{0}'", id);

                dtResult = dv.ToTable();
            }

            dgvSchedule.DataSource = dtResult;
            FormatGrid();

            // Reset ô tìm kiếm sau khi tải lại
            txbSearch.Text = "";
        }

        // --- MỚI: LOGIC TÌM KIẾM THEO MÃ LỚP ---
        private void TxbSearch_TextChanged(object sender, EventArgs e)
        {
            // Lấy dữ liệu đang hiển thị trên lưới
            DataTable dt = dgvSchedule.DataSource as DataTable;
            if (dt != null)
            {
                string keyword = txbSearch.Text.Trim();
                if (string.IsNullOrEmpty(keyword))
                {
                    dt.DefaultView.RowFilter = ""; // Xóa bộ lọc
                }
                else
                {
                    // Lọc theo Mã Lớp HOẶC Tên Lớp
                    dt.DefaultView.RowFilter = string.Format("MaLop LIKE '%{0}%' OR TenLop LIKE '%{0}%'", keyword);
                }
            }
        }

        // --- Helper Styles ---
        private void FormatGrid()
        {
            string[] hide = { "MaKyNang", "MaGiaoVien", "MaTroGiang", "MaPhong", "SiSoToiDa", "NgayBatDau" };
            foreach (string c in hide) if (dgvSchedule.Columns.Contains(c)) dgvSchedule.Columns[c].Visible = false;

            SetHeader("MaLop", "Mã Lớp");
            SetHeader("TenLop", "Tên Lớp");
            SetHeader("TenKyNang", "Môn Học");
            SetHeader("TenGV", "Giáo Viên");
            SetHeader("TenTG", "Trợ Giảng");
            SetHeader("TenPhong", "Phòng Học");
            SetHeader("Thu", "Lịch Học");
            SetHeader("CaHoc", "Ca Học");
            SetHeader("TrangThai", "Trạng Thái");
        }

        private void SetHeader(string colName, string text)
        {
            if (dgvSchedule.Columns.Contains(colName)) dgvSchedule.Columns[colName].HeaderText = text;
        }

        private void StyleGrid(DataGridView dgv)
        {
            dgv.Dock = DockStyle.Fill;
            dgv.BackgroundColor = Color.White;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.ColumnHeadersHeight = 40;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 150, 136);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.RowTemplate.Height = 35;
        }
    }
}