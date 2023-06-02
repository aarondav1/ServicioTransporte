using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioTransporte.Entities
{
    [Table("trAsignacionRutaBus")]
    public class AsignacionRutaBus
    {
        public int Id { get; set; }

        [ForeignKey("Ruta")]
        public int Id_Ruta { get; set; }

        [ForeignKey("Bus")]
        public int Id_Bus { get; set; }

        //PROPIEDADES DE NAVEGACION
        public Ruta Ruta { get; set; }
        public Bus Bus { get; set; }
    }
}
