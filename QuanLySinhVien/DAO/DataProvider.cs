using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms; // Thêm cái này để dùng MessageBox

namespace QuanLyTrungTam.DAO
{
    public class DataProvider
    {
        private static DataProvider instance;
        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        private DataProvider() { }

        // --- CẤU HÌNH CHUỖI KẾT NỐI ---
        // QUAN TRỌNG: Bạn phải sửa "Data Source" thành tên máy tính của bạn
        // Ví dụ: DESKTOP-ABC\SQLEXPRESS hoặc dấu chấm (.) nếu là bản Full
        private string connectionSTR = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyTrungTam;Integrated Security=True;Encrypt=False";

        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionSTR))
                {
                    connection.Open(); // <-- Lỗi thường xảy ra ở đây nếu sai tên Server
                    SqlCommand command = new SqlCommand(query, connection);
                    AddParameters(query, command, parameter);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(data);
                }
            }
            catch (Exception ex)
            {
                // HIỆN LỖI ĐỂ BIẾT NGUYÊN NHÂN (Sau khi chạy ổn thì comment dòng này lại)
                MessageBox.Show("Lỗi Database: " + ex.Message);
            }
            return data;
        }

        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionSTR))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    AddParameters(query, command, parameter);
                    data = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Database: " + ex.Message);
            }
            return data;
        }

        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionSTR))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    AddParameters(query, command, parameter);
                    data = command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Database: " + ex.Message);
            }
            return data;
        }

        private void AddParameters(string query, SqlCommand command, object[] parameter)
        {
            if (parameter != null)
            {
                string[] listPara = query.Split(new char[] { ' ', '\n', '\r', '\t', ',', '(', ')', '=' }, StringSplitOptions.RemoveEmptyEntries);
                int i = 0;
                foreach (string item in listPara)
                {
                    if (item.Contains("@"))
                    {
                        if (!command.Parameters.Contains(item.Trim()))
                        {
                            if (i < parameter.Length)
                                command.Parameters.AddWithValue(item.Trim(), parameter[i]);
                            i++;
                        }
                    }
                }
            }
        }
    }
}