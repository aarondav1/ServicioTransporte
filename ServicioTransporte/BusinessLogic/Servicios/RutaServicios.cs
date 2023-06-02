using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServicioTransporte.BusinessLogic.DTOs;
using ServicioTransporte.Entities;

namespace ServicioTransporte.BusinessLogic.Servicios
{
    public class RutaServicios
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RutaServicios(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<RutaDTO>> GetListaRutas()
        {
            var rutas = await context.Rutas.Where(rutaDb => rutaDb.Id_Estado == 1).ToListAsync();
            return mapper.Map<List<RutaDTO>>(rutas);
        }

        public async Task<List<RutaDTO>> GetRuta(string nombre)
        {
            var rutas = await context.Rutas.Where(rutaDb => rutaDb.Id_Estado == 1 && rutaDb.Nombre.Contains(nombre)).ToListAsync();
            return mapper.Map<List<RutaDTO>>(rutas);
        }

        //public async Task<RutaDTO> GetRutaConParadas(string nombre)
        //{
        //    var ruta = await context.Rutas.Where(rutaDb => rutaDb.Id_Estado == 1)
        //                    .Include(rutaDb => rutaDb.AsignacionesRutaParada)
        //                    .ThenInclude(arpDb => arpDb.Parada)
        //                    .FirstOrDefaultAsync(rutaDb => rutaDb.Nombre.Contains(nombre));
        //    return mapper.Map<RutaConParadasDTO>(ruta);
        //}
        public async Task<RutaConParadasDTO> GetRutaConParadas(int id)
        {
            var ruta = await context.Rutas.Where(rutaDb => rutaDb.Id_Estado == 1)
                            .Include(rutaDb => rutaDb.AsignacionesRutaParada)
                            .ThenInclude(arpDb => arpDb.Parada)
                            .FirstOrDefaultAsync(rutaDb => rutaDb.Id == id);
            return mapper.Map<RutaConParadasDTO>(ruta);
        }

        public async Task<List<RutaConParadasDTO>> GetRutasConParadas()
        {
            var ruta = await context.Rutas.Where(rutaDb => rutaDb.Id_Estado == 1)
                            .Include(rutaDb => rutaDb.AsignacionesRutaParada)
                            .ThenInclude(arpDb => arpDb.Parada).ToListAsync();
            return mapper.Map<List<RutaConParadasDTO>>(ruta);
        }
        //GetRutaConParadasNoAsociadas
        public async Task<RutaConParadasDTO> GetRutaConParadasNoAsociadas(int idRuta)
        {
            var ruta = await context.Rutas
                            .Where(rutaDb => rutaDb.Id_Estado == 1 && rutaDb.Id == idRuta)
                            .Include(rutaDb => rutaDb.AsignacionesRutaParada)
                            .ThenInclude(arpDb => arpDb.Parada)
                            .FirstOrDefaultAsync();

            var paradasAsociadas = ruta.AsignacionesRutaParada.Select(arpDb => arpDb.Parada.Id).ToList();
            var paradasNoAsociadas = await context.Paradas
                                .Where(paradaDb => !paradasAsociadas.Contains(paradaDb.Id) && paradaDb.Id_Estado == 1)
                                .ToListAsync();

            var rutaConParadasDTO = mapper.Map<RutaConParadasDTO>(ruta);
            rutaConParadasDTO.ParadasDTO = mapper.Map<List<ParadaDTO>>(paradasNoAsociadas);
            return rutaConParadasDTO;
        }

        public async Task<RutaCreacionDTO> PostRuta(RutaCreacionDTO rutaCreacionDTO)
        {
            var nombreRepetido = await context.Rutas.Where(rutaDb => rutaDb.Id_Estado == 1).AnyAsync(rutaDb => rutaDb.Nombre == rutaCreacionDTO.Nombre);

            if (nombreRepetido)
            {
                throw new ArgumentException($"Ya existe una ruta con el nombre: {rutaCreacionDTO.Nombre}");
            }
            var ruta = mapper.Map<Ruta>(rutaCreacionDTO);
            context.Add(ruta);
            await context.SaveChangesAsync();
            return rutaCreacionDTO;
        }

        public async Task PutRuta(RutaCreacionDTO rutaCreacionDTO, int id)
        {
            var ruta = await context.Rutas.Where(rutaDb => rutaDb.Id_Estado == 1).FirstOrDefaultAsync(rutaDb => rutaDb.Id == id);
            if (ruta == null)
            {
                throw new ArgumentException($"La ruta con id: {id} no existe");
            }

            mapper.Map(rutaCreacionDTO, ruta);
            context.Entry(ruta).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteRuta(int id)
        {
            var ruta = await context.Rutas.Where(rutaDb => rutaDb.Id_Estado == 1).FirstOrDefaultAsync(rutaDb => rutaDb.Id == id);
            if (ruta == null)
            {
                throw new ArgumentException($"La ruta con id: {id} no existe");
            }

            //VALIDAR QUE NO ESTE ASOCIADO A NINGUN BUS
            var tieneBusAsociado = await context.AsignacionesRutaBus.AnyAsync(x => x.Id_Ruta == id);
            if (tieneBusAsociado)
            {
                throw new ArgumentException($"La ruta con id: {id} esta asociado a uno o mas buses, primero elimine la asociacion");
            }

            //VALIDAR QUE NO ESTE ASOCIADO A NINGUNA PARADA
            var tieneParadaAsociada = await context.AsignacionesRutaParada.AnyAsync(x => x.Id_Ruta == id);
            if (tieneParadaAsociada)
            {
                throw new ArgumentException($"La ruta con id: {id} esta asociado a una o mas paradas, primero elimine la asociacion");
            }

            ruta.Id_Estado = 2;
            context.Update(ruta);
            await context.SaveChangesAsync();
        }

    }
}
