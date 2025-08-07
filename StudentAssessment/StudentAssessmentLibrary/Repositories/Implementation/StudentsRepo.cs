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
                        String query = "Insert Into Students" +
                                       "(Name, Field)" +
                                       "Values" +
                                       "(@name, @field)";
                        MySqlCommand command = new MySqlCommand(query, con);
                        command.Parameters.AddWithValue("@name", student.Name);
                        command.Parameters.AddWithValue("@field", student.Field);
                        int result = command.ExecuteNonQuery();
                        status = true;
                    }
                }
                catch (Exception ex)
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
        public bool delete(Students Id)
        {
            bool Status = false;
            using (MySqlConnection con = new MySqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    String query = "Delete From Students" +
                                   "Where Id = @Id";
                    MySqlCommand command = new MySqlCommand(query, con);
                    command.Parameters.AddWithValue("@Id", Id);
                    int result = command.ExecuteNonQuery();
                    Status = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: ", ex.Message);
                }
            }
            return Status;
        }

        public bool delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Students> Get(int Id)
        {
            using (MySqlConnection con = new MySqlConnection(ConnectionString))
                try
                {
                    con.Open();
                    string query = "Select *" +
                                   "From students" +
                                   "Where Id=@Id";
                    MySqlCommand command = new MySqlCommand(query, con);
                    command.Parameters.AddWithValue("@Id", Id);
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
                catch (Exception ex)
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
                    string query = "Select *" + 
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
                catch (Exception ex)
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
                    string query = "Update Students" +
                                  "Set Name=@name, Field=@field" +
                                  "Where Id=@Id";
                    MySqlCommand command = new MySqlCommand(query, con);
                    command.Parameters.AddWithValue("@Id", student.Id);
                    command.Parameters.AddWithValue("@name", student.Name);
                    command.Parameters.AddWithValue("@field", student.Field);
                    int result = command.ExecuteNonQuery();
                    Status = true;
                }
                catch (Exception ex)
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
