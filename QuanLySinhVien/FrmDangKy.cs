using QuanLyTrungTam.BUS;
using QuanLyTrungTam.DAO;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyTrungTam
{
    public partial class FrmDangKy : Form
    {
        private string currentMaHV;

        public FrmDangKy(string maHV)
        {
            InitializeComponent();
            this.currentMaHV = maHV;
            
            // Event Handlers
            btnDangKy.Click += btnDangKy_Click;
            cbKyNang.SelectedIndexChanged += CbKyNang_SelectedIndexChanged;
            
            this.Load += (s, e) => LoadKyNang();
        }

        void LoadKyNang()
        {
            // [REFACTOR] Use KyNangBUS
            cbKyNang.DataSource = KyNangBUS.Instance.GetListKyNang();
            cbKyNang.DisplayMember = "TenKyNang";
            cbKyNang.ValueMember = "MaKyNang";

            // Trigger lần đầu
            if (cbKyNang.Items.Count > 0) CbKyNang_SelectedIndexChanged(null, null);
        }

        private void CbKyNang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbKyNang.SelectedValue != null)
            {
                DataRowView row = cbKyNang.SelectedItem as DataRowView;
                string maKN = "";

                if (row != null)
                {
                    maKN = row["MaKyNang"].ToString();
                    decimal hocPhi = row["HocPhi"] != DBNull.Value ? Convert.ToDecimal(row["HocPhi"]) : 0;
                    lblHocPhi.Text = hocPhi.ToString("N0") + " VNĐ";
                    lblHocPhi.Tag = hocPhi;
                }
                else if (cbKyNang.SelectedValue is string)
                {
                    maKN = cbKyNang.SelectedValue.ToString();
                }

                if (!string.IsNullOrEmpty(maKN))
                {
                    // Load danh sách lớp theo kỹ năng
                    // [REFACTOR] Use LopHocBUS
                    cbLopHoc.DataSource = LopHocBUS.Instance.GetListLopByKyNang(maKN);
                    cbLopHoc.DisplayMember = "TenLop";
                    cbLopHoc.ValueMember = "MaLop";
                }
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (cbKyNang.SelectedValue == null) { MessageBox.Show("Vui lòng chọn môn học!"); return; }
            if (cbLopHoc.SelectedValue == null) { MessageBox.Show("Vui lòng chọn lớp học!"); return; }

            // Check Status
            DataRowView rowKN = cbKyNang.SelectedItem as DataRowView;
            if (rowKN != null && rowKN.DataView.Table.Columns.Contains("TrangThai"))
            {
                string trangThai = rowKN["TrangThai"].ToString().ToLower();
                if (trangThai == "0" || trangThai == "false" || trangThai.Contains("ngưng"))
                {
                    MessageBox.Show("Môn học này đang NGƯNG HOẠT ĐỘNG.\nKhông thể đăng ký!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string maLop = cbLopHoc.SelectedValue.ToString();
            decimal hocPhi = (lblHocPhi.Tag != null) ? Convert.ToDecimal(lblHocPhi.Tag) : 0;

            // [REFACTOR] Use TuitionBUS
            if (TuitionBUS.Instance.DangKyLop(currentMaHV, maLop, hocPhi))
            {
                // Update Status (Example)
                HocVienDAO.Instance.CapNhatTrangThaiHocVien(currentMaHV, "Đang học");
                
                MessageBox.Show("Đăng ký thành công!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Học viên đã đăng ký lớp này rồi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}