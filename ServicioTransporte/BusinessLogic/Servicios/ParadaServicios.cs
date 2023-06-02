using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServicioTransporte.BusinessLogic.DTOs;
using ServicioTransporte.Entities;

namespace ServicioTransporte.BusinessLogic.Servicios
{
    public class ParadaServicios
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ParadaServicios(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<ParadaDTO>> GetListaParadas()
        {
            var paradas = await context.Paradas.Where(paradaDb => paradaDb.Id_Estado == 1).ToListAsync();
            return mapper.Map<List<ParadaDTO>>(paradas);
        }

        public async Task<ParadaDTO> GetParada(int id)
        {
            var parada = await context.Paradas.Where(paradaDb => paradaDb.Id_Estado == 1).FirstOrDefaultAsync(paradaDb => paradaDb.Id == id);
            return mapper.Map<ParadaDTO>(parada);
        }

        public async Task<ParadaCreacionDTO> PostParada(ParadaCreacionDTO paradaCreacionDTO)
        {
            var direccionRepetida = await context.Paradas.Where(paradaDb => paradaDb.Id_Estado == 1).AnyAsync(paradaDb => paradaDb.Direccion == paradaCreacionDTO.Direccion);

            if (direccionRepetida)
            {
                throw new ArgumentException($"Ya existe una parada con la direccion: {paradaCreacionDTO.Direccion}");
            }
            var parada = mapper.Map<Parada>(paradaCreacionDTO);
            context.Add(parada);
            await context.SaveChangesAsync();
            return paradaCreacionDTO;
        }

        public async Task PutParada(ParadaCreacionDTO paradaCreacionDTO, int id)
        {
            var parada = await context.Paradas.Where(paradaDb => paradaDb.Id_Estado == 1).FirstOrDefaultAsync(paradaDb => paradaDb.Id == id);
            if (parada == null)
            {
                throw new ArgumentException($"La parada con id: {id} no existe");
            }

            mapper.Map(paradaCreacionDTO, parada);
            context.Entry(parada).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteParada(int id)
        {
            var parada = await context.Paradas.Where(paradaDb => paradaDb.Id_Estado == 1).FirstOrDefaultAsync(paradaDb => paradaDb.Id == id);
            if (parada == null)
            {
                throw new ArgumentException($"La parada con id: {id} no existe");
            }

            //VALIDAR QUE NO ESTE ASOCIADO A NINGUNA RUTA
            var tieneRutaAsociada = await context.AsignacionesRutaParada.AnyAsync(x => x.Id_Ruta == id);
            if (tieneRutaAsociada)
            {
                throw new ArgumentException($"La parada con id: {id} esta asociado a una o mas rutas, primero elimine la asociacion");
            }

            parada.Id_Estado = 2;
            context.Update(parada);
            await context.SaveChangesAsync();
        }

    }
}
