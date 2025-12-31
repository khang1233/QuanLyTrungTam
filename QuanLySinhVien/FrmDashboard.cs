using QuanLyTrungTam.DAO;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyTrungTam
{
    public partial class FrmDashboard : Form
    {
        // --- CẤU HÌNH MÀU SẮC (GIỮ NGUYÊN) ---
        private struct Colors
        {
            public static Color Primary = ColorTranslator.FromHtml("#009688");
            public static Color Secondary = ColorTranslator.FromHtml("#607D8B");
            public static Color Success = ColorTranslator.FromHtml("#4CAF50");
            public static Color Warning = ColorTranslator.FromHtml("#FF9800");
            public static Color Danger = ColorTranslator.FromHtml("#F44336");
            public static Color Info = ColorTranslator.FromHtml("#2196F3");
            public static Color Bg = ColorTranslator.FromHtml("#F2F4F8");
        }

        public FrmDashboard()
        {
            InitializeComponent();
            SetupDashboardUI();
        }

        // GIỮ NGUYÊN HÀM SetupDashboardUI() CỦA BẠN (HOẶC CỦA CODE TRƯỚC), CHỈ THAY ĐỔI LoadData

        private void SetupDashboardUI()
        {
            this.Text = "Dashboard Quản Trị Trung Tâm";
            this.BackColor = Colors.Bg;
            this.Size = new Size(1300, 800);
            this.Controls.Clear();

            // HEADER
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.White, Padding = new Padding(20) };
            Label lblTitle = new Label { Text = "TỔNG QUAN HOẠT ĐỘNG", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Colors.Primary, AutoSize = true, Location = new Point(20, 15) };
            Button btnRefresh = new Button { Text = "🔄 Làm Mới Dữ Liệu", Location = new Point(1100, 12), Size = new Size(160, 35), BackColor = Colors.Primary, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Anchor = AnchorStyles.Top | AnchorStyles.Right };
            btnRefresh.Click += (s, e) => LoadData();
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(btnRefresh);
            this.Controls.Add(pnlHeader);

            // MAIN SCROLL PANEL
            Panel pnlMain = new Panel { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(20) };
            this.Controls.Add(pnlMain);
            pnlMain.BringToFront();

            TableLayoutPanel tableMain = new TableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, ColumnCount = 1, RowCount = 5, Padding = new Padding(0, 0, 0, 50) };
            tableMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pnlMain.Controls.Add(tableMain);

            // ROW 1: KPIs
            TableLayoutPanel row1 = CreateRowLayout(4, 150);
            row1.Controls.Add(CreateKPICard("TỔNG HỌC VIÊN", "0", "Người", Colors.Info, "lblHocVien"), 0, 0);
            row1.Controls.Add(CreateKPICard("TỔNG LỢI NHUẬN", "0", "VNĐ", Colors.Success, "lblLoiNhuan"), 1, 0);
            row1.Controls.Add(CreateKPICard("LỚP ĐANG CHẠY", "0", "Lớp", Colors.Warning, "lblLopHoc"), 2, 0);
            row1.Controls.Add(CreateKPICard("SỐ MÔN ĐÀO TẠO", "0", "Môn", Colors.Secondary, "lblMonHoc"), 3, 0);
            tableMain.Controls.Add(row1);

            // ROW 2: KPIs
            TableLayoutPanel row2 = CreateRowLayout(4, 150);
            row2.Controls.Add(CreateKPICard("HỌC VIÊN NỢ PHÍ", "0", "Học viên", Colors.Danger, "lblNoPhi"), 0, 0);
            row2.Controls.Add(CreateKPICard("LỚP TUYỂN SINH", "0", "Lớp", Colors.Danger, "lblLopVang"), 1, 0);
            row2.Controls.Add(CreateKPICard("GIÁO VIÊN", "0", "Người", Colors.Primary, "lblGiaoVien"), 2, 0);
            row2.Controls.Add(CreateKPICard("TRỢ GIẢNG", "0", "Người", Colors.Secondary, "lblTroGiang"), 3, 0);
            tableMain.Controls.Add(row2);

            // ROW 3: CHARTS
            TableLayoutPanel row3 = CreateRowLayout(2, 400);
            row3.ColumnStyles.Clear();
            row3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            row3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            Chart chartFinance = CreateChart("Biểu Đồ Doanh Thu & Chi Phí", "ChartFinance");
            Chart chartStaff = CreateChart("Cơ Cấu Nhân Sự", "ChartStaff");
            row3.Controls.Add(WrapControl(chartFinance), 0, 0);
            row3.Controls.Add(WrapControl(chartStaff), 1, 0);
            tableMain.Controls.Add(row3);

            // ROW 4: CHART & GRID
            TableLayoutPanel row4 = CreateRowLayout(2, 400);
            row4.ColumnStyles.Clear();
            row4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            row4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Chart chartScore = CreateChart("Top 5 Lớp Điểm Cao", "ChartScore");

            DataGridView dgvLog = new DataGridView();
            StyleGrid(dgvLog);
            dgvLog.Name = "DgvLog";
            Panel pnlLog = CreateCardPanel();
            pnlLog.Controls.Add(dgvLog);
            Label lblLogTitle = new Label { Text = "NHẬT KÝ ĐĂNG NHẬP GẦN ĐÂY", Dock = DockStyle.Top, Font = new Font("Segoe UI", 10, FontStyle.Bold), Padding = new Padding(0, 0, 0, 10), Height = 30 };
            pnlLog.Controls.Add(lblLogTitle);

            row4.Controls.Add(WrapControl(chartScore), 0, 0);
            row4.Controls.Add(pnlLog, 1, 0);
            tableMain.Controls.Add(row4);

            this.Load += (s, e) => LoadData();
        }

        // =========================================================================
        // PHẦN QUAN TRỌNG: LOAD DỮ LIỆU
        // =========================================================================
        private void LoadData()
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                // 1. Load các con số thống kê
                UpdateLabel("lblHocVien", DashboardDAO.Instance.GetSoLuongHocVien().ToString("N0"));
                UpdateLabel("lblLoiNhuan", DashboardDAO.Instance.GetLoiNhuan().ToString("N0"));
                UpdateLabel("lblLopHoc", DashboardDAO.Instance.GetSoLuongLopHoc().ToString("N0"));
                UpdateLabel("lblMonHoc", DashboardDAO.Instance.GetSoLuongMon().ToString("N0"));

                UpdateLabel("lblNoPhi", DashboardDAO.Instance.GetSoLuongNoPhi().ToString("N0"));
                UpdateLabel("lblLopVang", DashboardDAO.Instance.GetSoLopChuaDu().ToString("N0")); // Lớp đang tuyển sinh

                int gv = DashboardDAO.Instance.GetSoLuongGiaoVien();
                int tg = DashboardDAO.Instance.GetSoLuongTroGiang();
                UpdateLabel("lblGiaoVien", gv.ToString("N0"));
                UpdateLabel("lblTroGiang", tg.ToString("N0"));

                // 2. Load Biểu đồ Tài chính
                var chartFin = this.Controls.Find("ChartFinance", true)[0] as Chart;
                if (chartFin != null)
                {
                    chartFin.Series.Clear();
                    DataTable dtFin = DashboardDAO.Instance.GetFinanceChartData();

                    Series sThu = new Series("Thu") { ChartType = SeriesChartType.Column, Color = Colors.Success };
                    Series sChi = new Series("Chi") { ChartType = SeriesChartType.Column, Color = Colors.Danger };

                    if (dtFin != null && dtFin.Rows.Count > 0)
                    {
                        foreach (DataRow r in dtFin.Rows)
                        {
                            sThu.Points.AddXY(r["ThoiGian"], r["TongThu"]);
                            sChi.Points.AddXY(r["ThoiGian"], r["TongChi"]);
                        }
                    }
                    chartFin.Series.Add(sThu);
                    chartFin.Series.Add(sChi);
                }

                // 3. Load Biểu đồ Nhân sự (Hình tròn)
                var chartStaff = this.Controls.Find("ChartStaff", true)[0] as Chart;
                if (chartStaff != null)
                {
                    chartStaff.Series.Clear();
                    Series sStaff = new Series("Nhân Sự") { ChartType = SeriesChartType.Doughnut, IsValueShownAsLabel = true };

                    if (gv > 0) sStaff.Points.AddXY("Giáo Viên (" + gv + ")", gv);
                    if (tg > 0) sStaff.Points.AddXY("Trợ Giảng (" + tg + ")", tg);

                    if (gv > 0) sStaff.Points[0].Color = Colors.Primary;
                    if (tg > 0 && gv > 0) sStaff.Points[1].Color = Colors.Warning;
                    else if (tg > 0) sStaff.Points[0].Color = Colors.Warning;

                    chartStaff.Series.Add(sStaff);
                }

                // 4. Load Biểu đồ Điểm
                var chartScore = this.Controls.Find("ChartScore", true)[0] as Chart;
                if (chartScore != null)
                {
                    chartScore.Series.Clear();
                    Series sScore = new Series("Điểm TB") { ChartType = SeriesChartType.Bar, Color = Colors.Info, IsValueShownAsLabel = true };
                    DataTable dtScore = DashboardDAO.Instance.GetTopClassScores();
                    if (dtScore != null)
                    {
                        foreach (DataRow r in dtScore.Rows)
                        {
                            sScore.Points.AddXY(r["TenLop"], r["DiemTB"]);
                        }
                    }
                    chartScore.Series.Add(sScore);
                }

                // 5. Load Log
                var dgvLog = this.Controls.Find("DgvLog", true)[0] as DataGridView;
                if (dgvLog != null)
                {
                    dgvLog.DataSource = DashboardDAO.Instance.GetSystemLog();
                    if (dgvLog.Columns.Count > 0) dgvLog.Columns[0].Width = 140;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally { Cursor = Cursors.Default; }
        }

        // --- CÁC HÀM HELPER VẼ GIAO DIỆN (GIỮ NGUYÊN ĐỂ KHÔNG BỊ LỖI) ---
        private Panel WrapControl(Control c) { Panel p = CreateCardPanel(); c.Dock = DockStyle.Fill; p.Controls.Add(c); return p; }
        private TableLayoutPanel CreateRowLayout(int cols, int height)
        {
            TableLayoutPanel tbl = new TableLayoutPanel { Dock = DockStyle.Top, ColumnCount = cols, RowCount = 1, Height = height, Margin = new Padding(0, 0, 0, 20) };
            for (int i = 0; i < cols; i++) tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / cols));
            return tbl;
        }
        private Panel CreateKPICard(string title, string value, string unit, Color color, string lblName)
        {
            Panel pnl = CreateCardPanel(); pnl.Margin = new Padding(5);
            Label lblTitle = new Label { Text = title, Dock = DockStyle.Top, ForeColor = Color.Gray, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            Label lblValue = new Label { Name = lblName, Text = value, Dock = DockStyle.Fill, ForeColor = color, Font = new Font("Segoe UI", 24, FontStyle.Bold), TextAlign = ContentAlignment.MiddleCenter };
            Label lblUnit = new Label { Text = unit, Dock = DockStyle.Bottom, ForeColor = Color.Silver, Font = new Font("Segoe UI", 8, FontStyle.Italic), TextAlign = ContentAlignment.TopRight };
            Panel bar = new Panel { Dock = DockStyle.Left, Width = 5, BackColor = color };
            pnl.Controls.Add(lblValue); pnl.Controls.Add(lblTitle); pnl.Controls.Add(lblUnit); pnl.Controls.Add(bar);
            return pnl;
        }
        private Panel CreateCardPanel() { return new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(10), Margin = new Padding(10) }; }
        private Chart CreateChart(string title, string name)
        {
            Chart c = new Chart { Name = name, Dock = DockStyle.Fill };
            ChartArea area = new ChartArea("Main"); area.BackColor = Color.White;
            c.ChartAreas.Add(area);
            c.Titles.Add(new Title(title, Docking.Top, new Font("Segoe UI", 10, FontStyle.Bold), Color.DimGray));
            c.Legends.Add(new Legend("Legend") { Docking = Docking.Bottom, Alignment = StringAlignment.Center });
            return c;
        }
        private void StyleGrid(DataGridView dgv)
        {
            dgv.Dock = DockStyle.Fill; dgv.BackgroundColor = Color.White; dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Colors.Secondary; dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.EnableHeadersVisualStyles = false; dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false; dgv.ReadOnly = true;
        }
        private void UpdateLabel(string name, string val) { var controls = this.Controls.Find(name, true); if (controls.Length > 0) controls[0].Text = val; }
    }
}