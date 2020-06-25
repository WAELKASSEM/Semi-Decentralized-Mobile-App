
namespace HealthCareMobileApp.Contracts
{
    class Prescription
    {
        public int Timestamp { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public string MedicineCode { get; set; }
        public string MedicineName { get; set; }
        public string Comment { get; set; }
    }

}

