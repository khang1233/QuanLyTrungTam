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
        // 1. THIẾT KẾ GIAO DIỆN (ĐÃ REFACTOR)
        // =================================================================================
        private void SetupBetterUI()
        {
            this.Text = "Tra Cứu & Thu Học Phí";
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.WindowState = FormWindowState.Maximized;
            this.Font = new Font("Segoe UI", 10F);

            // --- A. HEADER ---
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = Color.White, Padding = new Padding(20, 0, 20, 0) };
            Label lblTitle = new Label { 
                Text = "TRA CỨU & THU HỌC PHÍ", 
                Font = new Font("Segoe UI", 18, FontStyle.Bold), 
                ForeColor = Color.FromArgb(33, 150, 243), 
                AutoSize = true, 
                TextAlign = ContentAlignment.MiddleLeft 
            };
            lblTitle.Location = new Point(20, (pnlHeader.Height - lblTitle.Height) / 2);

            txbSearch.Location = new Point(350, 20);
            txbSearch.Width = 400;
            txbSearch.Font = new Font("Segoe UI", 11);
            txbSearch.BorderStyle = BorderStyle.FixedSingle;
            SetPlaceholder(txbSearch, "🔍 Nhập tên hoặc mã học viên...");
            txbSearch.TextChanged += Logic_SearchHV;

            pnlHeader.Controls.AddRange(new Control[] { lblTitle, txbSearch });
            this.Controls.Add(pnlHeader);

            // --- B. BODY ---
            SplitContainer split = new SplitContainer { Dock = DockStyle.Fill, SplitterWidth = 10, BackColor = Color.FromArgb(240, 242, 245) };
            split.FixedPanel = FixedPanel.Panel1;
            split.SplitterDistance = 450; 

            // TRÁI (DANH SÁCH)
            Panel pnlLeft = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10, 10, 5, 10) };
            GroupBox grpList = new GroupBox { 
                Text = "Danh Sách Học Viên", 
                Dock = DockStyle.Fill, 
                Font = new Font("Segoe UI", 10, FontStyle.Bold), 
                ForeColor = Color.DimGray,
                BackColor = Color.White
            };
            // Inner padding for GroupBox
            Panel pnlGridWrap = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };
            StyleGrid(dgvSearchResult);
            dgvSearchResult.CellClick += Logic_ChonHV;
            pnlGridWrap.Controls.Add(dgvSearchResult);
            grpList.Controls.Add(pnlGridWrap);
            pnlLeft.Controls.Add(grpList);
            split.Panel1.Controls.Add(pnlLeft);

            // PHẢI (CHI TIẾT & THANH TOÁN)
            Panel pnlRight = new Panel { Dock = DockStyle.Fill, Padding = new Padding(5, 10, 10, 10) };
            Panel pnlRightContent = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };

            // 1. Các lớp đang học
            GroupBox grpLop = new GroupBox { 
                Text = "Các Lớp Đang Theo Học", 
                Dock = DockStyle.Top, 
                Height = 300, 
                Font = new Font("Segoe UI", 10, FontStyle.Bold), 
                ForeColor = Color.DimGray,
                BackColor = Color.White
            };
            Panel pnlLopWrap = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };
            StyleGrid(dgvLopHoc);
            pnlLopWrap.Controls.Add(dgvLopHoc);
            grpLop.Controls.Add(pnlLopWrap);

            // 2. Tài chính & Thanh toán
            Panel pnlFinance = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Margin = new Padding(0, 10, 0, 0) };
            // -- Label Tài Chính
            lblTaiChinh.Dock = DockStyle.Fill;
            lblTaiChinh.TextAlign = ContentAlignment.MiddleCenter;
            lblTaiChinh.Font = new Font("Segoe UI", 14);
            lblTaiChinh.Text = "👈 Vui lòng chọn học viên từ danh sách bên trái";
            
            // -- Panel Thanh Toán (Bottom of Finance)
            Panel pnlPay = new Panel { Dock = DockStyle.Bottom, Height = 100, BackColor = Color.WhiteSmoke }; // Light Gray
            pnlPay.Paint += (s, e) => e.Graphics.DrawLine(Pens.Silver, 0, 0, pnlPay.Width, 0); // Top Border

            Label lblPayTitle = new Label { Text = "THU PHÍ:", Location = new Point(30, 35), AutoSize = true, Font = new Font("Segoe UI", 12, FontStyle.Bold) };
            
            txbDongTien.Location = new Point(130, 32);
            txbDongTien.Width = 250;
            txbDongTien.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            txbDongTien.ForeColor = Color.DarkRed;
            txbDongTien.TextAlign = HorizontalAlignment.Right;
            txbDongTien.BorderStyle = BorderStyle.FixedSingle;

            Button btnPay = new Button { 
                Text = "XÁC NHẬN THU", 
                Location = new Point(410, 30), 
                Size = new Size(180, 40),
                BackColor = Color.FromArgb(40, 167, 69), // Green
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnPay.FlatAppearance.BorderSize = 0;
            btnPay.Click += Logic_ThanhToanTrucTiep;

            pnlPay.Controls.AddRange(new Control[] { lblPayTitle, txbDongTien, btnPay });

            pnlFinance.Controls.Add(lblTaiChinh); // Fill
            pnlFinance.Controls.Add(pnlPay);      // Bottom

            // Add to Right Content
            pnlRightContent.Controls.Add(pnlFinance);
            pnlRightContent.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 10 }); // Spacer
            pnlRightContent.Controls.Add(grpLop); // Top

            pnlRight.Controls.Add(pnlRightContent);
            split.Panel2.Controls.Add(pnlRight);

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
            dgv.RowTemplate.Height = 40;
            dgv.EnableHeadersVisualStyles = false;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243); // Modern Blue
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 45;

            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.GridColor = Color.WhiteSmoke;
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
                dt.DefaultView.RowFilter = string.Format("MaHV LIKE '%{0}%' OR HoTen LIKE '%{0}%'", keyword);
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

            lblTaiChinh.Text = string.Format("Học Viên: {0}\n\nTổng Học Phí: {1:N0} đ\nĐã Đóng:      {2:N0} đ\n--------------------------\nCÒN NỢ:       {3:N0} VNĐ", 
                tenHV.ToUpper(), info.TongNo, info.DaDong, info.ConNo);

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

            decimal soTien;
            if (decimal.TryParse(inputTien, out soTien) && soTien > 0)
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
                    MessageBox.Show(string.Format("Số tiền đóng ({0:N0}) vượt quá nợ ({1:N0}).", soTien, conNo), "Cảnh báo");
                    txbDongTien.Text = conNo.ToString("N0");
                    return;
                }

                // XÁC NHẬN
                if (MessageBox.Show(string.Format("Xác nhận thu {0:N0} đ?", soTien), "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
            Form form = dlg as Form;
            if (form != null) { form.WindowState = FormWindowState.Maximized; form.TopMost = true; }

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
            g.DrawString(string.Format("Ngày: {0:dd/MM/yyyy HH:mm}", DateTime.Now), fSub, Brushes.Black, center, y, centerAlign); y += 60;

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