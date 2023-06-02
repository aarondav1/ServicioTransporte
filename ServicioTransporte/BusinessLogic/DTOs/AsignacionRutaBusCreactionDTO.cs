using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioTransporte.BusinessLogic.DTOs
{
    public class AsignacionRutaBusCreactionDTO
    {
        public int Id_Ruta { get; set; }
        public int Id_Bus { get; set; }
    }
}
