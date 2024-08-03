using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Webappi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoController : ControllerBase
    {
        [HttpPost]
        [Route("guardar")]
        public ActionResult<object> guardar([FromBody] Reposo.Auto Auto)
        {
            string respuesta = Auto.GuardarAuto(Auto);
            if (respuesta == "")
            {
                return Ok(new { cod_error = 0, msg = "Vehículo guardado satisfactoriamente" });
            }
            else
            {
                return BadRequest(new { cod_error = -1000, msg = respuesta }); ;
            }

        }
        [HttpGet]
        [Route("listar")]
        public ActionResult<object> listar()
        {
            Reposo.Auto Autos = new Reposo.Auto();
            return Ok(Autos.ListarVehiculos());
        }

    }
}