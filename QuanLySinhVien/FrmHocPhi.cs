using QuanLyTrungTam.BUS;
using QuanLyTrungTam.DAO;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyTrungTam
{
    public partial class FrmHocPhi : Form
    {
        private string currentMaHV = "";
        
        public FrmHocPhi()
        {
            InitializeComponent();
            
            // Init Data
            LoadData();

            // Event Handlers
            dgvSinhVien.CellClick += DgvSinhVien_CellClick;
            btnXacNhanDong.Click += BtnXacNhanDong_Click;
        }

        private void LoadData()
        {
            // Load list of students
            dgvSinhVien.DataSource = HocVienBUS.Instance.GetListHocVien();
            
            // Format Grid
            if (dgvSinhVien.Columns.Contains("MaHV")) dgvSinhVien.Columns["MaHV"].HeaderText = "Mã HV";
            if (dgvSinhVien.Columns.Contains("HoTen")) dgvSinhVien.Columns["HoTen"].HeaderText = "Họ Tên";
            if (dgvSinhVien.Columns.Contains("NgaySinh")) dgvSinhVien.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            
            dgvSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSinhVien.ReadOnly = true;
            dgvSinhVien.AllowUserToAddRows = false;
        }

        private void DgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSinhVien.Rows[e.RowIndex];
                currentMaHV = row.Cells["MaHV"].Value.ToString();
                string tenSV = row.Cells["HoTen"].Value.ToString();

                lblTenSV.Text = "Sinh viên: " + tenSV;

                // Calculate Finance Logic
                // Note: Using TuitionBUS or DAO directly as per existing patterns
                decimal tongNo = TuitionDAO.Instance.GetTongNo(currentMaHV);
                decimal daDong = TuitionDAO.Instance.GetDaDong(currentMaHV);
                decimal conNo = tongNo - daDong;

                lblTongHP.Text = string.Format("Tổng HP: {0:N0} VNĐ", tongNo);
                lblDaDong.Text = string.Format("Đã đóng: {0:N0} VNĐ", daDong);
                lblConNo.Text = string.Format("Còn nợ: {0:N0} VNĐ", conNo);

                if (conNo > 0) lblConNo.ForeColor = Color.Red;
                else lblConNo.ForeColor = Color.Green;
            }
        }

        private void BtnXacNhanDong_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaHV))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần đóng học phí!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal soTien;
            if (!decimal.TryParse(txbSoTienDong.Text, out soTien) || soTien <= 0)
            {
                MessageBox.Show("Số tiền nhập vào không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Perform Payment
            if (TuitionDAO.Instance.InsertThanhToan(currentMaHV, soTien, "Tiền mặt"))
            {
                MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Refresh Info
                // Re-trigger cell click to update labels
                if (dgvSinhVien.CurrentRow != null)
                {
                    DgvSinhVien_CellClick(null, new DataGridViewCellEventArgs(0, dgvSinhVien.CurrentRow.Index));
                }
                
                txbSoTienDong.Clear();
            }
            else
            {
                MessageBox.Show("Thanh toán thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}