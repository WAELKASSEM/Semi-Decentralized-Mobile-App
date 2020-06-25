using System;
using System.Threading.Tasks;
using HealthCareWebAPI.Models;
using HealthCareWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HealthCareWebAPI.Controller
{
    //NOTE : Id format check needs to be updated, when a final format is reached.
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuth]
    public class PatientController : ControllerBase
    {

        private readonly PatientService _patientService;
        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }


        // GET: api/Patient/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (id.Length != Requirements.PatientIdLength)
                return BadRequest();

            var patient = await _patientService.Get(id);

            if (patient == default(Patient))
            {
                var p = new Patient()
                {
                    Id = id,
                    Allergies = new List<string>(),
                    BloodType = string.Empty,
                    EmergencyContacts = new List<string>(),
                    MedicalNotes = new List<string>()
                };
                await _patientService.Create(p);
                return (Ok(p));
            }

            return Ok(patient);
        }

        // POST: api/Patient
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Patient value)
        {
            if (value.Id.Length != Requirements.PatientIdLength)
                return BadRequest();

            if (await _patientService.Get(value.Id) != default(Patient))
                return BadRequest("A Patient with the same Id already exists");

            await _patientService.Create(value);

            return Created($"api/Patients/{value.Id}", value);
        }

        // PUT: api/Patient/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Patient value)
        {
           

            if (await _patientService.Get(value.Id) == default(Patient))
                return NotFound("Patient Doesn't Exist");

            await _patientService.Update(id, value);

            return Ok(value);
        }

        // DELETE: api/Patient/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
           

            if (await _patientService.Get(id) == default(Patient))
                return NotFound("Patient Doesn't Exist");

            await _patientService.Remove(id);

            return Ok("Deleted");
        }
    }
}
