
namespace QuanLyTrungTam
{
    partial class FrmLop
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
            this.pnlInput = new System.Windows.Forms.Panel();
            
            this.gbGeneral = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbMonHoc = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txbMaLop = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txbTenLop = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbTrangThai = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.nmSiSo = new System.Windows.Forms.NumericUpDown();

            this.gbTime = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbThu = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbCaHoc = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbPhongHoc = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbGiaoVien = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbTroGiang = new System.Windows.Forms.ComboBox();

            this.pnlActions = new System.Windows.Forms.Panel();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();

            this.pnlSearch = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txbSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();

            this.pnlGridContainer = new System.Windows.Forms.Panel();
            this.dgvMain = new System.Windows.Forms.DataGridView();

            this.pnlHeader.SuspendLayout();
            this.pnlInput.SuspendLayout();
            this.gbGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmSiSo)).BeginInit();
            this.gbTime.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlGridContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
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
            this.lblTitle.Size = new System.Drawing.Size(437, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ LỚP HỌC & XẾP LỊCH";
            // 
            // pnlInput
            // 
            this.pnlInput.BackColor = System.Drawing.Color.Transparent;
            this.pnlInput.Controls.Add(this.gbGeneral);
            this.pnlInput.Controls.Add(this.gbTime);
            this.pnlInput.Controls.Add(this.pnlActions);
            this.pnlInput.Controls.Add(this.pnlSearch);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInput.Location = new System.Drawing.Point(10, 80);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.pnlInput.Size = new System.Drawing.Size(1260, 380);
            this.pnlInput.TabIndex = 1;
            // 
            // gbGeneral
            // 
            this.gbGeneral.BackColor = System.Drawing.Color.White;
            this.gbGeneral.Controls.Add(this.nmSiSo);
            this.gbGeneral.Controls.Add(this.label5);
            this.gbGeneral.Controls.Add(this.cbTrangThai);
            this.gbGeneral.Controls.Add(this.label4);
            this.gbGeneral.Controls.Add(this.txbTenLop);
            this.gbGeneral.Controls.Add(this.label3);
            this.gbGeneral.Controls.Add(this.txbMaLop);
            this.gbGeneral.Controls.Add(this.label2);
            this.gbGeneral.Controls.Add(this.cbMonHoc);
            this.gbGeneral.Controls.Add(this.label1);
            this.gbGeneral.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbGeneral.ForeColor = System.Drawing.Color.DimGray;
            this.gbGeneral.Location = new System.Drawing.Point(20, 15);
            this.gbGeneral.Name = "gbGeneral";
            this.gbGeneral.Size = new System.Drawing.Size(450, 290);
            this.gbGeneral.TabIndex = 0;
            this.gbGeneral.TabStop = false;
            this.gbGeneral.Text = "Thông Tin Chung";
            // 
            // label1 (MonHoc)
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(20, 38);
            this.label1.Text = "Môn Học:";
            // 
            // cbMonHoc
            // 
            this.cbMonHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMonHoc.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cbMonHoc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbMonHoc.FormattingEnabled = true;
            this.cbMonHoc.Location = new System.Drawing.Point(130, 35);
            this.cbMonHoc.Size = new System.Drawing.Size(300, 25);
            this.cbMonHoc.TabIndex = 1;
            // 
            // label2 (MaLop)
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(20, 88);
            this.label2.Text = "Mã Lớp:";
            // 
            // txbMaLop
            // 
            this.txbMaLop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txbMaLop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbMaLop.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txbMaLop.Location = new System.Drawing.Point(130, 85);
            this.txbMaLop.ReadOnly = true;
            this.txbMaLop.Size = new System.Drawing.Size(300, 25);
            this.txbMaLop.TabIndex = 3;
            // 
            // label3 (TenLop)
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(20, 138);
            this.label3.Text = "Tên Lớp:";
            // 
            // txbTenLop
            // 
            this.txbTenLop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbTenLop.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txbTenLop.Location = new System.Drawing.Point(130, 135);
            this.txbTenLop.Size = new System.Drawing.Size(300, 25);
            this.txbTenLop.TabIndex = 5;
            // 
            // label4 (TrangThai)
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(20, 188);
            this.label4.Text = "Trạng Thái:";
            // 
            // cbTrangThai
            // 
            this.cbTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTrangThai.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cbTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbTrangThai.FormattingEnabled = true;
            this.cbTrangThai.Items.AddRange(new object[] {
            "Đang học", "Đã kết thúc", "Tạm ngưng", "Sắp mở"});
            this.cbTrangThai.Location = new System.Drawing.Point(130, 185);
            this.cbTrangThai.Size = new System.Drawing.Size(300, 25);
            this.cbTrangThai.TabIndex = 7;
            // 
            // label5 (SiSo)
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(20, 238);
            this.label5.Text = "Sĩ Số:";
            // 
            // nmSiSo
            // 
            this.nmSiSo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nmSiSo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nmSiSo.Location = new System.Drawing.Point(130, 235);
            this.nmSiSo.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nmSiSo.Value = new decimal(new int[] { 20, 0, 0, 0 });
            this.nmSiSo.Size = new System.Drawing.Size(300, 25);
            this.nmSiSo.TabIndex = 9;
            // 
            // gbTime
            // 
            this.gbTime.BackColor = System.Drawing.Color.White;
            this.gbTime.Controls.Add(this.cbTroGiang);
            this.gbTime.Controls.Add(this.label10);
            this.gbTime.Controls.Add(this.cbGiaoVien);
            this.gbTime.Controls.Add(this.label9);
            this.gbTime.Controls.Add(this.cbPhongHoc);
            this.gbTime.Controls.Add(this.label8);
            this.gbTime.Controls.Add(this.cbCaHoc);
            this.gbTime.Controls.Add(this.label7);
            this.gbTime.Controls.Add(this.cbThu);
            this.gbTime.Controls.Add(this.label6);
            this.gbTime.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbTime.ForeColor = System.Drawing.Color.DimGray;
            this.gbTime.Location = new System.Drawing.Point(480, 15);
            this.gbTime.Name = "gbTime";
            this.gbTime.Size = new System.Drawing.Size(530, 290);
            this.gbTime.TabIndex = 1;
            this.gbTime.TabStop = false;
            this.gbTime.Text = "Thời Gian & Địa Điểm";
            // 
            // label6 (LichHoc)
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(20, 38);
            this.label6.Text = "Lịch Học (Thứ):";
            // 
            // cbThu
            // 
            this.cbThu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbThu.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cbThu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbThu.FormattingEnabled = true;
            this.cbThu.Items.AddRange(new object[] {
            "T2 (Thứ Hai)", "T3 (Thứ Ba)", "T4 (Thứ Tư)", "T5 (Thứ Năm)", "T6 (Thứ Sáu)", "T7 (Thứ Bảy)", "CN (Chủ Nhật)",
            "T2-T4", "T2-T5", "T2-T6", "T3-T5", "T3-T6", "T4-T6", "T5-T7", "T7-CN",
            "T2-T4-T6", "T3-T5-T7", "T2-T3-T4-T5-T6"});
            this.cbThu.Location = new System.Drawing.Point(130, 35);
            this.cbThu.Size = new System.Drawing.Size(300, 25);
            this.cbThu.TabIndex = 1;
            // 
            // label7 (CaHoc)
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(20, 88);
            this.label7.Text = "Ca Học (Giờ):";
            // 
            // cbCaHoc
            // 
            this.cbCaHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCaHoc.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cbCaHoc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbCaHoc.FormattingEnabled = true;
            this.cbCaHoc.Items.AddRange(new object[] {
            "Ca 1 (08:00 - 10:00)", "Ca 2 (10:00 - 12:00)",
            "Ca 3 (13:30 - 15:30)", "Ca 4 (15:30 - 17:30)",
            "Ca Tối 1 (17:45 - 19:15)", "Ca Tối 2 (19:30 - 21:00)"});
            this.cbCaHoc.Location = new System.Drawing.Point(130, 85);
            this.cbCaHoc.Size = new System.Drawing.Size(300, 25);
            this.cbCaHoc.TabIndex = 3;
            // 
            // label8 (PhongHoc)
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(20, 138);
            this.label8.Text = "Phòng Học:";
            // 
            // cbPhongHoc
            // 
            this.cbPhongHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPhongHoc.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cbPhongHoc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbPhongHoc.FormattingEnabled = true;
            this.cbPhongHoc.Location = new System.Drawing.Point(130, 135);
            this.cbPhongHoc.Size = new System.Drawing.Size(300, 25);
            this.cbPhongHoc.TabIndex = 5;
            // 
            // label9 (GiaoVien)
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(20, 188);
            this.label9.Text = "Giáo Viên:";
            // 
            // cbGiaoVien
            // 
            this.cbGiaoVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGiaoVien.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cbGiaoVien.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbGiaoVien.FormattingEnabled = true;
            this.cbGiaoVien.Location = new System.Drawing.Point(130, 185);
            this.cbGiaoVien.Size = new System.Drawing.Size(300, 25);
            this.cbGiaoVien.TabIndex = 7;
            // 
            // label10 (TroGiang)
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(20, 238);
            this.label10.Text = "Trợ Giảng:";
            // 
            // cbTroGiang
            // 
            this.cbTroGiang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTroGiang.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cbTroGiang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbTroGiang.FormattingEnabled = true;
            this.cbTroGiang.Location = new System.Drawing.Point(130, 235);
            this.cbTroGiang.Size = new System.Drawing.Size(300, 25);
            this.cbTroGiang.TabIndex = 9;
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.btnLamMoi);
            this.pnlActions.Controls.Add(this.btnXoa);
            this.pnlActions.Controls.Add(this.btnSua);
            this.pnlActions.Controls.Add(this.btnThem);
            this.pnlActions.Location = new System.Drawing.Point(1030, 25);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(160, 280);
            this.pnlActions.TabIndex = 2;
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
            this.btnThem.Size = new System.Drawing.Size(160, 45);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Mở Lớp Mới";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(0, 60);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(160, 45);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Cập Nhật";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(0, 120);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(160, 45);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa Lớp";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.BtnDel_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(0, 180);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(160, 45);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "Làm Mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.BtnLamMoi_Click);
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.White;
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.txbSearch);
            this.pnlSearch.Controls.Add(this.lblSearch);
            this.pnlSearch.Location = new System.Drawing.Point(20, 320);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(1070, 40);
            this.pnlSearch.TabIndex = 3;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSearch.Location = new System.Drawing.Point(10, 10);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(109, 19);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Tìm kiếm nhanh:";
            // 
            // txbSearch
            // 
            this.txbSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txbSearch.Location = new System.Drawing.Point(130, 8);
            this.txbSearch.Name = "txbSearch";
            this.txbSearch.Size = new System.Drawing.Size(400, 25);
            this.txbSearch.TabIndex = 1;
            this.txbSearch.TextChanged += new System.EventHandler(this.TxbSearch_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Orange;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(540, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(40, 26);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "🔍";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // pnlGridContainer
            // 
            this.pnlGridContainer.Controls.Add(this.dgvMain);
            this.pnlGridContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridContainer.Location = new System.Drawing.Point(10, 460);
            this.pnlGridContainer.Name = "pnlGridContainer";
            this.pnlGridContainer.Padding = new System.Windows.Forms.Padding(20, 10, 20, 20);
            this.pnlGridContainer.Size = new System.Drawing.Size(1260, 330);
            this.pnlGridContainer.TabIndex = 2;
            // 
            // dgvMain
            // 
            this.dgvMain.BackgroundColor = System.Drawing.Color.White;
            this.dgvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMain.ColumnHeadersHeight = 45;
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.Location = new System.Drawing.Point(20, 10);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(1220, 300);
            this.dgvMain.TabIndex = 0;
            // 
            // FrmLop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.pnlGridContainer);
            this.Controls.Add(this.pnlInput);
            this.Controls.Add(this.pnlHeader);
            this.Name = "FrmLop";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "Quản Lý Lớp Học";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlInput.ResumeLayout(false);
            this.gbGeneral.ResumeLayout(false);
            this.gbGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmSiSo)).EndInit();
            this.gbTime.ResumeLayout(false);
            this.gbTime.PerformLayout();
            this.pnlActions.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlGridContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlInput;
        private System.Windows.Forms.GroupBox gbGeneral;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbMonHoc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbMaLop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbTenLop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbTrangThai;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nmSiSo;
        private System.Windows.Forms.GroupBox gbTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbThu;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbCaHoc;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbPhongHoc;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbGiaoVien;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbTroGiang;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txbSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel pnlGridContainer;
        private System.Windows.Forms.DataGridView dgvMain;
    }
}