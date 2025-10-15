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

public class CustomerRepo : ICustomerRepo
{
    private readonly IConfiguration _configuration;
    private readonly string connection;
    public CustomerRepo(IConfiguration configuration)
    {
        _configuration = configuration;
        connection = _configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Wrong ConnectionString");
    }
    public async Task<bool> AddCustomer(Customer customer)
    {
        bool status = false;
        string query = " INSERT INTO customers(customerNumber, customerName, contactLastName, contactFirstName, phone, addressLine1, addressLine2, city, state, postalCode, country, salesRepEmployeeNumber, creditLimit) " +
                       " Values(@customerNumber, @customerName, @contactLastName, @contactFirstName, @phone, @addressLine1, @addressLine2, @city, @state, @postalCode, @country, @salesRepEmployeeNumber, @creditLimit) ";
        using (MySqlConnection con = new MySqlConnection(connection))
        using (MySqlCommand command = new MySqlCommand(query, con))
        {
            command.Parameters.AddWithValue("@customerNumber", customer.CustomerNumber);
            command.Parameters.AddWithValue("@customerName", customer.CustomerName);
            command.Parameters.AddWithValue("@contactLastName", customer.ContactLastName);
            command.Parameters.AddWithValue("@contactFirstName", customer.ContactFirstName);
            command.Parameters.AddWithValue("@phone", customer.Phone);
            command.Parameters.AddWithValue("@addressLine1", customer.AddressLine1);
            command.Parameters.AddWithValue("@addressLine2", customer.AddressLine2);
            command.Parameters.AddWithValue("@city", customer.City);
            command.Parameters.AddWithValue("@state", customer.State);
            command.Parameters.AddWithValue("@postalCode", customer.PostalCode);
            command.Parameters.AddWithValue("@country", customer.Country);
            command.Parameters.AddWithValue("@salesRepEmployeeNumber", customer.SalesRepEmployeeNumber);
            command.Parameters.AddWithValue("@creditLimit", customer.CreditLimit);
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
    public async Task<bool> UpdateCustomer(Customer customer)
    {
        bool status = false;
        string query = " UPDATE customers SET customerName = @customerName, " +
                        " contactLastName = @contactLastName, " +
                        " contactFirstName =  @contactFirstName, " +
                        " phone =  @phone, addressLine1 = @addressLine1, " +
                        " addressLine2 =  @addressLine2, " +
                        " city = @city, " +
                        " state = @state, " +
                        " postalCode = @postalCode, " +
                        " country = @country, " +
                        " salesRepEmployeeNumber = @salesRepEmployeeNumber, " +
                        " creditLimit = @creditLimit ";
        using (MySqlConnection con = new MySqlConnection(connection))
        using (MySqlCommand command = new MySqlCommand(query, con))
        {
            command.Parameters.AddWithValue("@customerNumber", customer.CustomerNumber);
            command.Parameters.AddWithValue("@customerName", customer.CustomerName);
            command.Parameters.AddWithValue("@contactLastName", customer.ContactLastName);
            command.Parameters.AddWithValue("@contactFirstName", customer.ContactFirstName);
            command.Parameters.AddWithValue("@phone", customer.Phone);
            command.Parameters.AddWithValue("@addressLine1", customer.AddressLine1);
            command.Parameters.AddWithValue("@addressLine2", customer.AddressLine2);
            command.Parameters.AddWithValue("@city", customer.City);
            command.Parameters.AddWithValue("@state", customer.State);
            command.Parameters.AddWithValue("@postalCode", customer.PostalCode);
            command.Parameters.AddWithValue("@country", customer.Country);
            command.Parameters.AddWithValue("@salesRepEmployeeNumber", customer.SalesRepEmployeeNumber);
            command.Parameters.AddWithValue("@creditLimit", customer.CreditLimit);
            try
            {
                await con.OpenAsync();
                int RowAffected = command.ExecuteNonQuery();
                if (RowAffected > 0)
                    status = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        return status;
    }
    public async Task<bool> DeleteCustomer(int id)
    {
        bool status = false;
        string query = " DELETE FROM customers WHERE customerNumber = @customerNumber ";
        using (MySqlConnection con = new MySqlConnection(connection))
        using (MySqlCommand command = new MySqlCommand(query, con))
        {
            try
            {
                await con.OpenAsync();
                command.Parameters.AddWithValue("@customerNumber", id);
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
    public async Task<List<Customer>> GetAllCustomer()
    {
        string query = " SELECT * FROM customers ";
        List<Customer> customers = new List<Customer>();
        using (MySqlConnection con = new MySqlConnection(connection))
        using (MySqlCommand command = new MySqlCommand(query, con))
        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
        {
            DataSet dataSet = new DataSet();
            try
            {
                await Task.Delay(100);
                // await con.OpenAsync();
                // using (MySqlDataReader reader = command.ExecuteReader())
                adapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        Customer customer = new Customer
                        {
                            CustomerNumber = Convert.ToInt32(dataRow["customerNumber"]),
                            CustomerName = dataRow["customerName"].ToString(),
                            ContactLastName = dataRow["contactLastName"].ToString(),
                            ContactFirstName = dataRow["contactFirstName"].ToString(),
                            Phone = dataRow["phone"].ToString(),
                            AddressLine1 = dataRow["addressLine1"].ToString(),
                            AddressLine2 = dataRow.IsNull("addressLine2") ? null : dataRow["addressLine2"].ToString(),
                            City = dataRow["city"].ToString(),
                            State = dataRow.IsNull("state") ? null : dataRow["state"].ToString(),
                            PostalCode = dataRow.IsNull("postalCode") ? null : dataRow["postalCode"].ToString(),
                            Country = dataRow["country"].ToString(),
                            SalesRepEmployeeNumber = dataRow.IsNull("salesRepEmployeeNumber") ? 0 : Convert.ToInt32(dataRow["salesRepEmployeeNumber"].ToString()),
                            CreditLimit = dataRow.IsNull("creditLimit") ? 0 : Convert.ToDecimal(dataRow["creditLimit"])
                        };
                        customers.Add(customer);
                    }
                    // while (reader.Read())
                    // {
                    //     Customer customer = new Customer
                    //     {
                    //         CustomerNumber = reader.GetInt32("customerNumber"),
                    //         CustomerName = reader.GetString("customerName"),
                    //         ContactLastName = reader.GetString("contactLastName"),
                    //         ContactFirstName = reader.GetString("contactFirstName"),
                    //         Phone = reader.GetString("phone"),
                    //         AddressLine1 = reader.GetString("addressLine1"),
                    //         AddressLine2 = reader.IsDBNull(reader.GetOrdinal("addressLine2")) ? " " : reader.GetString(reader.GetOrdinal("addressLine2")),
                    //         City = reader.GetString("city"),
                    //         State = reader.IsDBNull(reader.GetOrdinal("state")) ? " " : reader.GetString(reader.GetOrdinal("state")),
                    //         PostalCode = reader.IsDBNull(reader.GetOrdinal("postalCode")) ? " " : reader.GetString(reader.GetOrdinal("postalCode")),
                    //         Country = reader.GetString("country"),
                    //         SalesRepEmployeeNumber = reader.IsDBNull(reader.GetOrdinal("salesRepEmployeeNumber")) ? 0 : reader.GetInt32(reader.GetOrdinal("salesRepEmployeeNumber")),
                    //         CreditLimit = reader.IsDBNull(reader.GetOrdinal("creditLimit")) ? 0 : reader.GetDecimal(reader.GetOrdinal("creditLimit"))
                    //     };
                    //     customers.Add(customer);
                    // }
                    return customers;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        return new List<Customer>();
    }
    public async Task<Customer> GetCustomer(int id)
    {
        string query = " SELECT * FROM customers WHERE customerNumber = @customerNumber";
        using (MySqlConnection con = new MySqlConnection(connection))
        using (MySqlCommand command = new MySqlCommand(query, con))
        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
        {
            DataSet dataSet = new DataSet();
            try
            {
                // await con.OpenAsync();
                await Task.Delay(100);
                command.Parameters.AddWithValue("@customerNumber", id);
                adapter.Fill(dataSet);
                // using (MySqlDataReader reader = command.ExecuteReader())
                {
                    DataTable dataTable = dataSet.Tables[0];
                    DataColumn[] dataColumns = new DataColumn[1];
                    dataColumns[0] = dataTable.Columns["customerNumber"];
                    dataTable.PrimaryKey = dataColumns;
                    DataRow dataRow = dataTable.Rows.Find(id);
                    // if (reader.Read())
                    {
                        // return new Customer
                        // {
                        //     CustomerNumber = reader.GetInt32("customerNumber"),
                        //     CustomerName = reader.GetString("customerName"),
                        //     ContactLastName = reader.GetString("contactLastName"),
                        //     ContactFirstName = reader.GetString("contactFirstName"),
                        //     Phone = reader.GetString("phone"),
                        //     AddressLine1 = reader.GetString("addressLine1"),
                        //     AddressLine2 = reader.IsDBNull(reader.GetOrdinal("addressLine2")) ? " " : reader.GetString(reader.GetOrdinal("addressLine2")),
                        //     City = reader.GetString("city"),
                        //     State = reader.IsDBNull(reader.GetOrdinal("state")) ? " " : reader.GetString(reader.GetOrdinal("state")),
                        //     PostalCode = reader.IsDBNull(reader.GetOrdinal("postalCode")) ? " " : reader.GetString(reader.GetOrdinal("postalCode")),
                        //     Country = reader.GetString("country"),
                        //     SalesRepEmployeeNumber = reader.IsDBNull(reader.GetOrdinal("salesRepEmployeeNumber")) ? 0 : reader.GetInt32(reader.GetOrdinal("salesRepEmployeeNumber")),
                        //     CreditLimit = reader.IsDBNull(reader.GetOrdinal("creditLimit")) ? 0 : reader.GetDecimal(reader.GetOrdinal("creditLimit"))
                        // };
                        return new Customer
                        {
                            CustomerNumber = Convert.ToInt32(dataRow["customerNumber"]),
                            CustomerName = dataRow["customerName"].ToString(),
                            ContactLastName = dataRow["contactLastName"].ToString(),
                            ContactFirstName = dataRow["contactFirstName"].ToString(),
                            Phone = dataRow["phone"].ToString(),
                            AddressLine1 = dataRow["addressLine1"].ToString(),
                            AddressLine2 = dataRow.IsNull("addressLine2") ? null : dataRow["addressLine2"].ToString(),
                            City = dataRow["city"].ToString(),
                            State = dataRow.IsNull("state") ? null : dataRow["state"].ToString(),
                            PostalCode = dataRow.IsNull("postalCode") ? null : dataRow["postalCode"].ToString(),
                            Country = dataRow["country"].ToString(),
                            SalesRepEmployeeNumber = dataRow.IsNull("salesRepEmployeeNumber") ? 0 : Convert.ToInt32(dataRow["salesRepEmployeeNumber"]),
                            CreditLimit = dataRow.IsNull("creditLimit") ? 0 : Convert.ToDecimal(dataRow["creditLimit"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        return new Customer();
    }

    public async Task<Customer> GetCustomerByCheck(string checkNum)
    {
        string query = " sp_GetCustomerByCheck ";
        MySqlConnection con = new MySqlConnection(connection);
        MySqlCommand command = new MySqlCommand(query, con);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add("@checkNum", MySqlDbType.VarChar).Value = checkNum;
        await Task.Delay(100);
        try
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            DataTable dataTable = dataSet.Tables[0];
            dataTable.PrimaryKey = [dataTable.Columns["customerNumber"]];
            DataRow dataRow = dataTable.Rows[0];
            return new Customer
            {
                CustomerNumber = Convert.ToInt32(dataRow["customerNumber"]),
                CustomerName = dataRow["customerName"].ToString(),
                ContactLastName = dataRow["contactLastName"].ToString(),
                ContactFirstName = dataRow["contactFirstName"].ToString(),
                Phone = dataRow["phone"].ToString(),
                AddressLine1 = dataRow["addressLine1"].ToString(),
                AddressLine2 = dataRow.IsNull("addressLine2") ? null : dataRow["addressLine2"].ToString(),
                City = dataRow["city"].ToString(),
                State = dataRow.IsNull("state") ? null : dataRow["state"].ToString(),
                PostalCode = dataRow.IsNull("postalCode") ? null : dataRow["postalCode"].ToString(),
                Country = dataRow["country"].ToString(),
                SalesRepEmployeeNumber = dataRow.IsNull("salesRepEmployeeNumber") ? 0 : Convert.ToInt32(dataRow["salesRepEmployeeNumber"]),
                CreditLimit = dataRow.IsNull("creditLimit") ? 0 : Convert.ToDecimal(dataRow["creditLimit"])
            };
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception occured");
            Console.WriteLine("Error: " + e.Message);
        }
        return new Customer();
    }

    public async Task<List<Order>> GetOrdersByCustomerNumber(int id)
    {
        List<Order> orders = new List<Order>();
        string query = " SELECT orders.*, orderdetails.productCode, " +
        " orderdetails.quantityOrdered, " +
        " orderdetails.priceEach, " +
        " orderdetails.orderLineNumber " +
        " FROM orders " +
        " JOIN orderdetails ON orders.orderNumber = orderdetails.orderNumber " +
        " WHERE orders.customerNumber = @customerNumber;";
        MySqlConnection con = new MySqlConnection(connection);
        MySqlCommand command = new MySqlCommand(query, con);
        command.Parameters.AddWithValue("@customerNumber", id);
        await Task.Delay(100);
        try
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            DataTable dataTable = dataSet.Tables[0];
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Order order = new Order()
                {
                    OrderNumber = Convert.ToInt32(dataRow["orderNumber"]),
                    OrderDate = DateOnly.FromDateTime(Convert.ToDateTime(dataRow["orderDate"])),
                    RequiredDate = DateOnly.FromDateTime(Convert.ToDateTime(dataRow["requiredDate"])),
                    ShippedDate = dataRow.IsNull("shippedDate") ? null : DateOnly.FromDateTime(Convert.ToDateTime(dataRow["shippedDate"])),
                    Status = dataRow["status"].ToString(),
                    Comments = dataRow.IsNull("comments") ? null : dataRow["comments"].ToString(),
                    CustomerNumber = Convert.ToInt32(dataRow["customerNumber"]),
                    orderDetails = new OrderDetails()
                    {
                        ProductCode = dataRow["productCode"].ToString(),
                        QuantityOrdered = Convert.ToInt32(dataRow["quantityOrdered"]),
                        PriceEach = Convert.ToDecimal(dataRow["priceEach"]),
                        OrderLineNumber = Convert.ToInt32(dataRow["orderLineNumber"])
                    }
                };
                orders.Add(order);
            }
            return orders;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
        return new List<Order>();
    }

    public async Task<List<Payment>> GetPaymentDetailsByCustomer(int id)
    {
        string query = " SELECT * FROM payments WHERE customerNumber=@customerNumber; ";
        List<Payment> payments = new List<Payment>();
        MySqlConnection con = new MySqlConnection(connection);
        MySqlCommand command = new MySqlCommand(query, con);
        command.Parameters.AddWithValue("@customerNumber", id);
        await Task.Delay(100);
        try
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            DataTable dataTable = dataSet.Tables[0];
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Payment payment = new Payment
                {
                    CustomerNumber = Convert.ToInt32(dataRow["customerNumber"]),
                    CheckNumber = dataRow["checkNumber"].ToString(),
                    PaymentDate = DateOnly.FromDateTime(Convert.ToDateTime(dataRow["paymentDate"])),
                    Amount = Convert.ToDecimal(dataRow["amount"])
                };
                payments.Add(payment);
            }
            return payments;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
        return new List<Payment>();
    }
}