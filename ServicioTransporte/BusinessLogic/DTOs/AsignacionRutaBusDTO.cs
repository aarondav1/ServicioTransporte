using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioTransporte.BusinessLogic.DTOs
{
    public class AsignacionRutaBusDTO
    {
        public int Id { get; set; }
        public int Id_Ruta { get; set; }
        public int Id_Bus { get; set; }

        //PROPIEDADES DE NAVEGACION
        public RutaDTO RutaDTO { get; set; }
        public BusDTO BusDTO { get; set; }
    }
}
