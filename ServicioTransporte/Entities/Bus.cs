using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioTransporte.Entities
{
    [Table("trBus")]
    public class Bus
    {
        public int Id { get; set; }

        [ForeignKey("Estado")]
        public int Id_Estado { get; set; }
        public int Numero { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public int Capacidad { get; set; }
        public int Anio { get; set; }

        //PROPIEDAD DE NAVEGACION
        public List<AsignacionBusConductor> AsignacionesBusConductor { get; set; }
        public List<AsignacionRutaBus> AsignacionesRutaBus { get; set; }
        public Estado Estado { get; set; }
    }
}
