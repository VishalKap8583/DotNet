using Enterprise.Entities;

namespace Enterprise.Repositories.Interface;

public interface IProductRepo
{
    public Task<bool> AddProduct(Product product);
    public Task<bool> UpdateProduct(Product product);
    public Task<bool> DeleteProduct(string id);
    public Task<List<Product>> GetAllProduct();
    public Task<Product> GetProduct(string id);
    public Task<List<Product>> GetProductsByProductLine(string productLine);
}