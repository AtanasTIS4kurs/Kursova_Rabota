using GameStore.Models.DTO;

namespace GameStore.BL.Interfaces
{
    public interface ICompanyService
    {
        Task<List<Company>> GetAll();
        Task<Company?> GetById(string id);
        Task Create(Company company);
        Task Update(Company company);
        Task Delete(string id);
    }
}
