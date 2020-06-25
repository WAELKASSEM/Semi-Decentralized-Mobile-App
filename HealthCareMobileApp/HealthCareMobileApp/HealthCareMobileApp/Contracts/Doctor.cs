using SQLite;

namespace HealthCareMobileApp.Contracts
{
    class Doctor
    {
        [PrimaryKey]
        public string Address { get; set; }

        public string Name { get; set; }

        public string  Status { get; set; }

    }
}
