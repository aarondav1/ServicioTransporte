using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServicioTransporte.BusinessLogic.DTOs;
using ServicioTransporte.Entities;

namespace ServicioTransporte.BusinessLogic.Servicios
{
    public class AsignacionRutaParadaServicios
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AsignacionRutaParadaServicios(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<AsignacionRutaParadaDTO>> GetListaAsignacionRutaParada()
        {
            var asignacionRutaParada = await context.AsignacionesRutaParada.Include(arpDb => arpDb.Ruta)
                        .Include(arbDb => arbDb.Parada).ToListAsync();
            return mapper.Map<List<AsignacionRutaParadaDTO>>(asignacionRutaParada);
        }

        //OBTENER ASIGNACION POR NOMBRE DE LA RUTA
        public async Task<List<AsignacionRutaParadaDTO>> GetAsignacionRutaParada(string nombreRuta)
        {
            var asignacionRutaParada = await context.AsignacionesRutaParada.Include(arpDb => arpDb.Ruta)
                        .Include(arpDb => arpDb.Parada).Where(arpDb => arpDb.Ruta.Nombre.Contains(nombreRuta)).ToListAsync();
            return mapper.Map<List<AsignacionRutaParadaDTO>>(asignacionRutaParada);
        }

        public async Task<AsignacionRutaParadaCreacionDTO> PostAsignacionRutaParada(AsignacionRutaParadaCreacionDTO asignacionRutaParadaCreacionDTO)
        {
            var asignacionRepetida = await context.AsignacionesRutaParada.
                FirstOrDefaultAsync(arpDb => arpDb.Id_Ruta == asignacionRutaParadaCreacionDTO.Id_Ruta &&
                arpDb.Id_Parada == asignacionRutaParadaCreacionDTO.Id_Parada);
            if (asignacionRepetida != null)
            {
                throw new ArgumentException($"Ya existe una asignacion entre la ruta con id: {asignacionRepetida.Id_Ruta} " +
                    $" y la parada con id: {asignacionRepetida.Id_Parada}");
            }

            var ruta = await context.Rutas.FirstOrDefaultAsync(rutaDb => rutaDb.Id == asignacionRutaParadaCreacionDTO.Id_Ruta);
            var parada = await context.Paradas.FirstOrDefaultAsync(paradaDb => paradaDb.Id == asignacionRutaParadaCreacionDTO.Id_Parada);

            if (ruta == null)
            {
                throw new ArgumentException($"La ruta con id: {ruta.Id} se encuentra inactiva");
            }
            else if (ruta.Id_Estado == 2)
            {
                throw new ArgumentException($"La ruta con id: {ruta.Id} se encuentra inactiva");
            }

            if (parada == null)
            {
                throw new ArgumentException($"La parada con id: {parada.Id} no existe");
            }
            else if (parada.Id_Estado == 2)
            {
                throw new ArgumentException($"La parada con id: {parada.Id} se encuentra inactiva");
            }

            var asignacion = mapper.Map<AsignacionRutaParada>(asignacionRutaParadaCreacionDTO);
            context.Add(asignacion);
            await context.SaveChangesAsync();
            return asignacionRutaParadaCreacionDTO;
        }

        public async Task PutAsignacionRutaParada(AsignacionRutaParadaCreacionDTO asignacionRutaParadaCreacionDTO, int id)
        {
            var asignacion = await context.AsignacionesRutaParada.FirstOrDefaultAsync(arpDB => arpDB.Id == id);
            if (asignacion == null)
            {
                throw new ArgumentException($"La asignacion con id: {id} no existe");
            }

            var ruta = await context.Rutas.FirstOrDefaultAsync(rutaDb => rutaDb.Id == asignacionRutaParadaCreacionDTO.Id_Ruta);
            var parada = await context.Paradas.FirstOrDefaultAsync(paradaDb => paradaDb.Id == asignacionRutaParadaCreacionDTO.Id_Parada);

            if (ruta == null)
            {
                throw new ArgumentException($"La ruta con id: {ruta.Id} se encuentra inactiva");
            }
            else if (ruta.Id_Estado == 2)
            {
                throw new ArgumentException($"La ruta con id: {ruta.Id} se encuentra inactiva");
            }

            if (parada == null)
            {
                throw new ArgumentException($"La parada con id: {parada.Id} no existe");
            }
            else if (parada.Id_Estado == 2)
            {
                throw new ArgumentException($"La parada con id: {parada.Id} se encuentra inactiva");
            }

            mapper.Map(asignacionRutaParadaCreacionDTO, asignacion);
            context.Entry(asignacion).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsignacionRutaParada(int id)
        {
            var asignacion = await context.AsignacionesRutaParada.FirstOrDefaultAsync(arpDB => arpDB.Id == id);
            if (asignacion == null)
            {
                throw new ArgumentException($"La asignacion con id: {id} no existe");
            }
            context.Remove(asignacion);
            await context.SaveChangesAsync();
        }

        public async Task PostAsignacionesRutaParada(int idRuta, int[] idsParadas)
        {
            foreach (var idParada in idsParadas)
            {
                var asignacion = new AsignacionRutaParadaCreacionDTO
                {
                    Id_Ruta = idRuta,
                    Id_Parada = idParada
                };

                await PostAsignacionRutaParada(asignacion);
            }
        }

        public async Task DeleteAsignacionesRutaParada(int idRuta, int[] idsParadas)
        {
            var registrosAEliminar = await context.AsignacionesRutaParada
                .Where(arpDb => arpDb.Id_Ruta == idRuta && idsParadas.Contains(arpDb.Id_Parada))
                .ToListAsync();

            if (registrosAEliminar.Count == 0)
            {
                throw new ArgumentException("No se encontro una asociacion entre la ruta y la parada seleccionada");
            }

            context.AsignacionesRutaParada.RemoveRange(registrosAEliminar);
            await context.SaveChangesAsync();
        }

    }
}
