using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Enterprise.Entities;
using Enterprise.Repositories.Interface;
using Enterprise.Services.Interface;

[ApiController]
[Route("api/[controller]")]
public class OfficeController : ControllerBase
{
    private readonly IOfficeService _service;

    public OfficeController(IOfficeService service)
    {
        _service = service;
    }

    //(http://localhost:5174/api/Office/GetAll)
    [HttpGet("GetAll")]
    public async Task<List<Office>> GetAll()
    {
        Task<List<Office>> offices = _service.GetAllOffice();
        return await offices;
    }

    //(http://localhost:5174/api/Office/GetById/)
    [HttpGet("GetById/{id}")]
    public async Task<Office> Get(string id)
    {
        Task<Office> office = _service.GetOffice(id);
        return await office;
    }

    //(http://localhost:5174/api/Office/Add)
    [HttpPost("Add")]
    public async Task<bool> Add(Office office)
    {
        return await _service.AddOffice(office);
    }

    //(http://localhost:5174/api/Office/DeleteById/)
    [HttpDelete("DeleteById/{id}")]
    public async Task<bool> Delete(string id)
    {
        return await _service.DeleteOffice(id);
    }

    //(http://localhost:5174/api/Office/UpdateById/)
    [HttpPut("UpdateById/{id}")]
    public async Task<bool> Update(Office office)
    {
        return await _service.UpdateOffice(office);
    }

}
