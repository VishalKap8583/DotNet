using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentAssessmentLibrary.Entities;
using StudentAssessmentLibrary.Repositories.Interface;
using System.Linq.Expressions;

namespace StudentAssessmentLibrary.Repositories.Implementation
{
    public class StudentsRepo : IStudentRepo
    {
        String ConnectionString = "server=localhost; port=3306; database=studentdb; user=root; password=password";

        public bool Create(Students student)
        {
            bool status = false;
            using (MySqlConnection con = new MySqlConnection(ConnectionString))
            {
                try
                {
                    {
                        con.Open();
                        String query = "INSERT INTO students" +
                                       "(Id, Name, Field, Email, Address)" +
                                       "Values" +
                                       "(@id, @name, @field, @email, @address)";
                        MySqlCommand command = new MySqlCommand(query, con);
                        command.Parameters.AddWithValue("@id", student.Id);
                        command.Parameters.AddWithValue("@name", student.Name);
                        command.Parameters.AddWithValue("@field", student.Field);
                        command.Parameters.AddWithValue("@email", student.Email);
                        command.Parameters.AddWithValue("@address", student.Address);
                        int result = command.ExecuteNonQuery();
                        status = true;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: ", ex.Message);
                }
                finally
                {
                    con.Close();
                }
                return status;
            }
        }
        public bool Delete(int id)
        {
            bool Status = false;
            using (MySqlConnection con = new MySqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    String query = "DELETE FROM students " +
                                   "WHERE Id = @id";
                    MySqlCommand command = new MySqlCommand(query, con);
                    command.Parameters.AddWithValue("@Id", id);
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Status = true;
                        Console.WriteLine("Record is deleted...");

                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: "+ ex.Message);
                }

                finally
                {
                    con.Close();
                }
            }
            return Status;
        }

        public List<Students> Get(int id)
        {
            using (MySqlConnection con = new MySqlConnection(ConnectionString))
                try
                {
                    con.Open();
                    string query = "SELECT * " +
                                   "FROM students " +
                                   "WHERE Id=@id";
                    MySqlCommand command = new MySqlCommand(query, con);
                    command.Parameters.AddWithValue("@Id", id);
                    MySqlDataReader reader = command.ExecuteReader();
                    List<Students> students = new List<Students>();
                    while (reader.Read())
                    {
                        Students student = new Students
                        {
                            Id = reader.GetInt32("Id"),
                            Name = reader.GetString("Name"),
                            Field = reader.GetString("Field")
                        };
                        students.Add(student);
                    }
                    return students;
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("error: ", ex.Message);
                }
                finally
                {
                    con.Close();
                }
            return new List<Students>();
        }

        public List<Students> Getall()
        {
            using (MySqlConnection con = new MySqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    string query = "Select * " + 
                                   "from students";
                    MySqlCommand command = new MySqlCommand(query, con);
                    MySqlDataReader reader = command.ExecuteReader();
                    List<Students> students = new List<Students>();
                    while (reader.Read())
                    {
                        Students student = new Students
                        {
                            Id = reader.GetInt32("Id"),
                            Name = reader.GetString("name"),
                            Field = reader.GetString("field")
                        };
                        students.Add(student);
                    }
                    return students;
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: ", ex.Message);                    
                }
                finally
                {
                    con.Close();
                }
                return new List<Students>();
            }         
       }

        public bool Update(Students student)
        {
            bool Status = false;
            using (MySqlConnection con = new MySqlConnection(ConnectionString))
                try
                {
                    con.Open();
                    string query = "UPDATE students " +
                                   "SET Name=@name, Field=@field, Email=@email, Address=@address " +
                                   "WHERE Id=@id";
                    MySqlCommand command = new MySqlCommand(query, con);
                    command.Parameters.AddWithValue("@id", student.Id);
                    command.Parameters.AddWithValue("@name", student.Name);
                    command.Parameters.AddWithValue("@field", student.Field);
                    command.Parameters.AddWithValue("@email", student.Email);
                    command.Parameters.AddWithValue("@address", student.Address);
                    int result = command.ExecuteNonQuery();
                    Status = true;
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: ", ex.Message);
                }
                finally
                {
                    con.Close();
                }
            return Status;
        }
    }
}
