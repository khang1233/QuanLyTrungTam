using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyTrungTam.Utilities
{
    public static class GridViewHelper
    {
        // Define a struct/class for Column Config
        public class ColumnConfig
        {
            public string HeaderText { get; set; }
            public DataGridViewContentAlignment Alignment { get; set; } = DataGridViewContentAlignment.MiddleLeft;
            public string Format { get; set; } = "";
            public bool Visible { get; set; } = true;
            public float FillWeight { get; set; } = 100;
        }

        // Global Dictionary Mapping
        private static readonly Dictionary<string, ColumnConfig> _columnMap = new Dictionary<string, ColumnConfig>(StringComparer.OrdinalIgnoreCase)
        {
            // --- HỌC VIÊN ---
            { "MaHV", new ColumnConfig { HeaderText = "Mã Học Viên", Alignment = DataGridViewContentAlignment.MiddleLeft, FillWeight = 80 } },
            { "HoTen", new ColumnConfig { HeaderText = "Họ và Tên", Alignment = DataGridViewContentAlignment.MiddleLeft, FillWeight = 150 } },
            { "NgaySinh", new ColumnConfig { HeaderText = "Ngày Sinh", Alignment = DataGridViewContentAlignment.MiddleCenter, Format = "dd/MM/yyyy", FillWeight = 100 } },
            { "GioiTinh", new ColumnConfig { HeaderText = "Giới Tính", Alignment = DataGridViewContentAlignment.MiddleCenter, FillWeight = 80 } },
            { "SDT", new ColumnConfig { HeaderText = "Số Điện Thoại", Alignment = DataGridViewContentAlignment.MiddleCenter, FillWeight = 100 } },
            { "Email", new ColumnConfig { HeaderText = "Email", Alignment = DataGridViewContentAlignment.MiddleLeft, FillWeight = 150 } },
            { "DiaChi", new ColumnConfig { HeaderText = "Địa Chỉ", Alignment = DataGridViewContentAlignment.MiddleLeft, FillWeight = 200 } },
            { "TrangThai", new ColumnConfig { HeaderText = "Trạng Thái", Alignment = DataGridViewContentAlignment.MiddleCenter, FillWeight = 100 } },
            { "NgayGiaNhap", new ColumnConfig { HeaderText = "Ngày Gia Nhập", Alignment = DataGridViewContentAlignment.MiddleCenter, Format = "dd/MM/yyyy" } },

            // --- NHÂN SỰ ---
            { "MaNS", new ColumnConfig { HeaderText = "Mã Nhân Sự", FillWeight=80 } },
            { "LoaiNS", new ColumnConfig { HeaderText = "Chức Vụ", Alignment = DataGridViewContentAlignment.MiddleCenter } },
            { "ChuyenNganh", new ColumnConfig { HeaderText = "Chuyên Ngành" } },
            
            // --- MÔN HỌC (KỸ NĂNG) ---
            { "MaKyNang", new ColumnConfig { HeaderText = "Mã Môn", FillWeight = 80 } },
            { "TenKyNang", new ColumnConfig { HeaderText = "Tên Môn Học", FillWeight = 150 } },
            { "HinhThuc", new ColumnConfig { HeaderText = "Hình Thức", Alignment = DataGridViewContentAlignment.MiddleCenter } },
            { "SoBuoi", new ColumnConfig { HeaderText = "Số Buổi", Alignment = DataGridViewContentAlignment.MiddleCenter } },
            { "DonGia", new ColumnConfig { HeaderText = "Đơn Giá", Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" } },
            { "HocPhi", new ColumnConfig { HeaderText = "Học Phí", Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" } },
            { "MoTa", new ColumnConfig { HeaderText = "Mô Tả", Visible = false } }, // Ẩn mô tả dài

            // --- LỚP HỌC ---
            { "MaLop", new ColumnConfig { HeaderText = "Mã Lớp", FillWeight = 80 } },
            { "TenLop", new ColumnConfig { HeaderText = "Tên Lớp Học", FillWeight = 150 } },
            { "Thu", new ColumnConfig { HeaderText = "Lịch Học", Alignment = DataGridViewContentAlignment.MiddleCenter } },
            { "CaHoc", new ColumnConfig { HeaderText = "Ca Học", Alignment = DataGridViewContentAlignment.MiddleCenter } },
            { "SiSoToiDa", new ColumnConfig { HeaderText = "Sĩ Số", Alignment = DataGridViewContentAlignment.MiddleRight, FillWeight = 60 } },
            { "NgayBatDau", new ColumnConfig { HeaderText = "Ngày Bắt Đầu", Alignment = DataGridViewContentAlignment.MiddleCenter, Format = "dd/MM/yyyy" } },
            { "NgayKetThuc", new ColumnConfig { HeaderText = "Ngày Kết Thúc", Alignment = DataGridViewContentAlignment.MiddleCenter, Format = "dd/MM/yyyy" } },
            { "MaPhong", new ColumnConfig { HeaderText = "Mã Phòng", Visible = false } },
            { "TenPhong", new ColumnConfig { HeaderText = "Phòng Học", Alignment = DataGridViewContentAlignment.MiddleCenter } },
            { "MaGiaoVien", new ColumnConfig { HeaderText = "Mã GV", Visible = false } },
            { "TenGV", new ColumnConfig { HeaderText = "Giáo Viên" } },
            { "TenTG", new ColumnConfig { HeaderText = "Trợ Giảng" } },

            // --- TÀI CHÍNH / HỌC PHÍ ---
            { "TrangThaiHocPhi", new ColumnConfig { HeaderText = "Tình Trạng", Alignment = DataGridViewContentAlignment.MiddleCenter, FillWeight = 120 } },
            { "HocPhiLop", new ColumnConfig { HeaderText = "Học Phí", Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" } },
            { "NgayDangKy", new ColumnConfig { HeaderText = "Ngày ĐK", Alignment = DataGridViewContentAlignment.MiddleCenter, Format = "dd/MM/yyyy HH:mm" } },
            { "SoTienDong", new ColumnConfig { HeaderText = "Số Tiền", Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" } },
            { "HinhThucThanhToan", new ColumnConfig { HeaderText = "Hình Thức", Alignment = DataGridViewContentAlignment.MiddleCenter } },
            
            // --- CÁC CỘT RÁC CẦN ẨN ---
            { "sobuoifake", new ColumnConfig { Visible = false } },
            { "Password", new ColumnConfig { Visible = false } },
             { "MatKhau", new ColumnConfig { Visible = false } },
        };

        /// <summary>
        /// Main function to standardize DataGridView
        /// </summary>
        public static void StandardizeGrid(DataGridView dgv, List<string> hiddenColumns = null)
        {
            if (dgv == null) return;

            // 1. Basic Style
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ReadOnly = true; 
            dgv.EnableHeadersVisualStyles = false;
            
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243); // Blue Theme
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold); // Bold Header
            dgv.ColumnHeadersHeight = 40;

            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.RowTemplate.Height = 35;

            // 2. Column Mapping
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                // Check Global Map
                if (_columnMap.ContainsKey(col.Name) || _columnMap.ContainsKey(col.DataPropertyName))
                {
                    string key = _columnMap.ContainsKey(col.Name) ? col.Name : col.DataPropertyName;
                    var config = _columnMap[key];

                    col.HeaderText = config.HeaderText;
                    col.Visible = config.Visible;
                    
                    if (config.Visible)
                    {
                        col.DefaultCellStyle.Alignment = config.Alignment;
                        if (!string.IsNullOrEmpty(config.Format))
                        {
                            col.DefaultCellStyle.Format = config.Format;
                        }
                        // Only set FillWeight if needed, usually allow AutoSize to handle
                        // col.FillWeight = config.FillWeight; 
                    }
                }
            }

            // 3. Hide specific extra columns if passed
            if (hiddenColumns != null)
            {
                foreach (string cName in hiddenColumns)
                {
                    if (dgv.Columns.Contains(cName)) dgv.Columns[cName].Visible = false;
                }
            }
        }
    }
}
