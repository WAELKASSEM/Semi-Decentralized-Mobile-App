using HealthCareWebAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareWebAPI.Services
{
    public class ModificationsService
    {
        private readonly IMongoCollection<Modification> _modifications;

        public ModificationsService(IHealthCareDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _modifications = database.GetCollection<Modification>(settings.ModificationsCollectionName);
        }

        public async Task<IEnumerable<Modification>> PatientModifications(string patientId)
        {
            var p = await _modifications.FindAsync<Modification>(x => x.PatientAddress == patientId);
            return await p.ToListAsync<Modification>();
        }

        public async Task Insert(Modification modification)
        {
            await _modifications.InsertOneAsync(modification);
        }

        public async Task Delete(int id)
        {
            await _modifications.DeleteOneAsync(x => x.Timestamp == id);
        }
    }
}
