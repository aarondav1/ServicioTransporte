using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioTransporte.BusinessLogic.DTOs
{
    public class AsignacionBusConductorCreacionDTO
    {
        public int Id_Bus { get; set; }
        public int Id_Conductor { get; set; }
    }
}
