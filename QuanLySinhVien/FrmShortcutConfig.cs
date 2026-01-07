using QuanLyTrungTam.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyTrungTam
{
    public partial class FrmShortcutConfig : Form
    {
        private Dictionary<string, Keys> tempShortcuts;

        public FrmShortcutConfig()
        {
            InitializeComponent();
            
            this.KeyPreview = true;
            
            // Clone current shortcuts to temp
            tempShortcuts = new Dictionary<string, Keys>(ShortcutManager.Instance.Shortcuts);
            
            SetupGrid();
            LoadData();

            btnSave.Click += BtnSave_Click;
            btnReset.Click += BtnReset_Click;
        }

        private void SetupGrid()
        {
            dgvShortcuts.Columns.Add("Action", "Chức Năng");
            dgvShortcuts.Columns.Add("Key", "Phím Tắt");
            
            dgvShortcuts.Columns["Action"].ReadOnly = true;
            dgvShortcuts.Columns["Key"].ReadOnly = true; // Use event to capture key
            
            dgvShortcuts.BackgroundColor = Color.White;
            dgvShortcuts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvShortcuts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvShortcuts.AllowUserToAddRows = false;
            
            dgvShortcuts.KeyDown += DgvShortcuts_KeyDown;
        }

        private void LoadData()
        {
            dgvShortcuts.Rows.Clear();
            foreach (var kvp in tempShortcuts)
            {
                string keyText = new KeysConverter().ConvertToString(kvp.Value);
                dgvShortcuts.Rows.Add(kvp.Key, keyText);
            }
        }

        private void DgvShortcuts_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgvShortcuts.CurrentRow == null) return;
            
             // Ignore modifiers alone
            if (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.Menu || e.Alt) return;

            string action = dgvShortcuts.CurrentRow.Cells["Action"].Value.ToString();
            Keys newKey = e.KeyData;

            // Update temp
            if (tempShortcuts.ContainsKey(action))
            {
                tempShortcuts[action] = newKey;
                dgvShortcuts.CurrentRow.Cells["Key"].Value = new KeysConverter().ConvertToString(newKey);
            }
            
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            foreach (var kvp in tempShortcuts)
            {
                ShortcutManager.Instance.Remap(kvp.Key, kvp.Value);
            }
            MessageBox.Show("Đã lưu cấu hình phím tắt!");
            this.Close();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn khôi phục mặc định?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Logic to reset? ShortcutManager might need a Reset method.
                // For now just warn user or manual reset.
                // Assuming defaults are not stored separate. 
                // Skip for now or implement if needed.
                MessageBox.Show("Chức năng reset chưa được hỗ trợ sâu (chưa lưu default).");
            }
        }
    }
}
