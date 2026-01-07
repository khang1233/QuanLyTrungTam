using QuanLyTrungTam.BUS;
using QuanLyTrungTam.DTO;
using QuanLyTrungTam.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyTrungTam.DAO;

namespace QuanLyTrungTam
{
    public partial class FrmLop : Form
    {
        public FrmLop()
        {
            InitializeComponent();
            SetupCustomUI();
            LoadData();
        }

        // 2. THIẾT KẾ GIAO DIỆN
        private void SetupCustomUI()
        {
            // Cấu hình phím tắt
            this.KeyPreview = true;
            
            // Apply custom styles that designer might miss
            StyleGrid(dgvMain);
            SetPlaceholder(txbSearch, "Tìm kiếm...");
            
            LoadData();

            // Setup Default Values
            if (cbThu.Items.Count > 0) cbThu.SelectedIndex = 0;
            if (cbCaHoc.Items.Count > 0) cbCaHoc.SelectedIndex = 0;
            if (cbTrangThai.Items.Count > 0) cbTrangThai.SelectedIndex = 0;
        }

        // =========================================================
        // 1. EVENT HANDLERS (LINKED TO DESIGNER)
        // =========================================================
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // Kiểm tra chọn môn học
            if (cbMonHoc.SelectedValue == null || string.IsNullOrEmpty(cbMonHoc.Text))
            {
                MessageBox.Show("Vui lòng chọn Môn Học trước!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txbTenLop.Text)) { MessageBox.Show("Chưa nhập tên lớp!", "Thông báo"); return; }

            // [FIX LỖI 2]: KIỂM TRA CHUYÊN NGÀNH GIÁO VIÊN
            if (cbGiaoVien.SelectedValue != null)
            {
                DataRowView rowMon = cbMonHoc.SelectedItem as DataRowView;
                string chuyenNganhMon = "";
                if (rowMon != null && rowMon.DataView.Table.Columns.Contains("ChuyenNganh"))
                {
                    chuyenNganhMon = rowMon["ChuyenNganh"].ToString();
                }

                string maGV = GetVal(cbGiaoVien);
                string chuyenNganhGV = NhanVienDAO.Instance.GetChuyenNganh(maGV);

                if (!string.IsNullOrEmpty(chuyenNganhMon) && !string.IsNullOrEmpty(chuyenNganhGV))
                {
                    // So sánh không phân biệt hoa thường
                    if (!chuyenNganhMon.Equals(chuyenNganhGV, StringComparison.OrdinalIgnoreCase))
                    {
                        DialogResult confirm = MessageBox.Show(
                            $"Cảnh báo chuyên môn:\nGiáo viên này có chuyên ngành '{chuyenNganhGV}', khác với môn học '{chuyenNganhMon}'.\n\nBạn có chắc chắn muốn tiếp tục mở lớp không?",
                            "Cảnh báo lệch chuyên ngành",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning
                        );

                        if (confirm == DialogResult.No) return; // Hủy bỏ thao tác
                    }
                }
            }

            // --- KIỂM TRA LOGIC TRÙNG LỊCH ---
            string caHoc = cbCaHoc.Text;
            string lichHoc = cbThu.Text;
            string maPhong = GetVal(cbPhongHoc);
            string maGVCheck = GetVal(cbGiaoVien);
            string maTGCheck = GetVal(cbTroGiang);

            string conflictMsg = LopHocDAO.Instance.GetConflictMessage(maPhong, maGVCheck, maTGCheck, lichHoc, caHoc, "");

            if (conflictMsg != null)
            {
                MessageBox.Show(conflictMsg, "Cảnh báo trùng lịch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                bool result = LopHocBUS.Instance.InsertLopFull(txbMaLop.Text, txbTenLop.Text,
                    GetVal(cbMonHoc), maGVCheck, maTGCheck, maPhong,
                    lichHoc, caHoc, (int)nmSiSo.Value, DateTime.Now);

                if (result)
                {
                    MessageBox.Show(Constants.MSG_ADD_SUCCESS, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadListLopHoc();
                    ResetForm();
                }
                else MessageBox.Show("Thêm thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi Quyền Hạn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txbMaLop.Text)) return;

            string conflictMsg = LopHocDAO.Instance.GetConflictMessage(GetVal(cbPhongHoc), GetVal(cbGiaoVien), GetVal(cbTroGiang), cbThu.Text, cbCaHoc.Text, txbMaLop.Text);
            if (conflictMsg != null) { MessageBox.Show(conflictMsg, "Cảnh báo trùng lịch", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            try
            {
                bool result = LopHocBUS.Instance.UpdateLopFull(txbMaLop.Text, txbTenLop.Text,
                    GetVal(cbGiaoVien), GetVal(cbTroGiang), GetVal(cbPhongHoc),
                    cbThu.Text, cbCaHoc.Text, (int)nmSiSo.Value, cbTrangThai.Text);

                if (result) { MessageBox.Show(Constants.MSG_UPDATE_SUCCESS); LoadListLopHoc(); }
                else MessageBox.Show("Lỗi cập nhật!");
            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message, "Lỗi Quyền Hạn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txbMaLop.Text)) return;
            if (MessageBox.Show("Xóa lớp sẽ xóa hết đăng ký. Tiếp tục?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    if (LopHocBUS.Instance.DeleteLop(txbMaLop.Text)) { MessageBox.Show(Constants.MSG_DELETE_SUCCESS); LoadListLopHoc(); ResetForm(); }
                    else MessageBox.Show("Lỗi xóa lớp.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi Quyền Hạn", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e) => ResetForm();

        private void BtnSearch_Click(object sender, EventArgs e) => FilterData(txbSearch.Text);

        private void TxbSearch_TextChanged(object sender, EventArgs e) => FilterData(txbSearch.Text);

        // =========================================================
        // 2. SHORTCUTS
        // =========================================================
        protected override void OnKeyDown(KeyEventArgs e)
        {
            var sm = ShortcutManager.Instance;
            if (sm.IsMatch("Refresh", e.KeyData)) { ResetForm(); e.Handled = true; return; }
            if (sm.IsMatch("Save", e.KeyData)) { BtnAdd_Click(null, null); e.Handled = true; return; }
            if (sm.IsMatch("Update", e.KeyData)) { BtnEdit_Click(null, null); e.Handled = true; return; }
            if (sm.IsMatch("Delete", e.KeyData)) { BtnDel_Click(null, null); e.Handled = true; return; }
            
            base.OnKeyDown(e);
        }

        // =========================================================
        // 3. LOGIC & HELPERS
        // =========================================================

        void LoadData()
        {
            try
            {
                // Init Events
                if (cbMonHoc != null) 
                {
                    cbMonHoc.SelectedIndexChanged -= CbMonHoc_SelectedIndexChanged;
                    cbMonHoc.SelectedIndexChanged += CbMonHoc_SelectedIndexChanged;
                }
                if (dgvMain != null)
                {
                    dgvMain.CellClick -= DgvMain_CellClick;
                    dgvMain.CellClick += DgvMain_CellClick;
                    dgvMain.CellFormatting -= DgvMain_CellFormatting;
                    dgvMain.CellFormatting += DgvMain_CellFormatting;
                    dgvMain.DataError -= DgvMain_DataError;
                    dgvMain.DataError += DgvMain_DataError;
                }

                // Load Data Sources
                cbMonHoc.DataSource = KyNangBUS.Instance.GetListKyNangActive();
                cbMonHoc.DisplayMember = "TenKyNang"; cbMonHoc.ValueMember = "MaKyNang";

                cbPhongHoc.DataSource = PhongHocBUS.Instance.GetListPhong();
                cbPhongHoc.DisplayMember = "TenPhong"; cbPhongHoc.ValueMember = "MaPhong";

                cbGiaoVien.DataSource = NhanVienBUS.Instance.GetListGiaoVien();
                cbGiaoVien.DisplayMember = "HoTen"; cbGiaoVien.ValueMember = "MaNS";

                cbTroGiang.DataSource = NhanVienBUS.Instance.GetListTroGiang();
                cbTroGiang.DisplayMember = "HoTen"; cbTroGiang.ValueMember = "MaNS";

                LoadListLopHoc();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.MSG_LOAD_DATA_ERROR + ex.Message);
            }
        }

        private void DgvMain_DataError(object sender, DataGridViewDataErrorEventArgs e) { e.ThrowException = false; }

        private void CbMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cbGiaoVien.DataSource = null;
                if (cbMonHoc.SelectedValue == null) return;

                DataRowView rowMon = cbMonHoc.SelectedItem as DataRowView;
                string maMon = "";
                string chuyenNganhMon = "";

                if (rowMon != null)
                {
                    maMon = rowMon["MaKyNang"].ToString();
                    if (rowMon.DataView.Table.Columns.Contains("ChuyenNganh") && rowMon["ChuyenNganh"] != DBNull.Value)
                    {
                        chuyenNganhMon = rowMon["ChuyenNganh"].ToString().Trim();
                    }
                }
                else
                {
                    maMon = cbMonHoc.SelectedValue.ToString();
                }

                txbMaLop.Text = LopHocBUS.Instance.GetNewMaLop(maMon);
                cbGiaoVien.DataSource = NhanVienBUS.Instance.GetListGiaoVien(chuyenNganhMon);
                cbGiaoVien.DisplayMember = "HoTen"; cbGiaoVien.ValueMember = "MaNS";
                if (cbGiaoVien.Items.Count > 0) cbGiaoVien.SelectedIndex = 0;
            }
            catch { }
        }

        private void LoadListLopHoc()
        {
            DataTable dataLop = LopHocBUS.Instance.GetAllLop();
            if (!dataLop.Columns.Contains("NgayKetThuc")) dataLop.Columns.Add("NgayKetThuc", typeof(DateTime));

            foreach (DataRow row in dataLop.Rows)
            {
                try
                {
                    DateTime start = Convert.ToDateTime(row["NgayBatDau"]);
                    string thu = row["Thu"].ToString();
                    int soBuoi = row["SoBuoi"] != DBNull.Value ? Convert.ToInt32(row["SoBuoi"]) : 0;
                    row["NgayKetThuc"] = LopHocBUS.Instance.CalculateEndDate(start, thu, soBuoi);
                }
                catch { row["NgayKetThuc"] = row["NgayBatDau"]; }
            }

            dgvMain.DataSource = dataLop;
            
            // USE HELPER
            GridViewHelper.StandardizeGrid(dgvMain, new System.Collections.Generic.List<string> { 
                "MaKyNang", "MaGiaoVien", "MaTroGiang", "MaPhong", "SoBuoi", "IsDeleted" 
            });
        }

        private void FilterData(string keyword)
        {
            if (dgvMain.DataSource == null) return;
            DataTable dt = dgvMain.DataSource as DataTable;
            if (string.IsNullOrWhiteSpace(keyword) || keyword == "Tìm kiếm...") dt.DefaultView.RowFilter = "";
            else dt.DefaultView.RowFilter = string.Format("MaLop LIKE '%{0}%' OR TenLop LIKE '%{0}%'", keyword);
        }

        private void DgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                DataGridViewRow r = dgvMain.Rows[e.RowIndex];
                txbMaLop.Text = r.Cells["MaLop"].Value?.ToString();
                txbTenLop.Text = r.Cells["TenLop"].Value?.ToString();
                SetCombo(cbMonHoc, r.Cells["MaKyNang"].Value);
                SetCombo(cbGiaoVien, r.Cells["MaGiaoVien"].Value);
                SetCombo(cbTroGiang, r.Cells["MaTroGiang"].Value);
                SetCombo(cbPhongHoc, r.Cells["MaPhong"].Value);

                string thu = r.Cells["Thu"].Value?.ToString();
                if (cbThu.Items.Contains(thu)) cbThu.SelectedItem = thu; else cbThu.Text = thu;

                string ca = r.Cells["CaHoc"].Value?.ToString();
                if (cbCaHoc.Items.Contains(ca)) cbCaHoc.SelectedItem = ca; else cbCaHoc.Text = ca;

                nmSiSo.Value = r.Cells["SiSoToiDa"].Value != DBNull.Value ? Convert.ToInt32(r.Cells["SiSoToiDa"].Value) : 20;
                cbTrangThai.Text = r.Cells["TrangThai"].Value?.ToString();
            }
            catch { }
        }

        private void ResetForm()
        {
            txbMaLop.Clear(); txbTenLop.Clear(); 
            cbThu.SelectedIndex = 0; cbCaHoc.SelectedIndex = 0;
            if (cbMonHoc.Items.Count > 0) cbMonHoc.SelectedIndex = 0;
            SetPlaceholder(txbSearch, "Tìm kiếm..."); FilterData("");
        }

        private string GetVal(ComboBox cb) { return cb.SelectedValue != null ? cb.SelectedValue.ToString() : ""; }
        private void SetCombo(ComboBox cb, object val) { if (val != DBNull.Value && val != null) cb.SelectedValue = val; }

        private void DgvMain_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvMain.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                string s = e.Value.ToString();
                e.CellStyle.ForeColor = s.Contains("Đang") ? Color.Green : (s.Contains("kết thúc") ? Color.Red : Color.Blue);
            }
        }

        private void StyleGrid(DataGridView dgv)
        {
             dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false; 
            dgv.ColumnHeadersHeight = 45; 
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243); 
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold); 
        }
        

        private void SetPlaceholder(TextBox txt, string holder)
        {
            txt.Text = holder; txt.ForeColor = Color.Gray;
            txt.Enter += (s, e) => { if (txt.Text == holder) { txt.Text = ""; txt.ForeColor = Color.Black; } };
            txt.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txt.Text)) { txt.Text = holder; txt.ForeColor = Color.Gray; } };
        }
    }
}