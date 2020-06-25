using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareWebAPI.Models;
using HealthCareWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace HealthCareWebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuth]
    public class DrugsController : ControllerBase
    {
        private readonly DrugsService _drugs;

        public DrugsController(DrugsService service)
        {
            this._drugs = service;
        }
        // GET: api/Drugs
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string name)
        {
            name = name.ToUpper().Trim();
            var drug = await _drugs.Find(name);
            if (drug == null) return Ok(default(Drug));
            return Ok(drug);

        }
    }
}
