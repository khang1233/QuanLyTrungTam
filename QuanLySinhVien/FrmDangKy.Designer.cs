
namespace QuanLyTrungTam
{
    partial class FrmDangKy
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
            this.lblKyNang = new System.Windows.Forms.Label();
            this.cbKyNang = new System.Windows.Forms.ComboBox();
            this.lblLopHoc = new System.Windows.Forms.Label();
            this.cbLopHoc = new System.Windows.Forms.ComboBox();
            this.lblHocPhiLabel = new System.Windows.Forms.Label();
            this.lblHocPhi = new System.Windows.Forms.Label();
            this.btnDangKy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblKyNang
            // 
            this.lblKyNang.AutoSize = true;
            this.lblKyNang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblKyNang.Location = new System.Drawing.Point(50, 30);
            this.lblKyNang.Name = "lblKyNang";
            this.lblKyNang.Size = new System.Drawing.Size(98, 19);
            this.lblKyNang.TabIndex = 0;
            this.lblKyNang.Text = "Chọn Môn Học";
            // 
            // cbKyNang
            // 
            this.cbKyNang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKyNang.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cbKyNang.FormattingEnabled = true;
            this.cbKyNang.Location = new System.Drawing.Point(50, 55);
            this.cbKyNang.Name = "cbKyNang";
            this.cbKyNang.Size = new System.Drawing.Size(300, 28);
            this.cbKyNang.TabIndex = 1;
            // 
            // lblLopHoc
            // 
            this.lblLopHoc.AutoSize = true;
            this.lblLopHoc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLopHoc.Location = new System.Drawing.Point(50, 100);
            this.lblLopHoc.Name = "lblLopHoc";
            this.lblLopHoc.Size = new System.Drawing.Size(95, 19);
            this.lblLopHoc.TabIndex = 2;
            this.lblLopHoc.Text = "Chọn Lớp Học";
            // 
            // cbLopHoc
            // 
            this.cbLopHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLopHoc.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cbLopHoc.FormattingEnabled = true;
            this.cbLopHoc.Location = new System.Drawing.Point(50, 125);
            this.cbLopHoc.Name = "cbLopHoc";
            this.cbLopHoc.Size = new System.Drawing.Size(300, 28);
            this.cbLopHoc.TabIndex = 3;
            // 
            // lblHocPhiLabel
            // 
            this.lblHocPhiLabel.AutoSize = true;
            this.lblHocPhiLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHocPhiLabel.Location = new System.Drawing.Point(50, 170);
            this.lblHocPhiLabel.Name = "lblHocPhiLabel";
            this.lblHocPhiLabel.Size = new System.Drawing.Size(59, 19);
            this.lblHocPhiLabel.TabIndex = 4;
            this.lblHocPhiLabel.Text = "Học Phí:";
            // 
            // lblHocPhi
            // 
            this.lblHocPhi.AutoSize = true;
            this.lblHocPhi.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHocPhi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblHocPhi.Location = new System.Drawing.Point(120, 165);
            this.lblHocPhi.Name = "lblHocPhi";
            this.lblHocPhi.Size = new System.Drawing.Size(68, 25);
            this.lblHocPhi.TabIndex = 5;
            this.lblHocPhi.Text = "0 VNĐ";
            // 
            // btnDangKy
            // 
            this.btnDangKy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(81)))), ((int)(((byte)(181)))));
            this.btnDangKy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDangKy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangKy.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnDangKy.ForeColor = System.Drawing.Color.White;
            this.btnDangKy.Location = new System.Drawing.Point(90, 220);
            this.btnDangKy.Name = "btnDangKy";
            this.btnDangKy.Size = new System.Drawing.Size(220, 45);
            this.btnDangKy.TabIndex = 6;
            this.btnDangKy.Text = "XÁC NHẬN ĐĂNG KÝ";
            this.btnDangKy.UseVisualStyleBackColor = false;
            // 
            // FrmDangKy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.btnDangKy);
            this.Controls.Add(this.lblHocPhi);
            this.Controls.Add(this.lblHocPhiLabel);
            this.Controls.Add(this.cbLopHoc);
            this.Controls.Add(this.lblLopHoc);
            this.Controls.Add(this.cbKyNang);
            this.Controls.Add(this.lblKyNang);
            this.Name = "FrmDangKy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Đăng Ký Khóa Học";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblKyNang;
        private System.Windows.Forms.ComboBox cbKyNang;
        private System.Windows.Forms.Label lblLopHoc;
        private System.Windows.Forms.ComboBox cbLopHoc;
        private System.Windows.Forms.Label lblHocPhiLabel;
        private System.Windows.Forms.Label lblHocPhi;
        private System.Windows.Forms.Button btnDangKy;
    }
}