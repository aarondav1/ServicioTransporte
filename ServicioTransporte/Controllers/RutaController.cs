using Microsoft.AspNetCore.Mvc;
using ServicioTransporte.BusinessLogic.DTOs;
using ServicioTransporte.BusinessLogic.Servicios;

namespace ServicioTransporte.Controllers
{
    [ApiController]
    [Route("api/ruta")]
    public class RutaController: ControllerBase
    {
        private readonly RutaServicios rutaServicios;

        public RutaController(RutaServicios rutaServicios)
        {
            this.rutaServicios = rutaServicios;
        }

        [HttpGet(Name = "ListarRutas")]
        public async Task<ActionResult<List<RutaDTO>>> ListarRutas()
        {
            try
            {
                var rutas = await rutaServicios.GetListaRutas();
                return rutas;
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("{nombre}", Name = "ObtenerRutaPorNombre")]
        public async Task<ActionResult<List<RutaDTO>>> ObtenerRutaPorNombre(string nombre)
        {
            try
            {
                var rutas = await rutaServicios.GetRuta(nombre);
                if (rutas == null)
                {
                    return NotFound($"No existe ruta con el nombre {nombre}");
                }
                return rutas;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet("ObtenerRutaConParadas/{nombre}", Name = "ObtenerRutaConParadas")]
        //public async Task<ActionResult<RutaDTO>> ObtenerRutaConParadas(string nombre)
        //{
        //    try
        //    {
        //        var ruta = await rutaServicios.GetRutaConParadas(nombre);
        //        return ruta;
        //    }
        //    catch (ArgumentException ex) { return BadRequest(ex.Message); }
        //}
        [HttpGet("ObtenerRutaConParadas/{id:int}", Name = "ObtenerRutaConParadas")]
        public async Task<ActionResult<RutaConParadasDTO>> ObtenerRutaConParadas(int id)
        {
            try
            {
                var ruta = await rutaServicios.GetRutaConParadas(id);
                return ruta;
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("ObtenerRutasConParadas", Name = "ObtenerRutasConParadas")]
        public async Task<ActionResult<List<RutaConParadasDTO>>> ObtenerRutasConParadas()
        {
            try
            {
                var ruta = await rutaServicios.GetRutasConParadas();
                return ruta;
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("ObtenerRutaConParadasNoAsociadas/{id:int}", Name = "ObtenerRutaConParadasNoAsociadas")]
        public async Task<ActionResult<RutaConParadasDTO>> ObtenerRutaConParadasNoAsociadas(int id)
        {
            try
            {
                var ruta = await rutaServicios.GetRutaConParadasNoAsociadas(id);
                return ruta;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Name = "RegistrarRuta")]
        public async Task<ActionResult> RegistrarRuta([FromBody] RutaCreacionDTO rutaCreacionDTO)
        {
            try
            {
                await rutaServicios.PostRuta(rutaCreacionDTO);
                return Ok("Ruta agregada exitosamente");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}", Name = "ActualizarRuta")]
        public async Task<ActionResult> ActualizarRuta(RutaCreacionDTO rutaCreacionDTO, int id)
        {
            try
            {
                await rutaServicios.PutRuta(rutaCreacionDTO, id);

                return Ok("Actualizacion exitosa");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:int}", Name = "EliminarRuta")]
        public async Task<ActionResult> EliminarRuta(int id)
        {
            try
            {
                await rutaServicios.DeleteRuta(id);
                return Ok("Eliminacion exitosa");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
