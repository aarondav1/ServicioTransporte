namespace ServicioTransporte.BusinessLogic.DTOs
{
    public class BusCreacionDTO
    {
        public int Id_Estado { get; set; }
        public string Placa { get; set; }
        public int Numero { get; set; }
        public string Modelo { get; set; }
        public int Capacidad { get; set; }
        public int Anio { get; set; }

        public BusCreacionDTO()
        {
            this.Id_Estado= 1;
        }
    }
}
