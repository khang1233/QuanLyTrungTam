
namespace QuanLyTrungTam
{
    partial class FrmTraCuuHocPhi
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txbSearch = new System.Windows.Forms.TextBox();
            this.split = new System.Windows.Forms.SplitContainer();
            this.grpList = new System.Windows.Forms.GroupBox();
            this.dgvSearchResult = new System.Windows.Forms.DataGridView();
            this.pnlRightContent = new System.Windows.Forms.Panel();
            this.pnlDebt = new System.Windows.Forms.Panel();
            this.lblTaiChinh = new System.Windows.Forms.Label();
            this.pnlPay = new System.Windows.Forms.Panel();
            this.btnLapHoaDon = new System.Windows.Forms.Button();
            this.grpLop = new System.Windows.Forms.GroupBox();
            this.dgvLopHoc = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            this.grpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchResult)).BeginInit();
            this.pnlRightContent.SuspendLayout();
            this.pnlDebt.SuspendLayout();
            this.pnlPay.SuspendLayout();
            this.grpLop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLopHoc)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.pnlHeader.Controls.Add(this.txbSearch);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 70;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 70);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 22);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(227, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TRA CỨU HỌC VIÊN";
            // 
            // txbSearch
            // 
            this.txbSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txbSearch.Location = new System.Drawing.Point(350, 22);
            this.txbSearch.Name = "txbSearch";
            this.txbSearch.Size = new System.Drawing.Size(400, 27);
            this.txbSearch.TabIndex = 1;
            this.txbSearch.TextChanged += new System.EventHandler(this.Logic_SearchHV);
            // 
            // split
            // 
            this.split.BackColor = System.Drawing.Color.WhiteSmoke;
            this.split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split.Location = new System.Drawing.Point(0, 70);
            this.split.Name = "split";
            this.split.SplitterWidth = 10;
            // 
            // split.Panel1
            // 
            this.split.Panel1.Controls.Add(this.grpList);
            this.split.Panel1.Padding = new System.Windows.Forms.Padding(10);
            // 
            // split.Panel2
            // 
            this.split.Panel2.Controls.Add(this.pnlRightContent);
            this.split.Panel2.Padding = new System.Windows.Forms.Padding(0, 10, 10, 10);
            this.split.Size = new System.Drawing.Size(1200, 730);
            this.split.SplitterDistance = 600;
            this.split.TabIndex = 1;
            // 
            // grpList
            // 
            this.grpList.Controls.Add(this.dgvSearchResult);
            this.grpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpList.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpList.ForeColor = System.Drawing.Color.DimGray;
            this.grpList.Location = new System.Drawing.Point(10, 10);
            this.grpList.Name = "grpList";
            this.grpList.Padding = new System.Windows.Forms.Padding(10);
            this.grpList.Size = new System.Drawing.Size(580, 710);
            this.grpList.TabIndex = 0;
            this.grpList.TabStop = false;
            this.grpList.Text = " Danh sách học viên ";
            // 
            // dgvSearchResult
            // 
            this.dgvSearchResult.BackgroundColor = System.Drawing.Color.White;
            this.dgvSearchResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSearchResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearchResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSearchResult.Location = new System.Drawing.Point(10, 28);
            this.dgvSearchResult.Name = "dgvSearchResult";
            this.dgvSearchResult.Size = new System.Drawing.Size(560, 672);
            this.dgvSearchResult.TabIndex = 0;
            this.dgvSearchResult.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Logic_ChonHV);
            // 
            // pnlRightContent
            // 
            this.pnlRightContent.Controls.Add(this.pnlDebt);
            this.pnlRightContent.Controls.Add(this.pnlPay);
            this.pnlRightContent.Controls.Add(this.grpLop);
            this.pnlRightContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRightContent.Location = new System.Drawing.Point(0, 10);
            this.pnlRightContent.Name = "pnlRightContent";
            this.pnlRightContent.Size = new System.Drawing.Size(576, 710);
            this.pnlRightContent.TabIndex = 0;
            // 
            // grpLop
            // 
            this.grpLop.Controls.Add(this.dgvLopHoc);
            this.grpLop.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpLop.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpLop.ForeColor = System.Drawing.Color.DimGray;
            this.grpLop.Height = 250;
            this.grpLop.Location = new System.Drawing.Point(0, 0);
            this.grpLop.Name = "grpLop";
            this.grpLop.Padding = new System.Windows.Forms.Padding(10);
            this.grpLop.Size = new System.Drawing.Size(576, 250);
            this.grpLop.TabIndex = 0;
            this.grpLop.TabStop = false;
            this.grpLop.Text = " Các lớp đang theo học ";
            // 
            // dgvLopHoc
            // 
            this.dgvLopHoc.BackgroundColor = System.Drawing.Color.White;
            this.dgvLopHoc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLopHoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLopHoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLopHoc.Location = new System.Drawing.Point(10, 28);
            this.dgvLopHoc.Name = "dgvLopHoc";
            this.dgvLopHoc.Size = new System.Drawing.Size(556, 212);
            this.dgvLopHoc.TabIndex = 0;
            // 
            // pnlDebt
            // 
            this.pnlDebt.BackColor = System.Drawing.Color.White;
            this.pnlDebt.Controls.Add(this.lblTaiChinh);
            this.pnlDebt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDebt.Location = new System.Drawing.Point(0, 250);
            this.pnlDebt.Name = "pnlDebt";
            this.pnlDebt.Size = new System.Drawing.Size(576, 380);
            this.pnlDebt.TabIndex = 1;
            // 
            // lblTaiChinh
            // 
            this.lblTaiChinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTaiChinh.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblTaiChinh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTaiChinh.Location = new System.Drawing.Point(0, 0);
            this.lblTaiChinh.Name = "lblTaiChinh";
            this.lblTaiChinh.Size = new System.Drawing.Size(576, 380);
            this.lblTaiChinh.TabIndex = 0;
            this.lblTaiChinh.Text = "👈 Vui lòng chọn học viên từ danh sách bên trái";
            // 
            // pnlPay
            // 
            this.pnlPay.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlPay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPay.Controls.Add(this.btnLapHoaDon);
            this.pnlPay.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPay.Height = 80;
            this.pnlPay.Location = new System.Drawing.Point(0, 630);
            this.pnlPay.Name = "pnlPay";
            this.pnlPay.Padding = new System.Windows.Forms.Padding(100, 15, 100, 15);
            this.pnlPay.Size = new System.Drawing.Size(576, 80);
            this.pnlPay.TabIndex = 2;
            // 
            // btnLapHoaDon
            // 
            this.btnLapHoaDon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnLapHoaDon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLapHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLapHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLapHoaDon.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnLapHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnLapHoaDon.Location = new System.Drawing.Point(100, 15);
            this.btnLapHoaDon.Name = "btnLapHoaDon";
            this.btnLapHoaDon.Size = new System.Drawing.Size(374, 48);
            this.btnLapHoaDon.TabIndex = 0;
            this.btnLapHoaDon.Text = "📝 LẬP HÓA ĐƠN THANH TOÁN";
            this.btnLapHoaDon.UseVisualStyleBackColor = false;
            this.btnLapHoaDon.Click += new System.EventHandler(this.BtnLapHoaDon_Click);
            // 
            // FrmTraCuuHocPhi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.split);
            this.Controls.Add(this.pnlHeader);
            this.Name = "FrmTraCuuHocPhi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tra Cứu & Thu Học Phí";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
            this.split.ResumeLayout(false);
            this.grpList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchResult)).EndInit();
            this.pnlRightContent.ResumeLayout(false);
            this.pnlDebt.ResumeLayout(false);
            this.pnlPay.ResumeLayout(false);
            this.grpLop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLopHoc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txbSearch;
        private System.Windows.Forms.SplitContainer split;
        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.DataGridView dgvSearchResult;
        private System.Windows.Forms.Panel pnlRightContent;
        private System.Windows.Forms.GroupBox grpLop;
        private System.Windows.Forms.DataGridView dgvLopHoc;
        private System.Windows.Forms.Panel pnlDebt;
        private System.Windows.Forms.Label lblTaiChinh;
        private System.Windows.Forms.Panel pnlPay;
        private System.Windows.Forms.Button btnLapHoaDon;
    }
}
