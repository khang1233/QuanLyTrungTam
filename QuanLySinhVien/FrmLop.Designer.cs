namespace QuanLyTrungTam
{
    partial class FrmLop
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlInput = new System.Windows.Forms.Panel();
            this.btnLuuSua = new System.Windows.Forms.Button();
            this.btnMoiLop = new System.Windows.Forms.Button();
            this.btnXoaLop = new System.Windows.Forms.Button();
            this.dgvLop = new System.Windows.Forms.DataGridView();
            this.pnlInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLop)).BeginInit();
            this.SuspendLayout();

            // pnlInput
            this.pnlInput.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInput.Height = 220;
            this.pnlInput.Location = new System.Drawing.Point(0, 0);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.TabIndex = 0;

            // btnMoiLop
            this.btnMoiLop.BackColor = System.Drawing.Color.PaleGreen;
            this.btnMoiLop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoiLop.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnMoiLop.Location = new System.Drawing.Point(780, 30);
            this.btnMoiLop.Name = "btnMoiLop";
            this.btnMoiLop.Size = new System.Drawing.Size(150, 45);
            this.btnMoiLop.TabIndex = 10;
            this.btnMoiLop.Text = "MỞ LỚP MỚI";

            // btnLuuSua
            this.btnLuuSua.BackColor = System.Drawing.Color.SkyBlue;
            this.btnLuuSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuuSua.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuuSua.Location = new System.Drawing.Point(780, 85);
            this.btnLuuSua.Name = "btnLuuSua";
            this.btnLuuSua.Size = new System.Drawing.Size(150, 45);
            this.btnLuuSua.TabIndex = 11;
            this.btnLuuSua.Text = "LƯU CẬP NHẬT";

            // btnXoaLop
            this.btnXoaLop.BackColor = System.Drawing.Color.LightPink;
            this.btnXoaLop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaLop.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXoaLop.Location = new System.Drawing.Point(780, 140);
            this.btnXoaLop.Name = "btnXoaLop";
            this.btnXoaLop.Size = new System.Drawing.Size(150, 45);
            this.btnXoaLop.TabIndex = 12;
            this.btnXoaLop.Text = "XÓA LỚP";

            // dgvLop
            this.dgvLop.AllowUserToAddRows = false;
            this.dgvLop.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLop.BackgroundColor = System.Drawing.Color.White;
            this.dgvLop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLop.Location = new System.Drawing.Point(0, 220);
            this.dgvLop.Name = "dgvLop";
            this.dgvLop.ReadOnly = true;
            this.dgvLop.RowHeadersVisible = false;
            this.dgvLop.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            // FrmLop
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.dgvLop);
            this.Controls.Add(this.pnlInput);
            this.Text = "Quản Lý Lớp Học";
            this.pnlInput.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLop)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        public System.Windows.Forms.Panel pnlInput;
        public System.Windows.Forms.DataGridView dgvLop;
        public System.Windows.Forms.Button btnMoiLop;
        public System.Windows.Forms.Button btnLuuSua;
        public System.Windows.Forms.Button btnXoaLop;
    }
}