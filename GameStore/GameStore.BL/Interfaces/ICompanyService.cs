using GameStore.Models.DTO;

namespace GameStore.BL.Interfaces
{
    public interface ICompanyService
    {
        List<Company> GetAll();
        Company GetById(string id);
        void Create(Company company);
        void Update(Company company);
        void Delete(string id);
    }
}
