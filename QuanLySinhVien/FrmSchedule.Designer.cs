
namespace QuanLyTrungTam
{
    partial class FrmSchedule
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

            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlTool = new System.Windows.Forms.Panel();
            this.lblType = new System.Windows.Forms.Label();
            this.cbFilterType = new System.Windows.Forms.ComboBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.cbFilterValue = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txbSearch = new System.Windows.Forms.TextBox();
            this.pnlGridContainer = new System.Windows.Forms.Panel();
            this.grpGrid = new System.Windows.Forms.GroupBox();
            this.dgvSchedule = new System.Windows.Forms.DataGridView();
            
            this.pnlHeader.SuspendLayout();
            this.pnlTool.SuspendLayout();
            this.pnlGridContainer.SuspendLayout();
            this.grpGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 60;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1100, 60);
            this.pnlHeader.TabIndex = 0;
            this.pnlHeader.Visible = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(294, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TRA CỨU THỜI KHÓA BIỂU";
            // 
            // pnlTool
            // 
            this.pnlTool.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlTool.Controls.Add(this.txbSearch);
            this.pnlTool.Controls.Add(this.lblSearch);
            this.pnlTool.Controls.Add(this.btnSearch);
            this.pnlTool.Controls.Add(this.cbFilterValue);
            this.pnlTool.Controls.Add(this.lblValue);
            this.pnlTool.Controls.Add(this.cbFilterType);
            this.pnlTool.Controls.Add(this.lblType);
            this.pnlTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTool.Height = 100;
            this.pnlTool.Location = new System.Drawing.Point(0, 60);
            this.pnlTool.Name = "pnlTool";
            this.pnlTool.Size = new System.Drawing.Size(1100, 100);
            this.pnlTool.TabIndex = 1;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblType.Location = new System.Drawing.Point(20, 18);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(77, 19);
            this.lblType.TabIndex = 0;
            this.lblType.Text = "Xem theo:";
            // 
            // cbFilterType
            // 
            this.cbFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbFilterType.FormattingEnabled = true;
            this.cbFilterType.Items.AddRange(new object[] {
            "Tất cả lịch học",
            "Theo Giáo Viên",
            "Theo Phòng Học",
            "Theo Lớp Học"});
            this.cbFilterType.Location = new System.Drawing.Point(100, 15);
            this.cbFilterType.Name = "cbFilterType";
            this.cbFilterType.Size = new System.Drawing.Size(180, 25);
            this.cbFilterType.TabIndex = 1;
            this.cbFilterType.SelectedIndexChanged += new System.EventHandler(this.CbFilterType_SelectedIndexChanged);
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblValue.Location = new System.Drawing.Point(350, 18);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(117, 19);
            this.lblValue.TabIndex = 2;
            this.lblValue.Text = "Chọn đối tượng:";
            // 
            // cbFilterValue
            // 
            this.cbFilterValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterValue.Enabled = false;
            this.cbFilterValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbFilterValue.FormattingEnabled = true;
            this.cbFilterValue.Location = new System.Drawing.Point(470, 15);
            this.cbFilterValue.Name = "cbFilterValue";
            this.cbFilterValue.Size = new System.Drawing.Size(250, 25);
            this.cbFilterValue.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(750, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(120, 35);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "TẢI DỮ LIỆU";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSearch.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblSearch.Location = new System.Drawing.Point(20, 63);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(155, 19);
            this.lblSearch.TabIndex = 5;
            this.lblSearch.Text = "Tìm nhanh (Mã/Tên):";
            // 
            // txbSearch
            // 
            this.txbSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txbSearch.Location = new System.Drawing.Point(180, 60);
            this.txbSearch.Name = "txbSearch";
            this.txbSearch.Size = new System.Drawing.Size(400, 25);
            this.txbSearch.TabIndex = 6;
            this.txbSearch.TextChanged += new System.EventHandler(this.TxbSearch_TextChanged);
            // 
            // pnlGridContainer
            // 
            this.pnlGridContainer.Padding = new System.Windows.Forms.Padding(15);
            this.pnlGridContainer.Controls.Add(this.grpGrid);
            this.pnlGridContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridContainer.Location = new System.Drawing.Point(0, 160);
            this.pnlGridContainer.Name = "pnlGridContainer";
            this.pnlGridContainer.Size = new System.Drawing.Size(1100, 540);
            this.pnlGridContainer.TabIndex = 2;
            // 
            // grpGrid
            // 
            this.grpGrid.Controls.Add(this.dgvSchedule);
            this.grpGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpGrid.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpGrid.Location = new System.Drawing.Point(15, 15);
            this.grpGrid.Name = "grpGrid";
            this.grpGrid.Size = new System.Drawing.Size(1070, 510);
            this.grpGrid.TabIndex = 0;
            this.grpGrid.TabStop = false;
            this.grpGrid.Text = " Chi tiết lịch học ";
            // 
            // dgvSchedule
            // 
            this.dgvSchedule.BackgroundColor = System.Drawing.Color.White;
            this.dgvSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSchedule.Location = new System.Drawing.Point(3, 21);
            this.dgvSchedule.Name = "dgvSchedule";
            this.dgvSchedule.Size = new System.Drawing.Size(1064, 486);
            this.dgvSchedule.TabIndex = 0;
            // 
            // FrmSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.pnlGridContainer);
            this.Controls.Add(this.pnlTool);
            this.Controls.Add(this.pnlHeader);
            this.Name = "FrmSchedule";
            this.Text = "FrmSchedule";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlTool.ResumeLayout(false);
            this.pnlTool.PerformLayout();
            this.pnlGridContainer.ResumeLayout(false);
            this.grpGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlTool;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cbFilterType;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.ComboBox cbFilterValue;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txbSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Panel pnlGridContainer;
        private System.Windows.Forms.GroupBox grpGrid;
        private System.Windows.Forms.DataGridView dgvSchedule;
    }
}