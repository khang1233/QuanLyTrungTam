using QuanLyTrungTam.BUS;
using QuanLyTrungTam.DTO;
using QuanLyTrungTam.Utilities;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyTrungTam
{
    public partial class FrmDangKyAdmin : Form
    {
        private string currentMaHV = "";

        public FrmDangKyAdmin()
        {
            InitializeComponent();
            
            // Shortcuts
            this.KeyPreview = true;
            
            LoadDataHocVien("");
            LoadKyNang();
            
            // Adjust Splitter
            split.Panel1MinSize = 0;
            split.Panel2MinSize = 0;
            split.SplitterDistance = 450; 
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                LoadDataHocVien(txbSearch.Text);
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }

        void LoadDataHocVien(string keyword)
        {
            if (keyword == "Nhập tên hoặc SĐT...") keyword = "";
            
            DataTable dt = HocVienBUS.Instance.GetListHocVien();
            if (!string.IsNullOrEmpty(keyword))
            {
                dt.DefaultView.RowFilter = string.Format("MaHV LIKE '%{0}%' OR HoTen LIKE '%{0}%' OR SDT LIKE '%{0}%'", keyword);
                dt = dt.DefaultView.ToTable();
            }
            dgvHocVien.DataSource = dt;
            if (!string.IsNullOrEmpty(keyword))
            {
                dt.DefaultView.RowFilter = string.Format("MaHV LIKE '%{0}%' OR HoTen LIKE '%{0}%' OR SDT LIKE '%{0}%'", keyword);
                dt = dt.DefaultView.ToTable();
            }
            dgvHocVien.DataSource = dt;
            StyleHocVienGrid();
        }

        private void DgvHocVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow r = dgvHocVien.Rows[e.RowIndex];
                currentMaHV = r.Cells["MaHV"].Value.ToString();
                grpReg.Text = string.Format(" 2. Đăng Ký Cho: {0} ({1})", r.Cells["HoTen"].Value.ToString().ToUpper(), currentMaHV);
                LoadDanhSachDaDangKy();
            }
        }

        void LoadDanhSachDaDangKy()
        {
            dgvDaDangKy.DataSource = TuitionBUS.Instance.GetListDangKy(currentMaHV);

            if (!dgvDaDangKy.Columns.Contains("btnHuy"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Name = "btnHuy"; btn.HeaderText = ""; btn.Text = "Hủy";
                btn.UseColumnTextForButtonValue = true; 
                btn.FlatStyle = FlatStyle.Flat;
                btn.DefaultCellStyle.BackColor = Color.IndianRed; 
                btn.DefaultCellStyle.ForeColor = Color.White;
                dgvDaDangKy.Columns.Add(btn);
            }

            StyleListGrid();
        }

        private void DgvDaDangKy_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvDaDangKy.Columns[e.ColumnIndex].Name == "btnHuy")
            {
                // Security Check
                if (!UserSession.Instance.IsAdmin() && UserSession.Instance.CurrentUser.Quyen != "NhanSu" && UserSession.Instance.CurrentUser.Quyen != "GiaoVien")
                {
                     MessageBox.Show("Bạn không có quyền hủy đăng ký!", "Cảnh báo");
                     return;
                }

                string maLop = dgvDaDangKy.Rows[e.RowIndex].Cells["MaLop"].Value.ToString();

                if (TuitionBUS.Instance.HuyDangKy(currentMaHV, maLop))
                {
                    MessageBox.Show("Đã hủy thành công.");
                    LoadDanhSachDaDangKy();
                    LoadDataHocVien(txbSearch.Text); 
                }
            }
        }

        void LoadKyNang()
        {
            cbKyNang.DataSource = KyNangBUS.Instance.GetListKyNang();
            cbKyNang.DisplayMember = "TenKyNang"; cbKyNang.ValueMember = "MaKyNang";
        }

        private void CbKyNang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbKyNang.SelectedValue != null && cbKyNang.SelectedItem is DataRowView row)
            {
                decimal hp = row["HocPhi"] != DBNull.Value ? Convert.ToDecimal(row["HocPhi"]) : 0;
                lblHocPhi.Text = hp.ToString("N0") + " VNĐ"; lblHocPhi.Tag = hp;

                string maKN = row["MaKyNang"].ToString();
                DataTable dtLop = LopHocBUS.Instance.GetListLopByKyNang(maKN);
                DataView dvLop = new DataView(dtLop);
                dvLop.RowFilter = "TrangThai = 'Sắp mở' OR TrangThai = 'Đang học'";
                cbLopHoc.DataSource = dvLop; cbLopHoc.DisplayMember = "TenLop"; cbLopHoc.ValueMember = "MaLop";
            }
        }

        private void BtnDangKy_Click(object sender, EventArgs e)
        {
            // Security Check
            if (!UserSession.Instance.IsAdmin() && UserSession.Instance.CurrentUser.Quyen != "NhanSu" && UserSession.Instance.CurrentUser.Quyen != "GiaoVien")
            {
                 MessageBox.Show("Bạn không có quyền đăng ký khóa học!", "Cảnh báo");
                 return;
            }

            if (string.IsNullOrEmpty(currentMaHV)) { MessageBox.Show("Vui lòng chọn học viên trước!"); return; }
            if (cbLopHoc.SelectedValue == null) { MessageBox.Show("Vui lòng chọn lớp học!"); return; }
            if (lblHocPhi.Tag == null) return;

            decimal hp = 0; decimal.TryParse(lblHocPhi.Tag.ToString(), out hp);

            if (TuitionBUS.Instance.DangKyLop(currentMaHV, cbLopHoc.SelectedValue.ToString(), hp))
            {
                MessageBox.Show("Đăng ký thành công!");
                LoadDanhSachDaDangKy();
                LoadDataHocVien(txbSearch.Text);
            }
            else
            {
                MessageBox.Show("Học viên đã có trong lớp này rồi!");
            }
        }

        private void TxbSearch_TextChanged(object sender, EventArgs e)
        {
             LoadDataHocVien(txbSearch.Text);
        }

        // --- Styles ---
        private void StyleHocVienGrid()
        {
             // Use helper for standardization
             GridViewHelper.StandardizeGrid(dgvHocVien, new System.Collections.Generic.List<string> { 
                 "NgaySinh", "Email", "DiaChi", "NgayGiaNhap", "MaLop", "MaKyNang" 
             });
        }

        private void StyleListGrid()
        {
             GridViewHelper.StandardizeGrid(dgvDaDangKy, new System.Collections.Generic.List<string> { 
                 "MaLop", "NgayDangKy", "MaHV" 
             });
        }

        private void StyleGrid(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false;
            dgv.ColumnHeadersHeight = 40;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        }
        
        public void AutoSelectStudent(string maHV)
        {
            txbSearch.Text = maHV;
            txbSearch.ForeColor = Color.Black;
        }
    }
}