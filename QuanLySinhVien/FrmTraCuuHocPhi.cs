using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.Windows.Forms;
using QuanLyTrungTam.DAO;

namespace QuanLyTrungTam
{
    public partial class FrmTraCuuHocPhi : Form
    {
        // --- 1. KHAI BÁO CONTROL ---
        private TextBox txbSearch = new TextBox();
        private DataGridView dgvSearchResult = new DataGridView();
        private DataGridView dgvLopHoc = new DataGridView();
        private Label lblTaiChinh = new Label();
        private Button btnLapHoaDon;

        private string currentMaHV = "";

        // Biến in ấn
        private string _printTenHV = "";
        private string _printSoTien = "";
        private string _printHinhThuc = "";

        public FrmTraCuuHocPhi()
        {
            SetupBetterUI();
            LoadSearchData("");
        }

        // =================================================================================
        // GIAO DIỆN FORM CHÍNH
        // =================================================================================
        private void SetupBetterUI()
        {
            this.Text = "Tra Cứu & Thu Học Phí";
            this.BackColor = Color.WhiteSmoke;
            this.WindowState = FormWindowState.Maximized;
            this.Font = new Font("Segoe UI", 10F);

            // HEADER
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = Color.White };
            Label lblTitle = new Label { Text = "TRA CỨU HỌC VIÊN", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = ColorTranslator.FromHtml("#009688"), AutoSize = true, Location = new Point(20, 22) };
            txbSearch.Location = new Point(350, 22); txbSearch.Width = 400; txbSearch.Font = new Font("Segoe UI", 11);
            SetPlaceholder(txbSearch, "🔍 Nhập tên hoặc mã học viên...");
            txbSearch.TextChanged += Logic_SearchHV;
            pnlHeader.Controls.AddRange(new Control[] { lblTitle, txbSearch });
            this.Controls.Add(pnlHeader);

            // BODY
            SplitContainer split = new SplitContainer { Dock = DockStyle.Fill, SplitterWidth = 10, BackColor = Color.WhiteSmoke };

            // TRÁI
            GroupBox grpList = new GroupBox { Text = " Danh sách học viên ", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DimGray };
            grpList.Padding = new Padding(10);
            StyleGrid(dgvSearchResult);
            dgvSearchResult.CellClick += Logic_ChonHV;
            grpList.Controls.Add(dgvSearchResult);
            split.Panel1.Controls.Add(grpList);
            split.Panel1.Padding = new Padding(10);

            // PHẢI
            Panel pnlRightContent = new Panel { Dock = DockStyle.Fill };
            GroupBox grpLop = new GroupBox { Text = " Các lớp đang theo học ", Dock = DockStyle.Top, Height = 250, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DimGray };
            grpLop.Padding = new Padding(10);
            StyleGrid(dgvLopHoc);
            grpLop.Controls.Add(dgvLopHoc);

            Panel pnlDebt = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            lblTaiChinh.Dock = DockStyle.Fill;
            lblTaiChinh.TextAlign = ContentAlignment.MiddleCenter; // Căn giữa lại như cũ
            lblTaiChinh.Font = new Font("Segoe UI", 14);
            lblTaiChinh.Text = "👈 Vui lòng chọn học viên từ danh sách bên trái";
            pnlDebt.Controls.Add(lblTaiChinh);

            // PANEL THANH TOÁN
            Panel pnlPay = new Panel { Dock = DockStyle.Bottom, Height = 80, BackColor = ColorTranslator.FromHtml("#E8F5E9") };
            pnlPay.BorderStyle = BorderStyle.FixedSingle;
            btnLapHoaDon = new Button { Text = "📝 LẬP HÓA ĐƠN THANH TOÁN", Dock = DockStyle.Fill, BackColor = ColorTranslator.FromHtml("#FF9800"), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 14, FontStyle.Bold), Cursor = Cursors.Hand };
            Panel pnlBtnContainer = new Panel { Dock = DockStyle.Fill, Padding = new Padding(200, 15, 200, 15), BackColor = Color.Transparent };
            pnlBtnContainer.Controls.Add(btnLapHoaDon);
            btnLapHoaDon.Click += BtnLapHoaDon_Click;
            pnlPay.Controls.Add(pnlBtnContainer);

            pnlRightContent.Controls.Add(pnlDebt);
            pnlRightContent.Controls.Add(pnlPay);
            pnlRightContent.Controls.Add(grpLop);

            split.Panel2.Controls.Add(pnlRightContent);
            split.Panel2.Padding = new Padding(0, 10, 10, 10);

            this.Controls.Add(split);
            this.Controls.Add(pnlHeader);
        }

        private void StyleGrid(DataGridView dgv)
        {
            dgv.Dock = DockStyle.Fill; dgv.BackgroundColor = Color.White; dgv.BorderStyle = BorderStyle.None;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false; dgv.ReadOnly = true; dgv.RowTemplate.Height = 35;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#009688");
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;
        }

        // =================================================================================
        // LOGIC DỮ LIỆU
        // =================================================================================
        void LoadSearchData(string keyword)
        {
            DataTable dt = HocVienDAO.Instance.GetListHocVien();
            if (dt == null) return;
            if (!dt.Columns.Contains("TrangThaiHocPhi")) dt.Columns.Add("TrangThaiHocPhi", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                string maHV = row["MaHV"].ToString();
                decimal tongNo = TuitionDAO.Instance.GetTongNo(maHV);
                decimal daDong = TuitionDAO.Instance.GetDaDong(maHV);
                decimal conNo = tongNo - daDong;
                row["TrangThaiHocPhi"] = conNo > 0 ? "Còn nợ" : "Hoàn thành";
            }

            if (!string.IsNullOrEmpty(keyword) && keyword != "🔍 Nhập tên hoặc mã học viên...")
                dt.DefaultView.RowFilter = string.Format("MaHV LIKE '%{0}%' OR HoTen LIKE '%{0}%'", keyword);

            dgvSearchResult.DataSource = dt;
            SafeSetHeader(dgvSearchResult, "MaHV", "Mã HV");
            SafeSetHeader(dgvSearchResult, "HoTen", "Họ Tên");
            SafeSetHeader(dgvSearchResult, "TrangThaiHocPhi", "Học Phí");

            string[] colsToHide = { "DiaChi", "NgaySinh", "Email", "NgayGiaNhap", "MaLop", "MaKyNang", "SoDienThoai" };
            foreach (string col in colsToHide) SafeSetVisible(dgvSearchResult, col, false);

            if (dgvSearchResult.Columns.Contains("MaHV")) dgvSearchResult.Columns["MaHV"].Width = 80;
            if (dgvSearchResult.Columns.Contains("TrangThaiHocPhi")) dgvSearchResult.Columns["TrangThaiHocPhi"].Width = 110;
            ColorizeHocPhiColumn();
        }

        private void Logic_SearchHV(object sender, EventArgs e) { LoadSearchData(txbSearch.Text); }

        private void Logic_ChonHV(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSearchResult.Rows[e.RowIndex];
                currentMaHV = row.Cells["MaHV"].Value.ToString();
                string tenHV = row.Cells["HoTen"].Value.ToString();

                DataTable dtLop = TuitionDAO.Instance.GetListDangKy(currentMaHV);
                dgvLopHoc.DataSource = dtLop;

                SafeSetHeader(dgvLopHoc, "TenKyNang", "Môn Học");
                SafeSetHeader(dgvLopHoc, "TenLop", "Lớp");
                SafeSetHeader(dgvLopHoc, "HocPhiLop", "Học Phí");
                if (dgvLopHoc.Columns.Contains("HocPhiLop")) dgvLopHoc.Columns["HocPhiLop"].DefaultCellStyle.Format = "N0";

                SafeSetVisible(dgvLopHoc, "NgayDangKy", false);
                UpdateFinanceInfo(tenHV);
            }
        }

        // --- HÀM CẬP NHẬT TÀI CHÍNH (ĐÃ QUAY VỀ KIỂU CŨ GỌN GÀNG) ---
        void UpdateFinanceInfo(string tenHV)
        {
            decimal tongNo = TuitionDAO.Instance.GetTongNo(currentMaHV);

            // Query trực tiếp để lấy tổng tiền đã đóng (Đảm bảo cập nhật ngay)
            string qSum = "SELECT SUM(SoTienDong) FROM ThanhToan WHERE MaHV = @ma";
            object result = DataProvider.Instance.ExecuteScalar(qSum, new object[] { currentMaHV });
            decimal daDong = (result == DBNull.Value || result == null) ? 0 : Convert.ToDecimal(result);

            decimal conNo = tongNo - daDong;

            // Hiển thị gọn gàng như cũ
            string info = string.Format("Học Viên: {0}\n\nTổng Học Phí: {1:N0} VNĐ\n--------------------------\n{2}--------------------------\nCÒN NỢ:       {3:N0} VNĐ", 
                tenHV.ToUpper(), tongNo, (daDong > 0 ? string.Format("Đã Đóng:      {0:N0} VNĐ\n", daDong) : "(Chưa đóng khoản nào)\n"), conNo);

            lblTaiChinh.Text = info;
            lblTaiChinh.ForeColor = conNo > 0 ? Color.Red : Color.Green;
        }

        private void BtnLapHoaDon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaHV)) { MessageBox.Show("Vui lòng chọn học viên trước!"); return; }

            // Tính toán lại
            decimal tongNo = TuitionDAO.Instance.GetTongNo(currentMaHV);
            string qSum = "SELECT SUM(SoTienDong) FROM ThanhToan WHERE MaHV = @ma";
            object result = DataProvider.Instance.ExecuteScalar(qSum, new object[] { currentMaHV });
            decimal daDong = (result == DBNull.Value || result == null) ? 0 : Convert.ToDecimal(result);
            decimal conNo = tongNo - daDong;

            if (conNo <= 0) { MessageBox.Show("Học viên này đã hết nợ!"); return; }

            string tenHV = dgvSearchResult.CurrentRow.Cells["HoTen"].Value.ToString();

            // Mở Dialog
            FrmThanhToanDialog frmPay = new FrmThanhToanDialog(currentMaHV, tenHV, conNo);

            if (frmPay.ShowDialog() == DialogResult.OK)
            {
                decimal tienThu = frmPay.FinalAmount;
                string hinhThuc = frmPay.FinalMethod;

                if (TuitionDAO.Instance.InsertThanhToan(currentMaHV, tienThu, hinhThuc))
                {
                    ThucHienInHoaDon(currentMaHV, tenHV, tienThu, hinhThuc);
                    MessageBox.Show("✅ Giao dịch thành công!");
                    UpdateFinanceInfo(tenHV);
                    LoadSearchData(txbSearch.Text); // Refresh lưới bên trái
                }
                else MessageBox.Show("Lỗi lưu dữ liệu!");
            }
        }

        // --- IN ẤN ---
        private void ThucHienInHoaDon(string maHV, string tenHV, decimal soTien, string hinhThuc)
        {
            _printTenHV = tenHV; _printSoTien = string.Format("{0:N0} VNĐ", soTien); _printHinhThuc = hinhThuc;
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += VeHoaDon;
            PrintPreviewDialog dlg = new PrintPreviewDialog { Document = pd, Width = 800, Height = 600 };
            dlg.StartPosition = FormStartPosition.CenterScreen;
            dlg.ShowDialog();
        }

        private void VeHoaDon(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics; float w = e.PageBounds.Width; float y = 50;
            StringFormat center = new StringFormat { Alignment = StringAlignment.Center };

            g.DrawString("TRUNG TÂM ĐÀO TẠO", new Font("Arial", 22, FontStyle.Bold), Brushes.Blue, w / 2, y, center); y += 50;
            g.DrawString("BIÊN LAI THU TIỀN", new Font("Arial", 18, FontStyle.Bold), Brushes.Red, w / 2, y, center); y += 40;
            g.DrawString(string.Format("(Hình thức: {0})", _printHinhThuc), new Font("Arial", 11, FontStyle.Italic), Brushes.Black, w / 2, y, center); y += 50;

            float x = 100;
            g.DrawString(string.Format("Mã HV:   {0}", currentMaHV), new Font("Arial", 12), Brushes.Black, x, y); y += 35;
            g.DrawString(string.Format("Họ Tên:  {0}", _printTenHV), new Font("Arial", 12, FontStyle.Bold), Brushes.Black, x, y); y += 35;
            g.DrawString(string.Format("Số Tiền: {0}", _printSoTien), new Font("Arial", 16, FontStyle.Bold), Brushes.Red, x, y); y += 45;
            g.DrawString(string.Format("Ngày:    {0:dd/MM/yyyy HH:mm}", DateTime.Now), new Font("Arial", 12), Brushes.Gray, x, y);

            float rightX = w - 200;
            y += 50;
            g.DrawString("Người nộp tiền", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, x + 20, y);
            g.DrawString("Người thu tiền", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, rightX, y);

            y += 30;
            g.DrawString("(Ký, họ tên)", new Font("Arial", 10, FontStyle.Italic), Brushes.Gray, x + 30, y);
            g.DrawString("(Ký, đóng dấu)", new Font("Arial", 10, FontStyle.Italic), Brushes.Gray, rightX + 10, y);

            y += 40;
            // Bên thu tiền ký tên KHANG
            Font fKhang = new Font("Mistral", 20, FontStyle.Bold);
            g.DrawString("Khang", fKhang, Brushes.Black, rightX + 20, y);

            y += 40;
            g.DrawString("Trần Minh Khang", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, rightX - 20, y);
        }

        // Helpers
        private void SafeSetHeader(DataGridView dgv, string c, string t) { if (dgv.Columns.Contains(c)) dgv.Columns[c].HeaderText = t; }
        private void SafeSetVisible(DataGridView dgv, string c, bool v) { if (dgv.Columns.Contains(c)) dgv.Columns[c].Visible = v; }
        public void AutoSearch(string ma) { txbSearch.Text = ma; LoadSearchData(ma); }
        private void SetPlaceholder(TextBox t, string h)
        {
            t.Text = h; t.ForeColor = Color.Gray;
            t.Enter += (s, e) => { if (t.Text == h) { t.Text = ""; t.ForeColor = Color.Black; } };
            t.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(t.Text)) { t.Text = h; t.ForeColor = Color.Gray; } };
        }
        private void ColorizeHocPhiColumn()
        {
            foreach (DataGridViewRow r in dgvSearchResult.Rows)
            {
                object val = r.Cells["TrangThaiHocPhi"].Value;
                if (val != null && val.ToString() == "Còn nợ") r.Cells["TrangThaiHocPhi"].Style.ForeColor = Color.Red;
                else r.Cells["TrangThaiHocPhi"].Style.ForeColor = Color.Green;
            }
        }
    }

    // =========================================================================
    // FORM DIALOG THANH TOÁN (ĐÃ XÓA LỊCH SỬ - CHỈ CÒN NHẬP TIỀN & QR)
    // =========================================================================
    public class FrmThanhToanDialog : Form
    {
        public decimal FinalAmount { get; private set; }
        public string FinalMethod { get; private set; }

        private TextBox txbTien;
        private RadioButton rdoTienMat, rdoQR;
        private CheckBox chkXacNhan;
        private PictureBox picQR;
        private Label lblHuongDan;
        private string _maHV, _tenHV;
        private decimal _noHienTai;

        public FrmThanhToanDialog(string ma, string ten, decimal no)
        {
            _maHV = ma; _tenHV = ten; _noHienTai = no;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "HÓA ĐƠN & THANH TOÁN";
            this.Size = new Size(550, 650); // Thu nhỏ lại vì đã xóa bảng lịch sử
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;

            // 1. INFO TOP
            Label lblTitle = new Label { Text = "THÔNG TIN THANH TOÁN", Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.Navy };
            Panel pnlInfo = new Panel { Dock = DockStyle.Top, Height = 80, Padding = new Padding(20) };
            Label lblInfo = new Label { Text = string.Format("Học viên: {0} ({1})\nSố tiền nợ: {2:N0} VNĐ", _tenHV, _maHV, _noHienTai), Dock = DockStyle.Fill, Font = new Font("Segoe UI", 11), ForeColor = Color.Red };
            pnlInfo.Controls.Add(lblInfo);

            // 2. METHOD
            GroupBox grpMethod = new GroupBox { Text = "Chọn phương thức", Dock = DockStyle.Top, Height = 70, Padding = new Padding(10), Font = new Font("Segoe UI", 10) };
            rdoTienMat = new RadioButton { Text = "💵 Tiền Mặt", Location = new Point(50, 30), AutoSize = true, Checked = true };
            rdoQR = new RadioButton { Text = "🏦 Chuyển Khoản (QR)", Location = new Point(250, 30), AutoSize = true };
            grpMethod.Controls.AddRange(new Control[] { rdoTienMat, rdoQR });
            rdoTienMat.CheckedChanged += (s, e) => ToggleMode();
            rdoQR.CheckedChanged += (s, e) => ToggleMode();

            // 3. INPUT
            Panel pnlInput = new Panel { Dock = DockStyle.Top, Height = 60 };
            Label lblNhap = new Label { Text = "Số tiền thu:", Location = new Point(30, 20), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txbTien = new TextBox { Text = _noHienTai.ToString("N0"), Location = new Point(130, 18), Width = 200, Font = new Font("Segoe UI", 11, FontStyle.Bold), TextAlign = HorizontalAlignment.Right, ForeColor = Color.DarkRed };
            pnlInput.Controls.AddRange(new Control[] { lblNhap, txbTien });
            txbTien.TextChanged += (s, e) => { if (rdoQR.Checked) LoadQR(); };

            // 4. QR (Absolute positioning ở giữa)
            picQR = new PictureBox { Size = new Size(180, 180), Location = new Point(180, 260), SizeMode = PictureBoxSizeMode.Zoom, BorderStyle = BorderStyle.FixedSingle, Visible = false };
            lblHuongDan = new Label { Text = "Đang tải mã QR...", Location = new Point(0, 450), Width = 550, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 9, FontStyle.Italic), Visible = false };

            // 5. FOOTER (CHECKBOX & BUTTON)
            Panel pnlFooter = new Panel { Dock = DockStyle.Bottom, Height = 100, BackColor = Color.WhiteSmoke };

            chkXacNhan = new CheckBox
            {
                Text = "✅ Xác nhận đã nhận đủ tiền từ học viên",
                Location = new Point(30, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.Red,
                Cursor = Cursors.Hand
            };

            Button btnConfirm = new Button
            {
                Text = "🖨️ XÁC NHẬN IN PHIẾU",
                Location = new Point(100, 50),
                Size = new Size(350, 40),
                BackColor = Color.Navy,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnConfirm.Click += BtnConfirm_Click;

            pnlFooter.Controls.Add(chkXacNhan);
            pnlFooter.Controls.Add(btnConfirm);

            this.Controls.Add(pnlFooter);
            this.Controls.AddRange(new Control[] { lblHuongDan, picQR, pnlInput, grpMethod, pnlInfo, lblTitle });
        }

        private void ToggleMode()
        {
            if (rdoQR.Checked) { picQR.Visible = true; lblHuongDan.Visible = true; LoadQR(); }
            else { picQR.Visible = false; lblHuongDan.Visible = false; }
        }

        private void LoadQR()
        {
            try
            {
                string s = txbTien.Text.Replace(",", "").Replace(".", "").Trim();
                decimal tien;
                if (decimal.TryParse(s, out tien) && tien > 0)
                {
                    string url = string.Format("https://img.vietqr.io/image/MB-0705840113-compact.png?amount={0}&addInfo={1}", tien, _maHV);
                    picQR.LoadAsync(url);
                    lblHuongDan.Text = string.Format("Quét mã để chuyển: {0:N0} VNĐ", tien);
                }
            }
            catch { }
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (!chkXacNhan.Checked)
            {
                MessageBox.Show("Vui lòng tick vào ô 'Xác nhận đã nhận đủ tiền' trước khi in phiếu!", "Chưa xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string s = txbTien.Text.Replace(",", "").Replace(".", "").Trim();
            decimal tien;
            if (!decimal.TryParse(s, out tien) || tien <= 0) { MessageBox.Show("Số tiền không hợp lệ!"); return; }
            if (tien > _noHienTai) { MessageBox.Show("Thu quá số nợ!"); return; }

            FinalAmount = tien;
            FinalMethod = rdoTienMat.Checked ? "Tiền mặt" : "Chuyển khoản QR";

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}