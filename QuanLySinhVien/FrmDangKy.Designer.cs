namespace QuanLyTrungTam
{
    partial class FrmDangKy
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
            this.cbKyNang = new System.Windows.Forms.ComboBox();
            this.cbLopHoc = new System.Windows.Forms.ComboBox();
            this.lblHocPhi = new System.Windows.Forms.Label();
            this.btnDangKy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbKyNang
            // 
            this.cbKyNang.FormattingEnabled = true;
            this.cbKyNang.Location = new System.Drawing.Point(106, 93);
            this.cbKyNang.Name = "cbKyNang";
            this.cbKyNang.Size = new System.Drawing.Size(121, 24);
            this.cbKyNang.TabIndex = 0;
            // 
            // cbLopHoc
            // 
            this.cbLopHoc.FormattingEnabled = true;
            this.cbLopHoc.Location = new System.Drawing.Point(106, 144);
            this.cbLopHoc.Name = "cbLopHoc";
            this.cbLopHoc.Size = new System.Drawing.Size(121, 24);
            this.cbLopHoc.TabIndex = 1;
            // 
            // lblHocPhi
            // 
            this.lblHocPhi.AutoSize = true;
            this.lblHocPhi.Location = new System.Drawing.Point(88, 204);
            this.lblHocPhi.Name = "lblHocPhi";
            this.lblHocPhi.Size = new System.Drawing.Size(44, 16);
            this.lblHocPhi.TabIndex = 2;
            this.lblHocPhi.Text = "label1";
            // 
            // btnDangKy
            // 
            this.btnDangKy.Location = new System.Drawing.Point(91, 294);
            this.btnDangKy.Name = "btnDangKy";
            this.btnDangKy.Size = new System.Drawing.Size(75, 23);
            this.btnDangKy.TabIndex = 3;
            this.btnDangKy.Text = "button1";
            this.btnDangKy.UseVisualStyleBackColor = true;
            // 
            // FrmDangKy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnDangKy);
            this.Controls.Add(this.lblHocPhi);
            this.Controls.Add(this.cbLopHoc);
            this.Controls.Add(this.cbKyNang);
            this.Name = "FrmDangKy";
            this.Text = "FrmDangKy";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbKyNang;
        private System.Windows.Forms.ComboBox cbLopHoc;
        private System.Windows.Forms.Label lblHocPhi;
        private System.Windows.Forms.Button btnDangKy;
    }
}