using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QuanLyTrungTam.Utilities
{
    public class ShortcutManager
    {
        private static ShortcutManager instance;
        public static ShortcutManager Instance
        {
            get { if (instance == null) instance = new ShortcutManager(); return instance; }
        }

        // Dictionary lưu trữ KeyMapping: ActionName -> KeyStroke
        public Dictionary<string, Keys> Shortcuts { get; private set; }

        private ShortcutManager()
        {
            Shortcuts = new Dictionary<string, Keys>();
            LoadDefaults();
        }

        // 1. Cài đặt mặc định
        private void LoadDefaults()
        {
            // Có thể load từ file JSON/Settings ở đây
            Shortcuts["Refresh"] = Keys.Control | Keys.N;
            Shortcuts["Save"] = Keys.Control | Keys.S;
            Shortcuts["Update"] = Keys.Control | Keys.U;
            Shortcuts["Delete"] = Keys.Control | Keys.D;
            Shortcuts["GrantAccount"] = Keys.Control | Keys.C;
        }

        // 2. Map lại phím (Dùng cho Form Cài đặt)
        public void Remap(string action, Keys newKey)
        {
            if (Shortcuts.ContainsKey(action))
                Shortcuts[action] = newKey;
            else
                Shortcuts.Add(action, newKey);
        }

        // 3. Kiểm tra phím ấn có khớp hành động không
        public bool IsMatch(string action, Keys keyData)
        {
            if (Shortcuts.ContainsKey(action))
                return Shortcuts[action] == keyData;
            return false;
        }
        
        // 4. Lấy Text hiển thị (VD: "Ctrl+N")
        public string GetShortcutText(string action)
        {
            if (Shortcuts.ContainsKey(action))
                return new KeysConverter().ConvertToString(Shortcuts[action]);
            return "";
        }
    }
}
