using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using QuanLyTrungTam.DAO;
using QuanLyTrungTam.BUS;
using QuanLyTrungTam.DTO;
using System.Linq;

namespace QuanLyTrungTam
{
    public partial class FrmQuanLyHocVien : Form
    {
        // --- 1. KHAI BÁO BIẾN UI ---
        private Panel ui_pnlTop = new Panel { Dock = DockStyle.Top, Height = 250, BackColor = Color.White };
        private DataGridView ui_dgvHocVien = new DataGridView();

        // Các control nhập liệu
        private TextBox ui_txbMa, ui_txbTen, ui_txbSDT, ui_txbEmail, ui_txbDiaChi;
        private DateTimePicker ui_dtpNgaySinh;
        private ComboBox ui_cbTrangThai;

        // Tìm kiếm
        private TextBox ui_txbSearch;
        private Button ui_btnSearch;

        // Biến Logic lưu mã học viên đang chọn
        private string currentMaHV = "";

        public FrmQuanLyHocVien()
        {
            // Không dùng InitializeComponent() vì bạn đang code giao diện động
            SetupCustomUI();
            LoadData();
        }

        // =========================================================================
        // 1. TỰ ĐỘNG VẼ GIAO DIỆN (SETUP UI)
        // =========================================================================
        private void SetupCustomUI()
        {
            this.Controls.Add(ui_dgvHocVien);
            this.Controls.Add(ui_pnlTop);

            // Cấu hình GridView
            ui_dgvHocVien.Dock = DockStyle.Fill;
            ui_dgvHocVien.BackgroundColor = Color.WhiteSmoke;
            ui_dgvHocVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ui_dgvHocVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ui_dgvHocVien.ReadOnly = true;
            ui_dgvHocVien.AllowUserToAddRows = false;

            // 1. Khởi tạo control nhập liệu
            ui_txbMa = new TextBox { ReadOnly = true, BackColor = Color.LightYellow };
            ui_txbTen = new TextBox();
            ui_txbSDT = new TextBox();
            ui_txbEmail = new TextBox();
            ui_txbDiaChi = new TextBox();
            ui_dtpNgaySinh = new DateTimePicker { Format = DateTimePickerFormat.Short };

            ui_cbTrangThai = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            // Danh sách trạng thái chuẩn
            ui_cbTrangThai.Items.AddRange(new string[] { "Nhập học", "Đang học", "Bảo lưu", "Bỏ học", "Hoàn thành" });
            ui_cbTrangThai.SelectedIndex = 0;

            // 2. Vẽ Control Input lên Panel (Dùng hàm helper AddInput bên dưới)
            AddInput(ui_pnlTop, "Mã HV (Auto):", ui_txbMa, 20, 20);
            AddInput(ui_pnlTop, "Họ và Tên:", ui_txbTen, 20, 60);
            AddInput(ui_pnlTop, "Ngày Sinh:", ui_dtpNgaySinh, 20, 100);

            AddInput(ui_pnlTop, "Số Điện Thoại:", ui_txbSDT, 400, 20);
            AddInput(ui_pnlTop, "Email:", ui_txbEmail, 400, 60);
            AddInput(ui_pnlTop, "Địa Chỉ:", ui_txbDiaChi, 400, 100);
            AddInput(ui_pnlTop, "Trạng Thái:", ui_cbTrangThai, 780, 20);

            // 3. Dàn Nút Chức Năng (Đã thêm nút XÓA và căn chỉnh lại tọa độ X)
            // Khoảng cách giữa các nút là 140px
            Button btnLamMoi = CreateBtn("🔄 Làm Mới", Color.Gray, 20, 160);
            Button btnLuu = CreateBtn("💾 Lưu Mới", Color.Teal, 160, 160);
            Button btnCapNhat = CreateBtn("✏️ Cập Nhật", Color.DodgerBlue, 300, 160);

            // --- NÚT XÓA MỚI ---
            Button btnXoa = CreateBtn("❌ Xóa HV", Color.Crimson, 440, 160);

            Button btnDangKyLop = CreateBtn("📚 Đăng Ký Lớp", Color.Orange, 580, 160);
            Button btnThuPhi = CreateBtn("💰 Thu Học Phí", Color.MediumSeaGreen, 720, 160);
            Button btnCapTK = CreateBtn("🔐 Cấp Tài Khoản", Color.Purple, 860, 160);

            // 4. Thanh Tìm Kiếm
            Label lblSearch = new Label { Text = "Tìm kiếm:", Location = new Point(20, 212), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            ui_txbSearch = new TextBox { Location = new Point(100, 210), Width = 350, Font = new Font("Segoe UI", 10) };
            SetPlaceholder(ui_txbSearch, "Nhập mã số hoặc tên học viên...");

            ui_btnSearch = new Button { Text = "🔍", Location = new Point(460, 209), Size = new Size(50, 29), BackColor = Color.Navy, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };

            // Gắn sự kiện tìm kiếm
            ui_txbSearch.TextChanged += (s, e) => FilterData(ui_txbSearch.Text);
            ui_btnSearch.Click += (s, e) => FilterData(ui_txbSearch.Text);

            ui_pnlTop.Controls.AddRange(new Control[] { lblSearch, ui_txbSearch, ui_btnSearch });

            // 5. Gắn sự kiện cho các nút chức năng
            btnLamMoi.Click += (s, e) => ResetForm();
            btnLuu.Click += BtnThem_Click;
            btnCapNhat.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click; // Sự kiện xóa mới
            btnCapTK.Click += BtnCapTK_Click;

            // --- NÚT CHUYỂN TRANG ---
            btnDangKyLop.Click += (s, e) => {
                if (string.IsNullOrEmpty(currentMaHV)) { MessageBox.Show("Vui lòng chọn học viên trước!"); return; }
                if (ui_cbTrangThai.Text == "Bỏ học") { MessageBox.Show("Học viên này đã bỏ học, không thể đăng ký lớp!"); return; }

                fMain main = Application.OpenForms.OfType<fMain>().FirstOrDefault();
                if (main != null) main.NavigateToDangKy(currentMaHV);
            };

            btnThuPhi.Click += (s, e) => {
                if (string.IsNullOrEmpty(currentMaHV)) { MessageBox.Show("Vui lòng chọn học viên trước!"); return; }

                fMain main = Application.OpenForms.OfType<fMain>().FirstOrDefault();
                if (main != null) main.NavigateToThuHocPhi(currentMaHV);
            };

            // Thêm tất cả nút vào Panel
            ui_pnlTop.Controls.AddRange(new Control[] { btnLamMoi, btnLuu, btnCapNhat, btnXoa, btnDangKyLop, btnThuPhi, btnCapTK });
        }

        // =========================================================================
        // 2. LOGIC XỬ LÝ DỮ LIỆU & SỰ KIỆN
        // =========================================================================

        // Tải danh sách học viên lên Grid
        void LoadData()
        {
            ui_dgvHocVien.DataSource = HocVienBUS.Instance.GetListHocVien();

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
                dt.DefaultView.RowFilter = string.IsNullOrEmpty(keyword) ? "" : string.Format("MaHV LIKE '%{0}%' OR HoTen LIKE '%{0}%'", keyword);
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

        // --- CÁC SỰ KIỆN NÚT BẤM ---

        // 1. Thêm Học Viên
        private void BtnThem_Click(object sender, EventArgs e)
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

        // 2. Cập Nhật (Sửa) Học Viên
        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaHV)) return;

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

        // 3. Xóa Học Viên (MỚI THÊM)
        private void BtnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra đầu vào
            if (string.IsNullOrEmpty(currentMaHV))
            {
                MessageBox.Show("Vui lòng chọn học viên cần xóa!", "Chưa chọn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác nhận xóa (Quan trọng)
            string msg = string.Format("Bạn có chắc chắn muốn xóa học viên [{0}] (Mã: {1})?\n\n", ui_txbTen.Text, currentMaHV) +
                         "⚠️ CẢNH BÁO: Hành động này sẽ xóa vĩnh viễn:\n";


            if (MessageBox.Show(msg, "Xác nhận xóa dữ liệu", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                // Gọi BUS để xóa
                if (HocVienBUS.Instance.DeleteHocVien(currentMaHV))
                {
                    MessageBox.Show("Đã xóa học viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();   // Tải lại danh sách
                    ResetForm();  // Xóa trắng các ô nhập liệu
                }
                else
                {
                    MessageBox.Show("Xóa thất bại! Vui lòng kiểm tra lại kết nối hoặc dữ liệu.", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 4. Cấp Tài Khoản
        private void BtnCapTK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaHV))
            {
                MessageBox.Show("Vui lòng chọn học viên cần cấp tài khoản!");
                return;
            }

            // Mặc định pass là 123, Loại TK là HocVien
            if (AccountBUS.Instance.InsertAccount(currentMaHV, "123", "HocVien", currentMaHV))
            {
                MessageBox.Show("Đã cấp tài khoản thành công!\nTên đăng nhập: " + currentMaHV + "\nMật khẩu: 123");
            }
            else
            {
                MessageBox.Show("Học viên này đã có tài khoản rồi!");
            }
        }

        // =========================================================================
        // 3. CÁC HÀM HELPER (HỖ TRỢ UI)
        // =========================================================================

        // Hàm hỗ trợ vẽ Label + Control nhập liệu nhanh
        void AddInput(Panel p, string lb, Control c, int x, int y)
        {
            Label l = new Label { Text = lb, Location = new Point(x, y), AutoSize = true, Font = new Font("Segoe UI", 9) };
            c.Location = new Point(x + 110, y - 3);
            c.Width = 220;
            c.Font = new Font("Segoe UI", 10);
            p.Controls.Add(l);
            p.Controls.Add(c);
        }

        // Hàm tạo nút bấm có style đồng bộ
        Button CreateBtn(string t, Color c, int x, int y)
        {
            return new Button
            {
                Text = t,
                Location = new Point(x, y),
                Size = new Size(130, 35),
                BackColor = c,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }

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