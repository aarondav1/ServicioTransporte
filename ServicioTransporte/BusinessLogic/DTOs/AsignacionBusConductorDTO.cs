using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioTransporte.BusinessLogic.DTOs
{
    public class AsignacionBusConductorDTO
    {
        public int Id { get; set; }
        public int Id_Bus { get; set; }
        public int Id_Conductor { get; set; }
        public BusDTO BusDTO { get; set; }
        public ConductorDTO ConductorDTO { get; set; }


    }
}
