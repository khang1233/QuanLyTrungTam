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
    public partial class FrmSchedule : Form
    {
        public FrmSchedule()
        {
            InitializeComponent();
            
            // Shortcuts
            this.KeyPreview = true;
            
            cbFilterType.SelectedIndex = 0;
            LoadInitData();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.R || e.KeyCode == Keys.F5)
            {
                BtnSearch_Click(null, null);
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }

        private void LoadInitData()
        {
            // Use UserSession instead of AppSession
            if (UserSession.Instance.IsAdmin())
            {
                BtnSearch_Click(null, null);
            }
            else
            {
                // Lock UI for Teachers or non-Admins
                LockUIForTeacher();
                LoadTeacherSchedule();
            }
        }

        private void LockUIForTeacher()
        {
            cbFilterType.Visible = false;
            cbFilterValue.Visible = false;
            lblType.Visible = false;
            lblValue.Visible = false;
        }

        private void LoadTeacherSchedule()
        {
            string maNS = UserSession.Instance.CurrentUser.MaNguoiDung;

            string query = @"
        SELECT l.MaLop, l.TenLop, k.TenKyNang, l.Thu, l.CaHoc, p.TenPhong, l.TrangThai,
               ns1.HoTen as TenGV, ns2.HoTen as TenTG, l.NgayKetThuc, 12 as SoBuoi
        FROM LopHoc l
        JOIN KyNang k ON l.MaKyNang = k.MaKyNang
        LEFT JOIN PhongHoc p ON l.MaPhong = p.MaPhong
        LEFT JOIN NhanSu ns1 ON l.MaGiaoVien = ns1.MaNS
        LEFT JOIN NhanSu ns2 ON l.MaTroGiang = ns2.MaNS
        WHERE l.MaGiaoVien = @ma1 OR l.MaTroGiang = @ma2";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { maNS, maNS });

            dgvSchedule.DataSource = dt;
            FormatGrid();
        }

        private void CbFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbFilterValue.DataSource = null;
            cbFilterValue.Items.Clear();
            int type = cbFilterType.SelectedIndex;

            if (type == 0) // Todos
            {
                cbFilterValue.Enabled = false;
            }
            else
            {
                cbFilterValue.Enabled = true;
                if (type == 1) // GV
                {
                    DataTable dt = NhanVienDAO.Instance.GetListNhanVien();
                    DataView dv = new DataView(dt);
                    dv.RowFilter = "LoaiNS = 'GiaoVien'";
                    cbFilterValue.DataSource = dv;
                    cbFilterValue.DisplayMember = "HoTen"; cbFilterValue.ValueMember = "MaNS";
                }
                else if (type == 2) // Phòng
                {
                    cbFilterValue.DataSource = PhongHocDAO.Instance.GetListPhong();
                    cbFilterValue.DisplayMember = "TenPhong"; cbFilterValue.ValueMember = "MaPhong";
                }
                else if (type == 3) // Lớp
                {
                    cbFilterValue.DataSource = LopHocDAO.Instance.GetAllLop();
                    cbFilterValue.DisplayMember = "TenLop"; cbFilterValue.ValueMember = "MaLop";
                }
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            DataTable allLop = LopHocDAO.Instance.GetAllLop();

            if (cbFilterType.SelectedIndex == 0) // Todos
            {
                dtResult = allLop;
            }
            else
            {
                if (cbFilterValue.SelectedValue == null) { MessageBox.Show("Vui lòng chọn đối tượng!"); return; }

                string id = cbFilterValue.SelectedValue.ToString();
                DataView dv = new DataView(allLop);

                if (cbFilterType.SelectedIndex == 1) dv.RowFilter = $"MaGiaoVien = '{id}'";
                else if (cbFilterType.SelectedIndex == 2) dv.RowFilter = $"MaPhong = '{id}'";
                else if (cbFilterType.SelectedIndex == 3) dv.RowFilter = $"MaLop = '{id}'";

                dtResult = dv.ToTable();
            }

            dgvSchedule.DataSource = dtResult;
            FormatGrid();

            txbSearch.Text = "";
        }

        private void TxbSearch_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = dgvSchedule.DataSource as DataTable;
            if (dt != null)
            {
                string keyword = txbSearch.Text.Trim();
                if (string.IsNullOrEmpty(keyword))
                {
                    dt.DefaultView.RowFilter = "";
                }
                else
                {
                    dt.DefaultView.RowFilter = string.Format("MaLop LIKE '%{0}%' OR TenLop LIKE '%{0}%'", keyword);
                }
            }
        }

        private void FormatGrid()
        {
            string[] hide = { "MaKyNang", "MaGiaoVien", "MaTroGiang", "MaPhong", "SiSoToiDa", "NgayBatDau" };
            foreach (string c in hide) if (dgvSchedule.Columns.Contains(c)) dgvSchedule.Columns[c].Visible = false;

            SetHeader("MaLop", "Mã Lớp");
            SetHeader("TenLop", "Tên Lớp");
            SetHeader("TenKyNang", "Môn Học");
            SetHeader("TenGV", "Giáo Viên");
            SetHeader("TenTG", "Trợ Giảng");
            SetHeader("TenPhong", "Phòng Học");
            SetHeader("Thu", "Lịch Học");
            SetHeader("CaHoc", "Ca Học");
            SetHeader("TrangThai", "Trạng Thái");
            SetHeader("NgayKetThuc", "Ngày Kết Thúc");
            SetHeader("SoBuoi", "Số Buổi");

            if (dgvSchedule.Columns.Contains("NgayKetThuc")) 
            {
                dgvSchedule.Columns["NgayKetThuc"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvSchedule.Columns["NgayKetThuc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvSchedule.Columns.Contains("SoBuoi")) 
            {
                dgvSchedule.Columns["SoBuoi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            
            StyleGrid(dgvSchedule);
        }

        private void SetHeader(string colName, string text)
        {
            if (dgvSchedule.Columns.Contains(colName)) dgvSchedule.Columns[colName].HeaderText = text;
        }

        private void StyleGrid(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.ColumnHeadersHeight = 40;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.RowTemplate.Height = 35;
        }
    }
}