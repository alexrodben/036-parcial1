using Microsoft.AspNetCore.Mvc;
using Parcial1.Data;
using System.Collections.Generic;

namespace Parcial1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public ActionResult<object> Guardar([FromBody] Persona persona)
        {
            Persona personaData = new();
            string respuesta = personaData.GuardarPersona(persona);
            if (respuesta == "Persona guardada exitosamente.")
            {
                return Ok(new { cod_error = 0, msg = respuesta });
            }
            else
            {
                return BadRequest(new { cod_error = -1000, msg = respuesta });
            }
        }

        [HttpGet]
        [Route("")]
        public ActionResult<object> Listar()
        {
            Persona personaData = new();
            return Ok(personaData.ListarPersonas());
        }

        [HttpGet]
        [Route("{cui}")]
        public ActionResult<object> Obtener(string cui)
        {
            Persona personaData = new();
            Persona? persona = personaData.ObtenerPersona(cui);
            if (persona != null)
            {
                return Ok(persona);
            }
            else
            {
                return NotFound(new { cod_error = -1001, msg = "Persona no encontrada" });
            }
        }

        [HttpPut]
        [Route("")]
        public ActionResult<object> Actualizar([FromBody] Persona persona)
        {
            Persona personaData = new();
            string respuesta = personaData.ActualizarPersona(persona);
            if (respuesta == "Persona actualizada exitosamente.")
            {
                return Ok(new { cod_error = 0, msg = respuesta });
            }
            else
            {
                return BadRequest(new { cod_error = -1002, msg = respuesta });
            }
        }

        [HttpDelete]
        [Route("{cui}")]
        public ActionResult<object> Eliminar(string cui)
        {
            Persona personaData = new();
            string respuesta = personaData.EliminarPersona(cui);
            if (respuesta == "Persona eliminada exitosamente.")
            {
                return Ok(new { cod_error = 0, msg = respuesta });
            }
            else
            {
                return BadRequest(new { cod_error = -1003, msg = respuesta });
            }
        }
    }
}
