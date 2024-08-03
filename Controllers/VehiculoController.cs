using Microsoft.AspNetCore.Mvc;
using Parcial1.Data;

namespace Parcial1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public ActionResult<object> Guardar([FromBody] Vehiculo vehiculo)
        {
            Vehiculo vehiculoData = new();
            string respuesta = vehiculoData.GuardarVehiculo(vehiculo);
            if (respuesta == "")
            {
                return Ok(new { cod_error = 0, msg = "Vehículo guardado satisfactoriamente" });
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
            Vehiculo vehiculoData = new();
            return Ok(vehiculoData.ListarVehiculos());
        }

        [HttpGet]
        [Route("{placa}")]
        public ActionResult<object> Obtener(string placa)
        {
            Vehiculo vehiculoData = new();
            Vehiculo? vehiculo = vehiculoData.ObtenerVehiculo(placa);
            if (vehiculo != null)
            {
                return Ok(vehiculo);
            }
            else
            {
                return NotFound(new { cod_error = -1001, msg = "Vehículo no encontrado" });
            }
        }

        [HttpPut]
        [Route("")]
        public ActionResult<object> Actualizar([FromBody] Vehiculo vehiculo)
        {
            Vehiculo vehiculoData = new();
            string respuesta = vehiculoData.ActualizarVehiculo(vehiculo);
            if (respuesta == "Vehículo actualizado exitosamente.")
            {
                return Ok(new { cod_error = 0, msg = respuesta });
            }
            else
            {
                return BadRequest(new { cod_error = -1002, msg = respuesta });
            }
        }

        [HttpDelete]
        [Route("{placa}")]
        public ActionResult<object> Eliminar(string placa)
        {
            Vehiculo vehiculoData = new();
            string respuesta = vehiculoData.EliminarVehiculo(placa);
            if (respuesta == "Vehículo eliminado exitosamente.")
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
