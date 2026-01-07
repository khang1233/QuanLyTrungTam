
namespace QuanLyTrungTam
{
    partial class FrmQuanLyNhanSu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlInput = new System.Windows.Forms.Panel();
            this.gbContact = new System.Windows.Forms.GroupBox();
            this.ui_lblChuyenNganh = new System.Windows.Forms.Label();
            this.ui_cbChuyenNganh = new System.Windows.Forms.ComboBox();
            this.ui_txbEmail = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ui_txbSDT = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.ui_cbLoaiNS = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ui_dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.ui_txbTen = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ui_txbMa = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.btnCapTK = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.ui_btnSearch = new System.Windows.Forms.Button();
            this.ui_txbSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.pnlGridContainer = new System.Windows.Forms.Panel();
            this.ui_dgvNhanSu = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            this.pnlInput.SuspendLayout();
            this.gbContact.SuspendLayout();
            this.gbInfo.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlGridContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ui_dgvNhanSu)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(10, 10);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
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
            this.lblTitle.Size = new System.Drawing.Size(252, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ NHÂN SỰ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlInput
            // 
            this.pnlInput.BackColor = System.Drawing.Color.Transparent;
            this.pnlInput.Controls.Add(this.gbContact);
            this.pnlInput.Controls.Add(this.gbInfo);
            this.pnlInput.Controls.Add(this.pnlActions);
            this.pnlInput.Controls.Add(this.pnlSearch);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInput.Location = new System.Drawing.Point(10, 80);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.pnlInput.Size = new System.Drawing.Size(1260, 340);
            this.pnlInput.TabIndex = 1;
            // 
            // gbContact
            // 
            this.gbContact.BackColor = System.Drawing.Color.White;
            this.gbContact.Controls.Add(this.ui_lblChuyenNganh);
            this.gbContact.Controls.Add(this.ui_cbChuyenNganh);
            this.gbContact.Controls.Add(this.ui_txbEmail);
            this.gbContact.Controls.Add(this.label6);
            this.gbContact.Controls.Add(this.ui_txbSDT);
            this.gbContact.Controls.Add(this.label5);
            this.gbContact.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbContact.ForeColor = System.Drawing.Color.DimGray;
            this.gbContact.Location = new System.Drawing.Point(490, 10);
            this.gbContact.Name = "gbContact";
            this.gbContact.Size = new System.Drawing.Size(460, 240);
            this.gbContact.TabIndex = 1;
            this.gbContact.TabStop = false;
            this.gbContact.Text = "Thông Tin Liên Hệ";
            // 
            // ui_lblChuyenNganh
            // 
            this.ui_lblChuyenNganh.AutoSize = true;
            this.ui_lblChuyenNganh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ui_lblChuyenNganh.ForeColor = System.Drawing.Color.Black;
            this.ui_lblChuyenNganh.Location = new System.Drawing.Point(20, 143);
            this.ui_lblChuyenNganh.Name = "ui_lblChuyenNganh";
            this.ui_lblChuyenNganh.Size = new System.Drawing.Size(89, 15);
            this.ui_lblChuyenNganh.TabIndex = 6;
            this.ui_lblChuyenNganh.Text = "Chuyên Ngành:";
            // 
            // ui_cbChuyenNganh
            // 
            this.ui_cbChuyenNganh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ui_cbChuyenNganh.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.ui_cbChuyenNganh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ui_cbChuyenNganh.FormattingEnabled = true;
            this.ui_cbChuyenNganh.Location = new System.Drawing.Point(130, 140);
            this.ui_cbChuyenNganh.Name = "ui_cbChuyenNganh";
            this.ui_cbChuyenNganh.Size = new System.Drawing.Size(300, 25);
            this.ui_cbChuyenNganh.TabIndex = 7;
            // 
            // ui_txbEmail
            // 
            this.ui_txbEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ui_txbEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ui_txbEmail.Location = new System.Drawing.Point(130, 90);
            this.ui_txbEmail.Name = "ui_txbEmail";
            this.ui_txbEmail.Size = new System.Drawing.Size(300, 25);
            this.ui_txbEmail.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(20, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "Email:";
            // 
            // ui_txbSDT
            // 
            this.ui_txbSDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ui_txbSDT.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ui_txbSDT.Location = new System.Drawing.Point(130, 40);
            this.ui_txbSDT.Name = "ui_txbSDT";
            this.ui_txbSDT.Size = new System.Drawing.Size(300, 25);
            this.ui_txbSDT.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(20, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 15);
            this.label5.TabIndex = 2;
            this.label5.Text = "Số Điện Thoại:";
            // 
            // gbInfo
            // 
            this.gbInfo.BackColor = System.Drawing.Color.White;
            this.gbInfo.Controls.Add(this.ui_cbLoaiNS);
            this.gbInfo.Controls.Add(this.label4);
            this.gbInfo.Controls.Add(this.ui_dtpNgaySinh);
            this.gbInfo.Controls.Add(this.label3);
            this.gbInfo.Controls.Add(this.ui_txbTen);
            this.gbInfo.Controls.Add(this.label2);
            this.gbInfo.Controls.Add(this.ui_txbMa);
            this.gbInfo.Controls.Add(this.label1);
            this.gbInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbInfo.ForeColor = System.Drawing.Color.DimGray;
            this.gbInfo.Location = new System.Drawing.Point(20, 10);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Size = new System.Drawing.Size(450, 240);
            this.gbInfo.TabIndex = 0;
            this.gbInfo.TabStop = false;
            this.gbInfo.Text = "Thông Tin Cá Nhân";
            // 
            // ui_cbLoaiNS
            // 
            this.ui_cbLoaiNS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ui_cbLoaiNS.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.ui_cbLoaiNS.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ui_cbLoaiNS.FormattingEnabled = true;
            this.ui_cbLoaiNS.Items.AddRange(new object[] {
            "Giáo viên",
            "Trợ giảng",
            "Nhân viên"});
            this.ui_cbLoaiNS.Location = new System.Drawing.Point(130, 190);
            this.ui_cbLoaiNS.Name = "ui_cbLoaiNS";
            this.ui_cbLoaiNS.Size = new System.Drawing.Size(300, 25);
            this.ui_cbLoaiNS.TabIndex = 7;
            this.ui_cbLoaiNS.SelectedIndexChanged += new System.EventHandler(this.Ui_cbLoaiNS_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(20, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Chức Vụ:";
            // 
            // ui_dtpNgaySinh
            // 
            this.ui_dtpNgaySinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ui_dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ui_dtpNgaySinh.Location = new System.Drawing.Point(130, 140);
            this.ui_dtpNgaySinh.Name = "ui_dtpNgaySinh";
            this.ui_dtpNgaySinh.Size = new System.Drawing.Size(300, 25);
            this.ui_dtpNgaySinh.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(20, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ngày Sinh:";
            // 
            // ui_txbTen
            // 
            this.ui_txbTen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ui_txbTen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ui_txbTen.Location = new System.Drawing.Point(130, 90);
            this.ui_txbTen.Name = "ui_txbTen";
            this.ui_txbTen.Size = new System.Drawing.Size(300, 25);
            this.ui_txbTen.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(20, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Họ Tên:";
            // 
            // ui_txbMa
            // 
            this.ui_txbMa.BackColor = System.Drawing.Color.LightYellow;
            this.ui_txbMa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ui_txbMa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ui_txbMa.Location = new System.Drawing.Point(130, 40);
            this.ui_txbMa.Name = "ui_txbMa";
            this.ui_txbMa.ReadOnly = true;
            this.ui_txbMa.Size = new System.Drawing.Size(300, 25);
            this.ui_txbMa.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(20, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã Nhân Sự:";
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.btnCapTK);
            this.pnlActions.Controls.Add(this.btnLamMoi);
            this.pnlActions.Controls.Add(this.btnXoa);
            this.pnlActions.Controls.Add(this.btnSua);
            this.pnlActions.Controls.Add(this.btnThem);
            this.pnlActions.Location = new System.Drawing.Point(960, 20);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(200, 310);
            this.pnlActions.TabIndex = 2;
            // 
            // btnCapTK
            // 
            this.btnCapTK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(66)))), ((int)(((byte)(193)))));
            this.btnCapTK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCapTK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapTK.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCapTK.ForeColor = System.Drawing.Color.White;
            this.btnCapTK.Location = new System.Drawing.Point(0, 200);
            this.btnCapTK.Name = "btnCapTK";
            this.btnCapTK.Size = new System.Drawing.Size(180, 45);
            this.btnCapTK.TabIndex = 4;
            this.btnCapTK.Text = "🔐 Cấp Tài Khoản";
            this.btnCapTK.UseVisualStyleBackColor = false;
            this.btnCapTK.Click += new System.EventHandler(this.BtnCapTK_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(0, 150);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(180, 45);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "🔄 Làm Mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.BtnLamMoi_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(0, 100);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(180, 45);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "❌ Xóa NS";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.BtnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(0, 50);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(180, 45);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "✏️ Cập Nhật";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.BtnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(0, 0);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(180, 45);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "➕ Thêm NS";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.BtnThem_Click);
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.White;
            this.pnlSearch.Controls.Add(this.ui_btnSearch);
            this.pnlSearch.Controls.Add(this.ui_txbSearch);
            this.pnlSearch.Controls.Add(this.lblSearch);
            this.pnlSearch.Location = new System.Drawing.Point(20, 260);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(920, 60);
            this.pnlSearch.TabIndex = 3;
            // 
            // ui_btnSearch
            // 
            this.ui_btnSearch.BackColor = System.Drawing.Color.Navy;
            this.ui_btnSearch.FlatAppearance.BorderSize = 0;
            this.ui_btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ui_btnSearch.ForeColor = System.Drawing.Color.White;
            this.ui_btnSearch.Location = new System.Drawing.Point(510, 13);
            this.ui_btnSearch.Name = "ui_btnSearch";
            this.ui_btnSearch.Size = new System.Drawing.Size(50, 31);
            this.ui_btnSearch.TabIndex = 2;
            this.ui_btnSearch.Text = "🔍";
            this.ui_btnSearch.UseVisualStyleBackColor = false;
            // 
            // ui_txbSearch
            // 
            this.ui_txbSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ui_txbSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.ui_txbSearch.Location = new System.Drawing.Point(100, 15);
            this.ui_txbSearch.Name = "ui_txbSearch";
            this.ui_txbSearch.Size = new System.Drawing.Size(400, 27);
            this.ui_txbSearch.TabIndex = 1;
            this.ui_txbSearch.TextChanged += new System.EventHandler(this.Ui_txbSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSearch.Location = new System.Drawing.Point(15, 18);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(67, 19);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Tìm kiếm:";
            // 
            // pnlGridContainer
            // 
            this.pnlGridContainer.Controls.Add(this.ui_dgvNhanSu);
            this.pnlGridContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridContainer.Location = new System.Drawing.Point(10, 420);
            this.pnlGridContainer.Name = "pnlGridContainer";
            this.pnlGridContainer.Padding = new System.Windows.Forms.Padding(20, 0, 20, 20);
            this.pnlGridContainer.Size = new System.Drawing.Size(1260, 370);
            this.pnlGridContainer.TabIndex = 2;
            // 
            // ui_dgvNhanSu
            // 
            this.ui_dgvNhanSu.AllowUserToAddRows = false;
            this.ui_dgvNhanSu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ui_dgvNhanSu.BackgroundColor = System.Drawing.Color.White;
            this.ui_dgvNhanSu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ui_dgvNhanSu.ColumnHeadersHeight = 45;
            this.ui_dgvNhanSu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ui_dgvNhanSu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ui_dgvNhanSu.EnableHeadersVisualStyles = false;
            this.ui_dgvNhanSu.GridColor = System.Drawing.Color.WhiteSmoke;
            this.ui_dgvNhanSu.Location = new System.Drawing.Point(20, 0);
            this.ui_dgvNhanSu.Name = "ui_dgvNhanSu";
            this.ui_dgvNhanSu.ReadOnly = true;
            this.ui_dgvNhanSu.RowHeadersVisible = false;
            this.ui_dgvNhanSu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ui_dgvNhanSu.Size = new System.Drawing.Size(1220, 350);
            this.ui_dgvNhanSu.TabIndex = 0;
            // 
            // FrmQuanLyNhanSu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.pnlGridContainer);
            this.Controls.Add(this.pnlInput);
            this.Controls.Add(this.pnlHeader);
            this.Name = "FrmQuanLyNhanSu";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "Quản Lý Nhân Sự";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlInput.ResumeLayout(false);
            this.gbContact.ResumeLayout(false);
            this.gbContact.PerformLayout();
            this.gbInfo.ResumeLayout(false);
            this.gbInfo.PerformLayout();
            this.pnlActions.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlGridContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ui_dgvNhanSu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlInput;
        private System.Windows.Forms.GroupBox gbInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ui_txbMa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ui_txbTen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker ui_dtpNgaySinh;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ui_cbLoaiNS;
        private System.Windows.Forms.GroupBox gbContact;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ui_txbSDT;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ui_txbEmail;
        private System.Windows.Forms.Label ui_lblChuyenNganh;
        private System.Windows.Forms.ComboBox ui_cbChuyenNganh;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnCapTK;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox ui_txbSearch;
        private System.Windows.Forms.Button ui_btnSearch;
        private System.Windows.Forms.Panel pnlGridContainer;
        private System.Windows.Forms.DataGridView ui_dgvNhanSu;
    }
}