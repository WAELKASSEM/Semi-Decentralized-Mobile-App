using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareWebAPI.Models
{
    public class Patient
    {
        [BsonId]
        [BsonRequired]
        public string Id { get; set; } // Pending Formula.
        public string BloodType { get; set; } //string easier for JSon.
        public List<string> EmergencyContacts { get; set; } //Limited to 5.
        public List<string> MedicalNotes { get; set; } // Can Be Anything not specified Tokens.
        public List<string> Allergies { get; set; } //List Of Vaccine Names.

    }
}
