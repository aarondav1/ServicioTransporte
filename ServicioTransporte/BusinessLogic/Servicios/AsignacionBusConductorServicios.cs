using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServicioTransporte.BusinessLogic.DTOs;
using ServicioTransporte.Entities;

namespace ServicioTransporte.BusinessLogic.Servicios
{
    public class AsignacionBusConductorServicios
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AsignacionBusConductorServicios(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<AsignacionBusConductorDTO>> GetListaAsignacionBusConductor()
        {
            var asignacionBusConductor = await context.AsignacionesBusConductor.Include(abc => abc.Bus)
                            .Include(abc => abc.Conductor).ToListAsync();
            return mapper.Map<List<AsignacionBusConductorDTO>>(asignacionBusConductor);
        }

        public async Task<AsignacionBusConductorDTO> GetAsignacionBusConductor(int numeroBus)
        {
            var asignacionBusConductor = await context.AsignacionesBusConductor.Include(abc => abc.Bus)
                            .Include(abc => abc.Conductor).FirstOrDefaultAsync(abc => abc.Bus.Numero == numeroBus);
            return mapper.Map<AsignacionBusConductorDTO>(asignacionBusConductor);
        }

        public async Task<AsignacionBusConductorCreacionDTO> PostAsignacionBusConductor(AsignacionBusConductorCreacionDTO asignacionBusConductorCreacionDTO)
        {
            //VALIDAR QUE LA ASIGNACION NO ESTE YA REGISTRADA
            var asignacionRepetida = await context.AsignacionesBusConductor.
                FirstOrDefaultAsync(abc => abc.Id_Conductor == asignacionBusConductorCreacionDTO.Id_Conductor && 
                abc.Id_Bus == asignacionBusConductorCreacionDTO.Id_Bus);
            if(asignacionRepetida != null)
            {
                throw new ArgumentException($"Ya una asignacion entre el conductor con id: {asignacionRepetida.Id_Conductor} " +
                    $" y el bus con id: {asignacionRepetida.Id_Bus}");
            }

            var conductor = await context.Conductores.FirstOrDefaultAsync(conductorDb => conductorDb.Id == asignacionBusConductorCreacionDTO.Id_Conductor);
            var bus = await context.Buses.FirstOrDefaultAsync(busDb => busDb.Id == asignacionBusConductorCreacionDTO.Id_Bus);
            //VALIDAR QUE EL CONDUCTOR EXISTA Y NO SE ENCUENTRE INACTIVO
            if(conductor == null)
            {
                throw new ArgumentException($"El conductor con id: {asignacionBusConductorCreacionDTO.Id_Conductor} no existe");
            }
            else if(conductor.Id_Estado == 2)
            {
                throw new ArgumentException($"El conductor con id: {conductor.Id} se encuentra inactivo");
            }
            //VALIDAR QUE EL BUS EXISTA Y NO SE ENCUENTRE INACTIVO
            if(bus == null)
            {
                throw new ArgumentException($"El bus con id: {asignacionBusConductorCreacionDTO.Id_Bus} no existe");
            }
            else if(bus.Id_Estado == 2)
            {
                throw new ArgumentException($"El bus con id: {bus.Id} se encuentra inactivo");
            }

            var asignacion = mapper.Map<AsignacionBusConductor>(asignacionBusConductorCreacionDTO);
            context.Add(asignacion);
            await context.SaveChangesAsync();
            return asignacionBusConductorCreacionDTO;
        }

        public async Task PutAsignacionBusConductor(AsignacionBusConductorCreacionDTO asignacionBusConductorCreacionDTO, int id)
        {
            var asignacion = await context.AsignacionesBusConductor.FirstOrDefaultAsync(abc => abc.Id == id);
            if (asignacion == null)
            {
                throw new ArgumentException($"La asignacion con id: {id} no existe");
            }

            var conductor = await context.Conductores.FirstOrDefaultAsync(conductorDb => conductorDb.Id == asignacionBusConductorCreacionDTO.Id_Conductor);
            var bus = await context.Buses.FirstOrDefaultAsync(busDb => busDb.Id == asignacionBusConductorCreacionDTO.Id_Bus);
            if (conductor == null)
            {
                throw new ArgumentException($"El conductor con id: {asignacionBusConductorCreacionDTO.Id_Conductor} no existe");
            }
            else if (conductor.Id_Estado == 2)
            {
                throw new ArgumentException($"El conductor con id: {conductor.Id} se encuentra inactivo");
            }
            if (bus == null)
            {
                throw new ArgumentException($"El bus con id: {asignacionBusConductorCreacionDTO.Id_Bus} no existe");
            }
            else if (bus.Id_Estado == 2)
            {
                throw new ArgumentException($"El bus con id: {bus.Id} se encuentra inactivo");
            }

            mapper.Map(asignacionBusConductorCreacionDTO, asignacion);
            context.Entry(asignacion).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsignacionBusConductor(int id)
        {
            var asignacion = await context.AsignacionesBusConductor.FirstOrDefaultAsync(abc => abc.Id == id);
            if (asignacion == null)
            {
                throw new ArgumentException($"La asignacion con id: {id} no existe");
            }
            context.Remove(asignacion);
            await context.SaveChangesAsync();
        }

        public async Task DeletePorIdConductor(int idConductor)
        {
            var asignacionBusConductor = await context.AsignacionesBusConductor.FirstOrDefaultAsync(abc => 
                abc.Id_Conductor == idConductor);
            if (asignacionBusConductor == null)
            {
                throw new ArgumentException($"La asignacion con id conductor: {idConductor} no existe");
            }
            context.Remove(asignacionBusConductor);
            await context.SaveChangesAsync();
        }
    }
}
