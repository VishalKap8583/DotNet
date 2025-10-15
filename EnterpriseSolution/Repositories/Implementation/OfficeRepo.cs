using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Enterprise.Entities;
using Enterprise.Repositories.Interface;
using System.Data;

namespace Enterprise.Repositories.Implementation;

public class OfficeRepo : IOfficeRepo
{
    private readonly IConfiguration _configuration;
    private readonly string connection;
    public OfficeRepo(IConfiguration configuration)
    {
        _configuration = configuration;
        connection = _configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Wrong ConnectionString");
    }
    public async Task<bool> AddOffice(Office office)
    {
        bool status = false;
        string query = " INSERT INTO offices(officeCode, city, phone, addressLine1, addressLine2, state, country, postalCode, territory) " +
                       " Values(@officeCode, @city, @phone, @addressLine1, @addressLine2, @state, @country, @postalCode, @territory) ";
        using (MySqlConnection con = new MySqlConnection(connection))
        using (MySqlCommand command = new MySqlCommand(query, con))
        {
            command.Parameters.AddWithValue("@officeCode", office.OfficeCode);
            command.Parameters.AddWithValue("@city", office.City);
            command.Parameters.AddWithValue("@phone", office.Phone);
            command.Parameters.AddWithValue("@addressLine1", office.AddressLine1);
            command.Parameters.AddWithValue("@addressLine2", office.AddressLine2);
            command.Parameters.AddWithValue("@state", office.State);
            command.Parameters.AddWithValue("@country", office.Country);
            command.Parameters.AddWithValue("@postalCode", office.PostalCode);
            command.Parameters.AddWithValue("@territory", office.Territory);
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
    public async Task<bool> UpdateOffice(Office office)
    {
        bool status = false;
        string query = " UPDATE offices SET city = @city, " +
                        " phone = @phone, " +
                        " addressLine1 = @addressLine1, " +
                        " addressLine2 = @addressLine2, " +
                        " state = @state, " +
                        " country = @country, " +
                        " postalCode = @postalCode " +
                        " territory = @territory " +
                        " WHERE officeCode = @officeCode";

        using (MySqlConnection con = new MySqlConnection(connection))
        using (MySqlCommand command = new MySqlCommand(query, con))
        {
            command.Parameters.AddWithValue("@officeCode", office.OfficeCode);
            command.Parameters.AddWithValue("@city", office.City);
            command.Parameters.AddWithValue("@phone", office.Phone);
            command.Parameters.AddWithValue("@addressLine1", office.AddressLine1);
            command.Parameters.AddWithValue("@addressLine2", office.AddressLine2);
            command.Parameters.AddWithValue("@state", office.State);
            command.Parameters.AddWithValue("@country", office.Country);
            command.Parameters.AddWithValue("@postalCode", office.PostalCode);
            command.Parameters.AddWithValue("@territory", office.Territory);
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
    public async Task<bool> DeleteOffice(string id)
    {
        bool status = false;
        string query = " DELETE FROM offices WHERE officeCode = @officeCode ";
        using (MySqlConnection con = new MySqlConnection(connection))
        using (MySqlCommand command = new MySqlCommand(query, con))
        {
            try
            {
                await con.OpenAsync();
                command.Parameters.AddWithValue("@officeCode", id);
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
    public async Task<List<Office>> GetAllOffice()
    {
        string query = " SELECT * FROM offices ";
        List<Office> offices = new List<Office>();
        using (MySqlConnection con = new MySqlConnection(connection))
        using (MySqlCommand command = new MySqlCommand(query, con))
        {
            try
            {
                await con.OpenAsync();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Office ofc = new Office
                        {
                            OfficeCode = reader.GetString("officeCode"),
                            City = reader.GetString("city"),
                            Phone = reader.GetString("phone"),
                            AddressLine1 = reader.GetString("addressLine1"),
                            AddressLine2 = reader.IsDBNull(reader.GetOrdinal("addressLine2")) ? null : reader.GetString(reader.GetOrdinal("addressLine2")),
                            State = reader.IsDBNull(reader.GetOrdinal("country")) ? null : reader.GetString(reader.GetOrdinal("country")),
                            Country = reader.GetString("country"),
                            PostalCode = reader.GetString("postalCode"),
                            Territory = reader.GetString("territory")
                        };
                        offices.Add(ofc);
                    }
                    return offices;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        return new List<Office>();
    }
    public async Task<Office> GetOffice(string id)
    {
        string query = " SELECT * FROM offices WHERE officeCode = @officeCode ";
        using (MySqlConnection con = new MySqlConnection(connection))
        using (MySqlCommand command = new MySqlCommand(query, con))
        {
            try
            {
                await con.OpenAsync();
                command.Parameters.AddWithValue("@officeCode", id);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Office
                        {
                            OfficeCode = reader.GetString("officeCode"),
                            City = reader.GetString("city"),
                            Phone = reader.GetString("phone"),
                            AddressLine1 = reader.GetString("addressLine1"),
                            AddressLine2 = reader.IsDBNull(reader.GetOrdinal("addressLine2")) ? null : reader.GetString(reader.GetOrdinal("addressLine2")),
                            State = reader.IsDBNull(reader.GetOrdinal("country")) ? null : reader.GetString(reader.GetOrdinal("country")),
                            Country = reader.GetString("country"),
                            PostalCode = reader.GetString("postalCode"),
                            Territory = reader.GetString("territory")
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return new Office();
        }
    }
}