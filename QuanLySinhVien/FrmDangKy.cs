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
            SetupUI(); // <--- Hàm trang trí giao diện
            btnDangKy.Click += btnDangKy_Click;
            LoadKyNang();
        }

        // --- HÀM TRANG TRÍ GIAO DIỆN (MỚI THÊM) ---
        private void SetupUI()
        {
            // 1. Cấu hình chung Form
            this.BackColor = Color.White; // Nền trắng sạch sẽ
            this.Font = new Font("Segoe UI", 10F); // Font chữ hiện đại

            // 2. Trang trí ComboBox
            StyleCombobox(cbKyNang);
            StyleCombobox(cbLopHoc);

            // 3. Trang trí Label tiền
            lblHocPhi.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblHocPhi.ForeColor = ColorTranslator.FromHtml("#DC3545"); // Màu đỏ cho nổi bật
            lblHocPhi.Text = "0 VNĐ";

            // 4. Trang trí Nút Đăng Ký (Sửa button1 thành btnDangKy)
            btnDangKy.Text = "XÁC NHẬN ĐĂNG KÝ";
            btnDangKy.Size = new Size(200, 45);
            btnDangKy.FlatStyle = FlatStyle.Flat;
            btnDangKy.FlatAppearance.BorderSize = 0;
            btnDangKy.BackColor = ColorTranslator.FromHtml("#3F51B5"); // Xanh chủ đạo
            btnDangKy.ForeColor = Color.White;
            btnDangKy.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnDangKy.Cursor = Cursors.Hand;

            // 5. Căn giữa các control (Responsive đơn giản)
            CenterControl(cbKyNang, -80); // Dịch lên trên một chút
            CenterControl(cbLopHoc, -30);
            CenterControl(lblHocPhi, 30);
            CenterControl(btnDangKy, 100);
        }

        private void StyleCombobox(ComboBox cb)
        {
            cb.FlatStyle = FlatStyle.Flat;
            cb.Font = new Font("Segoe UI", 11F);
            cb.Size = new Size(300, 30); // Rộng hơn
        }

        private void CenterControl(Control c, int yOffset)
        {
            // Căn giữa theo chiều ngang của form
            c.Location = new Point((this.ClientSize.Width - c.Width) / 2, (this.ClientSize.Height / 2) + yOffset);
            c.Anchor = AnchorStyles.Top; // Giữ vị trí khi phóng to
        }

        // --- CÁC HÀM LOGIC CŨ (GIỮ NGUYÊN) ---
        void LoadKyNang()
        {
            cbKyNang.DataSource = KyNangDAO.Instance.GetListKyNang();
            cbKyNang.DisplayMember = "TenKyNang";
            cbKyNang.ValueMember = "MaKyNang";

            // Đăng ký sự kiện (nếu designer chưa gán)
            cbKyNang.SelectedIndexChanged -= CbKyNang_SelectedIndexChanged;
            cbKyNang.SelectedIndexChanged += CbKyNang_SelectedIndexChanged;

            CbKyNang_SelectedIndexChanged(null, null);
        }

        private void CbKyNang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbKyNang.SelectedValue != null)
            {
                // Xử lý an toàn khi load form
                DataRowView row = cbKyNang.SelectedItem as DataRowView;
                string maKN = "";

                if (row != null)
                {
                    maKN = row["MaKyNang"].ToString();
                    decimal hocPhi = row["HocPhi"] != DBNull.Value ? Convert.ToDecimal(row["HocPhi"]) : 0;
                    lblHocPhi.Text = hocPhi.ToString("N0") + " VNĐ";
                    lblHocPhi.Tag = hocPhi;
                    CenterControl(lblHocPhi, 30); // Căn giữa lại vì độ dài text thay đổi
                }
                else if (cbKyNang.SelectedValue is string) // Trường hợp value member trả về string trực tiếp
                {
                    maKN = cbKyNang.SelectedValue.ToString();
                }

                if (!string.IsNullOrEmpty(maKN))
                {
                    cbLopHoc.DataSource = LopHocDAO.Instance.GetListLopByKyNang(maKN);
                    cbLopHoc.DisplayMember = "TenLop";
                    cbLopHoc.ValueMember = "MaLop";
                }
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (cbLopHoc.SelectedValue == null) { MessageBox.Show("Vui lòng chọn lớp!"); return; }
            string maLop = cbLopHoc.SelectedValue.ToString();

            if (lblHocPhi.Tag == null) return;
            decimal hocPhi = Convert.ToDecimal(lblHocPhi.Tag);

            if (TuitionDAO.Instance.DangKyLop(currentMaHV, maLop, hocPhi))
            {
                MessageBox.Show("Đăng ký thành công! Vui lòng kiểm tra công nợ.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Bạn đã đăng ký lớp này rồi!", "Cảnh báo");
            }
        }
    }
}