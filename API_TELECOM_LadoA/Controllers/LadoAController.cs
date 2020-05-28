using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_TELECOM_LadoA.Domain.Model;
using API_TELECOM_LadoA.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_TELECOM_LadoA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LadoAController : ControllerBase
    {
        // GET: api/LadoA
        [HttpGet]
        public async Task<Clima> GetAsync()
        {
            return await LadoAService.GetClimaAsync();
        }

    }
}
