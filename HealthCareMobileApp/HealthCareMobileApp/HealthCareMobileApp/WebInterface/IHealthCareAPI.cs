using HealthCareMobileApp.Contracts;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCareMobileApp.WebInterface
{
    [Headers("x-api-key: 052e876dc900aaba74880f5a97a902a551933ebc3421e5d4d6d54c35db552455")]
    interface IHealthCareAPI
    {
        [Get("/api/Prescriptions/Patient/{id}")]
        Task<List<Prescription>> GetPrescriptions(string id);
        [Get("/api/Patient/{id}")]
        Task<Patient> GetPatientFile(string id);
        [Get("/api/Modifications/{id}")]
        Task<List<Modification>> GetModifications(string id);

        [Post("/api/Modifications/Respond")]
        Task<string> Respond([Body] Modification modification);

        [Delete("/api/Modifications/{id}")]
        Task<string> DeleteModification(int id);
        [Post("/api/Modifications")]
        Task<string> SubmitModification([Body] Modification modification);

        [Put("/api/Patient/{id}")]
        Task<string> UpdatePatientFile(string id, [Body] Patient value);

        [Get("/api/Doctors/{id}")]
        Task<bool> IsDoctor(string id);

        [Get("/api/Drugs/")]
        Task<Drug> GetDrug([Query] string name);

        [Post("/api/Prescriptions/")]
        Task<string> CreatePrescription([Body] Prescription prescription);
    }
}
