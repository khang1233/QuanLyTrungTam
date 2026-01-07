using QuanLyTrungTam.BUS;
using QuanLyTrungTam.DAO;
using QuanLyTrungTam.DTO;
using QuanLyTrungTam.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyTrungTam
{
    public partial class FrmDiemDanh : Form
    {
        public FrmDiemDanh()
        {
            InitializeComponent();
            
            // Shortcuts
            this.KeyPreview = true;
            
            SetPlaceholder(txbSearch, "Tìm kiếm học viên...");
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
                BtnLoad_Click(null, null);
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }

        private void LoadClasses()
        {
            DataTable dt;
            try
            {
                if (UserSession.Instance.IsAdmin())
                {
                    dt = LopHocBUS.Instance.GetAllLop(); 
                }
                else
                {
                    string maNS = UserSession.Instance.CurrentUser.MaNguoiDung;
                    dt = LopHocDAO.Instance.GetLopByNhanSu(maNS); 
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    cbLop.DisplayMember = "TenLop";
                    cbLop.ValueMember = "MaLop";
                    cbLop.DataSource = dt;
                }
                else
                {
                    cbLop.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lớp học: " + ex.Message);
            }
        }

        private void CbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbLop.SelectedValue == null) return;

                string maLop = "";
                if (cbLop.SelectedValue is DataRowView drv) maLop = drv["MaLop"].ToString();
                else maLop = cbLop.SelectedValue.ToString();

                DataRow info = LopHocDAO.Instance.GetClassScheduleInfo(maLop);

                if (info != null)
                {
                    DateTime startDate = Convert.ToDateTime(info["NgayBatDau"]);
                    string scheduleStr = info["Thu"].ToString();

                    int totalSessions = 12;
                    if (info.Table.Columns.Contains("SoBuoi") && info["SoBuoi"] != DBNull.Value)
                    {
                        totalSessions = Convert.ToInt32(info["SoBuoi"]);
                    }

                    List<BuoiHocDTO> listBuoi = ScheduleHelper.GenerateSchedule(startDate, scheduleStr, totalSessions);

                    cbBuoi.DataSource = listBuoi;
                    cbBuoi.DisplayMember = "HienThi";
                    cbBuoi.ValueMember = "Ngay";
                }
                else
                {
                    cbBuoi.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch học: " + ex.Message);
            }
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            if (cbLop.SelectedValue == null || cbBuoi.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Lớp và Buổi học!");
                return;
            }

            try
            {
                string maLop = "";
                DataRowView drv = cbLop.SelectedValue as DataRowView;
                if (drv != null) maLop = drv["MaLop"].ToString();
                else maLop = cbLop.SelectedValue.ToString();

                DateTime ngayDiemDanh = (DateTime)cbBuoi.SelectedValue;

                // Load Data
                DataTable dt = DiemDanhBUS.Instance.GetDiemDanhList(maLop, ngayDiemDanh);
                dgvDiemDanh.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message);
            }
        }

        private void TxbSearch_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = dgvDiemDanh.DataSource as DataTable;
            if (dt != null)
            {
                string k = txbSearch.Text;
                if (k == "Tìm kiếm học viên...") k = "";
                string safeKey = k.Replace("'", "''");
                dt.DefaultView.RowFilter = string.Format("MaHV LIKE '%{0}%' OR HoTen LIKE '%{0}%'", safeKey);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (dgvDiemDanh.Rows.Count == 0) return;

            // --- Finish Edit ---
            dgvDiemDanh.EndEdit();

            try
            {
                string maLop = "";
                DataRowView drv = cbLop.SelectedValue as DataRowView;
                if (drv != null) maLop = drv["MaLop"].ToString();
                else maLop = cbLop.SelectedValue.ToString();

                DateTime ngay = (DateTime)cbBuoi.SelectedValue;

                int count = 0;
                foreach (DataGridViewRow row in dgvDiemDanh.Rows)
                {
                    string maHV = row.Cells["colMaHV"].Value.ToString();

                    bool coMat = false;
                    if (row.Cells["colCoMat"].Value != null && row.Cells["colCoMat"].Value != DBNull.Value)
                    {
                        var val = row.Cells["colCoMat"].Value;
                        if (val is bool) coMat = (bool)val;
                        else if (val.ToString() == "1" || val.ToString().ToLower() == "true") coMat = true;
                    }

                    string lyDo = "";
                    if (row.Cells["colLyDo"].Value != null) lyDo = row.Cells["colLyDo"].Value.ToString();

                    if (DiemDanhBUS.Instance.SaveDiemDanh(maLop, maHV, ngay, coMat, lyDo)) count++;
                }
                MessageBox.Show(string.Format("Đã lưu điểm danh cho buổi ngày {0:dd/MM/yyyy}!", ngay), "Thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message);
            }
        }

        private void SetPlaceholder(TextBox t, string h)
        {
            t.Tag = h;
            t.Text = h; t.ForeColor = Color.Gray;
            t.Enter += (s, e) => { if (t.Text == h) { t.Text = ""; t.ForeColor = Color.Black; } };
            t.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(t.Text)) { t.Text = h; t.ForeColor = Color.Gray; } };
        }
    }
}
