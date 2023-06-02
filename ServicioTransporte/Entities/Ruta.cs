using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioTransporte.Entities
{
    [Table("trRuta")]
    public class Ruta
    {
        public int Id { get; set; }

        [ForeignKey("Estado")]
        public int Id_Estado { get; set; }
        public string Nombre { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }

        public Estado Estado { get; set; }
        public List<AsignacionRutaParada> AsignacionesRutaParada { get; set; }
    }
}
