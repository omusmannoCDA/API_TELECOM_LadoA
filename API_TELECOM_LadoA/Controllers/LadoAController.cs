using API_TELECOM_LadoA.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_TELECOM_LadoA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LadoAController : ControllerBase
    {

        // GET: api/LadoA
        [HttpGet]
        [Route("GetDB")]
        public string GetDB()
        {
            return LadoAService.getDB();
        }

        [HttpGet]
        [Route("GrabarLog_SaldoDiagnostico")]
        public string GrabarLog_SaldoDiagnostico()
        {
            return LadoAService.GrabarLog_SaldoDiagnostico("20200220","PRUEBA 5-6-2020","PRUEBA PRUEBA","OMAR","10.20.30.40");
        }

        [HttpGet]
        [Route("PostOracleProcedure1")]
        public string PostOracleProcedure1(string palabra)
        {
            return LadoAService.PostOracleProcedure1(palabra);
        }

        [HttpGet]
        [Route("PostOracleProcedure2")]
        public string PostOracleProcedure2(string palabra)
        {
            return LadoAService.PostOracleProcedure2(palabra);
        }

        [HttpGet]
        [Route("CONSULTAS_DIAGNOSTICO")]
        public string CONSULTAS_DIAGNOSTICO(string palabra)
        {
            return LadoAService.CONSULTAS_DIAGNOSTICO(palabra);
        }
    }
}
