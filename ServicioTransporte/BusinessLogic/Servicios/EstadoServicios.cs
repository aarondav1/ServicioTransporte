using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServicioTransporte.BusinessLogic.DTOs;

namespace ServicioTransporte.BusinessLogic.Servicios
{
    public class EstadoServicios
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public EstadoServicios(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<EstadoDTO>> GetListaEstados()
        {
            var estados = await context.Estados.ToListAsync();
            return mapper.Map<List<EstadoDTO>>(estados);
        }

        public async Task<EstadoDTO> GetEstado(int id)
        {
            var estado = await context.Estados.FirstOrDefaultAsync(estadoDb => estadoDb.Id == id);
            return mapper.Map<EstadoDTO>(estado);
        }

    }
}
