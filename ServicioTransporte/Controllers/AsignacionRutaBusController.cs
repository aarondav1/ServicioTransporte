using Microsoft.AspNetCore.Mvc;
using ServicioTransporte.BusinessLogic.DTOs;
using ServicioTransporte.BusinessLogic.Servicios;

namespace ServicioTransporte.Controllers
{
    [ApiController]
    [Route("api/AsignacionRutaBus")]
    public class AsignacionRutaBusController: ControllerBase
    {
        private readonly AsignacionRutaBusServicios asignacionRutaBusServicios;

        public AsignacionRutaBusController(AsignacionRutaBusServicios asignacionRutaBusServicios)
        {
            this.asignacionRutaBusServicios = asignacionRutaBusServicios;
        }

        [HttpGet(Name = "ListarAsigancionesRutaBus")]
        public async Task<ActionResult<List<AsignacionRutaBusDTO>>> ListarAsigancionesRutaBus()
        {
            try 
            {
                var asignaciones = await asignacionRutaBusServicios.GetListaAsignacionRutaBus();
                return asignaciones;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{numeroBus:int}", Name = "ObtenerAsignacionRutaBusPorNumero")]
        public async Task<ActionResult<AsignacionRutaBusDTO>> ObtenerAsignacionRutaBusPorNumero(int numeroBus)
        {
            try 
            {
                var asignacion = await asignacionRutaBusServicios.GetAsignacionRutaBusPorNumero(numeroBus);
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

        [HttpGet("nombreRuta/{nombreRuta}", Name = "ObtenerAsignacionRutaBusPorNombreRuta")]
        public async Task<ActionResult<AsignacionRutaBusDTO>> ObtenerAsignacionRutaBusPorNombreRuta(string nombreRuta)
        {
            try
            {
                var asignacion = await asignacionRutaBusServicios.GetAsignacionRutaBusPorNombreRuta(nombreRuta);
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

        [HttpPost(Name = "RegistrarAsignacionRutaBus")]
        public async Task<ActionResult> RegistrarAsignacionRutaBus([FromBody] AsignacionRutaBusCreactionDTO asignacionRutaBusCreactionDTO)
        {
            try
            {
                await asignacionRutaBusServicios.PostAsignacionRutaBus(asignacionRutaBusCreactionDTO);
                return Ok("Asignacion creada exitosamente");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}", Name = "ActualizarAsignacionRutaBus")]
        public async Task<ActionResult> ActualizarAsignacionRutaBus(AsignacionRutaBusCreactionDTO asignacionRutaBusCreactionDTO, int id)
        {
            try
            {
                await asignacionRutaBusServicios.PutAsignacionRutaBus (asignacionRutaBusCreactionDTO, id);
                return Ok("Actualizacion exitosa");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:int}", Name = "EliminarAsignacionRutaBus")]
        public async Task<ActionResult> EliminarAsignacionRutaBus(int id)
        {
            try
            {
                await asignacionRutaBusServicios.DeleteAsignacionRutaBus(id);
                return Ok("Eliminacion exitosa");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("AgregarAsignacionesRutaBus/{id_bus:int}", Name = "AgregarAsignacionesRutaBus")]
        public async Task<ActionResult> AgregarAsignacionesRutaBus(int id_bus, [FromQuery(Name = "Ids_Rutas")] string ids_Rutas)
        {
            try
            {
                var rutasIds = ids_Rutas.Split(',').Select(int.Parse).ToArray();
                await asignacionRutaBusServicios.PostAsignacionesRutaBus(id_bus, rutasIds);
                return Ok(new { message = "Asignacion exitosa" });
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("EliminarAsignacionesRutaBus/{id_bus:int}", Name = "EliminarAsignacionesRutaBus")]
        public async Task<ActionResult> EliminarAsignacionesRutaBus(int id_bus, [FromQuery(Name = "Ids_Rutas")] string ids_Rutas)
        {
            try
            {
                var rutasIds = ids_Rutas.Split(',').Select(int.Parse).ToArray();
                await asignacionRutaBusServicios.DeleteAsignacionesRutaBus(id_bus, rutasIds);
                return Ok("Eliminacion exitosa");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
