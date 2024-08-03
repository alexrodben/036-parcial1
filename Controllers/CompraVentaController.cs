using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parcial1.Data;
using System.Collections.Generic;

namespace Parcial1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraVentaController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public ActionResult<object> Guardar([FromBody] CompraVenta compraVenta)
        {
            CompraVenta compraVentaData = new();
            string respuesta = compraVentaData.GuardarCompraVenta(compraVenta);
            if (respuesta == "CompraVenta guardada exitosamente.")
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
            CompraVenta compraVentaData = new();
            return Ok(compraVentaData.ListarcompraVenta());
        }

        [HttpGet]
        [Route("{idCompraVenta}")]
        public ActionResult<object> Obtener(string idCompraVenta)
        {
            CompraVenta compraVentaData = new();
            CompraVenta? compraVenta = compraVentaData.ObtenerCompraVenta(idCompraVenta);
            if (compraVenta != null)
            {
                return Ok(compraVenta);
            }
            else
            {
                return NotFound(new { cod_error = -1001, msg = "CompraVenta no encontrada" });
            }
        }

        [HttpPut]
        [Route("")]
        public ActionResult<object> Actualizar([FromBody] CompraVenta compraVenta)
        {
            CompraVenta compraVentaData = new();
            string respuesta = compraVentaData.ActualizarCompraVenta(compraVenta);
            if (respuesta == "CompraVenta actualizada exitosamente.")
            {
                return Ok(new { cod_error = 0, msg = respuesta });
            }
            else
            {
                return BadRequest(new { cod_error = -1002, msg = respuesta });
            }
        }

        [HttpDelete]
        [Route("{idCompraVenta}")]
        public ActionResult<object> Eliminar(string idCompraVenta)
        {
            CompraVenta compraVentaData = new();
            string respuesta = compraVentaData.EliminarCompraVenta(idCompraVenta);
            if (respuesta == "CompraVenta eliminada exitosamente.")
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
