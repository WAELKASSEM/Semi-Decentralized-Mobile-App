using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareWebAPI.Models
{
    public interface IHealthCareDbSettings
    {
        string PatientsCollectionName { get; set; }
        string SensorsDataCollectionName { get; set; }
        string DrugsCollectionName { get; set; }
        string ModificationsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string PrescriptionsCollectionName { get; set; }
        string DoctorsCollectionName { get; set; }
        string DatabaseName { get; set; }
    }
}
