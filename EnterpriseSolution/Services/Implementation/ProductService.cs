using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Enterprise.Entities;
using Enterprise.Repositories.Implementation;
using Enterprise.Repositories.Interface;
using Enterprise.Services.Interface;

namespace Enterprise.Services.Implementation;

public class ProductService : IProductService
{
    private readonly IProductRepo repo;
    public ProductService(IProductRepo repo)
    {
        this.repo = repo;
    }
    public async Task<bool> AddProduct(Product product)
    {
        return await repo.AddProduct(product);
    }
    public async Task<bool> UpdateProduct(Product product)
    {
        return await repo.UpdateProduct(product);
    }
    public async Task<bool> DeleteProduct(string id)
    {
        return await repo.DeleteProduct(id);
    }
    public async Task<List<Product>> GetAllProduct()
    {
        return await repo.GetAllProduct();
    }
    public async Task<Product> GetProduct(string id)
    {
        return await repo.GetProduct(id);
    }
    public async Task<List<Product>> GetProductsByProductLine(string productLine)
    {
        return await repo.GetProductsByProductLine(productLine);
    }
}