using MySql.Data.MySqlClient;
using System.Data;

String ConnectionString ="Server=localhost;Port=3306; Database=employeeslist;User ID=root;Password=password;";

MySqlConnection connection = new MySqlConnection(ConnectionString);
    try
    {
        connection.Open();
        MySqlCommand cmd = new MySqlCommand("SELECT * FROM employees", connection);
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine($"ID: {reader["ID"]}, Name: {reader["Name"]}, Position: {reader["Position"]}");
        }
    }
    catch (MySqlException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    finally
    {
        connection.Close();
    }


