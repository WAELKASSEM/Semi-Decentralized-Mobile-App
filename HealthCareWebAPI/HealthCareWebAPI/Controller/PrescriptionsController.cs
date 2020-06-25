using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareWebAPI.Models;
using HealthCareWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using MongoDB.Bson;

namespace HealthCareWebAPI.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuth]
    public class PrescriptionsController : ControllerBase
    {
        private readonly PrescriptionService _prescriptions;
        private readonly DrugsService _drugs;
        public PrescriptionsController(PrescriptionService service,DrugsService dservice)
        {
            _prescriptions = service;
            _drugs = dservice;
        }
        // GET: api/Perscriptions
        [HttpGet("Patient/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrEmpty(id) || id.Length != Requirements.PatientIdLength)
                return BadRequest();

            var patient_prescriptions = await _prescriptions.PatientPerscriptions(id);
            foreach (var presc in patient_prescriptions)
            {
                presc.MedicineName = (await _drugs.Get(presc.MedicineCode)).BrandName;
            }
            return Ok(patient_prescriptions);

        }

        //This Will be only acessible for Doctors. Medical staff anyway.
        [HttpGet("Doctor/{id}")]
        public async Task<IActionResult> DoctorGet(string id, [FromQuery] string patientId)
        {
            //doctor verification.
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var doctor_query = await _prescriptions.DocPerscriptions(id, patientId);
            return Ok(doctor_query);
        }

        // POST: api/Perscriptions
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Prescription prescription)
        {
            prescription.Timestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            await _prescriptions.Add(prescription);
            return Ok();
        }
       

       

       
    }
}
