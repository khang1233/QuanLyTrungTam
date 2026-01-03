using QuanLyTrungTam.BUS;
using QuanLyTrungTam.DAO;
using QuanLyTrungTam.Utilities;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyTrungTam
{
    public partial class FrmDiem : Form
    {
        private ComboBox cbLop;
        private DataGridView dgvDiem;
        private TextBox txbSearch;
        private Button btnSave;

        public FrmDiem()
        {
            InitializeComponent();
            SetupUI();
            LoadClasses();
        }

        // =============================================
        // 1. THIẾT KẾ GIAO DIỆN (ĐÃ FIX LỖI HIỂN THỊ)
        // =============================================
        private void SetupUI()
        {
            this.Controls.Clear();
            this.Text = "Quản Lý Điểm Số & Xếp Loại";
            this.Size = new Size(1280, 800);
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 10F);
            this.Padding = new Padding(10);

            // --- A. PANEL HEADER (Top) ---
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = Color.White, Padding = new Padding(20, 0, 20, 0) };
            Label lblTitle = new Label { 
                Text = "QUẢN LÝ ĐIỂM SỐ", 
                Font = new Font("Segoe UI", 18, FontStyle.Bold), 
                ForeColor = Color.FromArgb(33, 150, 243), 
                AutoSize = true, 
                TextAlign = ContentAlignment.MiddleLeft 
            };
            lblTitle.Location = new Point(20, (pnlHeader.Height - lblTitle.Height) / 2);
            pnlHeader.Controls.Add(lblTitle);

            // --- FILTER PANEL ---
            Panel pnlFilter = new Panel { Dock = DockStyle.Top, Height = 80, BackColor = Color.Transparent };
            
            // Class Select
            Label lblLop = new Label { Text = "Chọn Lớp:", Location = new Point(20, 30), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DimGray };
            cbLop = new ComboBox { Location = new Point(110, 27), Width = 300, DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Flat, BackColor = Color.White, Font = new Font("Segoe UI", 11) };
            cbLop.SelectedIndexChanged += (s, e) => LoadDiem();

            // Search
            Label lblSearch = new Label { Text = "Tìm kiếm:", Location = new Point(450, 30), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DimGray };
            txbSearch = new TextBox { Location = new Point(530, 27), Width = 300, Font = new Font("Segoe UI", 11), BorderStyle = BorderStyle.FixedSingle };
            SetPlaceholder(txbSearch, "Mã hoặc tên học viên...");

            // Save Button
            btnSave = new Button
            {
                Text = "💾 LƯU BẢNG ĐIỂM",
                Location = new Point(pnlFilter.Width - 220, 20),
                Size = new Size(200, 45),
                BackColor = Color.FromArgb(40, 167, 69), // Green
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            pnlFilter.Controls.AddRange(new Control[] { lblLop, cbLop, lblSearch, txbSearch, btnSave });


            // --- B. DATAGRIDVIEW CONTAINER ---
            Panel pnlGridContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 10, 0, 10),
                BackColor = Color.Transparent
            };

            dgvDiem = new DataGridView();
            InitGridStyle(); 
            pnlGridContainer.Controls.Add(dgvDiem);

            // Order of adding affects Docking
            this.Controls.Add(pnlGridContainer);
            this.Controls.Add(pnlFilter);
            this.Controls.Add(pnlHeader);
        }

        // =============================================
        // HÀM KHỞI TẠO GRID & CỘT (FIX LỖI MẤT BẢNG)
        // =============================================
        private void InitGridStyle()
        {
            dgvDiem.Dock = DockStyle.Fill;
            dgvDiem.BackgroundColor = Color.White;
            dgvDiem.BorderStyle = BorderStyle.None;

            dgvDiem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDiem.AllowUserToAddRows = false;
            dgvDiem.RowHeadersVisible = false;
            dgvDiem.ColumnHeadersHeight = 45;
            dgvDiem.RowTemplate.Height = 40;
            dgvDiem.EnableHeadersVisualStyles = false;
            dgvDiem.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDiem.AutoGenerateColumns = false;

            // Style Header
            dgvDiem.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243); // Modern Blue
            dgvDiem.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDiem.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvDiem.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Style Cell
            dgvDiem.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvDiem.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDiem.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dgvDiem.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvDiem.GridColor = Color.WhiteSmoke;

            // --- ĐỊNH NGHĨA CỘT THỦ CÔNG (Đảm bảo bảng luôn hiện Header) ---
            AddCol("MaHV", "Mã HV", true, 100);
            AddCol("HoTen", "Họ Tên Học Viên", true, 200);
            AddCol("Diem15p1", "15 Phút (1)", false, 80);
            AddCol("Diem15p2", "15 Phút (2)", false, 80);
            AddCol("DiemGiuaKy", "Giữa Kỳ (x2)", false, 100);
            AddCol("DiemCuoiKy", "Cuối Kỳ (x3)", false, 100);
            AddCol("DiemTongKet", "Tổng Kết", true, 100);
            AddCol("XepLoai", "Xếp Loại", true, 120);
            AddCol("GhiChu", "Ghi Chú", false, 150);

            // Event tô màu
            dgvDiem.CellFormatting += DgvDiem_CellFormatting;

            // Event tìm kiếm
            txbSearch.TextChanged += (s, e) =>
            {
                DataTable dt = dgvDiem.DataSource as DataTable;
                if (dt != null)
                {
                    string k = txbSearch.Text.Trim();
                    if (k == "Mã hoặc tên học viên..." || string.IsNullOrEmpty(k)) dt.DefaultView.RowFilter = "";
                    else dt.DefaultView.RowFilter = string.Format("MaHV LIKE '%{0}%' OR HoTen LIKE '%{0}%'", k);
                }


            };
        }

        private void AddCol(string dataPropertyName, string headerText, bool readOnly, int width)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = dataPropertyName; // Tên cột trong Database
            col.HeaderText = headerText;             // Tên hiển thị
            col.Name = dataPropertyName;
            col.ReadOnly = readOnly;

            // Nếu là cột điểm cho phép nhập
            if (!readOnly)
            {
                col.DefaultCellStyle.BackColor = Color.LightYellow; // Tô màu vàng nhạt ô nhập liệu
                col.DefaultCellStyle.ForeColor = Color.Black;
            }
            // Nếu là cột tổng kết
            if (dataPropertyName == "DiemTongKet")
            {
                col.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                col.DefaultCellStyle.ForeColor = Color.Blue;
                col.DefaultCellStyle.Format = "N1";
            }

            dgvDiem.Columns.Add(col);
        }

        // =============================================
        // LOAD DATA
        // =============================================
        // File: FrmDiem.cs -> Tìm hàm LoadClasses và sửa thành:

        private void LoadClasses()
        {
            DataTable dt;

            // [REFACTOR] Sử dụng LopHocBUS thay vì DataProvider/LopHocDAO trực tiếp
            if (AppSession.CurrentUser.Quyen.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                dt = LopHocBUS.Instance.GetAllLop();
            }
            else
            {
                string maNS = AppSession.CurrentUser.MaNguoiDung;
                // Cần đảm bảo LopHocBUS có hàm này. Hiện tại LopHocBUS chưa wrap GetLopByNhanSu.
                // Sẽ thêm hàm này vào LopHocBUS ngay sau bước này.
                // Tạm thời comment nhắc nhở hoặc thêm vào BUS trước.
                // Để tránh lỗi compile, tôi sẽ thêm vào BUS trước.
                dt = LopHocDAO.Instance.GetLopByNhanSu(maNS); // Tạm giữ DAO cho đến khi thêm vào BUS
            }

            cbLop.DataSource = dt;
            cbLop.DisplayMember = "TenLop";
            cbLop.ValueMember = "MaLop";
        }

        private void LoadDiem()
        {
            if (cbLop.SelectedValue == null) return;
            string maLop = cbLop.SelectedValue.ToString();

            // [REFACTOR] Use DiemBUS
            DataTable dt = DiemBUS.Instance.GetBangDiemLop(maLop);
            // Thêm cột Xếp loại ảo vào DataTable để Grid hiển thị được
            if (!dt.Columns.Contains("XepLoai")) dt.Columns.Add("XepLoai", typeof(string));

            dgvDiem.DataSource = dt;
        }

        // =============================================
        // LOGIC TÔ MÀU XẾP LOẠI
        // =============================================
        private void DgvDiem_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvDiem.Columns[e.ColumnIndex].Name == "XepLoai")
            {
                var cell = dgvDiem.Rows[e.RowIndex].Cells["DiemTongKet"];
                var cellVal = cell != null ? cell.Value : null;
                if (cellVal != null && cellVal != DBNull.Value)
                {
                    double d = Convert.ToDouble(cellVal);
                    string xl = ""; Color c = Color.Black;

                    if (d < 3) { xl = "YẾU"; c = Color.Red; }
                    else if (d < 5) { xl = "TRUNG BÌNH"; c = Color.OrangeRed; }
                    else if (d < 7) { xl = "KHÁ"; c = Color.Blue; }
                    else if (d < 8.5) { xl = "GIỎI"; c = Color.Green; }
                    else { xl = "XUẤT SẮC"; c = Color.DarkGreen; }

                    e.Value = xl;
                    e.CellStyle.ForeColor = c;
                    e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
            }
        }

        // =============================================
        // LƯU ĐIỂM
        // =============================================
        // Thay thế sự kiện BtnSave_Click cũ bằng đoạn này
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cbLop.SelectedValue == null) return;
            string maLop = cbLop.SelectedValue.ToString();
            int count = 0;
            string errorMsg = ""; // Biến lưu lỗi nếu có

            foreach (DataGridViewRow r in dgvDiem.Rows)
            {
                // Bỏ qua dòng trống hoặc dòng mới
                if (r.IsNewRow) continue;
                if (r.Cells["MaHV"].Value == null || string.IsNullOrEmpty(r.Cells["MaHV"].Value.ToString())) continue;

                try
                {
                    string maHV = r.Cells["MaHV"].Value.ToString();

                    // Lấy điểm (xử lý null thành 0)
                    double d1 = GetVal(r.Cells["Diem15p1"].Value);
                    double d2 = GetVal(r.Cells["Diem15p2"].Value);
                    double dGK = GetVal(r.Cells["DiemGiuaKy"].Value);
                    double dCK = GetVal(r.Cells["DiemCuoiKy"].Value);
                    string ghiChu = "";
                    if (r.Cells["GhiChu"].Value != null) ghiChu = r.Cells["GhiChu"].Value.ToString();

                    // Validate điểm
                    if (d1 < 0 || d1 > 10 || d2 < 0 || d2 > 10 || dGK < 0 || dGK > 10 || dCK < 0 || dCK > 10)
                    {
                        MessageBox.Show(string.Format("Điểm của {0} không hợp lệ (0-10)!", r.Cells["HoTen"].Value), "Lỗi nhập liệu");
                        return;
                    }

                    // [REFACTOR] Gọi BUS lưu điểm
                    if (DiemBUS.Instance.LuuDiem(maHV, maLop, d1, d2, dGK, dCK, ghiChu))
                    {
                        count++;
                    }
                }
                catch (Exception ex)
                {
                    // Ghi lại lỗi đầu tiên gặp phải để thông báo
                    if (string.IsNullOrEmpty(errorMsg)) errorMsg = ex.Message;
                }
            }

            if (count > 0)
            {
                MessageBox.Show(string.Format("Đã lưu thành công {0} học viên!", count), "Thành công");
                LoadDiem(); // Tải lại để thấy Điểm tổng kết và Xếp loại mới nhất
            }
            else
            {
                // Nếu không lưu được ai, hiện lỗi ra để biết đường sửa
                if (!string.IsNullOrEmpty(errorMsg))
                    MessageBox.Show("Lỗi hệ thống: " + errorMsg, "Thất bại");
                else
                    MessageBox.Show("Không có dữ liệu hợp lệ để lưu.", "Thông báo");
            }
        }

        // Hàm hỗ trợ lấy giá trị số an toàn
        private double GetVal(object val)
        {
            if (val == null || val == DBNull.Value || string.IsNullOrWhiteSpace(val.ToString())) return 0;
            double res;
            if (double.TryParse(val.ToString(), out res)) return res;
            return 0;
        }
        private void SetPlaceholder(TextBox txt, string holder)
        {
            txt.Text = holder; txt.ForeColor = Color.Gray;
            txt.Enter += (s, e) => { if (txt.Text == holder) { txt.Text = ""; txt.ForeColor = Color.Black; } };
            txt.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txt.Text)) { txt.Text = holder; txt.ForeColor = Color.Gray; } };
        }
    }
}