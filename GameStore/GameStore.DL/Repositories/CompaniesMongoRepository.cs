﻿using GameStore.DL.Interfaces;
using GameStore.Models.Configuration;
using GameStore.Models.DTO;
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

        public async Task Create(Company company)
        {
           await _companiesCollection.InsertOneAsync(company);
        }

        public async Task Delete(string id)
        {
           await _companiesCollection.DeleteOneAsync(c => c.Id == id);
        }

        public async Task<List<Company>> GetAll()
        {
            var companies = await _companiesCollection.FindAsync(_ => true);
            return await companies.ToListAsync();
        }

        public async Task<Company?> GetById(string id)
        {
            var result = await _companiesCollection.FindAsync(c => c.Id == id);
            return await result.FirstOrDefaultAsync();
        }
        public async Task Update(Company company)
        {
            await _companiesCollection.ReplaceOneAsync(c => c.Id == company.Id, company);
        }
        public async Task<Company?> GetByName(string companyName)
        {
            var result = await _companiesCollection.FindAsync(c => c.Name == companyName);
            return await result.FirstOrDefaultAsync();
        }
        protected async Task<IEnumerable<Company?>> GetCompaniesAfterDateTime(DateTime date)
        {
            var result = await _companiesCollection.FindAsync(m => m.DateInserted >= date);
            return await result.ToListAsync();
        }
        public async Task<IEnumerable<Company?>> FullLoad()
        {
            return await GetAll();
        }

        public async Task<IEnumerable<Company?>> DifLoad(DateTime lastExecuted)
        {
            return await GetCompaniesAfterDateTime(lastExecuted);
        }
    }
}
