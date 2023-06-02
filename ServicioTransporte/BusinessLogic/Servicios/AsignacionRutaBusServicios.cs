using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServicioTransporte.BusinessLogic.DTOs;
using ServicioTransporte.Entities;

namespace ServicioTransporte.BusinessLogic.Servicios
{
    public class AsignacionRutaBusServicios
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AsignacionRutaBusServicios(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<AsignacionRutaBusDTO>> GetListaAsignacionRutaBus()
        {
            var asignacionRutaBus = await context.AsignacionesRutaBus.Include(arbDb => arbDb.Ruta)
                        .Include(arbDb => arbDb.Bus).ToListAsync();
            return mapper.Map<List<AsignacionRutaBusDTO>>(asignacionRutaBus);
        }

        //OBTENER ASIGNACION POR NUMERO DE BUS

        public async Task<AsignacionRutaBusDTO> GetAsignacionRutaBusPorNumero(int numeroBus)
        {
            var asignacionRutaBus = await context.AsignacionesRutaBus.Include(arbDb => arbDb.Ruta)
                        .Include(arbDb => arbDb.Bus).FirstOrDefaultAsync(arb => arb.Bus.Numero == numeroBus);
            return mapper.Map<AsignacionRutaBusDTO>(asignacionRutaBus);
        }
        //OBTENER ASIGNACION POR NOMBRE DE RUTA
        public async Task<AsignacionRutaBusDTO> GetAsignacionRutaBusPorNombreRuta(string nombreRuta)
        {
            var asignacionRutaBus = await context.AsignacionesRutaBus.Include(arbDb => arbDb.Ruta)
                        .Include(arbDb => arbDb.Bus).FirstOrDefaultAsync(arb => arb.Ruta.Nombre.Contains(nombreRuta));
            return mapper.Map<AsignacionRutaBusDTO>(asignacionRutaBus);
        }

        public async Task<AsignacionRutaBusCreactionDTO> PostAsignacionRutaBus(AsignacionRutaBusCreactionDTO asignacionRutaBusCreactionDTO)
        {
            var asignacionRepetida = await context.AsignacionesRutaBus.
                FirstOrDefaultAsync(abc => abc.Id_Ruta == asignacionRutaBusCreactionDTO.Id_Ruta &&
                abc.Id_Bus == asignacionRutaBusCreactionDTO.Id_Bus);
            if (asignacionRepetida != null)
            {
                throw new ArgumentException($"Ya existe una asignacion entre la ruta con id: {asignacionRepetida.Id_Ruta} " +
                    $" y el bus con id: {asignacionRepetida.Id_Bus}");
            }

            var ruta = await context.Rutas.FirstOrDefaultAsync(rutaDb => rutaDb.Id == asignacionRutaBusCreactionDTO.Id_Ruta);
            var bus = await context.Buses.FirstOrDefaultAsync(busDb => busDb.Id == asignacionRutaBusCreactionDTO.Id_Bus);
            if (ruta == null)
            {
                throw new ArgumentException($"La ruta con id: {ruta.Id} no existe");
            }
            else if (ruta.Id_Estado == 2)
            {
                throw new ArgumentException($"La ruta con id: {ruta.Id} se encuentra inactiva");
            }
            if (bus == null)
            {
                throw new ArgumentException($"El bus con id: {bus.Id} no existe");
            }
            else if (bus.Id_Estado == 2)
            {
                throw new ArgumentException($"El bus con id: {bus.Id} se encuentra inactivo");
            }
            
            var asignacion = mapper.Map<AsignacionRutaBus>(asignacionRutaBusCreactionDTO);
            context.Add(asignacion);
            await context.SaveChangesAsync();
            return asignacionRutaBusCreactionDTO;
        }

        public async Task PutAsignacionRutaBus(AsignacionRutaBusCreactionDTO asignacionRutaBusCreactionDTO, int id)
        {
            var asignacion = await context.AsignacionesRutaBus.FirstOrDefaultAsync(arbDb => arbDb.Id == id);
            if (asignacion == null)
            {
                throw new ArgumentException($"La asignacion con id: {id} no existe");
            }

            var ruta = await context.Rutas.FirstOrDefaultAsync(rutaDb => rutaDb.Id == asignacionRutaBusCreactionDTO.Id_Ruta);
            var bus = await context.Buses.FirstOrDefaultAsync(busDb => busDb.Id == asignacionRutaBusCreactionDTO.Id_Bus);
            if (ruta == null)
            {
                throw new ArgumentException($"La ruta con id: {ruta.Id} no existe");
            }
            else if (ruta.Id_Estado == 2)
            {
                throw new ArgumentException($"La ruta con id: {ruta.Id} se encuentra inactiva");
            }
            if (bus == null)
            {
                throw new ArgumentException($"El bus con id: {bus.Id} no existe");
            }
            else if (bus.Id_Estado == 2)
            {
                throw new ArgumentException($"El bus con id: {bus.Id} se encuentra inactivo");
            }

            mapper.Map(asignacionRutaBusCreactionDTO, asignacion);
            context.Entry(asignacion).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsignacionRutaBus(int id)
        {
            var asignacion = await context.AsignacionesRutaBus.FirstOrDefaultAsync(arbDb => arbDb.Id == id);
            if (asignacion == null)
            {
                throw new ArgumentException($"La asignacion con id: {id} no existe");
            }
            context.Remove(asignacion);
            await context.SaveChangesAsync();
        }

        public async Task PostAsignacionesRutaBus(int idBus, int[] idsRutas)
        {
            foreach (var idRuta in idsRutas)
            {
                var asignacion = new AsignacionRutaBusCreactionDTO
                {
                    Id_Ruta = idRuta,
                    Id_Bus = idBus
                };

                await PostAsignacionRutaBus(asignacion);
            }
        }

        public async Task DeleteAsignacionesRutaBus(int idBus, int[] idsRutas)
        {
            var registrosAEliminar = await context.AsignacionesRutaBus
                .Where(arb => arb.Id_Bus == idBus && idsRutas.Contains(arb.Id_Ruta))
                .ToListAsync();

            if (registrosAEliminar.Count == 0)
            {
                throw new ArgumentException("No se encontro una asociacion entre el bus y la ruta seleccionada");
            }

            context.AsignacionesRutaBus.RemoveRange(registrosAEliminar);
            await context.SaveChangesAsync();
        }

    }
}
