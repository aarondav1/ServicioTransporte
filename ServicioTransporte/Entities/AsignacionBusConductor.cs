using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioTransporte.Entities
{
    [Table("trAsignacionBusConductor")]
    public class AsignacionBusConductor
    {
        public int Id { get; set; }

        [ForeignKey("Bus")]
        public int Id_Bus { get; set; }

        [ForeignKey("Conductor")]
        public int Id_Conductor { get; set; }

        //PROPIEDADES DE NAVEGACION
        public Bus Bus { get; set; }
        public Conductor Conductor { get; set; }
    }
}
