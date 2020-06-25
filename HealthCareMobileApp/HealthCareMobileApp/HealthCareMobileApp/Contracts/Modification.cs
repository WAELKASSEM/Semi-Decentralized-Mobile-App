

namespace HealthCareMobileApp.Contracts
{
    class Modification
    {
        public int TimeStamp { get; set; }
        public  string PatientAddress { get; set; }
        public string DoctorAddress { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
    }
}
