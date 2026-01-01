using System;
using System.Drawing;
using System.Drawing.Printing; 
using System.Data;
using System.Windows.Forms;
using QuanLyTrungTam.DAO;
using QuanLyTrungTam.BUS;

namespace QuanLyTrungTam
{
    public partial class HocPhi : Form
    {
        // --- KHAI BÁO CONTROL ---
        private TextBox txbSearch = new TextBox();
        private DataGridView dgvSearchResult = new DataGridView();
        private DataGridView dgvLopHoc = new DataGridView();
        private Label lblTaiChinh = new Label();
        private TextBox txbDongTien = new TextBox();
        private string currentMaHV = "";

        // Biến dùng để in
        private string _tenHVIn = "";
        private string _soTienIn = "";
        private string _noiDungIn = "";

        public HocPhi()
        {
            // InitializeComponent(); // Bỏ comment nếu cần
            SetupBetterUI();    // Giao diện đẹp
            LoadSearchData(""); // Load dữ liệu ban đầu
        }

        // =================================================================================
        // 1. THIẾT KẾ GIAO DIỆN (GIỮ NGUYÊN CODE CỦA BẠN)
        // =================================================================================
        private void SetupBetterUI()
        {
            this.Text = "Tra Cứu Thu Học Phí";
            this.BackColor = Color.WhiteSmoke;
            this.WindowState = FormWindowState.Maximized;
            this.Font = new Font("Segoe UI", 10F);

            // --- A. HEADER ---
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.White, Padding = new Padding(20, 10, 20, 10) };
            Label lblTitle = new Label { Text = "TRA CỨU HỌC VIÊN", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = ColorTranslator.FromHtml("#009688"), AutoSize = true, Location = new Point(20, 15) };

            txbSearch.Location = new Point(350, 18);
            txbSearch.Width = 400;
            txbSearch.Font = new Font("Segoe UI", 11);
            SetPlaceholder(txbSearch, "🔍 Nhập tên hoặc mã học viên...");
            txbSearch.TextChanged += Logic_SearchHV;

            pnlHeader.Controls.AddRange(new Control[] { lblTitle, txbSearch });
            this.Controls.Add(pnlHeader);

            // --- B. BODY ---
            SplitContainer split = new SplitContainer { Dock = DockStyle.Fill, SplitterWidth = 10, BackColor = Color.WhiteSmoke };
            split.Width = 1200;
            split.FixedPanel = FixedPanel.Panel1;
            split.Panel1MinSize = 350;
            split.SplitterDistance = 400;
            split.Panel2MinSize = 350;

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
            lblTaiChinh.TextAlign = ContentAlignment.MiddleCenter;
            lblTaiChinh.Font = new Font("Segoe UI", 14);
            lblTaiChinh.Text = "👈 Vui lòng chọn học viên từ danh sách bên trái";
            pnlDebt.Controls.Add(lblTaiChinh);

            Panel pnlPay = new Panel { Dock = DockStyle.Bottom, Height = 80, BackColor = ColorTranslator.FromHtml("#E8F5E9") };
            pnlPay.BorderStyle = BorderStyle.FixedSingle;
            Label lblPayTitle = new Label { Text = "Thu Phí:", Location = new Point(30, 28), AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold) };

            txbDongTien.Location = new Point(120, 25);
            txbDongTien.Width = 200;
            txbDongTien.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            txbDongTien.ForeColor = Color.DarkRed;
            txbDongTien.TextAlign = HorizontalAlignment.Right;

            Button btnPay = new Button { Text = "XÁC NHẬN THU", Location = new Point(340, 22), Size = new Size(160, 35) };
            btnPay.FlatStyle = FlatStyle.Flat;
            btnPay.FlatAppearance.BorderSize = 0;
            btnPay.BackColor = ColorTranslator.FromHtml("#FFC107");
            btnPay.ForeColor = Color.Black;
            btnPay.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnPay.Cursor = Cursors.Hand;

            // GẮN SỰ KIỆN THANH TOÁN
            btnPay.Click += Logic_ThanhToanTrucTiep;

            pnlPay.Controls.AddRange(new Control[] { lblPayTitle, txbDongTien, btnPay });

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
            dgv.RowTemplate.Height = 35;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#009688");
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#B2DFDB");
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        // =================================================================================
        // 2. LOGIC XỬ LÝ
        // =================================================================================

        void LoadSearchData(string keyword)
        {
            // [REFACTOR] Dùng HocVienBUS
            DataTable dt = HocVienBUS.Instance.GetListHocVien();
            if (dt == null) return;

            if (!dt.Columns.Contains("TrangThaiHocPhi"))
                dt.Columns.Add("TrangThaiHocPhi", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                string maHV = row["MaHV"].ToString();
                
                // [REFACTOR] Dùng TuitionBUS.GetHocPhiInfo
                HocPhiInfo info = TuitionBUS.Instance.GetHocPhiInfo(maHV);
                
                row["TrangThaiHocPhi"] = info.ConNo > 0 ? "Còn nợ" : "Hoàn thành";
            }

            if (!string.IsNullOrEmpty(keyword) && keyword != "🔍 Nhập tên hoặc mã học viên...")
            {
                dt.DefaultView.RowFilter = $"MaHV LIKE '%{keyword}%' OR HoTen LIKE '%{keyword}%'";
            }

            dgvSearchResult.DataSource = dt;
            SafeSetHeader(dgvSearchResult, "MaHV", "Mã HV");
            SafeSetHeader(dgvSearchResult, "HoTen", "Họ Tên");
            SafeSetHeader(dgvSearchResult, "SoDienThoai", "SĐT");
            SafeSetHeader(dgvSearchResult, "TrangThaiHocPhi", "Học Phí");

            string[] colsToHide = { "DiaChi", "NgaySinh", "Email", "NgayGiaNhap", "MaLop", "MaKyNang" };
            foreach (string col in colsToHide) SafeSetVisible(dgvSearchResult, col, false);

            dgvSearchResult.Columns["MaHV"].Width = 80;
            dgvSearchResult.Columns["TrangThaiHocPhi"].Width = 110;
            ColorizeHocPhiColumn();
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

                // [REFACTOR] Dùng TuitionBUS
                DataTable dtLop = TuitionBUS.Instance.GetListDangKy(currentMaHV);
                dgvLopHoc.DataSource = dtLop;

                SafeSetHeader(dgvLopHoc, "TenKyNang", "Môn Học");
                SafeSetHeader(dgvLopHoc, "TenLop", "Lớp");
                SafeSetHeader(dgvLopHoc, "HocPhiLop", "Học Phí");
                if (dgvLopHoc.Columns.Contains("HocPhiLop"))
                    dgvLopHoc.Columns["HocPhiLop"].DefaultCellStyle.Format = "N0";

                SafeSetVisible(dgvLopHoc, "NgayDangKy", false);
                UpdateFinanceInfo(tenHV);
            }
        }

        void UpdateFinanceInfo(string tenHV)
        {
            // [REFACTOR] Dùng TuitionBUS.GetHocPhiInfo
            HocPhiInfo info = TuitionBUS.Instance.GetHocPhiInfo(currentMaHV);

            lblTaiChinh.Text = $"Học Viên: {tenHV.ToUpper()}\n\n" +
                               $"Tổng Học Phí: {info.TongNo:N0} đ\n" +
                               $"Đã Đóng:      {info.DaDong:N0} đ\n" +
                               $"--------------------------\n" +
                               $"CÒN NỢ:       {info.ConNo:N0} VNĐ";

            lblTaiChinh.ForeColor = info.ConNo > 0 ? Color.Red : Color.Green;
            txbDongTien.Clear();
            txbDongTien.Focus();
        }

        // =========================================================================
        // 3. LOGIC THANH TOÁN & IN HÓA ĐƠN (ĐÃ SỬA)
        // =========================================================================

        private void Logic_ThanhToanTrucTiep(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaHV))
            {
                MessageBox.Show("Vui lòng chọn học viên!");
                return;
            }

            string inputTien = txbDongTien.Text.Replace(",", "").Replace(".", "").Trim();

            if (decimal.TryParse(inputTien, out decimal soTien) && soTien > 0)
            {
                // [REFACTOR] Dùng TuitionBUS.GetHocPhiInfo
                HocPhiInfo info = TuitionBUS.Instance.GetHocPhiInfo(currentMaHV);
                decimal conNo = info.ConNo;

                if (conNo <= 0)
                {
                    MessageBox.Show("Học viên này đã hoàn thành học phí!", "Thông báo");
                    return;
                }
                if (soTien > conNo)
                {
                    MessageBox.Show($"Số tiền đóng ({soTien:N0}) vượt quá nợ ({conNo:N0}).", "Cảnh báo");
                    txbDongTien.Text = conNo.ToString("N0");
                    return;
                }

                // XÁC NHẬN
                if (MessageBox.Show($"Xác nhận thu {soTien:N0} đ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string noiDungThu = "Thu tại quầy";

                    // Lưu vào DB ([REFACTOR] Dùng TuitionBUS)
                    if (TuitionBUS.Instance.InsertThanhToan(currentMaHV, soTien, noiDungThu))
                    {
                        MessageBox.Show(" ✅  Thu tiền thành công!");

                        // Cập nhật giao diện tiền nợ
                        string tenHV = dgvSearchResult.CurrentRow.Cells["HoTen"].Value.ToString();
                        UpdateFinanceInfo(tenHV);

                        // --- 4. HỎI IN HÓA ĐƠN ---
                        DialogResult ask = MessageBox.Show("Bạn có muốn IN HÓA ĐƠN không?", "In Ấn", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (ask == DialogResult.Yes)
                        {
                            ThucHienInHoaDon(currentMaHV, tenHV, soTien, noiDungThu);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Số tiền không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- HÀM 1: CẤU HÌNH IN ---
        private void ThucHienInHoaDon(string maHV, string tenHV, decimal soTien, string noiDung)
        {
            // Lưu biến tạm để hàm vẽ dùng
            _tenHVIn = tenHV;
            _soTienIn = string.Format("{0:N0} VNĐ", soTien);
            _noiDungIn = noiDung;

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(VeHoaDon);

            PrintPreviewDialog dlg = new PrintPreviewDialog();
            dlg.Document = pd;

            // Cấu hình hiển thị to rõ
            dlg.Width = 1000;
            dlg.Height = 800;
            dlg.StartPosition = FormStartPosition.CenterScreen;
            if (dlg is Form form) { form.WindowState = FormWindowState.Maximized; form.TopMost = true; }

            dlg.ShowDialog();
        }

        // --- HÀM 2: VẼ HÓA ĐƠN ---
        private void VeHoaDon(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            float w = e.PageBounds.Width;
            float center = w / 2;
            float y = 50;

            // Font
            Font fTitle = new Font("Arial", 22, FontStyle.Bold);
            Font fSub = new Font("Arial", 11);
            Font fBold = new Font("Arial", 12, FontStyle.Bold);
            Font fContent = new Font("Arial", 12);
            Font fMoney = new Font("Arial", 16, FontStyle.Bold);

            StringFormat centerAlign = new StringFormat { Alignment = StringAlignment.Center };
            StringFormat rightAlign = new StringFormat { Alignment = StringAlignment.Far };

            // Header
            g.DrawString("TRUNG TÂM ĐÀO TẠO", fTitle, Brushes.Blue, center, y, centerAlign); y += 40;
            g.DrawString("BIÊN LAI THU TIỀN", new Font("Arial", 24, FontStyle.Bold), Brushes.Red, center, y, centerAlign); y += 50;
            g.DrawString($"Ngày: {DateTime.Now:dd/MM/yyyy HH:mm}", fSub, Brushes.Black, center, y, centerAlign); y += 60;

            // Body
            float left = 100;
            g.DrawString("Mã Học Viên:", fBold, Brushes.Black, left, y);
            g.DrawString(currentMaHV, fContent, Brushes.Black, left + 150, y); y += 40;

            g.DrawString("Tên Học Viên:", fBold, Brushes.Black, left, y);
            g.DrawString(_tenHVIn, fContent, Brushes.Black, left + 150, y); y += 40;

            g.DrawString("Nội dung:", fBold, Brushes.Black, left, y);
            g.DrawString(_noiDungIn, fContent, Brushes.Black, left + 150, y); y += 40;

            // Khung Tiền
            g.DrawRectangle(Pens.Black, left, y, w - 2 * left, 60);
            g.DrawString("SỐ TIỀN THU:", fBold, Brushes.Black, left + 20, y + 20);
            g.DrawString(_soTienIn, fMoney, Brushes.Red, w - left - 20, y + 15, rightAlign); y += 120;

            // Footer
            g.DrawString("Người nộp tiền", fBold, Brushes.Black, left + 20, y);
            g.DrawString("Người thu tiền", fBold, Brushes.Black, w - left - 150, y); y += 30;
            g.DrawString("(Ký tên)", fSub, Brushes.Gray, left + 35, y);
            g.DrawString("(Ký, đóng dấu)", fSub, Brushes.Gray, w - left - 130, y);
        }

        public void AutoSearch(string maHV)
        {
            txbSearch.Text = maHV;
            txbSearch.ForeColor = Color.Black;
            LoadSearchData(maHV);
            if (dgvSearchResult.Rows.Count > 0)
            {
                dgvSearchResult.Rows[0].Selected = true;
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
        private void ColorizeHocPhiColumn()
        {
            foreach (DataGridViewRow row in dgvSearchResult.Rows)
            {
                if (row.Cells["TrangThaiHocPhi"].Value == null) continue;
                string status = row.Cells["TrangThaiHocPhi"].Value.ToString();
                if (status == "Còn nợ")
                {
                    row.Cells["TrangThaiHocPhi"].Style.ForeColor = Color.Red;
                    row.Cells["TrangThaiHocPhi"].Style.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
                else
                {
                    row.Cells["TrangThaiHocPhi"].Style.ForeColor = Color.Green;
                    row.Cells["TrangThaiHocPhi"].Style.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
            }
        }
    }
}