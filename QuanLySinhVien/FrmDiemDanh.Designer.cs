
namespace QuanLyTrungTam
{
    partial class FrmDiemDanh
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txbSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.cbBuoi = new System.Windows.Forms.ComboBox();
            this.lblBuoi = new System.Windows.Forms.Label();
            this.cbLop = new System.Windows.Forms.ComboBox();
            this.lblLop = new System.Windows.Forms.Label();
            this.dgvDiemDanh = new System.Windows.Forms.DataGridView();
            this.colMaHV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCoMat = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colLyDo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiemDanh)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Controls.Add(this.btnSave);
            this.pnlTop.Controls.Add(this.txbSearch);
            this.pnlTop.Controls.Add(this.lblSearch);
            this.pnlTop.Controls.Add(this.btnLoad);
            this.pnlTop.Controls.Add(this.cbBuoi);
            this.pnlTop.Controls.Add(this.lblBuoi);
            this.pnlTop.Controls.Add(this.cbLop);
            this.pnlTop.Controls.Add(this.lblLop);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1467, 175);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(27, 12);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(276, 32);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "ĐIỂM DANH HỌC VIÊN";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(1200, 25);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(240, 62);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "💾 LƯU ĐIỂM DANH";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // txbSearch
            // 
            this.txbSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txbSearch.Location = new System.Drawing.Point(113, 112);
            this.txbSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txbSearch.Name = "txbSearch";
            this.txbSearch.Size = new System.Drawing.Size(699, 30);
            this.txbSearch.TabIndex = 6;
            this.txbSearch.TextChanged += new System.EventHandler(this.TxbSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSearch.ForeColor = System.Drawing.Color.White;
            this.lblSearch.Location = new System.Drawing.Point(27, 108);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(83, 23);
            this.lblSearch.TabIndex = 4;
            this.lblSearch.Text = "Tìm kiếm:";
            this.lblSearch.Text = "🔍";
            // 
            // btnLoad
            // 
            this.btnLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLoad.ForeColor = System.Drawing.Color.White;
            this.btnLoad.Location = new System.Drawing.Point(840, 52);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(173, 39);
            this.btnLoad.TabIndex = 4;
            this.btnLoad.Text = "Tải Danh Sách";
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            // 
            // cbBuoi
            // 
            this.cbBuoi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBuoi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbBuoi.FormattingEnabled = true;
            this.cbBuoi.Location = new System.Drawing.Point(520, 55);
            this.cbBuoi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbBuoi.Name = "cbBuoi";
            this.cbBuoi.Size = new System.Drawing.Size(292, 31);
            this.cbBuoi.TabIndex = 3;
            // 
            // lblBuoi
            // 
            this.lblBuoi.AutoSize = true;
            this.lblBuoi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBuoi.ForeColor = System.Drawing.Color.White;
            this.lblBuoi.Location = new System.Drawing.Point(427, 59);
            this.lblBuoi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBuoi.Name = "lblBuoi";
            this.lblBuoi.Size = new System.Drawing.Size(81, 23);
            this.lblBuoi.TabIndex = 2;
            this.lblBuoi.Text = "Buổi học:";
            // 
            // cbLop
            // 
            this.cbLop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLop.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbLop.FormattingEnabled = true;
            this.cbLop.Location = new System.Drawing.Point(113, 55);
            this.cbLop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbLop.Name = "cbLop";
            this.cbLop.Size = new System.Drawing.Size(292, 31);
            this.cbLop.TabIndex = 1;
            this.cbLop.SelectedIndexChanged += new System.EventHandler(this.CbLop_SelectedIndexChanged);
            // 
            // lblLop
            // 
            this.lblLop.AutoSize = true;
            this.lblLop.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLop.ForeColor = System.Drawing.Color.White;
            this.lblLop.Location = new System.Drawing.Point(27, 59);
            this.lblLop.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLop.Name = "lblLop";
            this.lblLop.Size = new System.Drawing.Size(76, 23);
            this.lblLop.TabIndex = 0;
            this.lblLop.Text = "Lớp học:";
            // 
            // dgvDiemDanh
            // 
            this.dgvDiemDanh.AllowUserToAddRows = false;
            this.dgvDiemDanh.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDiemDanh.BackgroundColor = System.Drawing.Color.White;
            this.dgvDiemDanh.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDiemDanh.ColumnHeadersHeight = 45;
            this.dgvDiemDanh.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaHV,
            this.colHoTen,
            this.colCoMat,
            this.colLyDo});
            this.dgvDiemDanh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDiemDanh.EnableHeadersVisualStyles = false;
            this.dgvDiemDanh.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvDiemDanh.Location = new System.Drawing.Point(0, 175);
            this.dgvDiemDanh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvDiemDanh.Name = "dgvDiemDanh";
            this.dgvDiemDanh.RowHeadersVisible = false;
            this.dgvDiemDanh.RowHeadersWidth = 51;
            this.dgvDiemDanh.RowTemplate.Height = 35;
            this.dgvDiemDanh.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDiemDanh.Size = new System.Drawing.Size(1467, 625);
            this.dgvDiemDanh.TabIndex = 1;
            // 
            // colMaHV
            // 
            this.colMaHV.DataPropertyName = "MaHV";
            this.colMaHV.HeaderText = "Mã HV";
            this.colMaHV.MinimumWidth = 6;
            this.colMaHV.Name = "colMaHV";
            this.colMaHV.ReadOnly = true;
            // 
            // colHoTen
            // 
            this.colHoTen.DataPropertyName = "HoTen";
            this.colHoTen.HeaderText = "Họ Tên Học Viên";
            this.colHoTen.MinimumWidth = 6;
            this.colHoTen.Name = "colHoTen";
            this.colHoTen.ReadOnly = true;
            // 
            // colCoMat
            // 
            this.colCoMat.DataPropertyName = "CoMat";
            this.colCoMat.HeaderText = "Có Mặt";
            this.colCoMat.MinimumWidth = 6;
            this.colCoMat.Name = "colCoMat";
            // 
            // colLyDo
            // 
            this.colLyDo.DataPropertyName = "LyDo";
            this.colLyDo.HeaderText = "Ghi Chú/Lý Do Vắng";
            this.colLyDo.MinimumWidth = 6;
            this.colLyDo.Name = "colLyDo";
            // 
            // FrmDiemDanh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1467, 800);
            this.Controls.Add(this.dgvDiemDanh);
            this.Controls.Add(this.pnlTop);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmDiemDanh";
            this.Text = "Điểm Danh Lớp Học";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiemDanh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ComboBox cbBuoi;
        private System.Windows.Forms.Label lblBuoi;
        private System.Windows.Forms.ComboBox cbLop;
        private System.Windows.Forms.Label lblLop;
        private System.Windows.Forms.TextBox txbSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.DataGridView dgvDiemDanh;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaHV;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoTen;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCoMat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLyDo;
    }
}