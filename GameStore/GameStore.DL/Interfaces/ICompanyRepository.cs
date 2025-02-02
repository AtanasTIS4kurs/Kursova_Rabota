using GameStore.Models.DTO;

namespace GameStore.DL.Interface
{
    public interface ICompanyRepository
    {
        List<Company> GetAll();
        Company GetById(string id);
        void Create(Company company);
        void Update(Company company);
        void Delete(string id);
        Company? GetByName(string companyName);
    }
}
