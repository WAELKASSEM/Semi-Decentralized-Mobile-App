
using System;
using System.Threading.Tasks;
using HealthCareWebAPI.Models;
using HealthCareWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HealthCareWebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuth]
    public class ModificationsController : ControllerBase
    {
        private readonly ModificationsService _modificationsService;
        private readonly PatientService _patientService;
        public ModificationsController(ModificationsService service, PatientService serv)
        {
            _modificationsService = service;
            _patientService = serv;
        }

        // GET: api/Modifications/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var collection = await _modificationsService.PatientModifications(id);
            return Ok(collection);
        }

        // POST: api/Modifications
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Modification modification)
        {
            modification.Timestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            await _modificationsService.Insert(modification);
            return Ok();
        }

        // PUT: api/Modifications/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           await _modificationsService.Delete(id);
            return Ok();
        }

        [HttpPost("Respond")]
        public async Task<IActionResult> UpdatePatientFile([FromBody] Modification mod)
        {
            await _patientService.UpdateSingle(mod);
            return Ok();
        }
    }
}
