using Enterprise.Entities;

namespace Enterprise.Repositories.Interface;

public interface IOfficeRepo
{
    public Task<bool> AddOffice(Office office);
    public Task<bool> UpdateOffice(Office office);
    public Task<bool> DeleteOffice(string id);
    public Task<List<Office>> GetAllOffice();
    public Task<Office> GetOffice(string id);
}