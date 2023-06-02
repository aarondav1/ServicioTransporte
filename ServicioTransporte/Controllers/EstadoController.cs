using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServicioTransporte.BusinessLogic.DTOs;
using ServicioTransporte.BusinessLogic.Servicios;
using ServicioTransporte.Entities;

namespace ServicioTransporte.Controllers
{
    [ApiController]
    [Route("api/estado")]
    public class EstadoController: ControllerBase
    {
        private readonly EstadoServicios estadoServicios;

        public EstadoController(EstadoServicios estadoServicios)
        {
            this.estadoServicios = estadoServicios;
        }


        [HttpGet(Name = "ListarEstados")]
        public async Task<ActionResult<List<EstadoDTO>>> ListarEstados()
        {
            try
            {
                var estados = await estadoServicios.GetListaEstados();
                return estados;
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("{id:int}", Name = "ObtenerEstado")]
        public async Task<ActionResult<EstadoDTO>> ObtenerEstado(int id)
        {
            try
            {
                var estado = await estadoServicios.GetEstado(id);
                if (estado == null)
                {
                    return NotFound($"No existe un estado con el id {id}");
                }
                return estado;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
