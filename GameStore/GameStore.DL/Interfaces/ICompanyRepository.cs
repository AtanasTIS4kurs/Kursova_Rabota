using GameStore.DL.Cache;
using GameStore.Models.DTO;

namespace GameStore.DL.Interface
{
    public interface ICompanyRepository : ICacheRepository<string, Company>
    {
        Task<List<Company>> GetAll();
        Task<Company?> GetById(string id);
        Task Create(Company company);
        Task Update(Company company);
        Task Delete(string id);
        Task<Company?> GetByName(string companyName);
    }
}
