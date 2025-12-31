using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public class DataProvider
{
    private string connectionSTR = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyTrungTam;Integrated Security=True;Encrypt=False";

    // Helper: extract parameter names in order of appearance (@name)
    private static string[] ExtractParameterNames(string sql)
    {
        var matches = Regex.Matches(sql, @"@[\w]+");
        var list = new System.Collections.Generic.List<string>();
        foreach (Match m in matches)
        {
            string name = m.Value;
            if (!list.Contains(name)) list.Add(name);
        }
        return list.ToArray();
    }

    public object ExecuteScalar(string query, object[] parameter = null)
    {
        object data = null;
        using (var connection = new SqlConnection(connectionSTR))
        using (var command = new SqlCommand(query, connection))
        {
            if (parameter != null)
            {
                var paramNames = ExtractParameterNames(query);
                for (int i = 0; i < paramNames.Length && i < parameter.Length; i++)
                {
                    command.Parameters.AddWithValue(paramNames[i], parameter[i] ?? DBNull.Value);
                }
            }
            connection.Open();
            data = command.ExecuteScalar();
        }
        return data;
    }

    public int ExecuteNonQuery(string query, object[] parameter = null)
    {
        int rows = 0;
        using (var connection = new SqlConnection(connectionSTR))
        using (var command = new SqlCommand(query, connection))
        {
            if (parameter != null)
            {
                var paramNames = ExtractParameterNames(query);
                for (int i = 0; i < paramNames.Length && i < parameter.Length; i++)
                {
                    command.Parameters.AddWithValue(paramNames[i], parameter[i] ?? DBNull.Value);
                }
            }
            connection.Open();
            rows = command.ExecuteNonQuery();
        }
        return rows;
    }
}