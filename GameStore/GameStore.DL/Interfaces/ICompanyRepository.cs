using GameStore.Models.DTO;

namespace GameStore.DL.Interface
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetAll();
        Task<Company?> GetById(string id);
        Task Create(Company company);
        Task Update(Company company);
        Task Delete(string id);
        Task<Company?> GetByName(string companyName);
    }
}
