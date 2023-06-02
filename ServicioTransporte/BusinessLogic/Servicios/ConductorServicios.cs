using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServicioTransporte.BusinessLogic.DTOs;
using ServicioTransporte.Entities;

namespace ServicioTransporte.BusinessLogic.Servicios
{
    public class ConductorServicios
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ConductorServicios(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        //LISTAR ACTIVOS E INACTIVOS
        public async Task<List<ConductorDTO>> LISTARTODOS()
        {
            var conductores = await context.Conductores.ToListAsync();
            return mapper.Map<List<ConductorDTO>>(conductores);
        }
        //
        public async Task<List<ConductorDTO>> GetListaConductores()
        {
            var conductores = await context.Conductores.Where(conductorDb => conductorDb.Id_Estado == 1).ToListAsync();
            return mapper.Map<List<ConductorDTO>>(conductores);
        }

        public async Task<ConductorDTO> GetConductor(int id)
        {
            var conductor = await context.Conductores.Where(conductorDb => conductorDb.Id_Estado == 1)
                            .FirstOrDefaultAsync(conductor => conductor.Id == id);
            return mapper.Map<ConductorDTO>(conductor);
        }

        public async Task<ConductorConBusesDTO> GetConductorConBuses(int id)
        {
            var conductor = await context.Conductores.Where(conductorDb => conductorDb.Id_Estado == 1)
                            .Include(conductorDb => conductorDb.AsignacionesBusConductor)
                            .ThenInclude(abcDb => abcDb.Bus)
                            .FirstOrDefaultAsync(conductor => conductor.Id == id);
            return mapper.Map<ConductorConBusesDTO>(conductor);
        }

        public async Task<List<ConductorConBusesDTO>> GetConductoresConBuses()
        {
            var conductor = await context.Conductores.Where(conductorDb => conductorDb.Id_Estado == 1)
                            .Include(conductorDb => conductorDb.AsignacionesBusConductor)
                            .ThenInclude(abcDb => abcDb.Bus).ToListAsync();
            return mapper.Map<List<ConductorConBusesDTO>>(conductor);
        }
        public async Task<ConductorCreacionDTO> PostConductor(ConductorCreacionDTO conductorCreacionDTO)
        {
            var cedulaRepetida = await context.Conductores.Where(conductorDb => conductorDb.Id_Estado == 1).AnyAsync(conductorDb => conductorDb.Cedula == conductorCreacionDTO.Cedula);

            if (cedulaRepetida)
            {
                throw new ArgumentException("Ya existe un conductor con la cedula ingresada");
            }
            var conductor = mapper.Map<Conductor>(conductorCreacionDTO);
            context.Add(conductor);
            await context.SaveChangesAsync();
            return conductorCreacionDTO;
        }

        public async Task PutConductor(ConductorCreacionDTO conductorCreacionDTO, int id)
        {
            var conductor = await context.Conductores.Where(conductorDb => conductorDb.Id_Estado == 1).FirstOrDefaultAsync(conductor => conductor.Id == id);
            if (conductor == null)
            {
                throw new ArgumentException("El conductor no existe");
            }

            mapper.Map(conductorCreacionDTO, conductor);
            //context.Update(conductor);
            context.Entry(conductor).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteConductor(int id)
        {
            //VALIDAR QUE NO ESTE ASOCIADO A NINGUN BUS
            var tieneBusAsociado = await context.AsignacionesBusConductor.AnyAsync(x => x.Id_Conductor == id);
            if (tieneBusAsociado)
            {
                throw new ArgumentException($"El conductor con id: {id} esta asociado a uno o mas buses, primero elimine la asociacion");
            }

            var conductor = await context.Conductores.Where(conductorDb => conductorDb.Id_Estado == 1).FirstOrDefaultAsync(conductorDb => conductorDb.Id == id);
            if (conductor == null)
            {
                throw new ArgumentException($"El conductor con id: {id} no existe");
            }
            conductor.Id_Estado = 2;
            context.Update(conductor);
            await context.SaveChangesAsync();
        }

    }
}
