namespace QuanLyTrungTam
{
    partial class FrmDashboard
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblNumSV = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblNumLop = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblDoanhThu = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelWelcome = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel1.Controls.Add(this.lblNumSV);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(40, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(220, 120);
            this.panel1.TabIndex = 2;
            // 
            // lblNumSV
            // 
            this.lblNumSV.AutoSize = true;
            this.lblNumSV.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold);
            this.lblNumSV.ForeColor = System.Drawing.Color.White;
            this.lblNumSV.Location = new System.Drawing.Point(20, 50);
            this.lblNumSV.Name = "lblNumSV";
            this.lblNumSV.Size = new System.Drawing.Size(42, 46);
            this.lblNumSV.TabIndex = 0;
            this.lblNumSV.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "TỔNG SINH VIÊN";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Orange;
            this.panel2.Controls.Add(this.lblNumLop);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(300, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(220, 120);
            this.panel2.TabIndex = 1;
            // 
            // lblNumLop
            // 
            this.lblNumLop.AutoSize = true;
            this.lblNumLop.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold);
            this.lblNumLop.ForeColor = System.Drawing.Color.White;
            this.lblNumLop.Location = new System.Drawing.Point(20, 50);
            this.lblNumLop.Name = "lblNumLop";
            this.lblNumLop.Size = new System.Drawing.Size(42, 46);
            this.lblNumLop.TabIndex = 0;
            this.lblNumLop.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(20, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 24);
            this.label3.TabIndex = 1;
            this.label3.Text = "LỚP HỌC";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.SeaGreen;
            this.panel3.Controls.Add(this.lblDoanhThu);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Location = new System.Drawing.Point(560, 100);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(250, 120);
            this.panel3.TabIndex = 0;
            // 
            // lblDoanhThu
            // 
            this.lblDoanhThu.AutoSize = true;
            this.lblDoanhThu.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.lblDoanhThu.ForeColor = System.Drawing.Color.White;
            this.lblDoanhThu.Location = new System.Drawing.Point(10, 55);
            this.lblDoanhThu.Name = "lblDoanhThu";
            this.lblDoanhThu.Size = new System.Drawing.Size(104, 35);
            this.lblDoanhThu.TabIndex = 0;
            this.lblDoanhThu.Text = "0 VNĐ";
       
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(20, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(174, 24);
            this.label5.TabIndex = 1;
            this.label5.Text = "ĐÃ THU HỌC PHÍ";
            // 
            // labelWelcome
            // 
            this.labelWelcome.AutoSize = true;
            this.labelWelcome.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.labelWelcome.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.labelWelcome.Location = new System.Drawing.Point(30, 30);
            this.labelWelcome.Name = "labelWelcome";
            this.labelWelcome.Size = new System.Drawing.Size(781, 40);
            this.labelWelcome.TabIndex = 3;
            this.labelWelcome.Text = "Chào mừng đến với Hệ thống Quản lý Sinh viên";
           // this.labelWelcome.Click += new System.EventHandler(this.labelWelcome_Click);
            // 
            // FrmDashboard
            // 
            this.ClientSize = new System.Drawing.Size(850, 500);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelWelcome);
            this.Name = "FrmDashboard";
            this.Text = "Trang Chủ";
         //   this.Load += new System.EventHandler(this.FrmDashboard_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblNumSV;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblNumLop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblDoanhThu;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelWelcome;
    }
}