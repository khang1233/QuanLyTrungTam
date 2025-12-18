using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyTrungTam.DAO;

namespace QuanLyTrungTam
{
    public partial class FrmQuanLyMonHoc : Form
    {
        // Khai báo các control
        private DataGridView dgvMonHoc = new DataGridView();
        private TextBox txbMa = new TextBox();
        private TextBox txbTen = new TextBox();
        private TextBox txbHocPhi = new TextBox();
        private TextBox txbMoTa = new TextBox(); // Dùng nhập số buổi/mô tả
        private Button btnThem = new Button { Text = "Thêm", BackColor = Color.LightGreen };
        private Button btnSua = new Button { Text = "Sửa", BackColor = Color.LightBlue };
        private Button btnXoa = new Button { Text = "Xóa", BackColor = Color.LightPink };

        public FrmQuanLyMonHoc()
        {
            // InitializeComponent(); // Bỏ comment nếu dùng Designer
            SetupUI();
            LoadData();
        }

        void SetupUI()
        {
            this.Text = "Quản lý danh mục môn học";
            this.BackColor = Color.White;

            // 1. Tạo Panel nhập liệu
            Panel pnlInput = new Panel { Dock = DockStyle.Top, Height = 150 };
            this.Controls.Add(pnlInput);

            // Helper tạo Label & TextBox
            AddInputControl(pnlInput, "Mã Kỹ Năng:", txbMa, 20, 20);     // Cũ: Mã Môn
            AddInputControl(pnlInput, "Tên Kỹ Năng:", txbTen, 300, 20);  // Cũ: Tên Môn
            AddInputControl(pnlInput, "Học Phí:", txbHocPhi, 20, 60);
            AddInputControl(pnlInput, "Mô tả chi tiết:", txbMoTa, 300, 60); // Cũ: Mô tả/Số buổi

            // 2. Tạo nút bấm
            btnThem.Location = new Point(20, 100); btnThem.Size = new Size(80, 30);
            btnSua.Location = new Point(110, 100); btnSua.Size = new Size(80, 30);
            btnXoa.Location = new Point(200, 100); btnXoa.Size = new Size(80, 30);

            pnlInput.Controls.Add(btnThem);
            pnlInput.Controls.Add(btnSua);
            pnlInput.Controls.Add(btnXoa);

            // Gán sự kiện
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;

            // 3. Tạo GridView
            dgvMonHoc.Dock = DockStyle.Fill;
            dgvMonHoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMonHoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMonHoc.CellClick += DgvMonHoc_CellClick;
            this.Controls.Add(dgvMonHoc);
            dgvMonHoc.BringToFront();
        }

        void AddInputControl(Panel p, string labelText, Control input, int x, int y)
        {
            Label lbl = new Label { Text = labelText, Location = new Point(x, y), AutoSize = true };
            input.Location = new Point(x + 100, y - 3);
            input.Size = new Size(150, 25);
            p.Controls.Add(lbl);
            p.Controls.Add(input);
        }

        // --- LOGIC ---
        void LoadData()
        {
            dgvMonHoc.DataSource = KyNangDAO.Instance.GetListKyNang();

            // --- THÊM ĐOẠN NÀY ĐỂ ĐỔI TÊN CỘT ---
            if (dgvMonHoc.Columns["MaKyNang"] != null) dgvMonHoc.Columns["MaKyNang"].HeaderText = "Mã Kỹ Năng";
            if (dgvMonHoc.Columns["TenKyNang"] != null) dgvMonHoc.Columns["TenKyNang"].HeaderText = "Tên Kỹ Năng";
            if (dgvMonHoc.Columns["HocPhi"] != null)
            {
                dgvMonHoc.Columns["HocPhi"].HeaderText = "Học Phí";
                dgvMonHoc.Columns["HocPhi"].DefaultCellStyle.Format = "N0"; // Định dạng tiền tệ
            }
            if (dgvMonHoc.Columns["MoTa"] != null) dgvMonHoc.Columns["MoTa"].HeaderText = "Mô Tả";
            if (dgvMonHoc.Columns["TrangThai"] != null) dgvMonHoc.Columns["TrangThai"].Visible = false; // Ẩn cột trạng thái nếu không cần
        }

        private void DgvMonHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMonHoc.Rows[e.RowIndex];
                txbMa.Text = row.Cells["MaKyNang"].Value.ToString();
                txbTen.Text = row.Cells["TenKyNang"].Value.ToString();
                txbHocPhi.Text = row.Cells["HocPhi"].Value.ToString();
                txbMoTa.Text = row.Cells["MoTa"].Value.ToString();
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            decimal.TryParse(txbHocPhi.Text, out decimal hp);
            if (KyNangDAO.Instance.InsertKyNang(txbMa.Text, txbTen.Text, hp, txbMoTa.Text))
            {
                MessageBox.Show("Thêm thành công!");
                LoadData();
            }
            else MessageBox.Show("Lỗi: Trùng mã hoặc lỗi dữ liệu.");
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            decimal.TryParse(txbHocPhi.Text, out decimal hp);
            if (KyNangDAO.Instance.UpdateKyNang(txbMa.Text, txbTen.Text, hp, txbMoTa.Text))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadData();
            }
            else MessageBox.Show("Lỗi khi sửa.");
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xóa môn này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (KyNangDAO.Instance.DeleteKyNang(txbMa.Text))
                {
                    MessageBox.Show("Đã xóa!");
                    LoadData();
                }
                else MessageBox.Show("Không thể xóa (Môn này đang có lớp học mở).");
            }
        }
    }
}