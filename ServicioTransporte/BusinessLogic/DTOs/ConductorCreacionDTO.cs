using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioTransporte.BusinessLogic.DTOs
{
    public class ConductorCreacionDTO
    {
        public int Id_Estado { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Cedula { get; set; }
        public int Id_Tipo_Licencia { get; set; }

        public ConductorCreacionDTO()
        {
            this.Id_Estado = 1;
        }
    }
}
