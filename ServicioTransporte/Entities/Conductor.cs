using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioTransporte.Entities
{
    [Table("trConductor")]
    public class Conductor
    {
        public int Id { get; set; }

        [ForeignKey("Estado")]
        public int Id_Estado { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Cedula { get; set; }

        [ForeignKey("TipoLicencia")]
        public int Id_Tipo_Licencia { get; set; }
        public TipoLicencia TipoLicencia { get; set; }
        public Estado Estado { get; set; }
        public List<AsignacionBusConductor> AsignacionesBusConductor { get; set; }
 
    }
}
