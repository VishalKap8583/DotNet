using HRWebApp.Entities;
using HRWebApp.Repositories.Interface;
using MySql.Data.MySqlClient;

namespace HRWebApp.Repositories.Implementation
{
    public class EmployeesRepository : IEmployeesRepository
    {
        String ConnectionString = "server=localhost;user=root;password=password;database=employeeslist";

        public async Task<bool> Create(Employee employee)
        {
            bool status = false;
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "INSERT INTO Employees (Name, Position) VALUES (@Name, @Position)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Position", employee.Position);
                    int result = await command.ExecuteNonQueryAsync();
                    if (result > 0)
                    {
                        status = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
            return status;
        }

        public async Task<bool> Delete(int id)
        {
            bool status = false;
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "DELETE FROM Employees WHERE Id = @Id";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    int result = await command.ExecuteNonQueryAsync();
                    if (result>0)
                    {  
                        status = true; 
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
            return status;
        }

        public async Task<Employee> Get(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "SELECT * FROM Employees WHERE Id = @Id";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Employee
                        {
                            Id = reader.GetInt32("Id"),
                            Name = reader.GetString("Name"),
                            Position = reader.GetString("Position")
                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
            return new Employee();
        }

        public async Task<List<Employee>> GetAll()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "SELECT * FROM Employees";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();
                    List<Employee> employees = new List<Employee>();
                    while (reader.Read())
                    {
                       Employee employee = new Employee
                        {
                            Id = reader.GetInt32("Id"),
                            Name = reader.GetString("Name"),
                            Position = reader.GetString("Position")
                        };
                        employees.Add(employee);
                    }
                    return employees;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
            return new List<Employee>();
        }

        public async Task<bool> Update(Employee employee)
        {
            bool status = false;
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = " UPDATE Employees SET Id = @Newid, Name = @Name, Position = @Position WHERE Id = @Id";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", employee.Id);
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Position", employee.Position);
                    int result = await command.ExecuteNonQueryAsync();
                    //status = true;
                    if(result>0)
                    {
                        status = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
            return status;
        }
    }
}
