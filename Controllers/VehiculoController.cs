using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Parcial1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public ActionResult<object> Guardar([FromBody] Data.Vehiculo Vehiculo)
        {
            string respuesta = Vehiculo.GuardarVehiculo(Vehiculo);
            if (respuesta == "")
            {
                return Ok(new { cod_error = 0, msg = "Vehiculo guardado satisfactoriamente" });
            }
            else
            {
                return BadRequest(new { cod_error = -1000, msg = respuesta }); ;
            }

        }
        [HttpGet]
        [Route("")]
        public ActionResult<object> listar()
        {
            Data.Vehiculo Vehiculos = new Data.Vehiculo();
            return Ok(Vehiculos.ListarVehiculos());
        }

    }
}