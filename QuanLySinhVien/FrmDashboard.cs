using QuanLyTrungTam.BUS;
using QuanLyTrungTam.Utilities;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyTrungTam
{
    public partial class FrmDashboard : Form
    {
        // --- CẤU HÌNH MÀU SẮC ---
        private struct Colors
        {
            public static Color Primary = Color.FromArgb(33, 150, 243); // Blue
            public static Color Secondary = ColorTranslator.FromHtml("#607D8B");
            public static Color Success = ColorTranslator.FromHtml("#4CAF50");
            public static Color Warning = ColorTranslator.FromHtml("#FF9800");
            public static Color Danger = ColorTranslator.FromHtml("#F44336");
            public static Color Info = ColorTranslator.FromHtml("#00BCD4"); 
            public static Color Bg = Color.FromArgb(240, 242, 245);
        }

        public FrmDashboard()
        {
            InitializeComponent();
            SetupKPIs();
            StyleGrid(dgvLog);
            
            // Shortcuts
            this.KeyPreview = true;
            this.Load += (s, e) => LoadData();
        }

        private void SetupKPIs()
        {
            // ROW 1
            row1.Controls.Add(CreateKPICard("TỔNG HỌC VIÊN", lblHocVien, "Người", Colors.Info), 0, 0);
            row1.Controls.Add(CreateKPICard("TỔNG LỢI NHUẬN", lblLoiNhuan, "VNĐ", Colors.Success), 1, 0);
            row1.Controls.Add(CreateKPICard("LỚP ĐANG CHẠY", lblLopHoc, "Lớp", Colors.Warning), 2, 0);
            row1.Controls.Add(CreateKPICard("SỐ MÔN ĐÀO TẠO", lblMonHoc, "Môn", Colors.Secondary), 3, 0);

            // ROW 2
            row2.Controls.Add(CreateKPICard("HỌC VIÊN NỢ PHÍ", lblNoPhi, "Học viên", Colors.Danger), 0, 0);
            row2.Controls.Add(CreateKPICard("LỚP TUYỂN SINH", lblLopVang, "Lớp", Colors.Danger), 1, 0);
            row2.Controls.Add(CreateKPICard("GIÁO VIÊN", lblGiaoVien, "Người", Colors.Primary), 2, 0);
            row2.Controls.Add(CreateKPICard("TRỢ GIẢNG", lblTroGiang, "Người", Colors.Secondary), 3, 0);
        }

        private Panel CreateKPICard(string title, Label lblVal, string unit, Color color)
        {
            Panel pnl = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(10), Margin = new Padding(10) };
            Label lblTitle = new Label { Text = title, Dock = DockStyle.Top, ForeColor = Color.Gray, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            
            // Style the existing label
            lblVal.Text = "0";
            lblVal.Dock = DockStyle.Fill;
            lblVal.ForeColor = color;
            lblVal.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblVal.TextAlign = ContentAlignment.MiddleCenter;

            Label lblUnit = new Label { Text = unit, Dock = DockStyle.Bottom, ForeColor = Color.Silver, Font = new Font("Segoe UI", 8, FontStyle.Italic), TextAlign = ContentAlignment.TopRight };
            Panel bar = new Panel { Dock = DockStyle.Left, Width = 5, BackColor = color };
            
            pnl.Controls.Add(lblVal); 
            pnl.Controls.Add(lblTitle); 
            pnl.Controls.Add(lblUnit); 
            pnl.Controls.Add(bar);
            return pnl;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                // 1. KPIs
                lblHocVien.Text = DashboardBUS.Instance.GetSoLuongHocVien().ToString("N0");
                lblLoiNhuan.Text = DashboardBUS.Instance.GetLoiNhuan().ToString("N0");
                lblLopHoc.Text = DashboardBUS.Instance.GetSoLuongLopHoc().ToString("N0");
                lblMonHoc.Text = DashboardBUS.Instance.GetSoLuongMon().ToString("N0");

                lblNoPhi.Text = DashboardBUS.Instance.GetSoLuongNoPhi().ToString("N0");
                lblLopVang.Text = DashboardBUS.Instance.GetSoLopChuaDu().ToString("N0");

                int gv = DashboardBUS.Instance.GetSoLuongGiaoVien();
                int tg = DashboardBUS.Instance.GetSoLuongTroGiang();
                lblGiaoVien.Text = gv.ToString("N0");
                lblTroGiang.Text = tg.ToString("N0");

                // 2. Charts
                // Finance
                chartFinance.Series.Clear();
                DataTable dtFin = DashboardBUS.Instance.GetFinanceChartData();
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
                chartFinance.Series.Add(sThu);
                chartFinance.Series.Add(sChi);

                // Staff
                chartStaff.Series.Clear();
                Series sStaff = new Series("Nhân Sự") { ChartType = SeriesChartType.Doughnut, IsValueShownAsLabel = true };
                if (gv > 0) sStaff.Points.AddXY("Giáo Viên (" + gv + ")", gv);
                if (tg > 0) sStaff.Points.AddXY("Trợ Giảng (" + tg + ")", tg);

                if (gv > 0 && sStaff.Points.Count > 0) sStaff.Points[0].Color = Colors.Primary;
                if (tg > 0 && sStaff.Points.Count > 1) sStaff.Points[1].Color = Colors.Warning;
                else if (tg > 0 && sStaff.Points.Count == 1) sStaff.Points[0].Color = Colors.Warning;

                chartStaff.Series.Add(sStaff);

                // Score
                chartScore.Series.Clear();
                Series sScore = new Series("Điểm TB") { ChartType = SeriesChartType.Bar, Color = Colors.Info, IsValueShownAsLabel = true };
                DataTable dtScore = DashboardBUS.Instance.GetTopClassScores();
                if (dtScore != null)
                {
                    foreach (DataRow r in dtScore.Rows) sScore.Points.AddXY(r["TenLop"], r["DiemTB"]);
                }
                chartScore.Series.Add(sScore);

                // Logs
                dgvLog.DataSource = DashboardBUS.Instance.GetSystemLog();
                if (dgvLog.Columns.Count > 0)
                {
                    dgvLog.Columns[0].Width = 140;
                    dgvLog.Columns["ThoiGian"].HeaderText = "Thời Gian";
                    dgvLog.Columns["TenDangNhap"].HeaderText = "Tên Đăng Nhập";
                    dgvLog.Columns["GhiChu"].HeaderText = "Ghi Chú";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally { Cursor = Cursors.Default; }
        }

        private void StyleGrid(DataGridView dgv)
        {
             GridViewHelper.StandardizeGrid(dgv);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyData == Keys.F5 || (e.Control && e.KeyCode == Keys.R))
            {
                LoadData();
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }
    }
}