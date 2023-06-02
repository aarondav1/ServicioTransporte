namespace ServicioTransporte.BusinessLogic.DTOs
{
    public class ParadaCreacionDTO
    {
        public int Id_Estado { get; set; }
        public string Direccion { get; set; }

        public ParadaCreacionDTO()
        {
            this.Id_Estado= 1;
        }
    }
}
