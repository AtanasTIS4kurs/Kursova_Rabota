using GameStore.Models.Configuration;
using GameStore.Models.DTO;
using GameStore.DL.Interface;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace GameStore.DL.Repositories
{
    internal class CompaniesMongoRepository : ICompanyRepository
    {
        private readonly IMongoCollection<Company> _companiesCollection;

        public CompaniesMongoRepository(IOptionsMonitor<MongoDbConfiguration> mongoConfig)
        {
            var client = new MongoClient(mongoConfig.CurrentValue.ConnectionString);
            var database = client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);

            _companiesCollection = database.GetCollection<Company>($"{nameof(Company)}");

        }

        public void Create(Company company)
        {
            _companiesCollection.InsertOne(company);
        }

        public void Delete(string id)
        {
            _companiesCollection.DeleteOne(c => c.Id == id);
        }

        public List<Company> GetAll()
        {
            return  _companiesCollection.Find(_ => true).ToList();
        }

        public Company GetById(string id) => _companiesCollection.Find(c => c.Id == id).FirstOrDefault();

        public void Update(Company company)
        {
            _companiesCollection.ReplaceOne(c => c.Id == company.Id, company);
        }
        public Company? GetByName(string companyName)
        {
            return _companiesCollection.Find(c => c.Name == companyName).FirstOrDefault();
        }
    }
}
