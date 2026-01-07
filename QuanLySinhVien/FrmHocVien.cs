using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyTrungTam.BUS;

namespace QuanLyTrungTam
{
    public partial class FrmHocVien : Form
    {
        public FrmHocVien()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try 
            {
                dgvHocVien.DataSource = HocVienBUS.Instance.GetListHocVien();
            }
            catch {}
        }
    }
}
