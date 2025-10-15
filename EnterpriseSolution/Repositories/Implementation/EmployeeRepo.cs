using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Enterprise.Entities;
using Enterprise.Repositories.Interface;

namespace Enterprise.Repositories.Implementation;

public class EmployeeRepo : IEmployeeRepo
{
    private readonly IConfiguration _configuration;
    private readonly string connection;
    public EmployeeRepo(IConfiguration configuration)
    {
        _configuration = configuration;
        connection = _configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Wrong ConnectionString");
    }

    public async Task<bool> AddEmployee(Employee employee)
    {
        bool status = false;
        string query = " INSERT INTO employees(employeeNumber, lastName, firstName, extension, email, officeCode, reportsTo, jobTitle) " +
                       " Values(@employeenumber, @lastname, @firstname, @extension, @email, @officecode, @reportsto, @jobtitle) ";
        using (MySqlConnection con = new MySqlConnection(connection))
        using (MySqlCommand command = new MySqlCommand(query, con))
        {
            command.Parameters.AddWithValue("@employeenumber", employee.EmployeeNumber);
            command.Parameters.AddWithValue("@lastname", employee.LastName);
            command.Parameters.AddWithValue("@firstname", employee.FirstName);
            command.Parameters.AddWithValue("@extension", employee.Extension);
            command.Parameters.AddWithValue("@email", employee.Email);
            command.Parameters.AddWithValue("@officecode", employee.OfficeCode);
            command.Parameters.AddWithValue("@reportsto", employee.ReportsTo);
            command.Parameters.AddWithValue("@jobtitle", employee.JobTitle);
            try
            {
                await con.OpenAsync();
                int RowAffected = command.ExecuteNonQuery();
                if (RowAffected > 0)
                    status = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        return status;
    }
    public async Task<bool> UpdateEmployee(Employee employee)
    {
        bool status = false;
        string query = " UPDATE employees SET lastName = @lastname, " +
                        " firstName = @firstname, " +
                        " extension = @extension, " +
                        " email = @email, " +
                        " officeCode = @officecode, " +
                        " reportsTo = @reportsto, " +
                        " jobTitle = @jobtitle " +
                        " WHERE employeeNumber = @employeenumber";

        using (MySqlConnection con = new MySqlConnection(connection))
        using (MySqlCommand command = new MySqlCommand(query, con))
        {
            command.Parameters.AddWithValue("@employeenumber", employee.EmployeeNumber);
            command.Parameters.AddWithValue("@lastname", employee.LastName);
            command.Parameters.AddWithValue("@firstname", employee.FirstName);
            command.Parameters.AddWithValue("@extension", employee.Extension);
            command.Parameters.AddWithValue("@email", employee.Email);
            command.Parameters.AddWithValue("@officecode", employee.OfficeCode);
            command.Parameters.AddWithValue("@reportsto", employee.ReportsTo);
            command.Parameters.AddWithValue("@jobtitle", employee.JobTitle);
            try
            {
                await con.OpenAsync();
                int RowAffected = command.ExecuteNonQuery();
                if (RowAffected > 0)
                    status = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        return status;
    }
    public async Task<bool> DeleteEmployee(int id)
    {
        bool status = false;
        // string query = " DELETE FROM employees WHERE employeeNumber = @employeenumber ";
        string query = " SELECT * FROM employees ";
        using (MySqlConnection con = new MySqlConnection(connection))
        using (MySqlCommand command = new MySqlCommand(query, con))
        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
        using (MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter))
        {
            DataSet dataSet = new DataSet();
            try
            {
                // await con.OpenAsync();
                // command.Parameters.AddWithValue("@employeenumber", id);
                // int RowAffected = command.ExecuteNonQuery();
                // if (RowAffected > 0)
                //     status = true;

                await Task.Delay(100);
                adapter.Fill(dataSet);
                {
                    DataTable dataTable = dataSet.Tables[0];
                    DataColumn[] dataColumns = new DataColumn[1];
                    dataColumns[0] = dataTable.Columns["employeeNumber"];
                    dataTable.PrimaryKey = dataColumns;
                    DataRow dataRow = dataTable.Rows.Find(id);
                    dataRow.Delete();
                    int RowAffected = adapter.Update(dataSet);
                    Console.WriteLine("status: ", RowAffected);
                    if (RowAffected > 0)
                        status = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        return status;
    }
    public async Task<List<Employee>> GetAllEmployee()
    {
        string query = " SELECT * FROM employees ";
        List<Employee> employees = new List<Employee>();
        using (MySqlConnection con = new MySqlConnection(connection))
        using (MySqlCommand command = new MySqlCommand(query, con))
        {
            try
            {
                DataSet dataSet = new DataSet();
                await con.OpenAsync();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee emp = new Employee
                        {
                            EmployeeNumber = reader.GetInt32("employeeNumber"),
                            LastName = reader.GetString("lastName"),
                            FirstName = reader.GetString("firstName"),
                            Extension = reader.GetString("extension"),
                            Email = reader.GetString("email"),
                            OfficeCode = reader.GetString("officeCode"),
                            ReportsTo = reader.IsDBNull(reader.GetOrdinal("reportsTo")) ? 0 : reader.GetInt32(reader.GetOrdinal("reportsTo")),
                            JobTitle = reader.GetString("jobTitle")
                        };
                        employees.Add(emp);
                    }
                    return employees;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        return new List<Employee>();
    }
    public async Task<Employee> GetEmployee(int id)
    {
        string query = " SELECT * FROM employees WHERE employeeNumber = @employeenumber ";
        using (MySqlConnection con = new MySqlConnection(connection))
        using (MySqlCommand command = new MySqlCommand(query, con))
        {
            try
            {
                await con.OpenAsync();
                command.Parameters.AddWithValue("@employeenumber", id);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Employee
                        {
                            EmployeeNumber = reader.GetInt32("employeeNumber"),
                            LastName = reader.GetString("lastName"),
                            FirstName = reader.GetString("firstName"),
                            Extension = reader.GetString("extension"),
                            Email = reader.GetString("email"),
                            OfficeCode = reader.GetString("officeCode"),
                            ReportsTo = reader.IsDBNull(reader.GetOrdinal("reportsTo")) ? 0 : reader.GetInt32(reader.GetOrdinal("reportsTo")),
                            JobTitle = reader.GetString("jobTitle")
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return new Employee();
        }
    }

    public async Task<List<Employee>> GetEmployeeByOfficeCode(string ofcCode)
    {
        List<Employee> employees = new List<Employee>();
        string query = "sp_GetEmployeesByOfficeCode";
        MySqlConnection con = new MySqlConnection(connection);
        MySqlCommand command = new MySqlCommand(query, con);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@ofcCode", MySqlDbType.VarChar).Value = ofcCode;
        await Task.Delay(100);
        try
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            DataTable dataTable = dataSet.Tables[0];
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Employee emp = new Employee()
                {
                    EmployeeNumber = Convert.ToInt32(dataRow["employeeNumber"]),
                    LastName = dataRow["lastName"].ToString(),
                    FirstName = dataRow["firstName"].ToString(),
                    Extension = dataRow["extension"].ToString(),
                    Email = dataRow["email"].ToString(),
                    OfficeCode = dataRow["officeCode"].ToString(),
                    ReportsTo = dataRow.IsNull("reportsTo") ? 0 : Convert.ToInt32(dataRow["reportsTo"]),
                    JobTitle = dataRow["jobTitle"].ToString()
                };
                employees.Add(emp);
            }
            return employees;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: ", e.Message);
        }
        return new List<Employee>();
    }
}