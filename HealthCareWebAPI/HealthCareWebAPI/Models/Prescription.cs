using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HealthCareWebAPI.Models
{
    public class Prescription
    {
        [BsonId]
        public int Timestamp { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public string MedicineCode { get; set; }
        public string MedicineName { get; set; }
        public string Comment { get; set; }
        public Prescription()
        {
            Timestamp = ObjectId.GenerateNewId().Timestamp;
        }
    }
}
