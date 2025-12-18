using QuanLyTrungTam.DAO;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyTrungTam
{
    public partial class FrmLop : Form
    {
        // ComboBox chọn Kỹ Năng (Thay thế cho ô Mã Khoa cũ)
        private ComboBox cbKyNang;

        public FrmLop()
        {
            InitializeComponent();
            SetupCleanUI(); // Làm sạch giao diện & Tạo điều khiển mới
            LoadCbKyNang(); // Nạp danh sách Kỹ năng
            LoadListLop();  // Nạp danh sách Lớp (TẤT CẢ)
        }

        // =========================================================================
        // 1. SETUP GIAO DIỆN (TỰ ĐỘNG ẨN CÁC Ô THỪA & TẠO COMBOBOX)
        // =========================================================================
        private void SetupCleanUI()
        {
            // A. TỰ ĐỘNG QUÉT VÀ ẨN CÁC Ô KHÔNG DÙNG ĐẾN
            string[] controlsToHide = { "txbMaKhoa", "txbMaMonHoc", "lblMaMonHoc", "labelMaMonHoc" };
            foreach (string name in controlsToHide)
            {
                Control c = this.Controls[name];
                if (c != null) c.Visible = false;
            }

            // Quét các Label để sửa văn bản cho đúng nghiệp vụ
            foreach (Control c in this.Controls)
            {
                if (c is Label)
                {
                    if (c.Text.Contains("Mã Môn")) c.Text = "Sĩ Số (HV):"; // Tận dụng Label này
                    if (c.Text.Contains("Mã Khoa")) c.Text = "Thuộc Kỹ Năng:";
                }
            }

            // B. TẬN DỤNG Ô "MÃ MÔN HỌC" ĐỂ HIỂN THỊ SỐ HỌC VIÊN
            Control txbSiSo = this.Controls["txbMaMonHoc"];
            if (txbSiSo != null)
            {
                txbSiSo.Visible = true; // Hiện lại
                txbSiSo.Enabled = false; // Chỉ xem, không cho sửa
                txbSiSo.BackColor = Color.WhiteSmoke;
                txbSiSo.Text = "0";
            }

            // C. TẠO COMBOBOX KỸ NĂNG (INPUT CHỌN)
            cbKyNang = new ComboBox();
            cbKyNang.DropDownStyle = ComboBoxStyle.DropDownList;
            cbKyNang.Size = new Size(250, 28);
            cbKyNang.Font = new Font("Segoe UI", 10F);

            // Đặt vị trí đè lên ô Mã Khoa cũ
            Control oldPlace = this.Controls["txbMaKhoa"];
            if (oldPlace != null) cbKyNang.Location = oldPlace.Location;
            else cbKyNang.Location = new Point(140, 100);

            this.Controls.Add(cbKyNang);
            cbKyNang.BringToFront();

            // Lưu ý: Không gán sự kiện SelectedIndexChanged để lọc nữa
            // Vì ta muốn danh sách luôn hiện TẤT CẢ (giống Tab Sinh Viên)
        }

        // =========================================================================
        // 2. TẢI DỮ LIỆU
        // =========================================================================
        void LoadCbKyNang()
        {
            try
            {
                DataTable dt = KyNangDAO.Instance.GetListKyNang();
                // Tạo cột hiển thị dạng "Mã - Tên"
                dt.Columns.Add("Display", typeof(string), "MaKyNang + ' - ' + TenKyNang");

                cbKyNang.DataSource = dt;
                cbKyNang.DisplayMember = "Display";
                cbKyNang.ValueMember = "MaKyNang";
            }
            catch { }
        }

        void LoadListLop()
        {
            // 1. LẤY TẤT CẢ LỚP HỌC (Sử dụng hàm mới thêm trong DAO)
            DataTable dt = LopHocDAO.Instance.GetAllLop();

            // 2. TỰ ĐỘNG TÍNH SĨ SỐ (Đếm từ bảng DangKy)
            if (!dt.Columns.Contains("SiSo"))
            {
                dt.Columns.Add("SiSo", typeof(int));
                foreach (DataRow row in dt.Rows)
                {
                    string maLop = row["MaLop"].ToString();
                    string queryCount = "SELECT COUNT(*) FROM DangKy WHERE MaLop = '" + maLop + "'";
                    int count = (int)DataProvider.Instance.ExecuteScalar(queryCount);
                    row["SiSo"] = count;
                }
            }

            // 3. HIỂN THỊ LÊN LƯỚI
            dgvLop.DataSource = dt;

            // 4. ĐẶT TÊN CỘT TIẾNG VIỆT
            SetHeader(dgvLop, "MaLop", "Mã Lớp");
            SetHeader(dgvLop, "TenLop", "Tên Lớp");
            SetHeader(dgvLop, "TenKyNang", "Thuộc Kỹ Năng"); // Cột này có được nhờ hàm GetAllLop join bảng
            SetHeader(dgvLop, "NgayBatDau", "Ngày Mở");
            SetHeader(dgvLop, "SiSo", "Sĩ Số");
            SetHeader(dgvLop, "LichHoc", "Lịch Học");

            // 5. Ẩn các cột mã không cần thiết
            if (dgvLop.Columns.Contains("MaKyNang")) dgvLop.Columns["MaKyNang"].Visible = false;
        }

        void SetHeader(DataGridView dgv, string colName, string text)
        {
            if (dgv.Columns.Contains(colName)) dgv.Columns[colName].HeaderText = text;
        }

        // =========================================================================
        // 3. SỰ KIỆN CLICK VÀO BẢNG (BINDING DỮ LIỆU)
        // =========================================================================
        private void dgvLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLop.Rows[e.RowIndex];

                txbMaLop.Text = row.Cells["MaLop"].Value?.ToString();
                txbTenLop.Text = row.Cells["TenLop"].Value?.ToString();

                // Hiển thị sĩ số lên ô text (ô Mã Môn Học cũ)
                Control txbSiSo = this.Controls["txbMaMonHoc"];
                if (txbSiSo != null && row.Cells["SiSo"].Value != null)
                {
                    txbSiSo.Text = row.Cells["SiSo"].Value.ToString();
                }

                // Chọn đúng Kỹ Năng trên ComboBox
                if (row.Cells["MaKyNang"].Value != null)
                {
                    cbKyNang.SelectedValue = row.Cells["MaKyNang"].Value.ToString();
                }

                if (row.Cells["NgayBatDau"].Value != DBNull.Value)
                    dtNgayMo.Value = Convert.ToDateTime(row.Cells["NgayBatDau"].Value);
            }
        }

        // =========================================================================
        // 4. CHỨC NĂNG THÊM - SỬA - XÓA
        // =========================================================================

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!CheckInput()) return;
            string maKN = cbKyNang.SelectedValue.ToString();

            if (LopHocDAO.Instance.InsertLop(txbMaLop.Text, txbTenLop.Text, maKN, dtNgayMo.Value))
            {
                MessageBox.Show("✅ Thêm lớp thành công!");
                LoadListLop(); // Refresh lại danh sách tổng
            }
            else MessageBox.Show("❌ Lỗi: Mã lớp đã tồn tại!");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!CheckInput()) return;
            string maKN = cbKyNang.SelectedValue.ToString();

            if (LopHocDAO.Instance.UpdateLop(txbMaLop.Text, txbTenLop.Text, maKN, dtNgayMo.Value))
            {
                MessageBox.Show("✅ Cập nhật thành công!");
                LoadListLop();
            }
            else MessageBox.Show("❌ Lỗi: Không tìm thấy lớp để sửa.");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maLop = txbMaLop.Text;
            if (string.IsNullOrEmpty(maLop)) return;

            if (MessageBox.Show($"Bạn có chắc muốn xóa lớp {maLop}?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (LopHocDAO.Instance.DeleteLop(maLop))
                {
                    MessageBox.Show("✅ Đã xóa!");
                    LoadListLop();
                    txbMaLop.Clear(); txbTenLop.Clear();
                }
                else MessageBox.Show("⚠️ Không thể xóa: Lớp này đang có học viên!");
            }
        }

        bool CheckInput()
        {
            if (cbKyNang.SelectedValue == null) { MessageBox.Show("Vui lòng chọn Kỹ Năng!"); return false; }
            if (string.IsNullOrWhiteSpace(txbMaLop.Text) || string.IsNullOrWhiteSpace(txbTenLop.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Lớp và Tên Lớp!");
                return false;
            }
            return true;
        }
    }
}