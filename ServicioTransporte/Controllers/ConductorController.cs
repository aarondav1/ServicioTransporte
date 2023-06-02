using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServicioTransporte.BusinessLogic.DTOs;
using ServicioTransporte.BusinessLogic.Servicios;
using ServicioTransporte.Entities;

namespace ServicioTransporte.Controllers
{
    [ApiController]
    [Route("api/conductor")]
    public class ConductorController : ControllerBase
    {
        private readonly ConductorServicios conductorServicios;

        public ConductorController(ConductorServicios conductorServicios)
        {
            this.conductorServicios = conductorServicios;
        }

        [HttpGet("{ListarTodos}", Name = "ListarTodosLosConductores")]
        public async Task<ActionResult<List<ConductorDTO>>> ListarTodosLosConductores()
        {
            try
            {
                var conductores = await conductorServicios.LISTARTODOS();
                return conductores;
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpGet(Name = "ListarConductores")]
        public async Task<ActionResult<List<ConductorDTO>>> ListarConductores()
        {
            try
            {
                var conductores = await conductorServicios.GetListaConductores();
                return conductores;
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("{id:int}", Name = "ObtenerConductor")]
        public async Task<ActionResult<ConductorDTO>> ObtenerConductor(int id)
        {
            try
            {
                var conductor = await conductorServicios.GetConductor(id);
                if (conductor == null)
                {
                    return NotFound($"No existe conductor con el id {id}");
                }
                return conductor;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerConBuses/{id:int}", Name = "ObtenerConductorConBuses")]
        public async Task<ActionResult<ConductorConBusesDTO>> ObtenerConductorConBuses(int id)
        {
            try
            {
                var conductores = await conductorServicios.GetConductorConBuses(id);
                return conductores;
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("ObtenerConductoresConBuses", Name = "ObtenerConductoresConBuses")]
        public async Task<ActionResult<List<ConductorConBusesDTO>>> ObtenerConductoresConBuses()
        {
            try
            {
                var conductores = await conductorServicios.GetConductoresConBuses();
                return conductores;
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpPost(Name = "RegistrarConductor")]
        public async Task<ActionResult> RegistrarConductor([FromBody] ConductorCreacionDTO conductorCreacionDTO)
        {
            try
            {
                await conductorServicios.PostConductor(conductorCreacionDTO);
                return Ok("Conductor agregado exitosamente");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}", Name = "ActualizarConductor")]
        public async Task<ActionResult> ActualizarConductor(ConductorCreacionDTO conductorCreacionDTO, int id)
        {
            try
            {
                await conductorServicios.PutConductor(conductorCreacionDTO, id);

                return Ok("Actualizacion exitosa");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:int}", Name = "EliminarConductor")]
        public async Task<ActionResult> EliminarConductor(int id)
        {
            try
            {
                await conductorServicios.DeleteConductor(id);
                return Ok("Eliminacion exitosa");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
