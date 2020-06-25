using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace HealthCareMobileApp.Contracts
{
    class Patient
    {
        public string Id { get; set; }
        public string BloodType { get; set; }
        public ObservableCollection<string> EmergencyContacts { get; set; }
        public List<string> MedicalNotes { get; set; }
        public List<string> Allergies { get; set; }

    }
}
