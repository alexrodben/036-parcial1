using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Webappi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        [HttpPost]
        [Route("guardar")]
        public ActionResult<object> guardar([FromBody] Reposo.Persona Persona)
        {
            string respuesta = Persona.GuardarPersona(Persona);
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
            Reposo.Persona Personas = new Reposo.Persona();
            return Ok(Personas.ListarPersonas());
        }

    }
}