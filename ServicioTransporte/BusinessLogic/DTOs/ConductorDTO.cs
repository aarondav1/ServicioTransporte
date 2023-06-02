namespace ServicioTransporte.BusinessLogic.DTOs
{
    public class ConductorDTO
    {
        public int Id { get; set; }
        public int Id_Estado { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Cedula { get; set; }
        public int Id_Tipo_Licencia { get; set; }
    }
}
