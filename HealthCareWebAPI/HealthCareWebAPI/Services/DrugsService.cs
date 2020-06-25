using HealthCareWebAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareWebAPI.Services
{
    [ApiKeyAuth]
    public class DrugsService
    {
        private readonly IMongoCollection<Drug> _drugs;

        public DrugsService(IHealthCareDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _drugs = database.GetCollection<Drug>(settings.DrugsCollectionName);
        }
        public async Task<Drug> Find(string name) =>
            await (await _drugs.FindAsync<Drug>(x => x.BrandName==name)).FirstOrDefaultAsync<Drug>();
        public async Task<Drug>Get(string code)
        {
            return await (await _drugs.FindAsync<Drug>(x => x._id == code)).FirstOrDefaultAsync<Drug>();

        }
    }
}
