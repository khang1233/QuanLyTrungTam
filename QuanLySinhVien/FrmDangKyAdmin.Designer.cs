
namespace QuanLyTrungTam
{
    partial class FrmDangKyAdmin
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
            this.split = new System.Windows.Forms.SplitContainer();
            this.grpLeft = new System.Windows.Forms.GroupBox();
            this.dgvHocVien = new System.Windows.Forms.DataGridView();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txbSearch = new System.Windows.Forms.TextBox();
            this.pnlRightContent = new System.Windows.Forms.Panel();
            this.grpList = new System.Windows.Forms.GroupBox();
            this.dgvDaDangKy = new System.Windows.Forms.DataGridView();
            this.grpReg = new System.Windows.Forms.GroupBox();
            this.btnDangKy = new System.Windows.Forms.Button();
            this.lblHocPhi = new System.Windows.Forms.Label();
            this.lblTien = new System.Windows.Forms.Label();
            this.cbLopHoc = new System.Windows.Forms.ComboBox();
            this.lblLop = new System.Windows.Forms.Label();
            this.cbKyNang = new System.Windows.Forms.ComboBox();
            this.lblMon = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            this.grpLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHocVien)).BeginInit();
            this.pnlSearch.SuspendLayout();
            this.pnlRightContent.SuspendLayout();
            this.grpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDaDangKy)).BeginInit();
            this.grpReg.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(4);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(27, 18, 0, 0);
            this.pnlHeader.Size = new System.Drawing.Size(1467, 74);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(27, 18);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(405, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ ĐĂNG KÝ & HỦY MÔN";
            // 
            // split
            // 
            this.split.BackColor = System.Drawing.Color.WhiteSmoke;
            this.split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.split.Location = new System.Drawing.Point(0, 74);
            this.split.Margin = new System.Windows.Forms.Padding(4);
            this.split.Name = "split";
            // 
            // split.Panel1
            // 
            this.split.Panel1.Controls.Add(this.grpLeft);
            this.split.Panel1.Padding = new System.Windows.Forms.Padding(13, 12, 7, 12);
            this.split.Panel1MinSize = 0;
            // 
            // split.Panel2
            // 
            this.split.Panel2.Controls.Add(this.pnlRightContent);
            this.split.Panel2.Padding = new System.Windows.Forms.Padding(7, 12, 13, 12);
            this.split.Panel2MinSize = 0;
            this.split.Size = new System.Drawing.Size(1467, 911);
            this.split.SplitterDistance = 121;
            this.split.SplitterWidth = 13;
            this.split.TabIndex = 1;
            // 
            // grpLeft
            // 
            this.grpLeft.BackColor = System.Drawing.Color.White;
            this.grpLeft.Controls.Add(this.dgvHocVien);
            this.grpLeft.Controls.Add(this.pnlSearch);
            this.grpLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLeft.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpLeft.ForeColor = System.Drawing.Color.DimGray;
            this.grpLeft.Location = new System.Drawing.Point(13, 12);
            this.grpLeft.Margin = new System.Windows.Forms.Padding(4);
            this.grpLeft.Name = "grpLeft";
            this.grpLeft.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.grpLeft.Size = new System.Drawing.Size(101, 887);
            this.grpLeft.TabIndex = 0;
            this.grpLeft.TabStop = false;
            this.grpLeft.Text = " 1. Chọn Học Viên ";
            // 
            // dgvHocVien
            // 
            this.dgvHocVien.BackgroundColor = System.Drawing.Color.White;
            this.dgvHocVien.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHocVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHocVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHocVien.Location = new System.Drawing.Point(7, 97);
            this.dgvHocVien.Margin = new System.Windows.Forms.Padding(4);
            this.dgvHocVien.Name = "dgvHocVien";
            this.dgvHocVien.RowHeadersWidth = 51;
            this.dgvHocVien.Size = new System.Drawing.Size(87, 784);
            this.dgvHocVien.TabIndex = 1;
            this.dgvHocVien.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvHocVien_CellClick);
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.White;
            this.pnlSearch.Controls.Add(this.txbSearch);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(7, 29);
            this.pnlSearch.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(7, 12, 7, 12);
            this.pnlSearch.Size = new System.Drawing.Size(87, 68);
            this.pnlSearch.TabIndex = 0;
            // 
            // txbSearch
            // 
            this.txbSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbSearch.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txbSearch.Location = new System.Drawing.Point(7, 12);
            this.txbSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txbSearch.Name = "txbSearch";
            this.txbSearch.Size = new System.Drawing.Size(73, 34);
            this.txbSearch.TabIndex = 0;
            this.txbSearch.Tag = "Nhập tên hoặc SĐT...";
            this.txbSearch.TextChanged += new System.EventHandler(this.TxbSearch_TextChanged);
            // 
            // pnlRightContent
            // 
            this.pnlRightContent.Controls.Add(this.grpList);
            this.pnlRightContent.Controls.Add(this.grpReg);
            this.pnlRightContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRightContent.Location = new System.Drawing.Point(7, 12);
            this.pnlRightContent.Margin = new System.Windows.Forms.Padding(4);
            this.pnlRightContent.Name = "pnlRightContent";
            this.pnlRightContent.Size = new System.Drawing.Size(1313, 887);
            this.pnlRightContent.TabIndex = 0;
            // 
            // grpList
            // 
            this.grpList.BackColor = System.Drawing.Color.White;
            this.grpList.Controls.Add(this.dgvDaDangKy);
            this.grpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpList.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpList.ForeColor = System.Drawing.Color.DimGray;
            this.grpList.Location = new System.Drawing.Point(0, 271);
            this.grpList.Margin = new System.Windows.Forms.Padding(4);
            this.grpList.Name = "grpList";
            this.grpList.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.grpList.Size = new System.Drawing.Size(1313, 616);
            this.grpList.TabIndex = 1;
            this.grpList.TabStop = false;
            this.grpList.Text = " 3. Danh sách môn đã đăng ký ";
            // 
            // dgvDaDangKy
            // 
            this.dgvDaDangKy.BackgroundColor = System.Drawing.Color.White;
            this.dgvDaDangKy.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDaDangKy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDaDangKy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDaDangKy.Location = new System.Drawing.Point(13, 35);
            this.dgvDaDangKy.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDaDangKy.Name = "dgvDaDangKy";
            this.dgvDaDangKy.RowHeadersWidth = 51;
            this.dgvDaDangKy.Size = new System.Drawing.Size(1287, 569);
            this.dgvDaDangKy.TabIndex = 0;
            this.dgvDaDangKy.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvDaDangKy_CellContentClick);
            // 
            // grpReg
            // 
            this.grpReg.BackColor = System.Drawing.Color.White;
            this.grpReg.Controls.Add(this.btnDangKy);
            this.grpReg.Controls.Add(this.lblHocPhi);
            this.grpReg.Controls.Add(this.lblTien);
            this.grpReg.Controls.Add(this.cbLopHoc);
            this.grpReg.Controls.Add(this.lblLop);
            this.grpReg.Controls.Add(this.cbKyNang);
            this.grpReg.Controls.Add(this.lblMon);
            this.grpReg.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpReg.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpReg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.grpReg.Location = new System.Drawing.Point(0, 0);
            this.grpReg.Margin = new System.Windows.Forms.Padding(4);
            this.grpReg.Name = "grpReg";
            this.grpReg.Padding = new System.Windows.Forms.Padding(4);
            this.grpReg.Size = new System.Drawing.Size(1313, 271);
            this.grpReg.TabIndex = 0;
            this.grpReg.TabStop = false;
            this.grpReg.Text = " 2. Thông Tin Đăng Ký ";
            // 
            // btnDangKy
            // 
            this.btnDangKy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnDangKy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDangKy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangKy.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnDangKy.ForeColor = System.Drawing.Color.White;
            this.btnDangKy.Location = new System.Drawing.Point(667, 148);
            this.btnDangKy.Margin = new System.Windows.Forms.Padding(4);
            this.btnDangKy.Name = "btnDangKy";
            this.btnDangKy.Size = new System.Drawing.Size(160, 55);
            this.btnDangKy.TabIndex = 6;
            this.btnDangKy.Text = "XÁC NHẬN";
            this.btnDangKy.UseVisualStyleBackColor = false;
            this.btnDangKy.Click += new System.EventHandler(this.BtnDangKy_Click);
            // 
            // lblHocPhi
            // 
            this.lblHocPhi.AutoSize = true;
            this.lblHocPhi.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHocPhi.ForeColor = System.Drawing.Color.Red;
            this.lblHocPhi.Location = new System.Drawing.Point(667, 80);
            this.lblHocPhi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHocPhi.Name = "lblHocPhi";
            this.lblHocPhi.Size = new System.Drawing.Size(99, 37);
            this.lblHocPhi.TabIndex = 5;
            this.lblHocPhi.Text = "0 VNĐ";
            // 
            // lblTien
            // 
            this.lblTien.AutoSize = true;
            this.lblTien.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTien.ForeColor = System.Drawing.Color.Black;
            this.lblTien.Location = new System.Drawing.Point(667, 49);
            this.lblTien.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTien.Name = "lblTien";
            this.lblTien.Size = new System.Drawing.Size(63, 20);
            this.lblTien.TabIndex = 4;
            this.lblTien.Text = "Học Phí:";
            // 
            // cbLopHoc
            // 
            this.cbLopHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLopHoc.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cbLopHoc.FormattingEnabled = true;
            this.cbLopHoc.Location = new System.Drawing.Point(347, 80);
            this.cbLopHoc.Margin = new System.Windows.Forms.Padding(4);
            this.cbLopHoc.Name = "cbLopHoc";
            this.cbLopHoc.Size = new System.Drawing.Size(292, 33);
            this.cbLopHoc.TabIndex = 3;
            // 
            // lblLop
            // 
            this.lblLop.AutoSize = true;
            this.lblLop.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLop.ForeColor = System.Drawing.Color.Black;
            this.lblLop.Location = new System.Drawing.Point(347, 49);
            this.lblLop.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLop.Name = "lblLop";
            this.lblLop.Size = new System.Drawing.Size(68, 20);
            this.lblLop.TabIndex = 2;
            this.lblLop.Text = "Lớp Học:";
            // 
            // cbKyNang
            // 
            this.cbKyNang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKyNang.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cbKyNang.FormattingEnabled = true;
            this.cbKyNang.ItemHeight = 25;
            this.cbKyNang.Location = new System.Drawing.Point(27, 80);
            this.cbKyNang.Margin = new System.Windows.Forms.Padding(4);
            this.cbKyNang.Name = "cbKyNang";
            this.cbKyNang.Size = new System.Drawing.Size(292, 33);
            this.cbKyNang.TabIndex = 1;
            this.cbKyNang.SelectedIndexChanged += new System.EventHandler(this.CbKyNang_SelectedIndexChanged);
            // 
            // lblMon
            // 
            this.lblMon.AutoSize = true;
            this.lblMon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMon.ForeColor = System.Drawing.Color.Black;
            this.lblMon.Location = new System.Drawing.Point(27, 49);
            this.lblMon.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMon.Name = "lblMon";
            this.lblMon.Size = new System.Drawing.Size(73, 20);
            this.lblMon.TabIndex = 0;
            this.lblMon.Text = "Môn Học:";
            // 
            // FrmDangKyAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1467, 985);
            this.Controls.Add(this.split);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmDangKyAdmin";
            this.Text = "FrmDangKyAdmin";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
            this.split.ResumeLayout(false);
            this.grpLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHocVien)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlRightContent.ResumeLayout(false);
            this.grpList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDaDangKy)).EndInit();
            this.grpReg.ResumeLayout(false);
            this.grpReg.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.SplitContainer split;
        private System.Windows.Forms.GroupBox grpLeft;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.TextBox txbSearch;
        private System.Windows.Forms.DataGridView dgvHocVien;
        private System.Windows.Forms.Panel pnlRightContent;
        private System.Windows.Forms.GroupBox grpReg;
        private System.Windows.Forms.Label lblMon;
        private System.Windows.Forms.ComboBox cbKyNang;
        private System.Windows.Forms.Label lblLop;
        private System.Windows.Forms.ComboBox cbLopHoc;
        private System.Windows.Forms.Label lblTien;
        private System.Windows.Forms.Label lblHocPhi;
        private System.Windows.Forms.Button btnDangKy;
        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.DataGridView dgvDaDangKy;
    }
}