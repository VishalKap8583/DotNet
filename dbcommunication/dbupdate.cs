using MySql.Data.MySqlClient;
using System;
using System.Data;

class DBupdate
{
    static void Main()
    {
        string ConnectionString = "Server=localhost; Port=3306; Database=sampledb; User=root; Password=password";

        using (MySqlConnection con = new MySqlConnection(ConnectionString))
        {
            try
            {
                con.Open();
                string createTableSql = "CREATE TABLE IF NOT EXISTS users (" +
                "id INT AUTO_INCREMENT PRIMARY KEY," +
                "name VARCHAR(255) NOT NULL," +
                "email VARCHAR(255) NOT NULL)";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        using (MySqlConnection con = new MySqlConnection(ConnectionString))
        {
            try
            {
                con.Open();
                string strCmd = "SELECT * FROM customers";
                using (MySqlCommand cmd = new MySqlCommand(strCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"customer_id: {reader["customer_id"]}, name: {reader["name"]}, email: {reader["email"]}, created_at: {reader["created_at"]}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

    }
}