using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareWebAPI.Models
{
    public class Drug
    {
        public string _id { get; set; }
        public string BrandName { get; set; }
        public string Strength { get; set; }
        public string Presentation { get; set; }
        public string Form { get; set; }
    }
}
