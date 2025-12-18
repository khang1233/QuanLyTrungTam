using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; // Thư viện biểu đồ
using QuanLyTrungTam.DAO;

namespace QuanLyTrungTam
{
    public partial class FrmDashboard : Form
    {
        public FrmDashboard()
        {
            // InitializeComponent(); 
            SetupModernDashboard();
            LoadData();
        }

        // =========================================================================
        // 1. THIẾT KẾ GIAO DIỆN (MODERN DASHBOARD UI)
        // =========================================================================
        private void SetupModernDashboard()
        {
            this.Text = "Tổng Quan Hệ Thống";
            this.BackColor = Color.WhiteSmoke;
            this.WindowState = FormWindowState.Maximized;
            this.Font = new Font("Segoe UI", 10F);

            // --- A. HEADER ---
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.White, Padding = new Padding(20, 15, 20, 15) };
            Label lblTitle = new Label { Text = "TỔNG QUAN HỆ THỐNG QUẢN LÝ", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = ColorTranslator.FromHtml("#37474F"), AutoSize = true, Dock = DockStyle.Left };
            pnlHeader.Controls.Add(lblTitle);
            this.Controls.Add(pnlHeader);

            // --- B. KPI CARDS ---
            TableLayoutPanel tblCards = new TableLayoutPanel();
            tblCards.Dock = DockStyle.Top;
            tblCards.Height = 160;
            tblCards.ColumnCount = 4;
            tblCards.Padding = new Padding(10);
            tblCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            tblCards.Controls.Add(CreateCard("TỔNG SINH VIÊN", "0", ColorTranslator.FromHtml("#0288D1"), "lblStudent"));
            tblCards.Controls.Add(CreateCard("LỚP HỌC", "0", ColorTranslator.FromHtml("#F57C00"), "lblClass"));
            tblCards.Controls.Add(CreateCard("MÔN HỌC", "0", ColorTranslator.FromHtml("#7B1FA2"), "lblSubject"));
            tblCards.Controls.Add(CreateCard("DOANH THU THỰC", "0 đ", ColorTranslator.FromHtml("#388E3C"), "lblRevenue"));

            this.Controls.Add(tblCards);

            // --- C. CHARTS ---
            SplitContainer splitCharts = new SplitContainer { Dock = DockStyle.Fill, SplitterWidth = 15, BackColor = Color.WhiteSmoke };
            splitCharts.Padding = new Padding(15);

            // >> Chart 1: Tài chính
            GroupBox grpPie = new GroupBox { Text = " Tình Hình Tài Chính ", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.DimGray, BackColor = Color.White };
            grpPie.Padding = new Padding(10);
            Chart chartPie = CreateChart("ChartPie", SeriesChartType.Doughnut);
            grpPie.Controls.Add(chartPie);
            splitCharts.Panel1.Controls.Add(grpPie);

            // >> Chart 2: Số lượng học viên
            GroupBox grpBar = new GroupBox { Text = " Số Lượng Học Viên Theo Môn ", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.DimGray, BackColor = Color.White };

            // Padding lớn để ép biểu đồ nhỏ lại
            grpBar.Padding = new Padding(20, 50, 20, 20);

            Chart chartBar = CreateChart("ChartBar", SeriesChartType.Column);
            grpBar.Controls.Add(chartBar);
            splitCharts.Panel2.Controls.Add(grpBar);

            this.Controls.Add(splitCharts);

            tblCards.BringToFront();
            pnlHeader.BringToFront();
        }

        private Panel CreateCard(string title, string value, Color bgColor, string lblName)
        {
            Panel card = new Panel { Dock = DockStyle.Fill, BackColor = bgColor, Margin = new Padding(10) };
            Label lblVal = new Label { Name = lblName, Text = value, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 24, FontStyle.Bold), ForeColor = Color.White };
            Label lblTit = new Label { Text = title, Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.WhiteSmoke };
            card.Controls.Add(lblVal);
            card.Controls.Add(lblTit);
            return card;
        }

        private Chart CreateChart(string name, SeriesChartType type)
        {
            Chart c = new Chart { Name = name, Dock = DockStyle.Fill };
            ChartArea area = new ChartArea("MainArea");

            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = false;

            // Chỉ hiện số nguyên trên trục Y
            area.AxisY.Interval = 1;

            // Chừa lề trên cùng để cột không chạm nóc
            area.AxisY.IsMarginVisible = true;

            c.ChartAreas.Add(area);

            Legend legend = new Legend("MainLegend");
            legend.Docking = Docking.Bottom;
            legend.Alignment = StringAlignment.Center;
            c.Legends.Add(legend);

            Series s = new Series("Data");
            s.ChartType = type;
            s.IsValueShownAsLabel = true;
            s.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            if (type == SeriesChartType.Doughnut)
            {
                s["PieLabelStyle"] = "Outside";
                s.Palette = ChartColorPalette.BrightPastel;
            }
            else
            {
                s.Color = ColorTranslator.FromHtml("#009688");
                s.IsVisibleInLegend = false;
                s["PointWidth"] = "0.5";
            }

            c.Series.Add(s);
            return c;
        }

        // =========================================================================
        // 2. NẠP DỮ LIỆU (ĐÃ CHỈNH ĐỂ CỘT THẤP XUỐNG)
        // =========================================================================
        private void LoadData()
        {
            try
            {
                // 1. Số liệu tổng quát
                int numSV = DashboardDAO.Instance.GetSoLuongHocVien();
                int numClass = DashboardDAO.Instance.GetSoLuongLopHoc();
                int numSubject = DashboardDAO.Instance.GetSoLuongMonHoc();

                decimal daThu = DashboardDAO.Instance.GetTongThucThu();
                decimal conNo = DashboardDAO.Instance.GetTongNo();

                // 2. Card
                SetLabelText("lblStudent", numSV.ToString());
                SetLabelText("lblClass", numClass.ToString());
                SetLabelText("lblSubject", numSubject.ToString());
                SetLabelText("lblRevenue", daThu.ToString("N0") + " đ");

                // 3. Biểu đồ Tròn
                Chart pie = this.Controls.Find("ChartPie", true)[0] as Chart;
                pie.Series["Data"].Points.Clear();

                pie.Series["Data"].Points.Add(new DataPoint(0, (double)daThu) { LegendText = "Đã thu", Color = ColorTranslator.FromHtml("#4CAF50") });
                pie.Series["Data"].Points.Add(new DataPoint(0, (double)conNo) { LegendText = "Còn nợ", Color = ColorTranslator.FromHtml("#F44336") });

                // 4. Biểu đồ Cột (SỬA ĐỔI ĐỂ CỘT THẤP XUỐNG)
                Chart bar = this.Controls.Find("ChartBar", true)[0] as Chart;
                bar.Series["Data"].Points.Clear();

                DataTable dtStudentCount = DashboardDAO.Instance.GetStudentCountBySkill();
                int maxCount = 0;

                foreach (DataRow row in dtStudentCount.Rows)
                {
                    string skill = row["TenKyNang"].ToString();
                    if (skill.Length > 15) skill = skill.Substring(0, 12) + "..."; // Rút gọn tên

                    int count = Convert.ToInt32(row["SoLuong"]);
                    if (count > maxCount) maxCount = count;

                    int pIndex = bar.Series["Data"].Points.AddXY(skill, count);
                    bar.Series["Data"].Points[pIndex].Label = count.ToString();
                }

                // [QUAN TRỌNG] Thiết lập chiều cao trục Y (Trần biểu đồ)
                // Công thức: Trần = Max thực tế + 3 đơn vị
                // Ví dụ: Cao nhất là 4 HV -> Trần sẽ là 7. Cột 4 sẽ nằm ở mức giữa, trông rất thoáng.
                if (maxCount > 0)
                {
                    bar.ChartAreas[0].AxisY.Maximum = maxCount + 3;
                }
                else
                {
                    bar.ChartAreas[0].AxisY.Maximum = 5; // Mặc định nếu chưa có dữ liệu
                }
            }
            catch { }
        }

        private void SetLabelText(string labelName, string text)
        {
            var controls = this.Controls.Find(labelName, true);
            if (controls.Length > 0) ((Label)controls[0]).Text = text;
        }
    }
}