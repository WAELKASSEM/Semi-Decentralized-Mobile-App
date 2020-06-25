using SQLite;

namespace HealthCareMobileApp.Contracts
{
    class PatientContact
    {
        [PrimaryKey]
        public string Address { get; set; }
        public string Name { get; set; }
    }
}
