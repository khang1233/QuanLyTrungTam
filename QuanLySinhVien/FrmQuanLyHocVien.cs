using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using QuanLyTrungTam.BUS;
using QuanLyTrungTam.DAO;
using QuanLyTrungTam.Utilities;
using System.Linq;

namespace QuanLyTrungTam
{
    public partial class FrmQuanLyHocVien : Form
    {
        // Biến Logic lưu mã học viên đang chọn
        private string currentMaHV = "";

        public FrmQuanLyHocVien()
        {
            InitializeComponent();
            
            // Cấu hình phím tắt
            this.KeyPreview = true;

            // Setup Placeholder logic
            SetPlaceholder(ui_txbSearch, "Nhập mã số hoặc tên học viên...");

            // Apply grid styling overrides if needed, though Designer handles most
            ui_dgvHocVien.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            ui_dgvHocVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            ui_dgvHocVien.AllowUserToAddRows = false;
            ui_dgvHocVien.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            ui_dgvHocVien.DefaultCellStyle.SelectionForeColor = Color.Black;

            LoadData();
        }

        // =========================================================================
        // 1. HỆ THỐNG PHÍM TẮT (CUSTOM SHORTCUTS)
        // =========================================================================
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // 1. Lấy Manager
            var sm = ShortcutManager.Instance;

            // 2. Refresh
            if (sm.IsMatch("Refresh", e.KeyData)) { BtnLamMoi_Click(null, null); e.Handled = true; return; }

            // 3. Save
            if (sm.IsMatch("Save", e.KeyData)) { BtnThem_Click(null, null); e.Handled = true; return; }

            // 4. Update
            if (sm.IsMatch("Update", e.KeyData)) { BtnSua_Click(null, null); e.Handled = true; return; }

            // 5. Delete
            if (sm.IsMatch("Delete", e.KeyData)) { BtnXoa_Click(null, null); e.Handled = true; return; }

            // 6. Grant Account (Xử lý xung đột Copy)
            if (sm.IsMatch("GrantAccount", e.KeyData))
            {
                // Nếu phím tắt là Ctrl+C (trùng Copy) và đang focus vào TextBox cần Copy
                if (e.KeyData == (Keys.Control | Keys.C) && ActiveControl is TextBox txt && txt.SelectionLength > 0)
                {
                    base.OnKeyDown(e); // Để mặc định cho Copy
                    return;
                }
                
                BtnCapTK_Click(null, null); 
                e.Handled = true; 
                return;
            }

            base.OnKeyDown(e);
        }

        // =========================================================================
        // 2. LOGIC XỬ LÝ DỮ LIỆU & SỰ KIỆN
        // =========================================================================

        // Tải danh sách học viên lên Grid
        void LoadData()
        {
            ui_dgvHocVien.DataSource = HocVienBUS.Instance.GetListHocVien();
            
            // Standardize Grid
            GridViewHelper.StandardizeGrid(ui_dgvHocVien);

            // Đăng ký lại sự kiện CellClick để tránh bị double event
            ui_dgvHocVien.CellClick -= DgvHocVien_CellClick;
            ui_dgvHocVien.CellClick += DgvHocVien_CellClick;
        }

        // Lọc dữ liệu tìm kiếm
        private void FilterData(string keyword)
        {
            if (keyword == "Nhập mã số hoặc tên học viên...") keyword = "";
            DataTable dt = ui_dgvHocVien.DataSource as DataTable;
            if (dt != null)
                dt.DefaultView.RowFilter = string.IsNullOrEmpty(keyword) ? "" : $"MaHV LIKE '%{keyword}%' OR HoTen LIKE '%{keyword}%'";
        }

        // Sự kiện khi click vào một dòng trong Grid
        private void DgvHocVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                DataGridViewRow r = ui_dgvHocVien.Rows[e.RowIndex];
                if (r.Cells["MaHV"].Value == null) return;

                currentMaHV = r.Cells["MaHV"].Value.ToString();

                // Đổ dữ liệu lên các ô input
                ui_txbMa.Text = currentMaHV;
                ui_txbTen.Text = r.Cells["HoTen"].Value.ToString();
                ui_txbSDT.Text = r.Cells["SDT"].Value.ToString();
                ui_txbEmail.Text = r.Cells["Email"].Value.ToString();
                ui_txbDiaChi.Text = r.Cells["DiaChi"].Value.ToString();
                ui_cbTrangThai.Text = r.Cells["TrangThai"].Value.ToString();

                if (r.Cells["NgaySinh"].Value != DBNull.Value)
                    ui_dtpNgaySinh.Value = Convert.ToDateTime(r.Cells["NgaySinh"].Value);
            }
            catch { }
        }

        // Làm mới form để nhập mới
        private void ResetForm()
        {
            currentMaHV = "";
            ui_txbMa.Text = HocVienBUS.Instance.GetNewMaHV();
            ui_txbTen.Clear();
            ui_txbSDT.Clear();
            ui_txbEmail.Clear();
            ui_txbDiaChi.Clear();
            ui_cbTrangThai.SelectedIndex = 0; // Mặc định là Nhập học

            FilterData(""); // Bỏ lọc tìm kiếm
            ui_txbTen.Focus();
        }

        // --- CÁC SỰ KIỆN NÚT BẤM (ĐÃ REFACTOR CHO DESIGNER) ---

        // 1. Làm Mới
        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
             ResetForm();
        }

        // 2. Thêm Học Viên
        private void BtnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string ma = HocVienBUS.Instance.GetNewMaHV();
                if (HocVienBUS.Instance.InsertHocVien(ma, ui_txbTen.Text, ui_dtpNgaySinh.Value, ui_txbSDT.Text, ui_txbEmail.Text, ui_txbDiaChi.Text, ui_cbTrangThai.Text))
                {
                    MessageBox.Show("Thêm học viên thành công!");
                    LoadData();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm học viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi Quyền Hạn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // 3. Cập Nhật (Sửa) Học Viên
        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaHV)) return;

            try
            {
                // Cập nhật thông tin
                if (HocVienBUS.Instance.UpdateHocVien(currentMaHV, ui_txbTen.Text, ui_dtpNgaySinh.Value, ui_txbSDT.Text, ui_txbEmail.Text, ui_txbDiaChi.Text, ui_cbTrangThai.Text))
                {
                    // Nếu trạng thái là Bỏ học -> Khóa tài khoản
                    AccountBUS.Instance.LockAccountByUserID(currentMaHV, (ui_cbTrangThai.Text == "Bỏ học"));

                    MessageBox.Show("Cập nhật thông tin thành công!");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message, "Lỗi Quyền Hạn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // 4. Xóa Học Viên
        private void BtnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra đầu vào
            if (string.IsNullOrEmpty(currentMaHV))
            {
                MessageBox.Show("Vui lòng chọn học viên cần xóa!", "Chưa chọn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác nhận xóa
            string msg = string.Format("Bạn có chắc chắn muốn xóa học viên [{0}] (Mã: {1})?\n\n", ui_txbTen.Text, currentMaHV) +
                         "⚠️ CẢNH BÁO: Hành động này sẽ xóa vĩnh viễn:\n";

            if (MessageBox.Show(msg, "Xác nhận xóa dữ liệu", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                try
                {
                    if (HocVienBUS.Instance.DeleteHocVien(currentMaHV))
                    {
                        MessageBox.Show("Đã xóa học viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();   
                        ResetForm();  
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại! Vui lòng kiểm tra lại kết nối hoặc dữ liệu.", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi Quyền Hạn", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        // 5. Cấp Tài Khoản
        private void BtnCapTK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaHV))
            {
                MessageBox.Show("Vui lòng chọn học viên cần cấp tài khoản!");
                return;
            }

            if (AccountBUS.Instance.InsertAccount(currentMaHV, "123", "HocVien", currentMaHV))
            {
                MessageBox.Show("Đã cấp tài khoản thành công!\nTên đăng nhập: " + currentMaHV + "\nMật khẩu: 123");
            }
            else
            {
                MessageBox.Show("Học viên này đã có tài khoản rồi!");
            }
        }

        private void BtnDangKyLop_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaHV)) { MessageBox.Show("Vui lòng chọn học viên trước!"); return; }
            if (ui_cbTrangThai.Text == "Bỏ học") { MessageBox.Show("Học viên này đã bỏ học, không thể đăng ký lớp!"); return; }
            fMain main = Application.OpenForms.OfType<fMain>().FirstOrDefault();
            if (main != null) main.NavigateToDangKy(currentMaHV);
        }

        private void BtnThuPhi_Click(object sender, EventArgs e)
        {
             if (string.IsNullOrEmpty(currentMaHV)) { MessageBox.Show("Vui lòng chọn học viên trước!"); return; }
             fMain main = Application.OpenForms.OfType<fMain>().FirstOrDefault();
             if (main != null) main.NavigateToThuHocPhi(currentMaHV);
        }

        private void Ui_txbSearch_TextChanged(object sender, EventArgs e)
        {
            FilterData(ui_txbSearch.Text);
        }

        private void Ui_btnSearch_Click(object sender, EventArgs e)
        {
            FilterData(ui_txbSearch.Text);
        }


        // =========================================================================
        // 3. CÁC HÀM HELPER
        // =========================================================================

        // Hàm tạo placeholder text cho ô tìm kiếm
        private void SetPlaceholder(TextBox txt, string holder)
        {
            txt.Text = holder;
            txt.ForeColor = Color.Gray;
            txt.Enter += (s, e) => { if (txt.Text == holder) { txt.Text = ""; txt.ForeColor = Color.Black; } };
            txt.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txt.Text)) { txt.Text = holder; txt.ForeColor = Color.Gray; } };
        }
    }
}