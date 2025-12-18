using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyTrungTam.DAO;

namespace QuanLyTrungTam
{
    public partial class FrmQuanLyHocVien : Form
    {
        // Các control nhập liệu
        private DataGridView dgvHocVien = new DataGridView();
        private TextBox txbMaHV = new TextBox();
        private TextBox txbHoTen = new TextBox();
        private TextBox txbSDT = new TextBox();
        private TextBox txbEmail = new TextBox();
        private TextBox txbDiaChi = new TextBox();
        private DateTimePicker dtpNgaySinh = new DateTimePicker();

        // Các nút chức năng
        private Button btnThem = new Button { Text = "Thêm HV", BackColor = Color.LightGreen };
        private Button btnSua = new Button { Text = "Sửa Info", BackColor = Color.LightBlue };
        private Button btnXoa = new Button { Text = "Xóa HV", BackColor = Color.LightPink };

        // NÚT ĐẶC BIỆT DÀNH CHO ADMIN
        private Button btnDangKyHo = new Button { Text = "Đăng Ký Lớp Cho HV Này", BackColor = Color.Orange, Width = 180 };
        private Button btnXemHocPhi = new Button { Text = "Quản Lý Học Phí HV Này", BackColor = Color.Gold, Width = 180 };

        public FrmQuanLyHocVien()
        {
            //InitializeComponent(); // Bỏ comment nếu dùng Designer
            SetupUI();
            LoadListHocVien();
        }

        private void SetupUI()
        {
            this.Text = "Quản lý hồ sơ học viên";
            this.BackColor = Color.White;
            this.Size = new Size(1000, 600);

            // 1. Panel Nhập liệu (Top)
            Panel pnlInput = new Panel { Dock = DockStyle.Top, Height = 180, BackColor = Color.WhiteSmoke };
            this.Controls.Add(pnlInput);

            // Helper để vẽ textbox nhanh
            AddControl(pnlInput, "Mã HV:", txbMaHV, 20, 20);
            AddControl(pnlInput, "Họ Tên:", txbHoTen, 300, 20);
            AddControl(pnlInput, "Ngày Sinh:", dtpNgaySinh, 600, 20);
            AddControl(pnlInput, "SĐT:", txbSDT, 20, 60);
            AddControl(pnlInput, "Email:", txbEmail, 300, 60);
            AddControl(pnlInput, "Địa Chỉ:", txbDiaChi, 600, 60);

            // 2. Buttons CRUD
            btnThem.Location = new Point(20, 110); btnThem.Size = new Size(100, 40);
            btnSua.Location = new Point(130, 110); btnSua.Size = new Size(100, 40);
            btnXoa.Location = new Point(240, 110); btnXoa.Size = new Size(100, 40);

            pnlInput.Controls.Add(btnThem);
            pnlInput.Controls.Add(btnSua);
            pnlInput.Controls.Add(btnXoa);

            // 3. Buttons Nghiệp vụ (Quan trọng với yêu cầu của bạn)
            btnDangKyHo.Location = new Point(450, 110); btnDangKyHo.Height = 40;
            btnXemHocPhi.Location = new Point(650, 110); btnXemHocPhi.Height = 40;

            pnlInput.Controls.Add(btnDangKyHo);
            pnlInput.Controls.Add(btnXemHocPhi);

            // Gán sự kiện
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;

            // Sự kiện mở Form con
            btnDangKyHo.Click += BtnDangKyHo_Click;
            btnXemHocPhi.Click += BtnXemHocPhi_Click;

            // 4. GridView (Fill)
            dgvHocVien.Dock = DockStyle.Fill;
            dgvHocVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHocVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHocVien.CellClick += DgvHocVien_CellClick;
            this.Controls.Add(dgvHocVien);
            dgvHocVien.BringToFront();
        }

        // Helper function
        void AddControl(Panel p, string lbl, Control c, int x, int y)
        {
            Label l = new Label { Text = lbl, Location = new Point(x, y), AutoSize = true };
            c.Location = new Point(x + 80, y - 3);
            c.Size = new Size(180, 25);
            if (c is DateTimePicker) c.Width = 180;
            p.Controls.Add(l);
            p.Controls.Add(c);
            if (c == txbMaHV)
            {
                c.Enabled = false; // Hoặc c.ReadOnly = true;
                c.Text = "Tự động tạo"; // Hiển thị chữ cho người dùng hiểu
                c.BackColor = Color.WhiteSmoke; // Làm mờ đi chút
            }
        }

        void LoadListHocVien()
        {
            dgvHocVien.DataSource = HocVienDAO.Instance.GetListHocVien();
        }

        // Binding dữ liệu khi chọn dòng
        private void DgvHocVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvHocVien.Rows[e.RowIndex];
                txbMaHV.Text = row.Cells["MaHV"].Value.ToString();
                txbHoTen.Text = row.Cells["HoTen"].Value.ToString();
                txbSDT.Text = row.Cells["SoDienThoai"].Value.ToString();
                txbEmail.Text = row.Cells["Email"].Value.ToString();
                txbDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
                if (row.Cells["NgaySinh"].Value != DBNull.Value)
                    dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
            }
        }

        // --- CHỨC NĂNG CRUD CƠ BẢN ---
        private void BtnThem_Click(object sender, EventArgs e)
        {
            // 1. Tự động lấy mã mới từ DAO
            string newMaHV = HocVienDAO.Instance.GetNewMaHV();

            // 2. Gọi hàm Insert với mã vừa tạo (thay vì txbMaHV.Text)
            if (HocVienDAO.Instance.InsertHocVien(newMaHV, txbHoTen.Text, dtpNgaySinh.Value, txbSDT.Text, txbEmail.Text, txbDiaChi.Text))
            {
                MessageBox.Show($"Thêm thành công học viên mã: {newMaHV}");
                LoadListHocVien();

                // Reset các ô nhập liệu sau khi thêm
                txbHoTen.Clear();
                txbSDT.Clear();
                txbEmail.Clear();
                txbDiaChi.Clear();
            }
            else
            {
                MessageBox.Show("Lỗi hệ thống không thể thêm mới.");
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (HocVienDAO.Instance.UpdateHocVien(txbMaHV.Text, txbHoTen.Text, dtpNgaySinh.Value, txbSDT.Text, txbEmail.Text, txbDiaChi.Text))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadListHocVien();
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa HV này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (HocVienDAO.Instance.DeleteHocVien(txbMaHV.Text))
                {
                    MessageBox.Show("Đã xóa!");
                    LoadListHocVien();
                }
                else MessageBox.Show("Không thể xóa (Học viên này đang có dữ liệu điểm/lớp).");
            }
        }

        // --- CHỨC NĂNG ADMIN ĐĂNG KÝ HỘ / XEM TIỀN HỘ ---
        // Đây chính là giải pháp cho yêu cầu của bạn

        // Trong file FrmQuanLyHocVien.cs

        // Nút Đăng Ký Lớp Cho HV Này
        private void BtnDangKyHo_Click(object sender, EventArgs e)
        {
            string maHV = txbMaHV.Text;
            if (string.IsNullOrEmpty(maHV) || maHV == "Tự động tạo")
            {
                MessageBox.Show("Vui lòng chọn một học viên từ danh sách!");
                return;
            }

            // Lấy instance của fMain đang chạy
            fMain mainForm = Application.OpenForms["fMain"] as fMain;

            if (mainForm != null)
            {
                // Gọi hàm điều hướng sang Tab Đăng Ký
                mainForm.GotoDangKyAdmin(maHV);
            }
        }

        // Nút Quản Lý Học Phí HV Này
        private void BtnXemHocPhi_Click(object sender, EventArgs e)
        {
            string maHV = txbMaHV.Text;
            if (string.IsNullOrEmpty(maHV) || maHV == "Tự động tạo")
            {
                MessageBox.Show("Vui lòng chọn một học viên từ danh sách!");
                return;
            }

            // Lấy instance của fMain đang chạy
            fMain mainForm = Application.OpenForms["fMain"] as fMain;

            if (mainForm != null)
            {
                // Gọi hàm điều hướng sang Tab Tra Cứu
                mainForm.GotoTraCuuPhi(maHV);
            }
        } 
    }
}