using Microsoft.AspNetCore.Mvc;
using ServicioTransporte.BusinessLogic.DTOs;
using ServicioTransporte.BusinessLogic.Servicios;

namespace ServicioTransporte.Controllers
{
    [ApiController]
    [Route("api/bus")]
    public class BusController: ControllerBase
    {
        private readonly BusServicios busServicios;

        public BusController(BusServicios busServicios)
        {
            this.busServicios = busServicios;
        }

        [HttpGet(Name = "ListarBuses")]
        public async Task<ActionResult<List<BusDTO>>> ListarBuses()
        {
            try
            {
                var buses = await busServicios.GetListaBuses();
                return buses;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{numeroBus:int}", Name = "ObtenerBus")]
        public async Task<ActionResult<BusDTO>> ObtenerBus(int numeroBus)
        {
            try
            {
                var bus = await busServicios.GetBus(numeroBus);
                if (bus == null)
                {
                    return NotFound($"No existe bus con el numero: {numeroBus}");
                }
                return bus;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet("ObtenerBusConRutas/{numeroBus:int}", Name = "ObtenerBusConRutas")]
        //public async Task<ActionResult<BusDTO>> ObtenerBusConRutas(int numeroBus)
        //{
        //    try
        //    {
        //        var bus = await busServicios.GetBusConRutas(numeroBus);
        //        return bus;
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpGet("ObtenerBusesConRutas", Name = "ObtenerBusesConRutas")]
        public async Task<ActionResult<List<BusConRutasDTO>>> ObtenerBusesConRutas()
        {
            try
            {
                var buses = await busServicios.GetBusesConRutas();
                return buses;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerBusConRutas/{id:int}", Name = "ObtenerBusConRutas")]
        public async Task<ActionResult<BusConRutasDTO>> ObtenerBusConRutas(int id)
        {
            try
            {
                var bus = await busServicios.GetBusConRutas(id);
                return bus;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerBusConRutasNoAsociadas/{id:int}", Name = "ObtenerBusConRutasNoAsociadas")]
        public async Task<ActionResult<BusConRutasDTO>> ObtenerBusConRutasNoAsociadas(int id)
        {
            try
            {
                var bus = await busServicios.GetBusConRutasNoAsociadas(id);
                return bus;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Name = "RegistrarBus")]
        public async Task<ActionResult> RegistrarBus([FromBody] BusCreacionDTO busCreacionDTO)
        {
            try
            {
                await busServicios.PostBus(busCreacionDTO);
                return Ok("Bus agregado exitosamente");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}", Name = "ActualizarBus")]
        public async Task<ActionResult> ActualizarBus(BusCreacionDTO busCreacionDTO, int id)
        {
            try
            {
                await busServicios.PutBus(busCreacionDTO, id);
                return Ok("Actualizacion exitosa");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:int}", Name = "EliminarBus")]
        public async Task<ActionResult> EliminarBus(int id)
        {
            try
            {
                await busServicios.DeleteBus(id);
                return Ok("Eliminacion exitosa");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
