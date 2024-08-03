using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Parcial1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraVentaController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public ActionResult<object> Guardar([FromBody] Data.CompraVenta CompraVenta)
        {
            string respuesta = CompraVenta.GuardarCompraVenta(CompraVenta);
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
            Data.CompraVenta CompraVentas = new Data.CompraVenta();
            return Ok(CompraVentas.ListarCompraVentas());
        }

    }
}