using Microsoft.AspNetCore.Mvc;
using ServicioTransporte.BusinessLogic.DTOs;
using ServicioTransporte.BusinessLogic.Servicios;

namespace ServicioTransporte.Controllers
{
    [ApiController]
    [Route("api/parada")]
    public class ParadaController: ControllerBase
    {
        private readonly ParadaServicios paradaServicios;

        public ParadaController(ParadaServicios paradaServicios)
        {
            this.paradaServicios = paradaServicios;
        }

        [HttpGet(Name = "ListarParadas")]
        public async Task<ActionResult<List<ParadaDTO>>> ListarParadas()
        {
            try
            {
                var paradas = await paradaServicios.GetListaParadas();
                return paradas;
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("{id:int}", Name = "ObtenerParada")]
        public async Task<ActionResult<ParadaDTO>> ObtenerParada(int id)
        {
            try
            {
                var parada = await paradaServicios.GetParada(id);
                if (parada == null)
                {
                    return NotFound($"No existe parada con el id: {id}");
                }
                return parada;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Name = "RegistrarParada")]
        public async Task<ActionResult> RegistrarParada([FromBody] ParadaCreacionDTO paradaCreacionDTO)
        {
            try
            {
                await paradaServicios.PostParada(paradaCreacionDTO);
                return Ok("Parada agregado exitosamente");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}", Name = "ActualizarParada")]
        public async Task<ActionResult> ActualizarParada(ParadaCreacionDTO paradaCreacionDTO, int id)
        {
            try
            {
                await paradaServicios.PutParada(paradaCreacionDTO, id);
                return Ok("Actualizacion exitosa");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:int}", Name = "EliminarParada")]
        public async Task<ActionResult> EliminarParada(int id)
        {
            try
            {
                await paradaServicios.DeleteParada(id);
                return Ok("Eliminacion exitosa");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
