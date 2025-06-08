using GameStore.BL.Interfaces;
using GameStore.DL.Interfaces;
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

        public async Task Create(Company company)
        {
           await _companyRepository.Create(company);
        }

        public async Task Delete(string id)
        {
            await _companyRepository.Delete(id);
        }

        public async Task<List<Company>> GetAll()
        {
            return await _companyRepository.GetAll();
        }

        public async Task<Company?> GetById(string id) => await _companyRepository.GetById(id);

        public async Task Update(Company company)
        {
            await _companyRepository.Update(company);
        }
    }
}
