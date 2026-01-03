    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    namespace QuanLyTrungTam
    {
        public partial class FrmHocVien : Form
        {
            public FrmHocVien()
            {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Quản Lý Học Viên";
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10);

            // Header
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.FromArgb(33, 150, 243) }; // Blue
            Label lblTitle = new Label { Text = "DANH SÁCH HỌC VIÊN", Location = new Point(20, 15), AutoSize = true, Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.White };
            pnlHeader.Controls.Add(lblTitle);
            this.Controls.Add(pnlHeader);
        }
        }
    }
