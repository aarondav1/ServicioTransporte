using Microsoft.AspNetCore.Mvc;
using ServicioTransporte.BusinessLogic.DTOs;
using ServicioTransporte.BusinessLogic.Servicios;

namespace ServicioTransporte.Controllers
{
    [ApiController]
    [Route("api/AsignacionRutaParada")]
    public class AsignacionRutaParadaController: ControllerBase
    {
        private readonly AsignacionRutaParadaServicios asignacionRutaParadaServicios;

        public AsignacionRutaParadaController(AsignacionRutaParadaServicios asignacionRutaParadaServicios)
        {
            this.asignacionRutaParadaServicios = asignacionRutaParadaServicios;
        }

        [HttpGet(Name = "ListarAsigancionesRutaParada")]
        public async Task<ActionResult<List<AsignacionRutaParadaDTO>>> ListarAsigancionesRutaParada()
        {
            try
            {
                var asignaciones = await asignacionRutaParadaServicios.GetListaAsignacionRutaParada();
                return asignaciones;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("nombreRuta/{nombreRuta}", Name = "ObtenerAsignacionRutaParadaPorNombreRuta")]
        public async Task<ActionResult<List<AsignacionRutaParadaDTO>>> ObtenerAsignacionRutaParadaPorNombreRuta(string nombreRuta)
        {
            try
            {
                var asignacion = await asignacionRutaParadaServicios.GetAsignacionRutaParada(nombreRuta);
                if (asignacion == null)
                {
                    return NotFound($"No existe asignacion asociada a la ruta: {nombreRuta}");
                }
                return asignacion;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Name = "RegistrarAsignacionRutaParada")]
        public async Task<ActionResult> RegistrarAsignacionRutaParada([FromBody] AsignacionRutaParadaCreacionDTO asignacionRutaBusCreactionDTO)
        {
            try
            {
                await asignacionRutaParadaServicios.PostAsignacionRutaParada(asignacionRutaBusCreactionDTO);
                return Ok("Asignacion creada exitosamente");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}", Name = "ActualizarAsignacionRutaParada")]
        public async Task<ActionResult> ActualizarAsignacionRutaParada(AsignacionRutaParadaCreacionDTO asignacionRutaBusCreactionDTO, int id)
        {
            try
            {
                await asignacionRutaParadaServicios.PutAsignacionRutaParada(asignacionRutaBusCreactionDTO, id);
                return Ok("Actualizacion exitosa");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:int}", Name = "EliminarAsignacionRutaParada")]
        public async Task<ActionResult> EliminarAsignacionRutaBus(int id)
        {
            try
            {
                await asignacionRutaParadaServicios.DeleteAsignacionRutaParada(id);
                return Ok("Eliminacion exitosa");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("AgregarAsignacionesRutaParada/{id_ruta:int}", Name = "AgregarAsignacionesRutaParada")]
        public async Task<ActionResult> AgregarAsignacionesRutaBus(int id_ruta, [FromQuery(Name = "Ids_Paradas")] string ids_Paradas)
        {
            try
            {
                var paradasIds = ids_Paradas.Split(',').Select(int.Parse).ToArray();
                await asignacionRutaParadaServicios.PostAsignacionesRutaParada(id_ruta, paradasIds);
                return Ok(new { message = "Asignacion exitosa" });
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("EliminarAsignacionesRutaParada/{id_ruta:int}", Name = "EliminarAsignacionesRutaParada")]
        public async Task<ActionResult> EliminarAsignacionesRutaParada(int id_ruta, [FromQuery(Name = "Ids_Paradas")] string ids_Paradas)
        {
            try
            {
                var paradasIds = ids_Paradas.Split(',').Select(int.Parse).ToArray();
                await asignacionRutaParadaServicios.DeleteAsignacionesRutaParada(id_ruta, paradasIds);
                return Ok("Eliminacion exitosa");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
