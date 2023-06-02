using Microsoft.AspNetCore.Mvc;
using ServicioTransporte.BusinessLogic.DTOs;
using ServicioTransporte.BusinessLogic.Servicios;
using System.Xml.Linq;

namespace ServicioTransporte.Controllers
{
    [ApiController]
    [Route("api/AsignacionBusConductor")]
    public class AsignacionBusConductorController: ControllerBase
    {
        private readonly AsignacionBusConductorServicios asignacionBusConductorServicios;

        public AsignacionBusConductorController(AsignacionBusConductorServicios asignacionBusConductorServicios)
        {
            this.asignacionBusConductorServicios = asignacionBusConductorServicios;
        }

        [HttpGet(Name = "ListarAsigancionesBusConductor")]
        public async Task<ActionResult<List<AsignacionBusConductorDTO>>> ListarAsigancionesBusConductor()
        {
            try
            {
                var asignaciones = await asignacionBusConductorServicios.GetListaAsignacionBusConductor();
                return asignaciones;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{numeroBus:int}", Name = "ObtenerAsignacionBusConductor")]
        public async Task<ActionResult<AsignacionBusConductorDTO>> ObtenerAsignacionBusConductor(int numeroBus)
        {
            try {
                var asignacion = await asignacionBusConductorServicios.GetAsignacionBusConductor(numeroBus);
                if (asignacion == null)
                {
                    return NotFound($"No existe asignacion asociada al bus con numero: {numeroBus}");
                }
                return asignacion;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Name = "RegistrarAsignacionBusConductor")]
        public async Task<ActionResult> RegistrarAsignacionBusConductor([FromBody] AsignacionBusConductorCreacionDTO asignacionBusConductorCreacionDTO)
        {
            try
            {
                await asignacionBusConductorServicios.PostAsignacionBusConductor(asignacionBusConductorCreacionDTO);
                return Ok("Asignacion creada exitosamente");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}", Name = "ActualizarAsignacionBusConductor")]
        public async Task<ActionResult> ActualizarAsignacionBusConductor(AsignacionBusConductorCreacionDTO asignacionBusConductorCreacionDTO, int id)
        {
            try
            {
                await asignacionBusConductorServicios.PutAsignacionBusConductor(asignacionBusConductorCreacionDTO, id);
                return Ok("Actualizacion exitosa");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("EliminarAsignacionBusConductor/{id:int}", Name = "EliminarAsignacionBusConductor")]
        public async Task<ActionResult> EliminarAsignacionBusConductor(int id)
        {
            try
            {
                await asignacionBusConductorServicios.DeleteAsignacionBusConductor(id);
                return Ok("Eliminacion exitosa");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("EliminarPorIdConductor/{idConductor:int}", Name = "EliminarPorIdConductor")]
        public async Task<ActionResult> EliminarPorIdConductor(int idConductor)
        {
            try
            {
                await asignacionBusConductorServicios.DeletePorIdConductor(idConductor);
                return Ok("Eliminacion exitosa");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
