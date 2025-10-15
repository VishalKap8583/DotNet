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

public class ProductRepo : IProductRepo
{
    private readonly IConfiguration _configuration;
    private readonly string connection;
    public ProductRepo(IConfiguration configuration)
    {
        _configuration = configuration;
        connection = _configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Wrong ConnectionString");
    }

    public async Task<bool> AddProduct(Product product)
    {
        bool status = false;
        string query = " INSERT INTO products(productCode, productName, productLine, productScale, productVendor, productDescription, quantityInStock, buyPrice, MSRP) " +
                       " Values(@productCode, @productName, @productLine, @productSale, @productVendor, @productDescription, @quantityInStock, @buyPrice, @msrp) ";
        using (MySqlConnection con = new MySqlConnection(connection))
        using (MySqlCommand command = new MySqlCommand(query, con))
        {
            command.Parameters.AddWithValue("@productCode", product.ProductCode);
            command.Parameters.AddWithValue("@productName", product.ProductName);
            command.Parameters.AddWithValue("@productLine", product.ProductLine);
            command.Parameters.AddWithValue("@productScale", product.ProductScale);
            command.Parameters.AddWithValue("@productVendor", product.ProductVendor);
            command.Parameters.AddWithValue("@productDescription", product.ProductDescription);
            command.Parameters.AddWithValue("@quantityInStock", product.QuantityInStock);
            command.Parameters.AddWithValue("@buyPrice", product.BuyPrice);
            command.Parameters.AddWithValue("@msrp", product.MSRP);
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
    public async Task<bool> UpdateProduct(Product product)
    {
        bool status = false;
        string query = " UPDATE products SET productCode = @productCode, " +
                       " productName =  @productName, " +
                       " productLine =  @productLine, " +
                       " productSale = @productScale, " +
                       " productVendor = @productVendor, " +
                       " productDescription = @productDescription, " +
                       " quantityInStock = @quantityInStock, " +
                       " buyPrice = @buyPrice, " +
                       " MSRP = @msrp) ";

        using (MySqlConnection con = new MySqlConnection(connection))
        using (MySqlCommand command = new MySqlCommand(query, con))
        {
            command.Parameters.AddWithValue("@productCode", product.ProductCode);
            command.Parameters.AddWithValue("@productName", product.ProductName);
            command.Parameters.AddWithValue("@productLine", product.ProductLine);
            command.Parameters.AddWithValue("@productScale", product.ProductScale);
            command.Parameters.AddWithValue("@productVendor", product.ProductVendor);
            command.Parameters.AddWithValue("@productDescription", product.ProductDescription);
            command.Parameters.AddWithValue("@quantityInStock", product.QuantityInStock);
            command.Parameters.AddWithValue("@buyPrice", product.BuyPrice);
            command.Parameters.AddWithValue("@msrp", product.MSRP);
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
    public async Task<bool> DeleteProduct(string id)
    {
        bool status = false;
        string query = " DELETE FROM products WHERE productCode = @productCode ";
        using (MySqlConnection con = new MySqlConnection(connection))
        using (MySqlCommand command = new MySqlCommand(query, con))
        {
            try
            {
                await con.OpenAsync();
                command.Parameters.AddWithValue("@productCode", id);
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
    public async Task<List<Product>> GetAllProduct()
    {
        string query = " SELECT * FROM products ";
        List<Product> products = new List<Product>();
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
                        Product product = new Product
                        {
                            ProductCode = reader.GetString("productCode"),
                            ProductName = reader.GetString("productName"),
                            ProductLine = reader.GetString("productLine"),
                            ProductScale = reader.GetString("productScale"),
                            ProductVendor = reader.GetString("productVendor"),
                            ProductDescription = reader.GetString("productDescription"),
                            QuantityInStock = reader.GetInt32("quantityInStock"),
                            BuyPrice = reader.GetDecimal("buyPrice"),
                            MSRP = reader.GetDecimal("msrp")
                        };
                        products.Add(product);
                    }
                    return products;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        return new List<Product>();
    }
    public async Task<Product> GetProduct(string id)
    {
        string query = " SELECT * FROM products WHERE productCode = @productCode ";
        using (MySqlConnection con = new MySqlConnection(connection))
        using (MySqlCommand command = new MySqlCommand(query, con))
        {
            try
            {
                await con.OpenAsync();
                command.Parameters.AddWithValue("@productCode", id);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Product
                        {
                            ProductCode = reader.GetString("productCode"),
                            ProductName = reader.GetString("productName"),
                            ProductLine = reader.GetString("productLine"),
                            ProductScale = reader.GetString("productScale"),
                            ProductVendor = reader.GetString("productVendor"),
                            ProductDescription = reader.GetString("productDescription"),
                            QuantityInStock = reader.GetInt32("quantityInStock"),
                            BuyPrice = reader.GetDecimal("buyPrice"),
                            MSRP = reader.GetDecimal("msrp")
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return new Product();
        }
    }

    public async Task<List<Product>> GetProductsByProductLine(string productLine)
    {
        string query = " SELECT products.*, productLines.textDescription " +
                       " FROM products JOIN productlines " +
                       " ON products.productLine = productlines.productLine " +
                       " WHERE products.productLine = @productLine";

        List<Product> products = new List<Product>();
        MySqlConnection con = new MySqlConnection(connection);
        MySqlCommand command = new MySqlCommand(query, con);
        command.Parameters.AddWithValue("@productLine", productLine);
        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
        DataSet dataSet = new DataSet();
        adapter.Fill(dataSet);
        await Task.Delay(100);
        try
        {
            DataTable dataTable = dataSet.Tables[0];
            dataTable.PrimaryKey = [dataTable.Columns["productCode"]];
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Product product = new Product
                {
                    ProductCode = dataRow["productCode"].ToString(),
                    ProductName = dataRow["productName"].ToString(),
                    ProductLine = dataRow["productLine"].ToString(),
                    ProductScale = dataRow["productScale"].ToString(),
                    ProductVendor = dataRow["productVendor"].ToString(),
                    ProductDescription = dataRow["productDescription"].ToString(),
                    QuantityInStock = Convert.ToInt32(dataRow["quantityInStock"]),
                    BuyPrice = Convert.ToDecimal(dataRow["buyPrice"]),
                    MSRP = Convert.ToDecimal(dataRow["msrp"]),
                    productLines = new ProductLines()
                    {
                        TextDescription = dataRow["textDescription"].ToString()
                    }
                };
                products.Add(product);
            }
            return products;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
        return products;
    }
}