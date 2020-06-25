using HealthCareWebAPI.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareWebAPI.Services
{
    public class PatientService
    {
        private readonly IMongoCollection<Patient> _patients;

        public PatientService(IHealthCareDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _patients = database.GetCollection<Patient>(settings.PatientsCollectionName);
        }

        public async Task<Patient> Get(string id) =>
            await (await _patients.FindAsync<Patient>(patient => patient.Id == id)).FirstOrDefaultAsync<Patient>();
        public async Task Create(Patient patient) =>
            await _patients.InsertOneAsync(patient);


        public async Task Update(string id, Patient patientIn) =>
           await _patients.ReplaceOneAsync(patient => patient.Id == id, patientIn);

        public async Task UpdateSingle(Modification mod)
        {
            var filter = Builders<Patient>.Filter.Eq("Id", mod.PatientAddress);
            var update = Builders<Patient>.Update.Push(mod.Type, mod.Data);
            var x = await _patients.FindOneAndUpdateAsync(filter, update);
            
        }
        public async Task Remove(Patient patientIn) =>
           await _patients.DeleteOneAsync(patient => patient.Id == patientIn.Id);

        public async Task Remove(string id) =>
           await _patients.DeleteOneAsync(patient => patient.Id == id);
    }
}
