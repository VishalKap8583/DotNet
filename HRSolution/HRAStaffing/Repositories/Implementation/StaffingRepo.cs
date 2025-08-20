using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRAStaffing.Entities;
using HRAStaffing.Repositories.Interface;
using MySql.Data.MySqlClient;

namespace HRAStaffing.Repositories.Implementation
{
    public class StaffingRepo : IStaffingRepo
    {
        String ConnectionString = "server=localhost;user=root;password=password;database=employeeslist";
        public bool Create(Employee employee)
        {
            bool status = false;
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Employees (Name, Position) VALUES (@Name, @Position)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Position", employee.Position);

                    int result = command.ExecuteNonQuery();
                    status = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return status;
        }

        public bool Delete(int employeeId)
        {
            bool status = false;
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Employees WHERE Id = @Id";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", employeeId);

                    int result = command.ExecuteNonQuery();
                    status = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return status;
        }

        public Employee Get(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
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
                    connection.Close();
                }
            }
            return new Employee();
        }

        public List<Employee> GetAll()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
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
                    connection.Close();
                }
            }
            return new List<Employee>();

        }

        public bool Update(Employee employee)
        {
            bool status = false;
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Employees SET Name = @Name, Position = @Position WHERE Id = @Id";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", employee.Id);
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Position", employee.Position);
                    int result = command.ExecuteNonQuery();
                    status = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return status;
        }
    }
}
