
namespace QuanLyTrungTam
{
    partial class FrmDiem
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();

            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.txbSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.cbLop = new System.Windows.Forms.ComboBox();
            this.lblLop = new System.Windows.Forms.Label();
            this.pnlGridContainer = new System.Windows.Forms.Panel();
            this.dgvDiem = new System.Windows.Forms.DataGridView();
            this.colMaHV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiem15p1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiem15p2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiemGiuaKy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiemCuoiKy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiemTongKet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colXepLoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.pnlHeader.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.pnlGridContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiem)).BeginInit();
            this.SuspendLayout();

            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 70;
            this.pnlHeader.Location = new System.Drawing.Point(10, 10);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1260, 70);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 19);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(229, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ ĐIỂM SỐ";
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.Transparent;
            this.pnlFilter.Controls.Add(this.btnSave);
            this.pnlFilter.Controls.Add(this.txbSearch);
            this.pnlFilter.Controls.Add(this.lblSearch);
            this.pnlFilter.Controls.Add(this.cbLop);
            this.pnlFilter.Controls.Add(this.lblLop);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Height = 80;
            this.pnlFilter.Location = new System.Drawing.Point(10, 80);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1260, 80);
            this.pnlFilter.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(1040, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(200, 45);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "💾 LƯU BẢNG ĐIỂM";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // txbSearch
            // 
            this.txbSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txbSearch.Location = new System.Drawing.Point(530, 27);
            this.txbSearch.Name = "txbSearch";
            this.txbSearch.Size = new System.Drawing.Size(300, 27);
            this.txbSearch.TabIndex = 3;
            this.txbSearch.TextChanged += new System.EventHandler(this.TxbSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSearch.ForeColor = System.Drawing.Color.DimGray;
            this.lblSearch.Location = new System.Drawing.Point(450, 30);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(76, 19);
            this.lblSearch.TabIndex = 2;
            this.lblSearch.Text = "Tìm kiếm:";
            // 
            // cbLop
            // 
            this.cbLop.BackColor = System.Drawing.Color.White;
            this.cbLop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbLop.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cbLop.FormattingEnabled = true;
            this.cbLop.Location = new System.Drawing.Point(110, 27);
            this.cbLop.Name = "cbLop";
            this.cbLop.Size = new System.Drawing.Size(300, 28);
            this.cbLop.TabIndex = 1;
            this.cbLop.SelectedIndexChanged += new System.EventHandler(this.CbLop_SelectedIndexChanged);
            // 
            // lblLop
            // 
            this.lblLop.AutoSize = true;
            this.lblLop.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLop.ForeColor = System.Drawing.Color.DimGray;
            this.lblLop.Location = new System.Drawing.Point(20, 30);
            this.lblLop.Name = "lblLop";
            this.lblLop.Size = new System.Drawing.Size(77, 19);
            this.lblLop.TabIndex = 0;
            this.lblLop.Text = "Chọn Lớp:";
            // 
            // pnlGridContainer
            // 
            this.pnlGridContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnlGridContainer.Controls.Add(this.dgvDiem);
            this.pnlGridContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridContainer.Location = new System.Drawing.Point(10, 160);
            this.pnlGridContainer.Name = "pnlGridContainer";
            this.pnlGridContainer.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.pnlGridContainer.Size = new System.Drawing.Size(1260, 630);
            this.pnlGridContainer.TabIndex = 2;
            // 
            // dgvDiem
            // 
            this.dgvDiem.AllowUserToAddRows = false;
            this.dgvDiem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDiem.BackgroundColor = System.Drawing.Color.White;
            this.dgvDiem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDiem.ColumnHeadersHeight = 45;
            this.dgvDiem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaHV,
            this.colHoTen,
            this.colDiem15p1,
            this.colDiem15p2,
            this.colDiemGiuaKy,
            this.colDiemCuoiKy,
            this.colDiemTongKet,
            this.colXepLoai,
            this.colGhiChu});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDiem.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDiem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDiem.EnableHeadersVisualStyles = false;
            this.dgvDiem.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvDiem.Location = new System.Drawing.Point(0, 10);
            this.dgvDiem.Name = "dgvDiem";
            this.dgvDiem.RowHeadersVisible = false;
            this.dgvDiem.RowTemplate.Height = 40;
            this.dgvDiem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDiem.Size = new System.Drawing.Size(1260, 610);
            this.dgvDiem.TabIndex = 0;
            this.dgvDiem.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgvDiem_CellFormatting);
            // 
            // colMaHV
            // 
            this.colMaHV.DataPropertyName = "MaHV";
            this.colMaHV.HeaderText = "Mã HV";
            this.colMaHV.Name = "colMaHV";
            this.colMaHV.ReadOnly = true;
            // 
            // colHoTen
            // 
            this.colHoTen.DataPropertyName = "HoTen";
            this.colHoTen.HeaderText = "Họ Tên Học Viên";
            this.colHoTen.Name = "colHoTen";
            this.colHoTen.ReadOnly = true;
            // 
            // colDiem15p1
            // 
            this.colDiem15p1.DataPropertyName = "Diem15p1";
            this.colDiem15p1.HeaderText = "15 Phút (1)";
            this.colDiem15p1.Name = "colDiem15p1";
            // 
            // colDiem15p2
            // 
            this.colDiem15p2.DataPropertyName = "Diem15p2";
            this.colDiem15p2.HeaderText = "15 Phút (2)";
            this.colDiem15p2.Name = "colDiem15p2";
            // 
            // colDiemGiuaKy
            // 
            this.colDiemGiuaKy.DataPropertyName = "DiemGiuaKy";
            this.colDiemGiuaKy.HeaderText = "Giữa Kỳ (x2)";
            this.colDiemGiuaKy.Name = "colDiemGiuaKy";
            // 
            // colDiemCuoiKy
            // 
            this.colDiemCuoiKy.DataPropertyName = "DiemCuoiKy";
            this.colDiemCuoiKy.HeaderText = "Cuối Kỳ (x3)";
            this.colDiemCuoiKy.Name = "colDiemCuoiKy";
            // 
            // colDiemTongKet
            // 
            this.colDiemTongKet.DataPropertyName = "DiemTongKet";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle2.Format = "N1";
            this.colDiemTongKet.DefaultCellStyle = dataGridViewCellStyle2;
            this.colDiemTongKet.HeaderText = "Tổng Kết";
            this.colDiemTongKet.Name = "colDiemTongKet";
            this.colDiemTongKet.ReadOnly = true;
            // 
            // colXepLoai
            // 
            this.colXepLoai.DataPropertyName = "XepLoai";
            this.colXepLoai.HeaderText = "Xếp Loại";
            this.colXepLoai.Name = "colXepLoai";
            this.colXepLoai.ReadOnly = true;
            // 
            // colGhiChu
            // 
            this.colGhiChu.DataPropertyName = "GhiChu";
            this.colGhiChu.HeaderText = "Ghi Chú";
            this.colGhiChu.Name = "colGhiChu";
            // 
            // FrmDiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.pnlGridContainer);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlHeader);
            this.Name = "FrmDiem";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "Quản Lý Điểm Số & Xếp Loại";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlGridContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblLop;
        private System.Windows.Forms.ComboBox cbLop;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txbSearch;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel pnlGridContainer;
        private System.Windows.Forms.DataGridView dgvDiem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaHV;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiem15p1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiem15p2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiemGiuaKy;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiemCuoiKy;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiemTongKet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colXepLoai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGhiChu;
    }
}