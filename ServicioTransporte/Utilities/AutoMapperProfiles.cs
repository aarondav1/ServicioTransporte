using AutoMapper;
using ServicioTransporte.BusinessLogic.DTOs;
using ServicioTransporte.Entities;

namespace ServicioTransporte.Utilities
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            //CONDUCTORES
            CreateMap<ConductorCreacionDTO, Conductor>();
            CreateMap<Conductor, ConductorDTO>();
            CreateMap<ConductorDTO, Conductor>();

            CreateMap<Conductor, ConductorConBusesDTO>()
                .ForMember(conductorDTO => conductorDTO.BusesDTO, opciones => opciones.MapFrom(MapConductorConBusesDTO));

            //BUSES
            CreateMap<BusCreacionDTO, Bus>();
            CreateMap<Bus, BusDTO>();
            CreateMap<BusDTO, Bus>();

            CreateMap<Bus, BusConRutasDTO>()
                .ForMember(busDTO => busDTO.RutasDTO, opciones => opciones.MapFrom(MapBusConRutasDTO));

            //RUTAS
            CreateMap<RutaCreacionDTO, Ruta>();
            CreateMap<Ruta, RutaDTO>();
            CreateMap<RutaDTO, Ruta>();
            CreateMap<Ruta, RutaConParadasDTO>()
                .ForMember(rutaDTO => rutaDTO.ParadasDTO, opciones => opciones.MapFrom(MapRutaConParadasDTO));

            //PARADAS
            CreateMap<ParadaCreacionDTO, Parada>();
            CreateMap<Parada, ParadaDTO>();
            CreateMap<ParadaDTO, Parada>();

            //ESTADOS
            CreateMap<Estado, EstadoDTO>();

            //ASIGNACION BUS CONDUCTOR
            CreateMap<AsignacionBusConductorCreacionDTO, AsignacionBusConductor>();
            CreateMap<AsignacionBusConductor, AsignacionBusConductorDTO>()
                .ForMember(abcDTO => abcDTO.ConductorDTO, opciones => opciones.MapFrom(MapABCconductor))
                .ForMember(abcDTO => abcDTO.BusDTO, opciones => opciones.MapFrom(MapABCbus));
            CreateMap<AsignacionBusConductorDTO, AsignacionBusConductor>();

            //ASIGNACION RUTA BUS
            CreateMap<AsignacionRutaBusCreactionDTO, AsignacionRutaBus>();
            CreateMap<AsignacionRutaBus, AsignacionRutaBusDTO>()
                .ForMember(arbDTO => arbDTO.BusDTO, opciones => opciones.MapFrom(MapARBbus))
                .ForMember(arbDTO => arbDTO.RutaDTO, opciones =>  opciones.MapFrom(MapARBruta));
            CreateMap<AsignacionRutaBusDTO, AsignacionRutaBus>();

            //ASIGNACION RUTA PARADA
            CreateMap<AsignacionRutaParadaCreacionDTO, AsignacionRutaParada>();
            CreateMap<AsignacionRutaParada, AsignacionRutaParadaDTO>()
                .ForMember(arpDTO => arpDTO.RutaDTO, opciones => opciones.MapFrom(MapARPruta))
                .ForMember(arpDTO => arpDTO.ParadaDTO, opciones => opciones.MapFrom(MapARPparada)); ;
            CreateMap<AsignacionRutaParadaDTO, AsignacionRutaParada>();

        }

        private List<BusDTO> MapConductorConBusesDTO(Conductor conductor, ConductorConBusesDTO conductorConBusesDTO)
        {
            var resultado = new List<BusDTO>();
            if(conductor != null)
            {
                foreach(var asigancionBusConductor in conductor.AsignacionesBusConductor)
                {
                    resultado.Add(new BusDTO()
                    {
                        Id = asigancionBusConductor.Id_Bus,
                        Id_Estado = asigancionBusConductor.Bus.Id_Estado,
                        Numero = asigancionBusConductor.Bus.Numero,
                        Placa = asigancionBusConductor.Bus.Placa,
                        Modelo = asigancionBusConductor.Bus.Modelo,
                        Capacidad = asigancionBusConductor.Bus.Capacidad,
                        Anio = asigancionBusConductor.Bus.Anio
                    });
                }
            }
            return resultado;
        }
        
        private List<RutaDTO> MapBusConRutasDTO(Bus bus, BusConRutasDTO busConRutasDTO)
        {
            var resultado = new List<RutaDTO>();
            if (bus != null)
            {
                foreach (var asignacionRutaBus in bus.AsignacionesRutaBus)
                {
                    resultado.Add(new RutaDTO()
                    {
                        Id = asignacionRutaBus.Id_Ruta,
                        Id_Estado = asignacionRutaBus.Ruta.Id_Estado,
                        Nombre = asignacionRutaBus.Ruta.Nombre,
                        Origen = asignacionRutaBus.Ruta.Origen,
                        Destino = asignacionRutaBus.Ruta.Destino
                    });
                }
            }
            return resultado;
        }
        
        private List<ParadaDTO> MapRutaConParadasDTO(Ruta ruta, RutaConParadasDTO rutaConParadasDTO)
        {
            var resultado = new List<ParadaDTO>();
            if(ruta != null)
            {
                foreach (var asignacionRutaParada in ruta.AsignacionesRutaParada)
                {
                    resultado.Add(new ParadaDTO()
                    {
                        Id = asignacionRutaParada.Id_Parada,
                        Id_Estado = asignacionRutaParada.Parada.Id_Estado,
                        Direccion = asignacionRutaParada.Parada.Direccion
                    });
                }
            }
            return resultado;
        }

        private ConductorDTO MapABCconductor(AsignacionBusConductor abc, AsignacionBusConductorDTO acbDTO)
        {
            var resultado = new ConductorDTO();
            if(abc != null)
            {
                resultado.Id = abc.Conductor.Id;
                resultado.Id_Estado = abc.Conductor.Id_Estado;
                resultado.Nombres = abc.Conductor.Nombres;
                resultado.Apellidos = abc.Conductor.Apellidos;
                resultado.Cedula = abc.Conductor.Cedula;
                resultado.Id_Tipo_Licencia = abc.Conductor.Id_Tipo_Licencia;
            };
            return resultado;
        }

        private BusDTO MapABCbus(AsignacionBusConductor abc, AsignacionBusConductorDTO acbDTO)
        {
            var resultado = new BusDTO();
            if(abc != null)
            {
                resultado.Id = abc.Bus.Id;
                resultado.Id_Estado = abc.Bus.Id_Estado;
                resultado.Numero = abc.Bus.Numero;
                resultado.Placa = abc.Bus.Placa;
                resultado.Modelo = abc.Bus.Modelo;
                resultado.Capacidad = abc.Bus.Capacidad;
                resultado.Anio = abc.Bus.Anio;
            };
            return resultado;
        }

        private BusDTO MapARBbus(AsignacionRutaBus arb, AsignacionRutaBusDTO arbDTO)
        {
            var resultado = new BusDTO();

            if(arb != null)
            {
                resultado.Id = arb.Bus.Id;
                resultado.Id_Estado = arb.Bus.Id_Estado;
                resultado.Numero = arb.Bus.Numero;
                resultado.Placa = arb.Bus.Placa;
                resultado.Modelo = arb.Bus.Modelo;
                resultado.Capacidad = arb.Bus.Capacidad;
                resultado.Anio = arb.Bus.Anio;
            }

            return resultado;
        }

        private RutaDTO MapARBruta(AsignacionRutaBus arb, AsignacionRutaBusDTO arbDTO)
        {
            var resultado = new RutaDTO();
            if(arb != null)
            {
                resultado.Id = arb.Ruta.Id;
                resultado.Id_Estado = arb.Ruta.Id_Estado;
                resultado.Nombre = arb.Ruta.Nombre;
                resultado.Origen = arb.Ruta.Origen;
                resultado.Destino = arb.Ruta.Destino;
            }
            return resultado;
        }

        private RutaDTO MapARPruta(AsignacionRutaParada arp, AsignacionRutaParadaDTO arpDTO)
        {
            var resultado = new RutaDTO();
            if (arp != null)
            {
                resultado.Id = arp.Ruta.Id;
                resultado.Id_Estado = arp.Ruta.Id_Estado;
                resultado.Nombre = arp.Ruta.Nombre;
                resultado.Origen = arp.Ruta.Origen;
                resultado.Destino = arp.Ruta.Destino;
            }
            return resultado;
        }
        private ParadaDTO MapARPparada(AsignacionRutaParada arp, AsignacionRutaParadaDTO arpDTO)
        {
            var resultado = new ParadaDTO();
            if (arp != null)
            {
                resultado.Id = arp.Parada.Id;
                resultado.Id_Estado = arp.Parada.Id_Estado;
                resultado.Direccion = arp.Parada.Direccion;
            }
            return resultado;
        }
        //private List<ConductorDTO> MapABC(List<AsignacionBusConductor> abc, AsignacionBusConductorDTO acbDTO)
        //{
        //    var resultado = new List<ConductorDTO>();
        //    if (abc == null) { return resultado; }
        //    foreach(var asignacion in abc)
        //    {
        //        resultado.Add(new ConductorDTO()
        //        {
        //            Id = asignacion.Conductor.Id,
        //            Id_Estado = asignacion.Conductor.Id_Estado,
        //            Nombres = asignacion.Conductor.Nombres,
        //            Apellidos = asignacion.Conductor.Apellidos,
        //            Cedula = asignacion.Conductor.Cedula,
        //            Id_Tipo_Licencia = asignacion.Conductor.Id_Tipo_Licencia
        //        });
        //    }

        //    return resultado;
        //}

    }
}
