namespace QuanLyTrungTam
{
    partial class FrmHocPhi
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
            this.dgvSinhVien = new System.Windows.Forms.DataGridView();
            this.lblTongHP = new System.Windows.Forms.Label();
            this.lblDaDong = new System.Windows.Forms.Label();
            this.lblConNo = new System.Windows.Forms.Label();
            this.lblTenSV = new System.Windows.Forms.Label();
            this.txbSoTienDong = new System.Windows.Forms.TextBox();
            this.btnXacNhanDong = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSinhVien)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSinhVien
            // 
            this.dgvSinhVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSinhVien.Location = new System.Drawing.Point(12, 126);
            this.dgvSinhVien.Name = "dgvSinhVien";
            this.dgvSinhVien.RowHeadersWidth = 51;
            this.dgvSinhVien.RowTemplate.Height = 24;
            this.dgvSinhVien.Size = new System.Drawing.Size(776, 253);
            this.dgvSinhVien.TabIndex = 0;
            // 
            // lblTongHP
            // 
            this.lblTongHP.AutoSize = true;
            this.lblTongHP.Location = new System.Drawing.Point(26, 27);
            this.lblTongHP.Name = "lblTongHP";
            this.lblTongHP.Size = new System.Drawing.Size(44, 16);
            this.lblTongHP.TabIndex = 1;
            this.lblTongHP.Text = "Tổng học phí";
            // 
            // lblDaDong
            // 
            this.lblDaDong.AutoSize = true;
            this.lblDaDong.Location = new System.Drawing.Point(276, 27);
            this.lblDaDong.Name = "lblDaDong";
            this.lblDaDong.Size = new System.Drawing.Size(44, 16);
            this.lblDaDong.TabIndex = 2;
            this.lblDaDong.Text = "Đã đóng";
            // 
            // lblConNo
            // 
            this.lblConNo.AutoSize = true;
            this.lblConNo.Location = new System.Drawing.Point(544, 27);
            this.lblConNo.Name = "lblConNo";
            this.lblConNo.Size = new System.Drawing.Size(44, 16);
            this.lblConNo.TabIndex = 3;
            this.lblConNo.Text = "Còn nợ";
            // 
            // lblTenSV
            // 
            this.lblTenSV.AutoSize = true;
            this.lblTenSV.Location = new System.Drawing.Point(26, 80);
            this.lblTenSV.Name = "lblTenSV";
            this.lblTenSV.Size = new System.Drawing.Size(44, 16);
            this.lblTenSV.TabIndex = 4;
            this.lblTenSV.Text = "Tên SV";
            // 
            // txbSoTienDong
            // 
            this.txbSoTienDong.Location = new System.Drawing.Point(29, 401);
            this.txbSoTienDong.Name = "txbSoTienDong";
            this.txbSoTienDong.Size = new System.Drawing.Size(201, 22);
            this.txbSoTienDong.TabIndex = 5;
            // 
            // btnXacNhanDong
            // 
            this.btnXacNhanDong.Location = new System.Drawing.Point(254, 395);
            this.btnXacNhanDong.Name = "btnXacNhanDong";
            this.btnXacNhanDong.Size = new System.Drawing.Size(124, 34);
            this.btnXacNhanDong.TabIndex = 6;
            this.btnXacNhanDong.Text = "Thanh Toán";
            this.btnXacNhanDong.UseVisualStyleBackColor = true;
            this.btnXacNhanDong.Click += new System.EventHandler(this.btnXacNhanDong_Click);
            // 
            // FrmHocPhi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnXacNhanDong);
            this.Controls.Add(this.txbSoTienDong);
            this.Controls.Add(this.lblTenSV);
            this.Controls.Add(this.lblConNo);
            this.Controls.Add(this.lblDaDong);
            this.Controls.Add(this.lblTongHP);
            this.Controls.Add(this.dgvSinhVien);
            this.Name = "FrmHocPhi";
            this.Text = "Quản Lý Học Phí";
            this.Load += new System.EventHandler(this.FrmHocPhi_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSinhVien)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // Khai báo các biến control để file Logic nhìn thấy
        private System.Windows.Forms.DataGridView dgvSinhVien;
        private System.Windows.Forms.Label lblTongHP;
        private System.Windows.Forms.Label lblDaDong;
        private System.Windows.Forms.Label lblConNo;
        private System.Windows.Forms.Label lblTenSV;
        private System.Windows.Forms.TextBox txbSoTienDong;
        private System.Windows.Forms.Button btnXacNhanDong;
    }
}