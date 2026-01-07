using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.Windows.Forms;
using QuanLyTrungTam.DAO;
using QuanLyTrungTam.DTO;
using QuanLyTrungTam.Utilities;

namespace QuanLyTrungTam
{
    public partial class FrmTraCuuHocPhi : Form
    {
        private string currentMaHV = "";
        
        // Print variables
        private string _printTenHV = "";
        private string _printSoTien = "";
        private string _printHinhThuc = "";

        public FrmTraCuuHocPhi()
        {
            InitializeComponent();
            
            // Shortcuts
            this.KeyPreview = true;
            
            LoadSearchData("");
            SetPlaceholder(txbSearch, "🔍 Nhập tên hoặc mã học viên...");
            
            // Initial Style
            StyleGrid(dgvSearchResult);
            StyleGrid(dgvLopHoc);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                LoadSearchData(txbSearch.Text);
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }

        private void StyleGrid(DataGridView dgv)
        {
             GridViewHelper.StandardizeGrid(dgv, new System.Collections.Generic.List<string> { 
                 "DiaChi", "NgaySinh", "Email", "NgayGiaNhap", "MaLop", "MaKyNang", "SoDienThoai" 
             });
        }

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
                dt.DefaultView.RowFilter = $"MaHV LIKE '%{keyword}%' OR HoTen LIKE '%{keyword}%'";

            dgvSearchResult.DataSource = dt;
            dgvSearchResult.DataSource = dt;
            
            // Register Event for persistent coloring
            dgvSearchResult.CellFormatting -= DgvSearchResult_CellFormatting;
            dgvSearchResult.CellFormatting += DgvSearchResult_CellFormatting;
        }

        private void DgvSearchResult_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvSearchResult.Columns[e.ColumnIndex].Name == "TrangThaiHocPhi" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (status == "Còn nợ")
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
                else if (status == "Hoàn thành")
                {
                    e.CellStyle.ForeColor = Color.Green;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
            }
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

                dgvLopHoc.DataSource = dtLop;
                
                // Helper handles headers automatically via mapping
                GridViewHelper.StandardizeGrid(dgvLopHoc, new System.Collections.Generic.List<string> { "NgayDangKy" });

                UpdateFinanceInfo(tenHV);
            }
        }

        void UpdateFinanceInfo(string tenHV)
        {
            decimal tongNo = TuitionDAO.Instance.GetTongNo(currentMaHV);
            string qSum = "SELECT SUM(SoTienDong) FROM ThanhToan WHERE MaHV = @ma";
            object result = DataProvider.Instance.ExecuteScalar(qSum, new object[] { currentMaHV });
            decimal daDong = (result == DBNull.Value || result == null) ? 0 : Convert.ToDecimal(result);

            decimal conNo = tongNo - daDong;

            string info = string.Format("Học Viên: {0}\n\nTổng Học Phí: {1:N0} VNĐ\n--------------------------\n{2}--------------------------\nCÒN NỢ:       {3:N0} VNĐ", 
                tenHV.ToUpper(), tongNo, (daDong > 0 ? string.Format("Đã Đóng:      {0:N0} VNĐ\n", daDong) : "(Chưa đóng khoản nào)\n"), conNo);

            lblTaiChinh.Text = info;
            lblTaiChinh.ForeColor = conNo > 0 ? Color.Red : Color.Green;
        }

        private void BtnLapHoaDon_Click(object sender, EventArgs e)
        {
            // Security Check
            if (UserSession.Instance != null && !UserSession.Instance.IsAdmin() && UserSession.Instance.CurrentUser.Quyen != "NhanSu") 
            {
                 MessageBox.Show("Bạn không có quyền thu học phí! Chỉ Admin hoặc Nhân sự mới được phép.", "Truy cập bị từ chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return;
            }

            if (string.IsNullOrEmpty(currentMaHV)) { MessageBox.Show("Vui lòng chọn học viên trước!"); return; }

            decimal tongNo = TuitionDAO.Instance.GetTongNo(currentMaHV);
            string qSum = "SELECT SUM(SoTienDong) FROM ThanhToan WHERE MaHV = @ma";
            object result = DataProvider.Instance.ExecuteScalar(qSum, new object[] { currentMaHV });
            decimal daDong = (result == DBNull.Value || result == null) ? 0 : Convert.ToDecimal(result);
            decimal conNo = tongNo - daDong;

            if (conNo <= 0) { MessageBox.Show("Học viên này đã hết nợ!"); return; }

            string tenHV = dgvSearchResult.CurrentRow.Cells["HoTen"].Value.ToString();

            // Use Inner Class FrmThanhToanDialog
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
                    LoadSearchData(txbSearch.Text);
                }
                else MessageBox.Show("Lỗi lưu dữ liệu!");
            }
        }

        // --- IN ẤN ---
        private void ThucHienInHoaDon(string maHV, string tenHV, decimal soTien, string hinhThuc)
        {
            _printTenHV = tenHV; _printSoTien = $"{soTien:N0} VNĐ"; _printHinhThuc = hinhThuc;
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
            g.DrawString($"(Hình thức: {_printHinhThuc})", new Font("Arial", 11, FontStyle.Italic), Brushes.Black, w / 2, y, center); y += 50;

            float x = 100;
            g.DrawString($"Mã HV:   {currentMaHV}", new Font("Arial", 12), Brushes.Black, x, y); y += 35;
            g.DrawString($"Họ Tên:  {_printTenHV}", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, x, y); y += 35;
            g.DrawString($"Số Tiền: {_printSoTien}", new Font("Arial", 16, FontStyle.Bold), Brushes.Red, x, y); y += 45;
            g.DrawString($"Ngày:    {DateTime.Now:dd/MM/yyyy HH:mm}", new Font("Arial", 12), Brushes.Gray, x, y);

            float rightX = w - 200;
            y += 50;
            g.DrawString("Người nộp tiền", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, x + 20, y);
            g.DrawString("Người thu tiền", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, rightX, y);

            y += 30;
            g.DrawString("(Ký, họ tên)", new Font("Arial", 10, FontStyle.Italic), Brushes.Gray, x + 30, y);
            g.DrawString("(Ký, đóng dấu)", new Font("Arial", 10, FontStyle.Italic), Brushes.Gray, rightX + 10, y);

            y += 40;
            Font fKhang = new Font("Mistral", 20, FontStyle.Bold);
            g.DrawString("Khang", fKhang, Brushes.Black, rightX + 20, y);
            y += 40;
            g.DrawString("Trần Minh Khang", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, rightX - 20, y);
        }

        private void SafeSetHeader(DataGridView dgv, string c, string t) { if (dgv.Columns.Contains(c)) dgv.Columns[c].HeaderText = t; }
        private void SafeSetVisible(DataGridView dgv, string c, bool v) { if (dgv.Columns.Contains(c)) dgv.Columns[c].Visible = v; }
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
                if (r.Cells["TrangThaiHocPhi"].Value?.ToString() == "Còn nợ") r.Cells["TrangThaiHocPhi"].Style.ForeColor = Color.Red;
                else r.Cells["TrangThaiHocPhi"].Style.ForeColor = Color.Green;
            }
        }
        public void AutoSearch(string maHV)
        {
            txbSearch.Text = maHV;
            Logic_SearchHV(null, null);
        }
    }
    
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
            this.Size = new Size(550, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;

            Label lblTitle = new Label { Text = "THÔNG TIN THANH TOÁN", Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.Navy };
            Panel pnlInfo = new Panel { Dock = DockStyle.Top, Height = 80, Padding = new Padding(20) };
            Label lblInfo = new Label { Text = $"Học viên: {_tenHV} ({_maHV})\nSố tiền nợ: {_noHienTai:N0} VNĐ", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 11), ForeColor = Color.Red };
            pnlInfo.Controls.Add(lblInfo);

            GroupBox grpMethod = new GroupBox { Text = "Chọn phương thức", Dock = DockStyle.Top, Height = 70, Padding = new Padding(10), Font = new Font("Segoe UI", 10) };
            rdoTienMat = new RadioButton { Text = "💵 Tiền Mặt", Location = new Point(50, 30), AutoSize = true, Checked = true };
            rdoQR = new RadioButton { Text = "🏦 Chuyển Khoản (QR)", Location = new Point(250, 30), AutoSize = true };
            grpMethod.Controls.AddRange(new Control[] { rdoTienMat, rdoQR });
            rdoTienMat.CheckedChanged += (s, e) => ToggleMode();
            rdoQR.CheckedChanged += (s, e) => ToggleMode();

            Panel pnlInput = new Panel { Dock = DockStyle.Top, Height = 60 };
            Label lblNhap = new Label { Text = "Số tiền thu:", Location = new Point(30, 20), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txbTien = new TextBox { Text = _noHienTai.ToString("N0"), Location = new Point(130, 18), Width = 200, Font = new Font("Segoe UI", 11, FontStyle.Bold), TextAlign = HorizontalAlignment.Right, ForeColor = Color.DarkRed };
            pnlInput.Controls.AddRange(new Control[] { lblNhap, txbTien });
            txbTien.TextChanged += (s, e) => { if (rdoQR.Checked) LoadQR(); };

            picQR = new PictureBox { Size = new Size(180, 180), Location = new Point(180, 260), SizeMode = PictureBoxSizeMode.Zoom, BorderStyle = BorderStyle.FixedSingle, Visible = false };
            lblHuongDan = new Label { Text = "Đang tải mã QR...", Location = new Point(0, 450), Width = 550, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 9, FontStyle.Italic), Visible = false };

            Panel pnlFooter = new Panel { Dock = DockStyle.Bottom, Height = 100, BackColor = Color.WhiteSmoke };
            chkXacNhan = new CheckBox { Text = "✅ Xác nhận đã nhận đủ tiền từ học viên", Location = new Point(30, 15), AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.Red, Cursor = Cursors.Hand };
            Button btnConfirm = new Button { Text = "🖨️ XÁC NHẬN IN PHIẾU", Location = new Point(100, 50), Size = new Size(350, 40), BackColor = Color.FromArgb(33, 150, 243), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11, FontStyle.Bold), Cursor = Cursors.Hand };
            btnConfirm.Click += BtnConfirm_Click;
            pnlFooter.Controls.Add(chkXacNhan);
            pnlFooter.Controls.Add(btnConfirm);

            this.Controls.Add(pnlFooter);
            this.Controls.AddRange(new Control[] { lblHuongDan, picQR, pnlInput, grpMethod, pnlInfo, lblTitle });
        }

        private void ToggleMode() { if (rdoQR.Checked) { picQR.Visible = true; lblHuongDan.Visible = true; LoadQR(); } else { picQR.Visible = false; lblHuongDan.Visible = false; } }
        private void LoadQR() { try { string s = txbTien.Text.Replace(",", "").Replace(".", "").Trim(); decimal tien; if (decimal.TryParse(s, out tien) && tien > 0) { picQR.LoadAsync(string.Format("https://img.vietqr.io/image/MB-0705840113-compact.png?amount={0}&addInfo={1}", tien, _maHV)); lblHuongDan.Text = string.Format("Quét mã để chuyển: {0:N0} VNĐ", tien); } } catch { } }
        private void BtnConfirm_Click(object sender, EventArgs e) {
            if (!chkXacNhan.Checked) { MessageBox.Show("Vui lòng tick vào ô 'Xác nhận' trước!", "Chưa xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            string s = txbTien.Text.Replace(",", "").Replace(".", "").Trim(); decimal tien;
            if (!decimal.TryParse(s, out tien) || tien <= 0) { MessageBox.Show("Số tiền không hợp lệ!"); return; }
            if (tien > _noHienTai) { MessageBox.Show("Thu quá số nợ!"); return; }
            FinalAmount = tien; FinalMethod = rdoTienMat.Checked ? "Tiền mặt" : "Chuyển khoản QR";
            this.DialogResult = DialogResult.OK; this.Close();
        }
    }
}