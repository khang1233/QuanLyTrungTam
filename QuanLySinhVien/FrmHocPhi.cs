using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyTrungTam.DAO;
using QuanLyTrungTam.DTO;

namespace QuanLyTrungTam
{
    public partial class FrmHocPhi : Form
    {
        private string currentMaHV;

        public FrmHocPhi(string maHV)
        {
            // 1. Gọi hàm khởi tạo từ file Designer (Code bạn gửi nằm ở đây)
            InitializeComponent();

            this.currentMaHV = maHV;

            // 2. Setup giao diện và load dữ liệu
            SetupUI();
            LoadThongTinHocPhi();
        }

        // --- HÀM TRANG TRÍ GIAO DIỆN ---
        private void SetupUI()
        {
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10F);

            // Tạo Panel chứa thông tin
            Panel pnlInfo = new Panel { Dock = DockStyle.Top, Height = 100, BackColor = Color.WhiteSmoke };
            this.Controls.Add(pnlInfo);

            // Định dạng các Label
            StyleLabel(lblTongHP, pnlInfo, 20, Color.Blue);
            StyleLabel(lblDaDong, pnlInfo, 300, Color.Green);
            StyleLabel(lblConNo, pnlInfo, 580, Color.Red);

            // Label tên SV
            lblTenSV.Parent = pnlInfo;
            lblTenSV.Location = new Point(20, 60);
            lblTenSV.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblTenSV.ForeColor = Color.Gray;

            // Định dạng GridView
            dgvSinhVien.Dock = DockStyle.Fill;
            dgvSinhVien.BackgroundColor = Color.White;
            dgvSinhVien.BorderStyle = BorderStyle.None;
            dgvSinhVien.EnableHeadersVisualStyles = false;
            dgvSinhVien.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#3F51B5");
            dgvSinhVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Panel chứa Grid
            Panel pnlGrid = new Panel { Dock = DockStyle.Top, Height = 250 };
            pnlGrid.Controls.Add(dgvSinhVien);
            this.Controls.Add(pnlGrid);
            pnlGrid.BringToFront();
            pnlInfo.SendToBack();

            // Panel Footer (Nút bấm)
            Panel pnlAction = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            this.Controls.Add(pnlAction);
            pnlAction.BringToFront();

            // TextBox & Button
            txbSoTienDong.Parent = pnlAction;
            txbSoTienDong.Location = new Point(20, 20);
            txbSoTienDong.Size = new Size(200, 30);

            btnXacNhanDong.Parent = pnlAction;
            btnXacNhanDong.Location = new Point(240, 18);
            btnXacNhanDong.Size = new Size(150, 32);
            btnXacNhanDong.BackColor = ColorTranslator.FromHtml("#28A745");
            btnXacNhanDong.ForeColor = Color.White;
            btnXacNhanDong.FlatStyle = FlatStyle.Flat;
            btnXacNhanDong.FlatAppearance.BorderSize = 0;
        }

        private void StyleLabel(Label lbl, Panel parent, int x, Color color)
        {
            lbl.Parent = parent;
            lbl.Location = new Point(x, 20);
            lbl.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lbl.ForeColor = color;
        }

        // --- HÀM LOAD DỮ LIỆU ---
        private void LoadThongTinHocPhi()
        {
            // Load danh sách môn học đã đăng ký
            dgvSinhVien.DataSource = TuitionDAO.Instance.GetListDangKy(currentMaHV);

            // Đổi tên cột hiển thị
            if (dgvSinhVien.Columns["TenKyNang"] != null) dgvSinhVien.Columns["TenKyNang"].HeaderText = "Kỹ Năng";
            if (dgvSinhVien.Columns["TenLop"] != null) dgvSinhVien.Columns["TenLop"].HeaderText = "Lớp Học";
            if (dgvSinhVien.Columns["HocPhiLop"] != null)
            {
                dgvSinhVien.Columns["HocPhiLop"].HeaderText = "Học Phí";
                dgvSinhVien.Columns["HocPhiLop"].DefaultCellStyle.Format = "N0";
            }
            if (dgvSinhVien.Columns["NgayDangKy"] != null) dgvSinhVien.Columns["NgayDangKy"].Visible = false;

            // Tính toán tiền
            decimal tong = TuitionDAO.Instance.GetTongNo(currentMaHV);
            decimal daDong = TuitionDAO.Instance.GetDaDong(currentMaHV);
            decimal conNo = tong - daDong;

            lblTongHP.Text = $"TỔNG HỌC PHÍ: {tong:N0} đ";
            lblDaDong.Text = $"ĐÃ ĐÓNG: {daDong:N0} đ";
            lblConNo.Text = $"CÒN NỢ: {conNo:N0} đ";
            lblConNo.ForeColor = conNo > 0 ? Color.Red : Color.Green;

            lblTenSV.Text = "Mã HV: " + currentMaHV;
        }

        // --- CÁC SỰ KIỆN (Bắt buộc phải có để khớp với Designer) ---

        // 1. Sự kiện Click nút Thanh toán
        private void btnXacNhanDong_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txbSoTienDong.Text, out decimal soTien))
            {
                if (soTien <= 0) { MessageBox.Show("Số tiền phải lớn hơn 0"); return; }

                if (TuitionDAO.Instance.InsertThanhToan(currentMaHV, soTien, "Đóng tiền tại quầy"))
                {
                    MessageBox.Show("Thanh toán thành công!");
                    LoadThongTinHocPhi();
                    txbSoTienDong.Clear();
                }
                else MessageBox.Show("Lỗi hệ thống!");
            }
            else MessageBox.Show("Vui lòng nhập số tiền hợp lệ!");
        }

        // 2. Sự kiện Load Form (Designer gọi hàm này nên bắt buộc phải có)
        private void FrmHocPhi_Load_1(object sender, EventArgs e)
        {
            // Để trống cũng được vì đã load ở Constructor
        }
    }
}