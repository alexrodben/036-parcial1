using Microsoft.AspNetCore.Mvc;

namespace Parcial1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public ActionResult<object> Guardar([FromBody] Data.Persona Persona)
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
        [Route("")]
        public ActionResult<object> Listar()
        {
            Data.Persona Personas = new();
            return Ok(Personas.ListarPersonas());
        }

    }
}