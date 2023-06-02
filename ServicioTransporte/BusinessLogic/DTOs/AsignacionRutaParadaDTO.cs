namespace ServicioTransporte.BusinessLogic.DTOs
{
    public class AsignacionRutaParadaDTO
    {
        public int Id { get; set; }
        public int Id_Ruta { get; set; }
        public int Id_Parada { get; set; }

        //PROPIEDADES DE NAVEGACION
        public RutaDTO RutaDTO { get; set; }
        public ParadaDTO ParadaDTO { get; set; }
    }
}
