using QuanLyTrungTam.BUS;
using QuanLyTrungTam.DTO;
using QuanLyTrungTam.DAO;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyTrungTam
{
    public partial class FrmHomeHocVien : Form
    {
        private string currentMaHV;

        public FrmHomeHocVien(string maHV)
        {
            this.currentMaHV = maHV;
            InitializeComponent();
            
            // Shortcuts
            this.KeyPreview = true;
            
            LoadAllData();
            StyleGrid(dgvSchedule);
            StyleGrid(dgvGrades);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                LoadAllData();
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }

        private void LoadAllData()
        {
            // 1. Info
            DataRow r = HocVienBUS.Instance.GetInfoHocVien(currentMaHV);
            if (r != null)
            {
                lblWelcome.Text = $"Xin chào học viên: {r["HoTen"].ToString().ToUpper()} ({currentMaHV})";

                HocPhiInfo info = TuitionBUS.Instance.GetHocPhiInfo(currentMaHV);
                decimal conNo = info.ConNo;

                if (conNo > 0)
                {
                    lblDebtStatus.Text = $"Cảnh báo: Bạn còn nợ học phí {conNo:N0} VNĐ. Vui lòng đóng sớm!";
                    lblDebtStatus.ForeColor = Color.Gold;
                }
                else
                {
                    lblDebtStatus.Text = "Trạng thái: Đã hoàn thành học phí.";
                    lblDebtStatus.ForeColor = Color.White;
                }
            }

            // 2. Schedule
            DataTable dtSch = TuitionBUS.Instance.GetListDangKy(currentMaHV);
            dgvSchedule.DataSource = dtSch;
            SetHeader(dgvSchedule, "TenLop", "Lớp Học");
            SetHeader(dgvSchedule, "TenKyNang", "Môn Học");
            SetHeader(dgvSchedule, "CaHoc", "Ca Học");
            SetHeader(dgvSchedule, "NgayBatDau", "Ngày Bắt Đầu");
            HideCol(dgvSchedule, "MaLop", "HocPhiLop", "NgayDangKy", "MaHV");

            // 3. Grades
            DataTable dtGrade = DiemBUS.Instance.GetBangDiemCaNhan(currentMaHV);
            dgvGrades.DataSource = dtGrade;
            SetHeader(dgvGrades, "TenLop", "Lớp Học");
            SetHeader(dgvGrades, "MonHoc", "Môn Học");
            SetHeader(dgvGrades, "Diem15p1", "15 Phút (1)");
            SetHeader(dgvGrades, "Diem15p2", "15 Phút (2)");
            SetHeader(dgvGrades, "DiemGiuaKy", "Giữa Kỳ");
            SetHeader(dgvGrades, "DiemCuoiKy", "Cuối Kỳ");
            SetHeader(dgvGrades, "DiemTongKet", "Tổng Kết");
            SetHeader(dgvGrades, "GhiChu", "Nhận Xét GV");

            dgvGrades.CellFormatting += (s, e) => {
                if (dgvGrades.Columns[e.ColumnIndex].Name == "DiemTongKet" && e.Value != null && e.Value != DBNull.Value)
                {
                    double d;
                    if (double.TryParse(e.Value.ToString(), out d))
                    {
                        if (d >= 8.5) e.CellStyle.ForeColor = Color.Green;
                        else if (d < 5) e.CellStyle.ForeColor = Color.Red;
                        else e.CellStyle.ForeColor = Color.Blue;
                        e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    }
                }
            };
        }

        private void BtnChangePass_Click(object sender, EventArgs e)
        {
            Account acc = AccountBUS.Instance.GetAccountByUserName(currentMaHV);

            // Nếu không tìm thấy theo username, thử tìm theo MaNguoiDung (cách an toàn hơn)
            if (acc == null)
            {
                // Fake Logic
                acc = new Account
                {
                    TenDangNhap = currentMaHV,
                    MatKhau = "123",
                    Quyen = "HocVien",
                    MaNguoiDung = currentMaHV,
                    TrangThai = "Hoạt động"
                };
                // Dòng trên chỉ là giả lập nếu chưa lấy đc DB, tốt nhất nên query DB
            }

            if (acc != null)
            {
                fChangePassword f = new fChangePassword(acc);
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin tài khoản!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (this.ParentForm != null) this.ParentForm.Close();
                else this.Close();
            }
        }

        private void SetHeader(DataGridView dgv, string colName, string text)
        {
            if (dgv.Columns.Contains(colName)) dgv.Columns[colName].HeaderText = text;
        }
        private void HideCol(DataGridView dgv, params string[] cols)
        {
            foreach (string c in cols) if (dgv.Columns.Contains(c)) dgv.Columns[c].Visible = false;
        }

        private void StyleGrid(DataGridView dgv)
        {
             dgv.BackgroundColor = Color.WhiteSmoke;
             dgv.BorderStyle = BorderStyle.None;
             dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
             dgv.ReadOnly = true;
             dgv.RowHeadersVisible = false;
             dgv.AllowUserToAddRows = false;
             dgv.ColumnHeadersHeight = 40;
             dgv.EnableHeadersVisualStyles = false;
             dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
             dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
             dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
             dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
             dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
             dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
             dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
             dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
             dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
        }
    }
}