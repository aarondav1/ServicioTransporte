using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioTransporte.Entities
{
    [Table("trEstado")]
    public class Estado
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        
        //...
    }
}
