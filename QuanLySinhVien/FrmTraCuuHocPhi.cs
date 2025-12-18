using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using QuanLyTrungTam.DAO;

namespace QuanLyTrungTam
{
    public partial class FrmTraCuuHocPhi : Form
    {
        // --- KHAI BÁO CONTROL ---
        private TextBox txbSearch = new TextBox();
        private DataGridView dgvSearchResult = new DataGridView();
        private DataGridView dgvLopHoc = new DataGridView();
        private Label lblTaiChinh = new Label();
        private TextBox txbDongTien = new TextBox();
        private string currentMaHV = "";

        public FrmTraCuuHocPhi()
        {
            // InitializeComponent(); // Bỏ comment nếu cần
            SetupBetterUI();    // <--- Giao diện mới đẹp hơn
            LoadSearchData(""); // Load dữ liệu ban đầu
        }

        // =================================================================================
        // 1. THIẾT KẾ GIAO DIỆN (BEAUTIFUL UI)
        // =================================================================================
        private void SetupBetterUI()
        {
            this.Text = "Tra Cứu Thu Học Phí";
            this.BackColor = Color.WhiteSmoke;
            this.WindowState = FormWindowState.Maximized;
            this.Font = new Font("Segoe UI", 10F); // Font chuẩn toàn form

            // --- A. HEADER (THANH TÌM KIẾM) ---
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.White, Padding = new Padding(20, 10, 20, 10) };

            Label lblTitle = new Label { Text = "TRA CỨU HỌC VIÊN", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = ColorTranslator.FromHtml("#009688"), AutoSize = true, Location = new Point(20, 15) };

            // Ô tìm kiếm đẹp hơn
            txbSearch.Location = new Point(350, 18);
            txbSearch.Width = 400;
            txbSearch.Font = new Font("Segoe UI", 11);
            SetPlaceholder(txbSearch, "🔍 Nhập tên hoặc mã học viên...");
            txbSearch.TextChanged += Logic_SearchHV;

            pnlHeader.Controls.AddRange(new Control[] { lblTitle, txbSearch });
            this.Controls.Add(pnlHeader);

            // --- B. BODY (CHIA 2 CỘT) ---
            SplitContainer split = new SplitContainer { Dock = DockStyle.Fill, SplitterWidth = 10, BackColor = Color.WhiteSmoke };
            split.Width = 1200; // Gán tạm chiều rộng lớn để tránh lỗi tính toán
                                // -------------------------------

            // Sau đó mới set các thông số khác
            split.FixedPanel = FixedPanel.Panel1;
            split.Panel1MinSize = 350; // Giới hạn nhỏ nhất
            split.SplitterDistance = 400; // Bây giờ set 500 sẽ không lỗi vì 500 < 1200
            // >>> TRÁI: DANH SÁCH HỌC VIÊN
          //  split.Dock = DockStyle.Fill;
            split.Panel2MinSize = 350;
            // GroupBox danh sách
            GroupBox grpList = new GroupBox { Text = " Danh sách học viên ", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DimGray };
            grpList.Padding = new Padding(10);

            // GridView Trái
            StyleGrid(dgvSearchResult);
            dgvSearchResult.CellClick += Logic_ChonHV;
            grpList.Controls.Add(dgvSearchResult);
            split.Panel1.Controls.Add(grpList);
            split.Panel1.Padding = new Padding(10); // Padding cho cột trái

            // >> CỘT PHẢI: CHI TIẾT (70% màn hình)
            Panel pnlRightContent = new Panel { Dock = DockStyle.Fill };

            // 1. Phần Trên: Danh sách lớp
            GroupBox grpLop = new GroupBox { Text = " Các lớp đang theo học ", Dock = DockStyle.Top, Height = 250, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DimGray };
            grpLop.Padding = new Padding(10);

            StyleGrid(dgvLopHoc);
            grpLop.Controls.Add(dgvLopHoc);

            // 2. Phần Giữa: Hiển thị Nợ (To, Rõ)
            Panel pnlDebt = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            lblTaiChinh.Dock = DockStyle.Fill;
            lblTaiChinh.TextAlign = ContentAlignment.MiddleCenter;
            lblTaiChinh.Font = new Font("Segoe UI", 14);
            lblTaiChinh.Text = "👈 Vui lòng chọn học viên từ danh sách bên trái";
            pnlDebt.Controls.Add(lblTaiChinh);

            // 3. Phần Dưới: Thanh toán (Căn chỉnh thẳng hàng)
            Panel pnlPay = new Panel { Dock = DockStyle.Bottom, Height = 80, BackColor = ColorTranslator.FromHtml("#E8F5E9") }; // Màu xanh nhạt dịu mắt
            pnlPay.BorderStyle = BorderStyle.FixedSingle;

            Label lblPayTitle = new Label { Text = "Thu Phí:", Location = new Point(30, 28), AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold) };

            txbDongTien.Location = new Point(120, 25);
            txbDongTien.Width = 200;
            txbDongTien.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            txbDongTien.ForeColor = Color.DarkRed;
            txbDongTien.TextAlign = HorizontalAlignment.Right; // Số nằm bên phải

            Button btnPay = new Button { Text = "XÁC NHẬN THU", Location = new Point(340, 22), Size = new Size(160, 35) };
            btnPay.FlatStyle = FlatStyle.Flat;
            btnPay.FlatAppearance.BorderSize = 0;
            btnPay.BackColor = ColorTranslator.FromHtml("#FFC107"); // Màu vàng Amber
            btnPay.ForeColor = Color.Black;
            btnPay.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnPay.Cursor = Cursors.Hand;
            btnPay.Click += Logic_ThanhToanTrucTiep;

            pnlPay.Controls.AddRange(new Control[] { lblPayTitle, txbDongTien, btnPay });

            // Ráp vào cột phải
            pnlRightContent.Controls.Add(pnlDebt); // Giữa (Fill)
            pnlRightContent.Controls.Add(pnlPay);  // Đáy
            pnlRightContent.Controls.Add(grpLop);  // Đỉnh

            split.Panel2.Controls.Add(pnlRightContent);
            split.Panel2.Padding = new Padding(0, 10, 10, 10); // Padding cột phải

            this.Controls.Add(split);
            this.Controls.Add(pnlHeader);
        }

        // --- HÀM TRANG TRÍ GRID VIEW ĐẸP ---
        private void StyleGrid(DataGridView dgv)
        {
            dgv.Dock = DockStyle.Fill;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowTemplate.Height = 35; // Dòng cao thoáng

            // Header Style
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#009688"); // Xanh Teal
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;

            // Cell Style
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#B2DFDB"); // Xanh nhạt khi chọn
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        // =================================================================================
        // 2. LOGIC XỬ LÝ (GIỮ NGUYÊN LOGIC CŨ ĐÃ FIX LỖI)
        // =================================================================================

        void LoadSearchData(string keyword)
        {
            DataTable dt = HocVienDAO.Instance.GetListHocVien();
            if (dt == null) return;

            if (!string.IsNullOrEmpty(keyword) && keyword != "🔍 Nhập tên hoặc mã học viên...")
            {
                dt.DefaultView.RowFilter = string.Format("MaHV LIKE '%{0}%' OR HoTen LIKE '%{0}%'", keyword);
            }

            dgvSearchResult.DataSource = dt;

            // Config cột
            SafeSetHeader(dgvSearchResult, "MaHV", "Mã HV");
            SafeSetHeader(dgvSearchResult, "HoTen", "Họ Tên");
            SafeSetHeader(dgvSearchResult, "SoDienThoai", "SĐT");

            string[] colsToHide = { "DiaChi", "NgaySinh", "Email", "NgayGiaNhap", "MaLop", "MaKyNang" };
            foreach (string col in colsToHide) SafeSetVisible(dgvSearchResult, col, false);

            if (dgvSearchResult.Columns.Contains("MaHV")) { dgvSearchResult.Columns["MaHV"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None; dgvSearchResult.Columns["MaHV"].Width = 80; }
        }

        private void Logic_SearchHV(object sender, EventArgs e)
        {
            LoadSearchData(txbSearch.Text);
        }

        private void Logic_ChonHV(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSearchResult.Rows[e.RowIndex];
                currentMaHV = row.Cells["MaHV"].Value.ToString();
                string tenHV = row.Cells["HoTen"].Value.ToString();

                // Load Lớp
                DataTable dtLop = TuitionDAO.Instance.GetListDangKy(currentMaHV);
                dgvLopHoc.DataSource = dtLop;

                SafeSetHeader(dgvLopHoc, "TenKyNang", "Môn Học");
                SafeSetHeader(dgvLopHoc, "TenLop", "Lớp");
                SafeSetHeader(dgvLopHoc, "HocPhiLop", "Học Phí");
                if (dgvLopHoc.Columns.Contains("HocPhiLop"))
                    dgvLopHoc.Columns["HocPhiLop"].DefaultCellStyle.Format = "N0";

                SafeSetVisible(dgvLopHoc, "NgayDangKy", false);

                // Load Tài Chính
                UpdateFinanceInfo(tenHV);
            }
        }

        void UpdateFinanceInfo(string tenHV)
        {
            decimal tongNo = TuitionDAO.Instance.GetTongNo(currentMaHV);
            decimal daDong = TuitionDAO.Instance.GetDaDong(currentMaHV);
            decimal conNo = tongNo - daDong;

            lblTaiChinh.Text = $"Học Viên: {tenHV.ToUpper()}\n\n" +
                               $"Tổng Học Phí: {tongNo:N0} đ\n" +
                               $"Đã Đóng:      {daDong:N0} đ\n" +
                               $"--------------------------\n" +
                               $"CÒN NỢ:       {conNo:N0} VNĐ";

            lblTaiChinh.ForeColor = conNo > 0 ? Color.Red : Color.Green;

            txbDongTien.Clear();
            txbDongTien.Focus();
        }

        private void Logic_ThanhToanTrucTiep(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaHV)) { MessageBox.Show("Vui lòng chọn học viên!"); return; }

            if (decimal.TryParse(txbDongTien.Text.Replace(",", ""), out decimal soTien) && soTien > 0)
            {
                if (MessageBox.Show($"Xác nhận thu {soTien:N0} đ?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (TuitionDAO.Instance.InsertThanhToan(currentMaHV, soTien, "Thu tại quầy"))
                    {
                        MessageBox.Show("✅ Thu tiền thành công!");
                        UpdateFinanceInfo(dgvSearchResult.CurrentRow.Cells["HoTen"].Value.ToString());
                    }
                }
            }
            else MessageBox.Show("Số tiền không hợp lệ!");
        }
        public void AutoSearch(string maHV)
        {
            // Gán mã HV vào ô tìm kiếm
            txbSearch.Text = maHV;
            txbSearch.ForeColor = Color.Black; // Đổi màu chữ nếu đang là placeholder

            // Gọi lại logic tìm kiếm có sẵn
            // Lưu ý: Logic_SearchHV gọi LoadSearchData
            LoadSearchData(maHV);

            // Tự động chọn dòng đầu tiên nếu tìm thấy
            if (dgvSearchResult.Rows.Count > 0)
            {
                dgvSearchResult.Rows[0].Selected = true;
                // Gọi sự kiện click để load chi tiết bên phải
                Logic_ChonHV(dgvSearchResult, new DataGridViewCellEventArgs(0, 0));
            }
        }

        // --- HELPERS ---
        private void SafeSetHeader(DataGridView dgv, string colName, string text) { if (dgv.Columns.Contains(colName)) dgv.Columns[colName].HeaderText = text; }
        private void SafeSetVisible(DataGridView dgv, string colName, bool vis) { if (dgv.Columns.Contains(colName)) dgv.Columns[colName].Visible = vis; }
        private void SetPlaceholder(TextBox txt, string holder)
        {
            txt.Text = holder; txt.ForeColor = Color.Gray;
            txt.Enter += (s, e) => { if (txt.Text == holder) { txt.Text = ""; txt.ForeColor = Color.Black; } };
            txt.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txt.Text)) { txt.Text = holder; txt.ForeColor = Color.Gray; } };
        }
    }
}