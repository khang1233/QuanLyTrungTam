using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using QuanLyTrungTam.BUS;
using QuanLyTrungTam.DTO;
using QuanLyTrungTam.Utilities;

namespace QuanLyTrungTam
{
    public partial class FrmQuanLyNhanSu : Form
    {
        // Biến logic
        private string currentMaNS = "";

        public FrmQuanLyNhanSu()
        {
            InitializeComponent();
            
            // Cấu hình phím tắt
            this.KeyPreview = true;

            // Apply grid styling overrides if needed
            ui_dgvNhanSu.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            ui_dgvNhanSu.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            
            LoadComboBoxData(); 
            LoadData();   
            
            // Mặc định chọn loại nhân sự đầu tiên
            if (ui_cbLoaiNS.Items.Count > 0) ui_cbLoaiNS.SelectedIndex = 0;
        }

        // =========================================================================
        // 1. HỆ THỐNG PHÍM TẮT (CUSTOM SHORTCUTS)
        // =========================================================================
        protected override void OnKeyDown(KeyEventArgs e)
        {
            var sm = ShortcutManager.Instance;

            // Refresh
            if (sm.IsMatch("Refresh", e.KeyData)) { BtnLamMoi_Click(null, null); e.Handled = true; return; }
            // Save
            if (sm.IsMatch("Save", e.KeyData)) { BtnThem_Click(null, null); e.Handled = true; return; }
            // Update
            if (sm.IsMatch("Update", e.KeyData)) { BtnSua_Click(null, null); e.Handled = true; return; }
            // Delete
            if (sm.IsMatch("Delete", e.KeyData)) { BtnXoa_Click(null, null); e.Handled = true; return; }
            // Grant Account
            if (sm.IsMatch("GrantAccount", e.KeyData))
            {
                if (e.KeyData == (Keys.Control | Keys.C) && ActiveControl is TextBox txt && txt.SelectionLength > 0)
                {
                    base.OnKeyDown(e);
                    return;
                }
                BtnCapTK_Click(null, null); 
                e.Handled = true; 
                return;
            }

            base.OnKeyDown(e);
        }

        // =========================================================
        // 2. LOGIC LOAD DATA & XỬ LÝ
        // =========================================================

        void LoadData()
        {
            ui_dgvNhanSu.DataSource = NhanVienBUS.Instance.GetListNhanVien();

            // Gắn sự kiện Click để binding dữ liệu
            ui_dgvNhanSu.CellClick -= DgvNhanSu_CellClick;
            ui_dgvNhanSu.CellClick += DgvNhanSu_CellClick;

            // Gắn sự kiện Tô màu trạng thái
            ui_dgvNhanSu.CellFormatting -= DgvNhanSu_CellFormatting;
            ui_dgvNhanSu.CellFormatting += DgvNhanSu_CellFormatting;

            // USE HELPER
            GridViewHelper.StandardizeGrid(ui_dgvNhanSu);
        }

        // Hàm tô màu chữ trên GridView
        private void DgvNhanSu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (ui_dgvNhanSu.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                string status = e.Value.ToString();

                if (status == "Đang giảng dạy")
                {
                    e.CellStyle.ForeColor = Color.Green;
                    e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                }
                else if (status == "Đang trống lớp")
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Italic);
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Blue; // Cho nhân viên văn phòng
                }
            }
        }

        void LoadComboBoxData()
        {
            try
            {
                List<string> listCN = KyNangBUS.Instance.GetListChuyenNganh();
                ui_cbChuyenNganh.Items.Clear();
                foreach (string cn in listCN)
                {
                    ui_cbChuyenNganh.Items.Add(cn);
                }
                if (ui_cbChuyenNganh.Items.Count > 0) ui_cbChuyenNganh.SelectedIndex = 0;
            }
            catch { }
        }

        // Logic ẩn/hiện ComboBox Chuyên Ngành
        private void Ui_cbLoaiNS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ui_cbLoaiNS.Text == "Giáo viên")
            {
                ui_cbChuyenNganh.Enabled = true;
                ui_cbChuyenNganh.Visible = true;
                ui_lblChuyenNganh.Visible = true;
            }
            else
            {
                ui_cbChuyenNganh.Enabled = false;
                ui_cbChuyenNganh.SelectedIndex = -1;
                ui_cbChuyenNganh.Visible = false;
                ui_lblChuyenNganh.Visible = false;
            }
        }

        private void DgvNhanSu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                DataGridViewRow r = ui_dgvNhanSu.Rows[e.RowIndex];
                currentMaNS = r.Cells["MaNS"].Value.ToString();

                ui_txbMa.Text = currentMaNS;
                ui_txbTen.Text = r.Cells["HoTen"].Value.ToString();
                ui_txbSDT.Text = r.Cells["SDT"].Value.ToString();
                ui_txbEmail.Text = r.Cells["Email"].Value.ToString();

                ui_cbLoaiNS.Text = r.Cells["LoaiNS"].Value.ToString();

                if (r.Cells["NgaySinh"].Value != DBNull.Value)
                    ui_dtpNgaySinh.Value = Convert.ToDateTime(r.Cells["NgaySinh"].Value);

                if (ui_dgvNhanSu.Columns.Contains("ChuyenNganh") && r.Cells["ChuyenNganh"].Value != DBNull.Value)
                {
                    string cn = r.Cells["ChuyenNganh"].Value.ToString();
                    if (!string.IsNullOrEmpty(cn)) ui_cbChuyenNganh.Text = cn;
                }
            }
            catch { }
        }

        private void Ui_txbSearch_TextChanged(object sender, EventArgs e)
        {
            string key = ui_txbSearch.Text;
            DataTable dt = ui_dgvNhanSu.DataSource as DataTable;
            if (dt != null) dt.DefaultView.RowFilter = $"HoTen LIKE '%{key}%' OR MaNS LIKE '%{key}%'";
        }

        private void ResetForm()
        {
            currentMaNS = "";
            ui_txbMa.Clear(); ui_txbTen.Clear(); ui_txbSDT.Clear(); ui_txbEmail.Clear();
            ui_cbLoaiNS.SelectedIndex = 0;
            LoadComboBoxData();
            LoadData(); 
            ui_txbTen.Focus();
        }

        // =========================================================
        // 3. CÁC HÀM XỬ LÝ SỰ KIỆN (ACTIONS)
        // =========================================================

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (ui_cbLoaiNS.Text == "Giáo viên" && string.IsNullOrEmpty(ui_cbChuyenNganh.Text))
            {
                MessageBox.Show("Giáo viên bắt buộc phải có Chuyên ngành!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (NhanVienBUS.Instance.InsertNhanSu(ui_txbTen.Text, ui_dtpNgaySinh.Value, ui_txbSDT.Text, ui_txbEmail.Text, ui_cbLoaiNS.Text, ui_cbChuyenNganh.Text))
                {
                    MessageBox.Show("Thêm nhân sự thành công!");
                    LoadData();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại! Vui lòng kiểm tra lại thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi Quyền Hạn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaNS)) return;

            if (ui_cbLoaiNS.Text == "Giáo viên" && string.IsNullOrEmpty(ui_cbChuyenNganh.Text))
            {
                MessageBox.Show("Giáo viên bắt buộc phải có Chuyên ngành!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (NhanVienBUS.Instance.UpdateNhanSu(currentMaNS, ui_txbTen.Text, ui_dtpNgaySinh.Value, ui_txbSDT.Text, ui_txbEmail.Text, ui_cbLoaiNS.Text, ui_cbChuyenNganh.Text))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    LoadData();
                }
                else MessageBox.Show("Lỗi cập nhật!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi Quyền Hạn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaNS)) return;
            if (MessageBox.Show($"Bạn có chắc muốn xóa nhân sự {currentMaNS}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    if (NhanVienBUS.Instance.DeleteNhanVien(currentMaNS))
                    {
                        MessageBox.Show("Đã xóa!");
                        ResetForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi Quyền Hạn", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void BtnCapTK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaNS)) return;

            // Logic cấp quyền: Nhân viên -> Admin, còn lại là user thường
            string quyen = (ui_cbLoaiNS.Text == "Nhân viên") ? "Admin" : (ui_cbLoaiNS.Text == "Trợ giảng" ? "TroGiang" : "GiaoVien");

            try
            {
                // Chỉ Admin mới được cấp tài khoản (thường là vậy, nhưng AccountBUS chưa check auth, nên ta thêm check ở đây hoặc BUS)
                // Tuy nhiên, logic này nằm ở UI. Để chắc chắn, nên thêm check trong AccountBUS.InsertAccount.
                // Tạm thời thêm check tại UI để phản hồi nhanh.
                if (!UserSession.Instance.IsAdmin())
                {
                     MessageBox.Show("Chỉ Quản trị viên mới có quyền cấp tài khoản!", "Lỗi Quyền Hạn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     return;
                }

                if (AccountBUS.Instance.InsertAccount(currentMaNS, "123", quyen, currentMaNS))
                    MessageBox.Show(string.Format("Cấp TK thành công!\nUser: {0}\nPass: 123", currentMaNS));
                else
                    MessageBox.Show("Nhân sự này đã có tài khoản rồi!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}