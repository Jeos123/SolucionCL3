using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CL3.Entidades;
using CL3.DAO;

namespace CL3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DenunciaApiController : ControllerBase
    {
        [HttpGet("GetTrabajador")]
        public async Task<ActionResult<List<Trabajador>>> GetTrabajador()
        {
            var lista = await Task.Run(() => (new TrabajadorDAO()).GetTrabajador());
            return Ok(lista);
        }

        [HttpPost("AddTrabajador")]
        public async Task<ActionResult<String>> InsertarTrabajador(Trabajador trabajador)
        {
            var mensaje = await Task.Run(() => (new TrabajadorDAO()).Agregar(trabajador));
            return Ok(mensaje);
        }

        [HttpPut("UpdateTrabajador")]
        public async Task<ActionResult<String>> ActualizarTrabajador(Trabajador trabajador)
        {
            var mensaje = await Task.Run(() => (new TrabajadorDAO()).Actualizar(trabajador));
            return Ok(mensaje);
        }
        [HttpDelete("EliminarTrabajador")]
        public async Task<ActionResult<String>> EliminarTrabajador(string dni)
        {
            var mensaje = await Task.Run(() => (new TrabajadorDAO()).Eliminar(dni));
            return Ok(mensaje);
        }
    }
}
