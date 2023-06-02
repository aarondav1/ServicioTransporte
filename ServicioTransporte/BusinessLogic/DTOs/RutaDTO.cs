using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioTransporte.BusinessLogic.DTOs
{
    public class RutaDTO
    {
        public int Id { get; set; }
        public int Id_Estado { get; set; }
        public string Nombre { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
    }
}
