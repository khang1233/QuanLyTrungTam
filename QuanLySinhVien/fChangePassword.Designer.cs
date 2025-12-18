namespace QuanLyTrungTam
{
    partial class fChangePassword
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txbUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txbPassOld = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txbPassNew = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txbReEnter = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 22);
            this.label1.TabIndex = 9;
            this.label1.Text = "Tên đăng nhập:";
            // 
            // txbUser
            // 
            this.txbUser.Location = new System.Drawing.Point(150, 25);
            this.txbUser.Name = "txbUser";
            this.txbUser.ReadOnly = true;
            this.txbUser.Size = new System.Drawing.Size(200, 28);
            this.txbUser.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 22);
            this.label2.TabIndex = 7;
            this.label2.Text = "Mật khẩu cũ:";
            // 
            // txbPassOld
            // 
            this.txbPassOld.Location = new System.Drawing.Point(150, 65);
            this.txbPassOld.Name = "txbPassOld";
            this.txbPassOld.Size = new System.Drawing.Size(200, 28);
            this.txbPassOld.TabIndex = 6;
            this.txbPassOld.UseSystemPasswordChar = true;
            this.txbPassOld.TextChanged += new System.EventHandler(this.txbPassOld_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 22);
            this.label3.TabIndex = 5;
            this.label3.Text = "Mật khẩu mới:";
            // 
            // txbPassNew
            // 
            this.txbPassNew.Location = new System.Drawing.Point(150, 105);
            this.txbPassNew.Name = "txbPassNew";
            this.txbPassNew.Size = new System.Drawing.Size(200, 28);
            this.txbPassNew.TabIndex = 4;
            this.txbPassNew.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 22);
            this.label4.TabIndex = 3;
            this.label4.Text = "Nhập lại:";
            // 
            // txbReEnter
            // 
            this.txbReEnter.Location = new System.Drawing.Point(150, 145);
            this.txbReEnter.Name = "txbReEnter";
            this.txbReEnter.Size = new System.Drawing.Size(200, 28);
            this.txbReEnter.TabIndex = 2;
            this.txbReEnter.UseSystemPasswordChar = true;
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.ForestGreen;
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(150, 190);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(90, 35);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "CẬP NHẬT";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Red;
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(260, 190);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 35);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "THOÁT";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // fChangePassword
            // 
            this.ClientSize = new System.Drawing.Size(400, 250);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txbReEnter);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txbPassNew);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txbPassOld);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txbUser);
            this.Controls.Add(this.label1);
            this.Name = "fChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đổi Mật Khẩu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label label1, label2, label3, label4;
        private System.Windows.Forms.TextBox txbUser, txbPassOld, txbPassNew, txbReEnter;
        private System.Windows.Forms.Button btnUpdate, btnExit;
    }
}