    namespace QuanLyTrungTam
    {
        partial class FrmLop
        {
            private System.ComponentModel.IContainer components = null;

            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                    components.Dispose();
                base.Dispose(disposing);
            }

            #region Windows Form Designer generated code

            private void InitializeComponent()
            {
                this.dgvLop = new System.Windows.Forms.DataGridView();
                this.label1 = new System.Windows.Forms.Label();
                this.txbMaLop = new System.Windows.Forms.TextBox();
                this.label2 = new System.Windows.Forms.Label();
                this.txbTenLop = new System.Windows.Forms.TextBox();
                this.label3 = new System.Windows.Forms.Label();
                this.txbMaKhoa = new System.Windows.Forms.TextBox();
                this.label4 = new System.Windows.Forms.Label();
                this.txbMaMonHoc = new System.Windows.Forms.TextBox();
                this.label5 = new System.Windows.Forms.Label();
                this.dtNgayMo = new System.Windows.Forms.DateTimePicker();
                this.btnThem = new System.Windows.Forms.Button();
                this.btnSua = new System.Windows.Forms.Button();
                this.btnXoa = new System.Windows.Forms.Button();

                ((System.ComponentModel.ISupportInitialize)(this.dgvLop)).BeginInit();
                this.SuspendLayout();

                // dgvLop
                this.dgvLop.Location = new System.Drawing.Point(25, 230);
                this.dgvLop.Size = new System.Drawing.Size(700, 300);
                this.dgvLop.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLop_CellClick);

                // Mã lớp
                this.label1.Text = "Mã Lớp:";
                this.label1.Location = new System.Drawing.Point(25, 20);

                this.txbMaLop.Location = new System.Drawing.Point(140, 20);
                this.txbMaLop.Size = new System.Drawing.Size(250, 26);

                // Tên lớp
                this.label2.Text = "Tên Lớp:";
                this.label2.Location = new System.Drawing.Point(25, 60);

                this.txbTenLop.Location = new System.Drawing.Point(140, 60);
                this.txbTenLop.Size = new System.Drawing.Size(250, 26);

                // Mã khoa
                this.label3.Text = "Mã Khoa:";
                this.label3.Location = new System.Drawing.Point(25, 100);

                this.txbMaKhoa.Location = new System.Drawing.Point(140, 100);
                this.txbMaKhoa.Size = new System.Drawing.Size(250, 26);

                // Mã môn học
                this.label4.Text = "Mã Môn Học:";
                this.label4.Location = new System.Drawing.Point(25, 140);

                this.txbMaMonHoc.Location = new System.Drawing.Point(140, 140);
                this.txbMaMonHoc.Size = new System.Drawing.Size(250, 26);

                // Ngày mở
                this.label5.Text = "Ngày Mở:";
                this.label5.Location = new System.Drawing.Point(25, 180);

                this.dtNgayMo.Location = new System.Drawing.Point(140, 180);
                this.dtNgayMo.Size = new System.Drawing.Size(250, 26);

                // Buttons
                this.btnThem.Text = "Thêm";
                this.btnThem.Location = new System.Drawing.Point(450, 20);
                this.btnThem.Size = new System.Drawing.Size(120, 35);
                this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

                this.btnSua.Text = "Sửa";
                this.btnSua.Location = new System.Drawing.Point(450, 70);
                this.btnSua.Size = new System.Drawing.Size(120, 35);
                this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

                this.btnXoa.Text = "Xóa";
                this.btnXoa.Location = new System.Drawing.Point(450, 120);
                this.btnXoa.Size = new System.Drawing.Size(120, 35);
                this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

                // Form
                this.ClientSize = new System.Drawing.Size(760, 550);
                this.Controls.Add(this.dgvLop);
                this.Controls.Add(this.label1); this.Controls.Add(this.txbMaLop);
                this.Controls.Add(this.label2); this.Controls.Add(this.txbTenLop);
                this.Controls.Add(this.label3); this.Controls.Add(this.txbMaKhoa);
                this.Controls.Add(this.label4); this.Controls.Add(this.txbMaMonHoc);
                this.Controls.Add(this.label5); this.Controls.Add(this.dtNgayMo);
                this.Controls.Add(this.btnThem); this.Controls.Add(this.btnSua); this.Controls.Add(this.btnXoa);

                this.Text = "Quản Lý Lớp";
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

                ((System.ComponentModel.ISupportInitialize)(this.dgvLop)).EndInit();
                this.ResumeLayout(false);
                this.PerformLayout();
            }

            #endregion

            private System.Windows.Forms.DataGridView dgvLop;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.TextBox txbMaLop;
            private System.Windows.Forms.Label label2;
            private System.Windows.Forms.TextBox txbTenLop;
            private System.Windows.Forms.Label label3;
            private System.Windows.Forms.TextBox txbMaKhoa;
            private System.Windows.Forms.Label label4;
            private System.Windows.Forms.TextBox txbMaMonHoc;
            private System.Windows.Forms.Label label5;
            private System.Windows.Forms.DateTimePicker dtNgayMo;
            private System.Windows.Forms.Button btnThem;
            private System.Windows.Forms.Button btnSua;
            private System.Windows.Forms.Button btnXoa;
        }
    }
