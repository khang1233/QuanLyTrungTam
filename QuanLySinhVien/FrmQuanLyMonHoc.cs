using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using QuanLyTrungTam.BUS;
using QuanLyTrungTam.DTO;
using QuanLyTrungTam.Utilities;

namespace QuanLyTrungTam
{
    public partial class FrmQuanLyMonHoc : Form
    {
        // Biến logic
        private string curMaKN = "";

        public FrmQuanLyMonHoc()
        {
            InitializeComponent();
            
            // Cấu hình phím tắt
            this.KeyPreview = true;

            // Setup Placeholder logic
            SetPlaceholder(txbSearch, "Nhập tên môn cần tìm..."); 

            // Apply specific grid styling that designer might not capture perfectly
            dgvMonHoc.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            dgvMonHoc.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            
            try { LoadData(); } catch { }
            
            if (cbHinhThuc.Items.Count > 0) cbHinhThuc.SelectedIndex = 0;
            if (cbTrangThai.Items.Count > 0) cbTrangThai.SelectedIndex = 0;
        }

        // =========================================================================
        // 1. HỆ THỐNG PHÍM TẮT (CUSTOM SHORTCUTS)
        // =========================================================================
        protected override void OnKeyDown(KeyEventArgs e)
        {
            var sm = ShortcutManager.Instance;

            if (sm.IsMatch("Refresh", e.KeyData)) { ResetForm(); e.Handled = true; return; }
            if (sm.IsMatch("Save", e.KeyData)) { BtnThem_Click(null, null); e.Handled = true; return; }
            if (sm.IsMatch("Update", e.KeyData)) { BtnSua_Click(null, null); e.Handled = true; return; }
            if (sm.IsMatch("Delete", e.KeyData)) { BtnXoa_Click(null, null); e.Handled = true; return; }

            base.OnKeyDown(e);
        }

        // =========================================================================
        // 2. LOGIC TÌM KIẾM & XỬ LÝ
        // =========================================================================

        // [MỚI] Hàm lọc dữ liệu trên GridView
        private void FilterData(string keyword)
        {
            if (keyword == "Nhập tên môn cần tìm...") keyword = "";
            DataTable dt = dgvMonHoc.DataSource as DataTable;
            if (dt != null)
            {
                if (string.IsNullOrEmpty(keyword))
                {
                    dt.DefaultView.RowFilter = "";
                }
                else
                {
                    dt.DefaultView.RowFilter = string.Format("MaKyNang LIKE '%{0}%' OR TenKyNang LIKE '%{0}%'", keyword);
                }
            }
        }

        private void UpdateHocPhi(object sender, EventArgs e)
        {
            decimal tong = numSoBuoi.Value * numDonGia.Value;
            lblTongHocPhi.Text = $"TỔNG HỌC PHÍ: {tong:N0} VNĐ";
        }

        void LoadData()
        {
            dgvMonHoc.DataSource = KyNangBUS.Instance.GetListKyNang();

            // Gắn lại sự kiện
            dgvMonHoc.DataError -= DgvMonHoc_DataError;
            dgvMonHoc.DataError += DgvMonHoc_DataError;
            dgvMonHoc.CellClick -= DgvMonHoc_CellClick;
            dgvMonHoc.CellClick += DgvMonHoc_CellClick;
            dgvMonHoc.CellFormatting -= DgvMonHoc_CellFormatting;
            dgvMonHoc.CellFormatting += DgvMonHoc_CellFormatting;

            // Use Helper
            GridViewHelper.StandardizeGrid(dgvMonHoc, new System.Collections.Generic.List<string> { "sobuoifake" });
        }

        private void DgvMonHoc_DataError(object sender, DataGridViewDataErrorEventArgs e) { e.ThrowException = false; }

        private void DgvMonHoc_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvMonHoc.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                string val = e.Value.ToString().ToLower();
                bool isActive = (val == "1" || val == "true" || val == "đang hoạt động");
                e.Value = isActive ? "Đang hoạt động" : "Ngưng hoạt động";
                e.CellStyle.ForeColor = isActive ? Color.Green : Color.Red;
            }
        }

        private void DgvMonHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                DataGridViewRow r = dgvMonHoc.Rows[e.RowIndex];

                curMaKN = r.Cells["MaKyNang"].Value.ToString();

                txbMa.Text = curMaKN;
                txbMa.ReadOnly = true;
                txbMa.BackColor = Color.LightYellow;

                txbTen.Text = r.Cells["TenKyNang"].Value.ToString();
                txbMoTa.Text = r.Cells["MoTa"].Value.ToString();
                cbHinhThuc.Text = r.Cells["HinhThuc"].Value.ToString();

                decimal soBuoi = 1;
                if (r.Cells["SoBuoi"].Value != DBNull.Value) soBuoi = Convert.ToDecimal(r.Cells["SoBuoi"].Value);
                if (soBuoi < 1) soBuoi = 1;
                if (soBuoi > numSoBuoi.Maximum) numSoBuoi.Maximum = soBuoi;
                numSoBuoi.Value = soBuoi;

                if (r.Cells["HocPhi"].Value != DBNull.Value)
                {
                    decimal tongHocPhi = Convert.ToDecimal(r.Cells["HocPhi"].Value);
                    decimal donGia = (soBuoi > 0) ? (tongHocPhi / soBuoi) : 0;
                    if (donGia > numDonGia.Maximum) numDonGia.Maximum = donGia * 2;
                    numDonGia.Value = donGia;
                }

                string statusStr = r.Cells["TrangThai"].Value.ToString().ToLower();
                cbTrangThai.SelectedIndex = (statusStr == "1" || statusStr == "true" || statusStr == "đang hoạt động") ? 0 : 1;
            }
            catch { }
        }

        // --- BUTTON EVENTS ---

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbMa.Text)) { MessageBox.Show("Vui lòng nhập Mã Kỹ Năng!"); txbMa.Focus(); return; }
            if (string.IsNullOrWhiteSpace(txbTen.Text)) { MessageBox.Show("Vui lòng nhập Tên Kỹ Năng!"); txbTen.Focus(); return; }

            string status = cbTrangThai.SelectedIndex == 0 ? "1" : "0";

            try
            {
                if (KyNangBUS.Instance.InsertKyNang(
                    txbMa.Text.Trim().ToUpper(),
                    txbTen.Text,
                    cbHinhThuc.Text,
                    txbMoTa.Text,
                    (int)numSoBuoi.Value,
                    numDonGia.Value,
                    status))
                {
                    MessageBox.Show("Thêm môn thành công!");
                    LoadData(); ResetForm();
                }
                else MessageBox.Show("Lỗi: Mã kỹ năng có thể đã tồn tại!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi Quyền Hạn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(curMaKN)) { MessageBox.Show("Chọn môn cần sửa!"); return; }
            string status = cbTrangThai.SelectedIndex == 0 ? "1" : "0";

            try
            {
                if (KyNangBUS.Instance.UpdateKyNang(
                    curMaKN,
                    txbTen.Text,
                    cbHinhThuc.Text,
                    txbMoTa.Text,
                    (int)numSoBuoi.Value,
                    numDonGia.Value,
                    status))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    LoadData();
                }
                else MessageBox.Show("Lỗi cập nhật!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi Quyền Hạn", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(curMaKN)) return;
            if (MessageBox.Show($"Xóa môn {txbTen.Text}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    if (KyNangBUS.Instance.DeleteKyNang(curMaKN)) { MessageBox.Show("Đã xóa."); LoadData(); ResetForm(); }
                    else MessageBox.Show("Không thể xóa (đang được sử dụng).");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi Quyền Hạn", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
             ResetForm();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
             FilterData(txbSearch.Text);
        }

        private void TxbSearch_TextChanged(object sender, EventArgs e)
        {
             FilterData(txbSearch.Text);
        }

        private void ResetForm()
        {
            curMaKN = "";
            txbMa.ReadOnly = false; txbMa.BackColor = Color.White; txbMa.Clear();
            txbTen.Clear(); txbMoTa.Clear(); txbSearch.Clear();
            numSoBuoi.Value = 12; numDonGia.Value = 100000;
            cbTrangThai.SelectedIndex = 0; txbMa.Focus();
            FilterData(""); // Reset bộ lọc
        }

        private void SetPlaceholder(TextBox txt, string holder)
        {
            txt.Text = holder;
            txt.ForeColor = Color.Gray;
            txt.Enter += (s, e) => { if (txt.Text == holder) { txt.Text = ""; txt.ForeColor = Color.Black; } };
            txt.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txt.Text)) { txt.Text = holder; txt.ForeColor = Color.Gray; } };
        }
    }
}