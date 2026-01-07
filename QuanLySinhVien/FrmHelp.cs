using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyTrungTam
{
    public partial class FrmHelp : Form
    {
        public FrmHelp()
        {
            InitializeComponent();
            LoadShortcuts();
        }

        private void LoadShortcuts()
        {
            // 1. Setup Columns
            dgvShortcuts.Columns.Add("Action", "Chức Năng");
            dgvShortcuts.Columns.Add("Key", "Phím Tắt");
            
            // 2. Load Data
            var shortcuts = QuanLyTrungTam.Utilities.ShortcutManager.Instance.Shortcuts;
            
            foreach (var kvp in shortcuts)
            {
                string actionName = GetFriendlyName(kvp.Key);
                string keyText = new KeysConverter().ConvertToString(kvp.Value);
                dgvShortcuts.Rows.Add(actionName, keyText);
            }

            // 3. Styling
            dgvShortcuts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            dgvShortcuts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvShortcuts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvShortcuts.EnableHeadersVisualStyles = false;
        }

        private string GetFriendlyName(string key)
        {
            switch (key)
            {
                case "Refresh": return "Làm mới / Tải lại dữ liệu";
                case "Save": return "Lưu dữ liệu (Thêm mới)";
                case "Update": return "Cập nhật thông tin (Sửa)";
                case "Delete": return "Xóa dữ liệu";
                case "GrantAccount": return "Cấp tài khoản nhanh (Admin)";
                default: return key;
            }
        }
    }
}
