    using System.Configuration;
    using System.Data.SqlClient;

    public class Database
    {
        public SqlConnection GetConnection()   // ← đổi protected thành public
        {
            string connStr = ConfigurationManager.ConnectionStrings["QLSV"].ConnectionString;
            return new SqlConnection(connStr);
        }
    }

