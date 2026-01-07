using QuanLyTrungTam.BUS;
using QuanLyTrungTam.DTO;
using QuanLyTrungTam.Utilities;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyTrungTam
{
    public partial class FrmTaiChinh : Form
    {
        public FrmTaiChinh()
        {
            InitializeComponent();
            
            // Shortcuts
            this.KeyPreview = true;
            
            // Security Check
            if (!UserSession.Instance.IsAdmin() && UserSession.Instance.CurrentUser.Quyen != "NhanSu")
            {
                MessageBox.Show("Bạn không có quyền truy cập báo cáo tài chính!", "Truy cập bị từ chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.BeginInvoke(new MethodInvoker(Close));
                return;
            }

            // Mặc định load dữ liệu từ đầu tháng đến hiện tại
            dtpFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            LoadData();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                LoadData();
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }

        private void BtnXem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            DateTime from = dtpFrom.Value;
            DateTime to = dtpTo.Value;

            if (from > to)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Lỗi ngày tháng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dt = FinanceBUS.Instance.GetHistoryByDate(from, to);
            dgvHistory.DataSource = dt;
            FormatGrid();

            decimal totalRevenue = FinanceBUS.Instance.GetRevenueByDate(from, to);
            lblTongTien.Text = string.Format("{0:N0} VNĐ", totalRevenue);
        }

        private void FormatGrid()
        {
            if (dgvHistory.Columns.Count == 0) return;
            
            StyleGrid(dgvHistory);

            // Rename Headers
            SetHeader("IdGD", "Mã GD");
            SetHeader("NgayGD", "Ngày Giao Dịch");
            SetHeader("LoaiGD", "Loại");
            SetHeader("SoTien", "Số Tiền");
            SetHeader("NoiDung", "Nội Dung");
            SetHeader("MaDoiTuong", "Đối Tượng");

            if (dgvHistory.Columns.Contains("NgayGD"))
                dgvHistory.Columns["NgayGD"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            if (dgvHistory.Columns.Contains("SoTien"))
            {
                dgvHistory.Columns["SoTien"].DefaultCellStyle.Format = "N0";
                dgvHistory.Columns["SoTien"].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvHistory.Columns["SoTien"].DefaultCellStyle.ForeColor = Color.DarkGreen;
            }

            if (dgvHistory.Columns.Contains("IdGD") && dgvHistory.Columns["IdGD"] != null)
            {
                dgvHistory.Columns["IdGD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvHistory.Columns["IdGD"].Width = 80;
            }
        }

        private void SetHeader(string colName, string text)
        {
            if (dgvHistory.Columns.Contains(colName)) dgvHistory.Columns[colName].HeaderText = text;
        }
        
        private void StyleGrid(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false;
            dgv.ColumnHeadersHeight = 40;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.RowTemplate.Height = 35;
        }
    }
}