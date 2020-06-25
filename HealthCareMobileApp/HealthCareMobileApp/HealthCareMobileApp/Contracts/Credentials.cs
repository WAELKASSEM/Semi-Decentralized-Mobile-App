using SQLite;
namespace HealthCareMobileApp.Contracts
{
    class Credentials
    {
        [PrimaryKey]
        public string Address { get; set; }

        public string PrivateKey { get; set; }
        
    }
}
