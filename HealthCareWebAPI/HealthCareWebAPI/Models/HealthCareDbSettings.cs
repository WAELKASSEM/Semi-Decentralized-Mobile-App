
namespace HealthCareWebAPI.Models
{
    public class HealthCareDbSettings : IHealthCareDbSettings
    {
        public string PatientsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string SensorsDataCollectionName { get; set; }
        public string DrugsCollectionName { get; set; }
        public string PrescriptionsCollectionName { get; set; }
        public string DoctorsCollectionName { get; set; }
        public string ModificationsCollectionName { get; set; }
    }
}
