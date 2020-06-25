using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareWebAPI.Models
{
    public class Modification
    {
        [BsonId]
        public int Timestamp { get; set; }
        public string PatientAddress { get; set; }
        public string DoctorAddress { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
        public Modification()
        {
            Timestamp = ObjectId.GenerateNewId().Timestamp;
        }

    }
}
