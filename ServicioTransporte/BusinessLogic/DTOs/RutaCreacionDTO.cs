namespace ServicioTransporte.BusinessLogic.DTOs
{
    public class RutaCreacionDTO
    {
        public int Id_Estado { get; set; }
        public string Nombre { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }

        public RutaCreacionDTO()
        {
            this.Id_Estado= 1;
        }
    }
}
