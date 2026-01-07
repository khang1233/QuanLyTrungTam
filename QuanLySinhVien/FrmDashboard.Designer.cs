
namespace QuanLyTrungTam
{
    partial class FrmDashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();

            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tableMain = new System.Windows.Forms.TableLayoutPanel();
            this.row1 = new System.Windows.Forms.TableLayoutPanel();
            this.row2 = new System.Windows.Forms.TableLayoutPanel();
            this.row3 = new System.Windows.Forms.TableLayoutPanel();
            this.chartFinance = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartStaff = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.row4 = new System.Windows.Forms.TableLayoutPanel();
            this.chartScore = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlLog = new System.Windows.Forms.Panel();
            this.dgvLog = new System.Windows.Forms.DataGridView();
            this.lblLogTitle = new System.Windows.Forms.Label();
            
            // Labels for KPIs (We will declare them as fields to access them)
            this.lblHocVien = new System.Windows.Forms.Label();
            this.lblLoiNhuan = new System.Windows.Forms.Label();
            this.lblLopHoc = new System.Windows.Forms.Label();
            this.lblMonHoc = new System.Windows.Forms.Label();
            this.lblNoPhi = new System.Windows.Forms.Label();
            this.lblLopVang = new System.Windows.Forms.Label();
            this.lblGiaoVien = new System.Windows.Forms.Label();
            this.lblTroGiang = new System.Windows.Forms.Label();

            this.pnlHeader.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.tableMain.SuspendLayout();
            this.row3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartFinance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartStaff)).BeginInit();
            this.row4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartScore)).BeginInit();
            this.pnlLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            this.SuspendLayout();

            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.pnlHeader.Controls.Add(this.btnRefresh);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 60;
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20);
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1300, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(1100, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(160, 35);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "🔄 Làm Mới Dữ Liệu";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(268, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TỔNG QUAN HOẠT ĐỘNG";
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.Controls.Add(this.tableMain);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 60);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.Size = new System.Drawing.Size(1300, 740);
            this.pnlMain.TabIndex = 1;
            // 
            // tableMain
            // 
            this.tableMain.AutoSize = true;
            this.tableMain.ColumnCount = 1;
            this.tableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableMain.Controls.Add(this.row1, 0, 0);
            this.tableMain.Controls.Add(this.row2, 0, 1);
            this.tableMain.Controls.Add(this.row3, 0, 2);
            this.tableMain.Controls.Add(this.row4, 0, 3);
            this.tableMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableMain.Location = new System.Drawing.Point(20, 20);
            this.tableMain.Name = "tableMain";
            this.tableMain.RowCount = 5;
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableMain.Size = new System.Drawing.Size(1260, 600); 
            this.tableMain.TabIndex = 0;
            // 
            // row1
            // 
            this.row1.ColumnCount = 4;
            this.row1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.row1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.row1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.row1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.row1.Dock = System.Windows.Forms.DockStyle.Top;
            this.row1.Height = 150;
            this.row1.Location = new System.Drawing.Point(3, 3);
            this.row1.Name = "row1";
            this.row1.RowCount = 1;
            this.row1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.row1.Size = new System.Drawing.Size(1254, 150);
            this.row1.TabIndex = 0;
            
            // row2
            // 
            this.row2.ColumnCount = 4;
            this.row2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.row2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.row2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.row2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.row2.Dock = System.Windows.Forms.DockStyle.Top;
            this.row2.Height = 150;
            this.row2.Location = new System.Drawing.Point(3, 159);
            this.row2.Name = "row2";
            this.row2.RowCount = 1;
            this.row2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.row2.Size = new System.Drawing.Size(1254, 150);
            this.row2.TabIndex = 1;
            
            // row3
            // 
            this.row3.ColumnCount = 2;
            this.row3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.row3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.row3.Controls.Add(this.chartFinance, 0, 0);
            this.row3.Controls.Add(this.chartStaff, 1, 0);
            this.row3.Dock = System.Windows.Forms.DockStyle.Top;
            this.row3.Height = 400;
            this.row3.Location = new System.Drawing.Point(3, 315);
            this.row3.Name = "row3";
            this.row3.RowCount = 1;
            this.row3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.row3.Size = new System.Drawing.Size(1254, 400);
            this.row3.TabIndex = 2;

            // chartFinance
            chartArea1.Name = "ChartArea1";
            this.chartFinance.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartFinance.Legends.Add(legend1);
            this.chartFinance.Name = "chartFinance";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartFinance.Series.Add(series1);
            this.chartFinance.Size = new System.Drawing.Size(800, 394);
            this.chartFinance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartFinance.TabIndex = 0;
            this.chartFinance.Text = "chartFinance";
            title1.Name = "Title1";
            title1.Text = "Biểu Đồ Doanh Thu & Chi Phí";
            this.chartFinance.Titles.Add(title1);

            // chartStaff
            chartArea2.Name = "ChartArea1";
            this.chartStaff.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartStaff.Legends.Add(legend2);
            this.chartStaff.Name = "chartStaff";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartStaff.Series.Add(series2);
            this.chartStaff.Size = new System.Drawing.Size(400, 394);
            this.chartStaff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartStaff.TabIndex = 1;
            this.chartStaff.Text = "chartStaff";
            title2.Name = "Title1";
            title2.Text = "Cơ Cấu Nhân Sự";
            this.chartStaff.Titles.Add(title2);

            // row4
            this.row4.ColumnCount = 2;
            this.row4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.row4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.row4.Controls.Add(this.chartScore, 0, 0);
            this.row4.Controls.Add(this.pnlLog, 1, 0);
            this.row4.Dock = System.Windows.Forms.DockStyle.Top;
            this.row4.Height = 400;
            this.row4.Location = new System.Drawing.Point(3, 721);
            this.row4.Name = "row4";
            this.row4.RowCount = 1;
            this.row4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.row4.Size = new System.Drawing.Size(1254, 400);
            this.row4.TabIndex = 3;

            // chartScore
            chartArea3.Name = "ChartArea1";
            this.chartScore.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chartScore.Legends.Add(legend3);
            this.chartScore.Name = "chartScore";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chartScore.Series.Add(series3);
            this.chartScore.Size = new System.Drawing.Size(600, 394);
            this.chartScore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartScore.TabIndex = 0;
            this.chartScore.Text = "chartScore";
            title3.Name = "Title1";
            title3.Text = "Top 5 Lớp Điểm Cao";
            this.chartScore.Titles.Add(title3);

            // pnlLog & dgvLog
            this.pnlLog.Controls.Add(this.dgvLog);
            this.pnlLog.Controls.Add(this.lblLogTitle);
            this.pnlLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLog.BackColor = System.Drawing.Color.White;
            this.pnlLog.Name = "pnlLog";
            
            this.dgvLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLog.Name = "dgvLog";
            this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            
            this.lblLogTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLogTitle.Text = "NHẬT KÝ ĐĂNG NHẬP GẦN ĐÂY";
            this.lblLogTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLogTitle.Height = 30;
            this.lblLogTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // FrmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1300, 800);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlHeader);
            this.Name = "FrmDashboard";
            this.Text = "Dashboard Quản Trị Trung Tâm";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.tableMain.ResumeLayout(false);
            this.row3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartFinance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartStaff)).EndInit();
            this.row4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartScore)).EndInit();
            this.pnlLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TableLayoutPanel tableMain;
        private System.Windows.Forms.TableLayoutPanel row1;
        private System.Windows.Forms.TableLayoutPanel row2;
        private System.Windows.Forms.TableLayoutPanel row3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartFinance;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartStaff;
        private System.Windows.Forms.TableLayoutPanel row4;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartScore;
        private System.Windows.Forms.Panel pnlLog;
        private System.Windows.Forms.DataGridView dgvLog;
        private System.Windows.Forms.Label lblLogTitle;
        
        // Dynamic Labels accessed by Logic
        public System.Windows.Forms.Label lblHocVien;
        public System.Windows.Forms.Label lblLoiNhuan;
        public System.Windows.Forms.Label lblLopHoc;
        public System.Windows.Forms.Label lblMonHoc;
        public System.Windows.Forms.Label lblNoPhi;
        public System.Windows.Forms.Label lblLopVang;
        public System.Windows.Forms.Label lblGiaoVien;
        public System.Windows.Forms.Label lblTroGiang;
    }
}