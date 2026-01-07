
namespace QuanLyTrungTam
{
    partial class FrmQuanLyHocVien
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
            this.ui_pnlTop = new System.Windows.Forms.Panel();
            this.ui_dgvHocVien = new System.Windows.Forms.DataGridView();
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ui_txbMa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ui_txbTen = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ui_dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.ui_cbTrangThai = new System.Windows.Forms.ComboBox();
            this.gbContact = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ui_txbSDT = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ui_txbEmail = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ui_txbDiaChi = new System.Windows.Forms.TextBox();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnDangKyLop = new System.Windows.Forms.Button();
            this.btnThuPhi = new System.Windows.Forms.Button();
            this.btnCapTK = new System.Windows.Forms.Button();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.ui_txbSearch = new System.Windows.Forms.TextBox();
            this.ui_btnSearch = new System.Windows.Forms.Button();
            this.pnlGridContainer = new System.Windows.Forms.Panel();
            this.pnlHeader.SuspendLayout();
            this.ui_pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ui_dgvHocVien)).BeginInit();
            this.gbInfo.SuspendLayout();
            this.gbContact.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlGridContainer.SuspendLayout();
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
            this.lblTitle.Size = new System.Drawing.Size(236, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ HỌC VIÊN";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ui_pnlTop
            // 
            this.ui_pnlTop.BackColor = System.Drawing.Color.Transparent;
            this.ui_pnlTop.Controls.Add(this.pnlSearch);
            this.ui_pnlTop.Controls.Add(this.pnlActions);
            this.ui_pnlTop.Controls.Add(this.gbContact);
            this.ui_pnlTop.Controls.Add(this.gbInfo);
            this.ui_pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.ui_pnlTop.Location = new System.Drawing.Point(10, 80);
            this.ui_pnlTop.Name = "ui_pnlTop";
            this.ui_pnlTop.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.ui_pnlTop.Size = new System.Drawing.Size(1260, 370);
            this.ui_pnlTop.TabIndex = 1;
            // 
            // gbInfo
            // 
            this.gbInfo.BackColor = System.Drawing.Color.White;
            this.gbInfo.Controls.Add(this.ui_cbTrangThai);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(20, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã HV:";
            // 
            // ui_txbMa
            // 
            this.ui_txbMa.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ui_txbMa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ui_txbMa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ui_txbMa.Location = new System.Drawing.Point(120, 35);
            this.ui_txbMa.Name = "ui_txbMa";
            this.ui_txbMa.ReadOnly = true;
            this.ui_txbMa.Size = new System.Drawing.Size(300, 25);
            this.ui_txbMa.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(20, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Họ Tên:";
            // 
            // ui_txbTen
            // 
            this.ui_txbTen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ui_txbTen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ui_txbTen.Location = new System.Drawing.Point(120, 80);
            this.ui_txbTen.Name = "ui_txbTen";
            this.ui_txbTen.Size = new System.Drawing.Size(300, 25);
            this.ui_txbTen.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(20, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ngày Sinh:";
            // 
            // ui_dtpNgaySinh
            // 
            this.ui_dtpNgaySinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ui_dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ui_dtpNgaySinh.Location = new System.Drawing.Point(120, 125);
            this.ui_dtpNgaySinh.Name = "ui_dtpNgaySinh";
            this.ui_dtpNgaySinh.Size = new System.Drawing.Size(300, 25);
            this.ui_dtpNgaySinh.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(20, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Trạng Thái:";
            // 
            // ui_cbTrangThai
            // 
            this.ui_cbTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ui_cbTrangThai.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.ui_cbTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ui_cbTrangThai.FormattingEnabled = true;
            this.ui_cbTrangThai.Items.AddRange(new object[] {
            "Nhập học",
            "Đang học",
            "Bảo lưu",
            "Bỏ học",
            "Hoàn thành"});
            this.ui_cbTrangThai.Location = new System.Drawing.Point(120, 170);
            this.ui_cbTrangThai.Name = "ui_cbTrangThai";
            this.ui_cbTrangThai.Size = new System.Drawing.Size(300, 25);
            this.ui_cbTrangThai.TabIndex = 7;
            // 
            // gbContact
            // 
            this.gbContact.BackColor = System.Drawing.Color.White;
            this.gbContact.Controls.Add(this.ui_txbDiaChi);
            this.gbContact.Controls.Add(this.label7);
            this.gbContact.Controls.Add(this.ui_txbEmail);
            this.gbContact.Controls.Add(this.label6);
            this.gbContact.Controls.Add(this.ui_txbSDT);
            this.gbContact.Controls.Add(this.label5);
            this.gbContact.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbContact.ForeColor = System.Drawing.Color.DimGray;
            this.gbContact.Location = new System.Drawing.Point(490, 10);
            this.gbContact.Name = "gbContact";
            this.gbContact.Size = new System.Drawing.Size(450, 240);
            this.gbContact.TabIndex = 1;
            this.gbContact.TabStop = false;
            this.gbContact.Text = "Thông Tin Liên Hệ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(20, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Số ĐT:";
            // 
            // ui_txbSDT
            // 
            this.ui_txbSDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ui_txbSDT.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ui_txbSDT.Location = new System.Drawing.Point(120, 35);
            this.ui_txbSDT.Name = "ui_txbSDT";
            this.ui_txbSDT.Size = new System.Drawing.Size(300, 25);
            this.ui_txbSDT.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(20, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "Email:";
            // 
            // ui_txbEmail
            // 
            this.ui_txbEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ui_txbEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ui_txbEmail.Location = new System.Drawing.Point(120, 80);
            this.ui_txbEmail.Name = "ui_txbEmail";
            this.ui_txbEmail.Size = new System.Drawing.Size(300, 25);
            this.ui_txbEmail.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(20, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 15);
            this.label7.TabIndex = 4;
            this.label7.Text = "Địa Chỉ:";
            // 
            // ui_txbDiaChi
            // 
            this.ui_txbDiaChi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ui_txbDiaChi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ui_txbDiaChi.Location = new System.Drawing.Point(120, 125);
            this.ui_txbDiaChi.Name = "ui_txbDiaChi";
            this.ui_txbDiaChi.Size = new System.Drawing.Size(300, 25);
            this.ui_txbDiaChi.TabIndex = 5;
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.btnCapTK);
            this.pnlActions.Controls.Add(this.btnThuPhi);
            this.pnlActions.Controls.Add(this.btnDangKyLop);
            this.pnlActions.Controls.Add(this.btnXoa);
            this.pnlActions.Controls.Add(this.btnCapNhat);
            this.pnlActions.Controls.Add(this.btnLuu);
            this.pnlActions.Controls.Add(this.btnLamMoi);
            this.pnlActions.Location = new System.Drawing.Point(960, 20);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(180, 340);
            this.pnlActions.TabIndex = 2;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(0, 0);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(160, 40);
            this.btnLamMoi.TabIndex = 0;
            this.btnLamMoi.Text = "🔄 Làm Mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.BtnLamMoi_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(0, 45);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(160, 40);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "💾 Lưu Mới";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.BtnThem_Click);
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnCapNhat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCapNhat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCapNhat.ForeColor = System.Drawing.Color.White;
            this.btnCapNhat.Location = new System.Drawing.Point(0, 90);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(160, 40);
            this.btnCapNhat.TabIndex = 2;
            this.btnCapNhat.Text = "✏️ Cập Nhật";
            this.btnCapNhat.UseVisualStyleBackColor = false;
            this.btnCapNhat.Click += new System.EventHandler(this.BtnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(0, 135);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(160, 40);
            this.btnXoa.TabIndex = 3;
            this.btnXoa.Text = "❌ Xóa HV";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.BtnXoa_Click);
            // 
            // btnDangKyLop
            // 
            this.btnDangKyLop.BackColor = System.Drawing.Color.Orange;
            this.btnDangKyLop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDangKyLop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangKyLop.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDangKyLop.ForeColor = System.Drawing.Color.White;
            this.btnDangKyLop.Location = new System.Drawing.Point(0, 195);
            this.btnDangKyLop.Name = "btnDangKyLop";
            this.btnDangKyLop.Size = new System.Drawing.Size(160, 40);
            this.btnDangKyLop.TabIndex = 4;
            this.btnDangKyLop.Text = "📚 Đăng Ký Lớp";
            this.btnDangKyLop.UseVisualStyleBackColor = false;
            this.btnDangKyLop.Click += new System.EventHandler(this.BtnDangKyLop_Click);
            // 
            // btnThuPhi
            // 
            this.btnThuPhi.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnThuPhi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThuPhi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThuPhi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThuPhi.ForeColor = System.Drawing.Color.White;
            this.btnThuPhi.Location = new System.Drawing.Point(0, 240);
            this.btnThuPhi.Name = "btnThuPhi";
            this.btnThuPhi.Size = new System.Drawing.Size(160, 40);
            this.btnThuPhi.TabIndex = 5;
            this.btnThuPhi.Text = "💰 Thu Học Phí";
            this.btnThuPhi.UseVisualStyleBackColor = false;
            this.btnThuPhi.Click += new System.EventHandler(this.BtnThuPhi_Click);
            // 
            // btnCapTK
            // 
            this.btnCapTK.BackColor = System.Drawing.Color.Purple;
            this.btnCapTK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCapTK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapTK.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCapTK.ForeColor = System.Drawing.Color.White;
            this.btnCapTK.Location = new System.Drawing.Point(0, 285);
            this.btnCapTK.Name = "btnCapTK";
            this.btnCapTK.Size = new System.Drawing.Size(160, 40);
            this.btnCapTK.TabIndex = 6;
            this.btnCapTK.Text = "🔐 Cấp Tài Khoản";
            this.btnCapTK.UseVisualStyleBackColor = false;
            this.btnCapTK.Click += new System.EventHandler(this.BtnCapTK_Click);
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.White;
            this.pnlSearch.Controls.Add(this.ui_btnSearch);
            this.pnlSearch.Controls.Add(this.ui_txbSearch);
            this.pnlSearch.Controls.Add(this.lblSearch);
            this.pnlSearch.Location = new System.Drawing.Point(20, 270);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(920, 50);
            this.pnlSearch.TabIndex = 3;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSearch.Location = new System.Drawing.Point(15, 15);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(67, 19);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Tìm kiếm:";
            // 
            // ui_txbSearch
            // 
            this.ui_txbSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ui_txbSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ui_txbSearch.Location = new System.Drawing.Point(100, 12);
            this.ui_txbSearch.Name = "ui_txbSearch";
            this.ui_txbSearch.Size = new System.Drawing.Size(400, 25);
            this.ui_txbSearch.TabIndex = 1;
            this.ui_txbSearch.TextChanged += new System.EventHandler(this.Ui_txbSearch_TextChanged);
            // 
            // ui_btnSearch
            // 
            this.ui_btnSearch.BackColor = System.Drawing.Color.Navy;
            this.ui_btnSearch.FlatAppearance.BorderSize = 0;
            this.ui_btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ui_btnSearch.ForeColor = System.Drawing.Color.White;
            this.ui_btnSearch.Location = new System.Drawing.Point(510, 10);
            this.ui_btnSearch.Name = "ui_btnSearch";
            this.ui_btnSearch.Size = new System.Drawing.Size(50, 28);
            this.ui_btnSearch.TabIndex = 2;
            this.ui_btnSearch.Text = "🔍";
            this.ui_btnSearch.UseVisualStyleBackColor = false;
            this.ui_btnSearch.Click += new System.EventHandler(this.Ui_btnSearch_Click);
            // 
            // pnlGridContainer
            // 
            this.pnlGridContainer.Controls.Add(this.ui_dgvHocVien);
            this.pnlGridContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridContainer.Location = new System.Drawing.Point(10, 450);
            this.pnlGridContainer.Name = "pnlGridContainer";
            this.pnlGridContainer.Padding = new System.Windows.Forms.Padding(20, 0, 20, 20);
            this.pnlGridContainer.Size = new System.Drawing.Size(1260, 340);
            this.pnlGridContainer.TabIndex = 2;
            // 
            // ui_dgvHocVien
            // 
            this.ui_dgvHocVien.AllowUserToAddRows = false;
            this.ui_dgvHocVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ui_dgvHocVien.BackgroundColor = System.Drawing.Color.White;
            this.ui_dgvHocVien.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ui_dgvHocVien.ColumnHeadersHeight = 45;
            this.ui_dgvHocVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ui_dgvHocVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ui_dgvHocVien.EnableHeadersVisualStyles = false;
            this.ui_dgvHocVien.Location = new System.Drawing.Point(20, 0);
            this.ui_dgvHocVien.Name = "ui_dgvHocVien";
            this.ui_dgvHocVien.ReadOnly = true;
            this.ui_dgvHocVien.RowHeadersVisible = false;
            this.ui_dgvHocVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ui_dgvHocVien.Size = new System.Drawing.Size(1220, 320);
            this.ui_dgvHocVien.TabIndex = 0;
            // 
            // FrmQuanLyHocVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.pnlGridContainer);
            this.Controls.Add(this.ui_pnlTop);
            this.Controls.Add(this.pnlHeader);
            this.Name = "FrmQuanLyHocVien";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "Quản Lý Học Viên";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ui_pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ui_dgvHocVien)).EndInit();
            this.gbInfo.ResumeLayout(false);
            this.gbInfo.PerformLayout();
            this.gbContact.ResumeLayout(false);
            this.gbContact.PerformLayout();
            this.pnlActions.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlGridContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel ui_pnlTop;
        private System.Windows.Forms.GroupBox gbInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ui_txbMa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ui_txbTen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker ui_dtpNgaySinh;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ui_cbTrangThai;
        private System.Windows.Forms.GroupBox gbContact;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ui_txbSDT;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ui_txbEmail;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox ui_txbDiaChi;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnDangKyLop;
        private System.Windows.Forms.Button btnThuPhi;
        private System.Windows.Forms.Button btnCapTK;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox ui_txbSearch;
        private System.Windows.Forms.Button ui_btnSearch;
        private System.Windows.Forms.Panel pnlGridContainer;
        private System.Windows.Forms.DataGridView ui_dgvHocVien;
    }
}
