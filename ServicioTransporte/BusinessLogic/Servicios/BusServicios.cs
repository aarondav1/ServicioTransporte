using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServicioTransporte.BusinessLogic.DTOs;
using ServicioTransporte.Entities;

namespace ServicioTransporte.BusinessLogic.Servicios
{
    public class BusServicios
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public BusServicios(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        public async Task<List<BusDTO>> GetListaBuses()
        {
            var buses = await context.Buses.Where(busDb => busDb.Id_Estado == 1).ToListAsync();
            return mapper.Map<List<BusDTO>>(buses);
        }

        public async Task<BusDTO> GetBus(int numeroBus)
        {
            var bus = await context.Buses.Where(busDb => busDb.Id_Estado == 1).FirstOrDefaultAsync(busDb => busDb.Numero == numeroBus);
            return mapper.Map<BusDTO>(bus);
        }

        //public async Task<BusDTO> GetBusConRutas(int numeroBus)
        //{
        //    var bus = await context.Buses.Where(busDb => busDb.Id_Estado == 1)
        //                    .Include(busDb => busDb.AsignacionesRutaBus)
        //                    .ThenInclude(abcDb => abcDb.Ruta)
        //                    .FirstOrDefaultAsync(busDb => busDb.Numero == numeroBus);
        //    return mapper.Map<BusConRutasDTO>(bus);
        //}
        public async Task<List<BusConRutasDTO>> GetBusesConRutas()
        {
            var bus = await context.Buses.Where(busDb => busDb.Id_Estado == 1)
                            .Include(busDb => busDb.AsignacionesRutaBus)
                            .ThenInclude(abcDb => abcDb.Ruta).ToListAsync();
            return mapper.Map<List<BusConRutasDTO>>(bus);
        }
        public async Task<BusConRutasDTO> GetBusConRutas(int idBus)
        {
            var bus = await context.Buses.Where(busDb => busDb.Id_Estado == 1)
                            .Include(busDb => busDb.AsignacionesRutaBus)
                            .ThenInclude(abcDb => abcDb.Ruta)
                            .FirstOrDefaultAsync(busDb => busDb.Id == idBus);
            return mapper.Map<BusConRutasDTO>(bus);
        }

        public async Task<BusConRutasDTO> GetBusConRutasNoAsociadas(int idBus)
        {
            var bus = await context.Buses
                            .Where(busDb => busDb.Id_Estado == 1 && busDb.Id == idBus)
                            .Include(busDb => busDb.AsignacionesRutaBus)
                            .ThenInclude(abcDb => abcDb.Ruta)
                            .FirstOrDefaultAsync();

            var rutasAsociadas = bus.AsignacionesRutaBus.Select(abc => abc.Ruta.Id).ToList();
            var rutasNoAsociadas = await context.Rutas
                            .Where(rutaDb => !rutasAsociadas.Contains(rutaDb.Id) && rutaDb.Id_Estado == 1)
                            .ToListAsync();

            var busConRutasDTO = mapper.Map<BusConRutasDTO>(bus);
            busConRutasDTO.RutasDTO = mapper.Map<List<RutaDTO>>(rutasNoAsociadas);

            return busConRutasDTO;
        }


        public async Task<BusCreacionDTO> PostBus(BusCreacionDTO busCreacionDTO)
        {
            var placaRepetida = await context.Buses.Where(busDb => busDb.Id_Estado == 1).AnyAsync(busDb => busDb.Placa == busCreacionDTO.Placa);

            if (placaRepetida)
            {
                throw new ArgumentException($"Ya existe un bus con la placa: {busCreacionDTO.Placa}");
            }
            var bus = mapper.Map<Bus>(busCreacionDTO);
            context.Add(bus);
            await context.SaveChangesAsync();
            return busCreacionDTO;
        }

        public async Task PutBus(BusCreacionDTO busCreacionDTO, int id)
        {
            var bus = await context.Buses.Where(busDb => busDb.Id_Estado == 1).FirstOrDefaultAsync(busDb => busDb.Id == id);
            if (bus == null)
            {
                throw new ArgumentException($"El bus con id: {id} no existe");
            }

            mapper.Map(busCreacionDTO, bus);
            context.Entry(bus).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteBus(int id)
        {
            //VALIDAR QUE NO ESTE ASOCIADO A NINGUN CONDUCTOR
            var tieneConductorAsociado = await context.AsignacionesBusConductor.AnyAsync(x => x.Id_Bus == id);
            if (tieneConductorAsociado)
            {
                throw new ArgumentException("El Bus esta asociado a un conductor, primero elimine la asociacion");
            }

            //VALIDAR QUE NO ESTE ASOCIADO A NINGUNA RUTA
            var tieneRutaAsociada = await context.AsignacionesRutaBus.AnyAsync(x => x.Id_Bus == id);
            if (tieneRutaAsociada)
            {
                throw new ArgumentException($"El Bus con id: {id} esta asociado a una o mas rutas, primero elimine la asociacion");
            }

            var bus = await context.Buses.Where(busDb => busDb.Id_Estado == 1).FirstOrDefaultAsync(busDb => busDb.Id == id);
            if (bus == null)
            {
                throw new ArgumentException($"El bus con id: {id} no existe");
            }
            bus.Id_Estado = 2;
            context.Update(bus);
            await context.SaveChangesAsync();
        }

    }
}
