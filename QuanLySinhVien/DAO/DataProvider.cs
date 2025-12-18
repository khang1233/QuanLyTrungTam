using System;
using System.Data;
using System.Data.SqlClient; // Thư viện để kết nối SQL
using System.Linq;

namespace QuanLyTrungTam.DAO
{
    public class DataProvider
    {
        // Design Pattern Singleton: Chỉ tạo duy nhất 1 instance
        private static DataProvider instance;

        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        private DataProvider() { }

        // --- CHUỖI KẾT NỐI (QUAN TRỌNG NHẤT) ---
        // Đã sửa thành LocalDB theo đúng máy của bạn
        // Lưu ý: Phải giữ nguyên dấu @ ở phía trước dấu ngoặc kép
        private string connectionSTR = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QuanLyTrungTam;Integrated Security=True;Encrypt=False";

        // 1. Hàm chạy câu lệnh SELECT (Lấy dữ liệu dạng bảng)
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
                connection.Close();
            }
            return data;
        }

        // 2. Hàm chạy câu lệnh INSERT, UPDATE, DELETE (Trả về số dòng thành công)
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteNonQuery();
                connection.Close();
            }
            return data;
        }

        // 3. Hàm lấy 1 giá trị duy nhất (Dùng cho COUNT, SUM...)
        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteScalar();
                connection.Close();
            }
            return data;
        }
    }
}