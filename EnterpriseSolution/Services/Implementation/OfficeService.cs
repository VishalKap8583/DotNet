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

public class OfficeService : IOfficeService
{
    private readonly IOfficeRepo repo;
    public OfficeService(IOfficeRepo repo)
    {
        this.repo = repo;
    }
    public async Task<bool> AddOffice(Office office)
    {
        return await repo.AddOffice(office);
    }
    public async Task<bool> UpdateOffice(Office office)
    {
        return await repo.UpdateOffice(office);
    }
    public async Task<bool> DeleteOffice(string id)
    {
        return await repo.DeleteOffice(id);
    }
    public async Task<List<Office>> GetAllOffice()
    {
        return await repo.GetAllOffice();
    }
    public async Task<Office> GetOffice(string id)
    {
        return await repo.GetOffice(id);
    }
}