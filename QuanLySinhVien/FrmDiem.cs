using QuanLyTrungTam.BUS;
using QuanLyTrungTam.DAO;
using QuanLyTrungTam.DTO;
using QuanLyTrungTam.Utilities;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyTrungTam
{
    public partial class FrmDiem : Form
    {
        public FrmDiem()
        {
            InitializeComponent();
            
            // Shortcuts
            this.KeyPreview = true;
            
            SetPlaceholder(txbSearch, "Mã hoặc tên học viên...");
            LoadClasses();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (ShortcutManager.Instance.IsMatch("Save", e.KeyData))
            {
                BtnSave_Click(null, null);
                e.Handled = true;
            }
            else if (ShortcutManager.Instance.IsMatch("Refresh", e.KeyData))
            {
                LoadDiem();
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }

        private void CbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDiem();
        }

        private void LoadClasses()
        {
            DataTable dt;

            // Phân quyền: Admin xem hết, Giáo viên/Nhân sự xem lớp mình phụ trách
            if (UserSession.Instance.IsAdmin())
            {
                dt = DataProvider.Instance.ExecuteQuery("SELECT MaLop, TenLop FROM LopHoc");
            }
            else
            {
                // [TODO] Move GetLopByNhanSu to BUS layer properly
                string maNS = UserSession.Instance.CurrentUser.MaNguoiDung;
                dt = LopHocDAO.Instance.GetLopByNhanSu(maNS); 
            }

            cbLop.DataSource = dt;
            cbLop.DisplayMember = "TenLop";
            cbLop.ValueMember = "MaLop";
        }

        private void LoadDiem()
        {
            if (cbLop.SelectedValue == null) return;
            string maLop = cbLop.SelectedValue.ToString();

            DataTable dt = DiemBUS.Instance.GetBangDiemLop(maLop);
            if (!dt.Columns.Contains("XepLoai")) dt.Columns.Add("XepLoai", typeof(string));

            dgvDiem.DataSource = dt;
        }

        private void DgvDiem_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvDiem.Columns[e.ColumnIndex].Name == "colXepLoai")
            {
                var cell = dgvDiem.Rows[e.RowIndex].Cells["colDiemTongKet"];
                var cellVal = cell != null ? cell.Value : null;
                if (cellVal != null && cellVal != DBNull.Value)
                {
                    double d = Convert.ToDouble(cellVal);
                    string xl = ""; Color c = Color.Black;

                    if (d < 5) { xl = "YẾU"; c = Color.Red; } // Simplified: <5 Yeu
                    else if (d < 7) { xl = "TRUNG BÌNH"; c = Color.OrangeRed; }
                    else if (d < 8) { xl = "KHÁ"; c = Color.Blue; }
                    else if (d < 9) { xl = "GIỎI"; c = Color.Green; }
                    else { xl = "XUẤT SẮC"; c = Color.DarkGreen; }

                    e.Value = xl;
                    e.CellStyle.ForeColor = c;
                    e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cbLop.SelectedValue == null) return;
            string maLop = cbLop.SelectedValue.ToString();
            int count = 0;
            string errorMsg = ""; 

            foreach (DataGridViewRow r in dgvDiem.Rows)
            {
                if (r.IsNewRow) continue;
                if (r.Cells["colMaHV"].Value == null || string.IsNullOrEmpty(r.Cells["colMaHV"].Value.ToString())) continue;

                try
                {
                    string maHV = r.Cells["colMaHV"].Value.ToString();

                    double d1 = GetVal(r.Cells["colDiem15p1"].Value);
                    double d2 = GetVal(r.Cells["colDiem15p2"].Value);
                    double dGK = GetVal(r.Cells["colDiemGiuaKy"].Value);
                    double dCK = GetVal(r.Cells["colDiemCuoiKy"].Value);
                    string ghiChu = r.Cells["colGhiChu"].Value != null ? r.Cells["colGhiChu"].Value.ToString() : "";

                    if (d1 < 0 || d1 > 10 || d2 < 0 || d2 > 10 || dGK < 0 || dGK > 10 || dCK < 0 || dCK > 10)
                    {
                        MessageBox.Show(string.Format("Điểm của {0} không hợp lệ (0-10)!", r.Cells["colHoTen"].Value), "Lỗi nhập liệu");
                        return;
                    }

                    // Gọi BUS lưu điểm
                    // Lưu ý: BUS cần kiểm tra quyền hạn nếu cần thiết (ví dụ Giáo viên chỉ sửa lớp mình dạy)
                    // Hiện tại BUS chưa check quyền lớp cụ thể, nhưng Logic LoadClasses đã filter rồi.
                    if (DiemBUS.Instance.LuuDiem(maHV, maLop, d1, d2, dGK, dCK, ghiChu))
                    {
                        count++;
                    }
                }
                catch (Exception ex)
                {
                    if (string.IsNullOrEmpty(errorMsg)) errorMsg = ex.Message;
                }
            }

            if (count > 0)
            {
                MessageBox.Show(string.Format("Đã lưu thành công {0} học viên!", count), "Thành công");
                LoadDiem(); 
            }
            else
            {
                if (!string.IsNullOrEmpty(errorMsg))
                    MessageBox.Show("Lỗi hệ thống: " + errorMsg, "Thất bại");
                else
                    MessageBox.Show("Không có dữ liệu hợp lệ để lưu hoặc không có thay đổi.", "Thông báo");
            }
        }

        private void TxbSearch_TextChanged(object sender, EventArgs e)
        {
             DataTable dt = dgvDiem.DataSource as DataTable;
            if (dt != null)
            {
                string k = txbSearch.Text.Trim();
                if (k == "Mã hoặc tên học viên..." || string.IsNullOrEmpty(k)) dt.DefaultView.RowFilter = "";
                else dt.DefaultView.RowFilter = string.Format("MaHV LIKE '%{0}%' OR HoTen LIKE '%{0}%'", k);
            }
        }

        private double GetVal(object val)
        {
            if (val == null || val == DBNull.Value || string.IsNullOrWhiteSpace(val.ToString())) return 0;
            if (double.TryParse(val.ToString(), out double res)) return res;
            return 0;
        }

        private void SetPlaceholder(TextBox txt, string holder)
        {
            txt.Text = holder; txt.ForeColor = Color.Gray;
            txt.Enter += (s, e) => { if (txt.Text == holder) { txt.Text = ""; txt.ForeColor = Color.Black; } };
            txt.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txt.Text)) { txt.Text = holder; txt.ForeColor = Color.Gray; } };
        }
    }
}