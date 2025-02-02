using GameStore.BL.Interfaces;
using GameStore.DL.Interface;
using GameStore.Models.DTO;

namespace GameStore.BL.Services
{
    internal class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public void Create(Company company)
        {
            _companyRepository.Create(company);
        }

        public void Delete(string id)
        {
            _companyRepository.Delete(id);
        }

        public List<Company> GetAll()
        {
            return _companyRepository.GetAll();
        }

        public Company GetById(string id) => _companyRepository.GetById(id);

        public void Update(Company company)
        {
            _companyRepository.Update(company);
        }
    }
}
