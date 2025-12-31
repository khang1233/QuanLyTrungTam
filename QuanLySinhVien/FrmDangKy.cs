using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using QuanLyTrungTam.DAO;

namespace QuanLyTrungTam
{
    public partial class FrmDangKy : Form
    {
        private string currentMaHV;

        public FrmDangKy(string maHV)
        {
            InitializeComponent();
            this.currentMaHV = maHV;
            SetupUI();

            // Sử dụng sự kiện Load để đảm bảo dữ liệu lên đủ
            this.Load += (s, e) => LoadKyNang();

            // Gán sự kiện click
            btnDangKy.Click += btnDangKy_Click;
        }

        // --- HÀM TRANG TRÍ GIAO DIỆN ---
        private void SetupUI()
        {
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10F);
            this.StartPosition = FormStartPosition.CenterParent; // Hiện giữa form cha
            this.Text = "Đăng Ký Khóa Học";

            StyleCombobox(cbKyNang);
            StyleCombobox(cbLopHoc);

            lblHocPhi.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblHocPhi.ForeColor = ColorTranslator.FromHtml("#DC3545");
            lblHocPhi.Text = "0 VNĐ";

            btnDangKy.Text = "XÁC NHẬN ĐĂNG KÝ";
            btnDangKy.Size = new Size(220, 45);
            btnDangKy.FlatStyle = FlatStyle.Flat;
            btnDangKy.FlatAppearance.BorderSize = 0;
            btnDangKy.BackColor = ColorTranslator.FromHtml("#3F51B5");
            btnDangKy.ForeColor = Color.White;
            btnDangKy.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnDangKy.Cursor = Cursors.Hand;

            // Layout thủ công (nên dùng TableLayoutPanel hoặc Anchor nếu có thể)
            CenterControl(cbKyNang, -80);
            CenterControl(cbLopHoc, -30);
            CenterControl(lblHocPhi, 30);
            CenterControl(btnDangKy, 100);
        }

        private void StyleCombobox(ComboBox cb)
        {
            cb.FlatStyle = FlatStyle.Flat;
            cb.Font = new Font("Segoe UI", 11F);
            cb.Size = new Size(300, 30);
            cb.DropDownStyle = ComboBoxStyle.DropDownList; // Chặn người dùng gõ linh tinh
        }

        private void CenterControl(Control c, int yOffset)
        {
            c.Location = new Point((this.ClientSize.Width - c.Width) / 2, (this.ClientSize.Height / 2) + yOffset);
            c.Anchor = AnchorStyles.Top;
        }

        // --- LOGIC XỬ LÝ ---
        void LoadKyNang()
        {
            cbKyNang.DataSource = KyNangDAO.Instance.GetListKyNang();
            cbKyNang.DisplayMember = "TenKyNang";
            cbKyNang.ValueMember = "MaKyNang";

            // Gán sự kiện thay đổi lựa chọn
            cbKyNang.SelectedIndexChanged -= CbKyNang_SelectedIndexChanged;
            cbKyNang.SelectedIndexChanged += CbKyNang_SelectedIndexChanged;

            // Trigger lần đầu
            if (cbKyNang.Items.Count > 0) CbKyNang_SelectedIndexChanged(null, null);
        }

        private void CbKyNang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbKyNang.SelectedValue != null)
            {
                DataRowView row = cbKyNang.SelectedItem as DataRowView;
                string maKN = "";

                if (row != null)
                {
                    maKN = row["MaKyNang"].ToString();
                    decimal hocPhi = row["HocPhi"] != DBNull.Value ? Convert.ToDecimal(row["HocPhi"]) : 0;
                    lblHocPhi.Text = hocPhi.ToString("N0") + " VNĐ";
                    lblHocPhi.Tag = hocPhi;
                    CenterControl(lblHocPhi, 30);
                }
                else if (cbKyNang.SelectedValue is string)
                {
                    maKN = cbKyNang.SelectedValue.ToString();
                }

                if (!string.IsNullOrEmpty(maKN))
                {
                    // Load danh sách lớp theo kỹ năng
                    cbLopHoc.DataSource = LopHocDAO.Instance.GetListLopByKyNang(maKN);
                    cbLopHoc.DisplayMember = "TenLop";
                    cbLopHoc.ValueMember = "MaLop";
                }
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra dữ liệu đầu vào
            if (cbKyNang.SelectedValue == null) { MessageBox.Show("Vui lòng chọn môn học!"); return; }
            if (cbLopHoc.SelectedValue == null) { MessageBox.Show("Vui lòng chọn lớp học!"); return; }

            // ========================================================================
            // [FIX 1] KIỂM TRA MÔN NGƯNG HOẠT ĐỘNG
            // ========================================================================
            DataRowView rowKN = cbKyNang.SelectedItem as DataRowView;
            if (rowKN != null && rowKN.DataView.Table.Columns.Contains("TrangThai"))
            {
                string trangThai = rowKN["TrangThai"].ToString().ToLower();
                // Kiểm tra các trường hợp: "0", "false", "ngưng hoạt động"
                if (trangThai == "0" || trangThai == "false" || trangThai.Contains("ngưng"))
                {
                    MessageBox.Show("Môn học này đang NGƯNG HOẠT ĐỘNG.\nKhông thể đăng ký!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Dừng lại ngay
                }
            }

            string maLop = cbLopHoc.SelectedValue.ToString();
            decimal hocPhi = (lblHocPhi.Tag != null) ? Convert.ToDecimal(lblHocPhi.Tag) : 0;

            // 2. Thực hiện đăng ký
            if (TuitionDAO.Instance.DangKyLop(currentMaHV, maLop, hocPhi))
            {
                // --- BẮT ĐẦU ĐOẠN DEBUG ---

                // 1. Hiện thông báo để xem Mã HV có đúng là HV08 không
                MessageBox.Show("Bắt đầu cập nhật cho Mã: " + currentMaHV);

                // 2. Gọi hàm update mới sửa
                bool ketQua = HocVienDAO.Instance.CapNhatTrangThaiHocVien(currentMaHV, "Đang học");

                // 3. Thông báo kết quả
                if (ketQua)
                    MessageBox.Show("Đã Update thành công trong Database!");
                else
                    MessageBox.Show("Lỗi: Không Update được (Có thể sai Mã HV)!");

                // --- KẾT THÚC ĐOẠN DEBUG ---

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Học viên đã đăng ký lớp này rồi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}