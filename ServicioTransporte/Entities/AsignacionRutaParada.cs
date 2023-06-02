using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioTransporte.Entities
{
    [Table("trAsignacionRutaParada")]
    public class AsignacionRutaParada
    {
        public int Id { get; set; }

        [ForeignKey("Ruta")]
        public int Id_Ruta { get; set; }

        [ForeignKey("Parada")]
        public int Id_Parada { get; set; }

        //PROPIEDADES DE NAVEGACION
        public Ruta Ruta { get; set; }
        public Parada Parada { get; set; }

    }
}
