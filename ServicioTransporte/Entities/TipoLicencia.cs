using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioTransporte.Entities
{
    [Table("trTipoLicencia")]
    public class TipoLicencia
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
}
