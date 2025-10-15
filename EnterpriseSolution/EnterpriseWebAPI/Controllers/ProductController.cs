using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Enterprise.Entities;
using Enterprise.Repositories.Interface;
using Enterprise.Services.Interface;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    //(http://localhost:5174/api/Product/GetAll)
    [HttpGet("GetAll")]
    public async Task<List<Product>> GetAll()
    {
        Task<List<Product>> products = _service.GetAllProduct();
        return await products;
    }

    //(http://localhost:5174/api/Product/GetById)
    [HttpGet("GetById/{id}")]
    public async Task<Product> Get(string id)
    {
        Task<Product> Product = _service.GetProduct(id);
        return await Product;
    }

    //(http://localhost:5174/api/Product/Add)
    [HttpPost("Add")]
    public async Task<bool> Add(Product product)
    {
        return await _service.AddProduct(product);
    }

    //(http://localhost:5174/api/Product/DeleteById)
    [HttpDelete("DeleteById/{id}")]
    public async Task<bool> Delete(string id)
    {
        return await _service.DeleteProduct(id);
    }

    //(http://localhost:5174/api/Product/UpdateById)
    [HttpPut("UpdateById/{id}")]
    public async Task<bool> Update(Product product)
    {
        return await _service.UpdateProduct(product);
    }

    //(http://localhost:5174/api/Product/GetByProductline)
    [HttpGet("GetByProductline/{productLine}")]
    public async Task<List<Product>> GetProductsByProductLine(string productLine)
    {
        Task<List<Product>> Products = _service.GetProductsByProductLine(productLine);
        return await Products;
    }
}
