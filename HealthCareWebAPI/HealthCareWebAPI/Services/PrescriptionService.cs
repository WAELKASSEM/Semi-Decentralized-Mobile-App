using HealthCareWebAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCareWebAPI.Services
{
    public class PrescriptionService
    {
        private readonly IMongoCollection<Prescription> _prescriptions;

        public PrescriptionService(IHealthCareDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _prescriptions = database.GetCollection<Prescription>(settings.PatientsCollectionName);
        }
        public async Task Add(Prescription perscription) => await _prescriptions.InsertOneAsync(perscription);
        public async Task Add(IEnumerable<Prescription> perscriptions) => await _prescriptions.InsertManyAsync(perscriptions);

        public async Task<IEnumerable<Prescription>> PatientPerscriptions(string patientId)
        {
            // Patient Only 
            var p = await _prescriptions.FindAsync<Prescription>(x => x.PatientId == patientId);
            return await p.ToListAsync<Prescription>();
        }
        public async Task<IEnumerable<Prescription>> DocPerscriptions(string docId, string patientId = "")
        {
            var p = string.IsNullOrEmpty(patientId) ? await _prescriptions.FindAsync<Prescription>(x => x.DoctorId == docId) :
                await _prescriptions.FindAsync<Prescription>(x => x.DoctorId == docId && x.PatientId == patientId);

            return await p.ToListAsync<Prescription>();
        }

    }
}

