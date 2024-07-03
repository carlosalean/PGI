using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_PGI.Model
{

    public enum TipoEstadoTarea
    {
        Activa,
        EnProgreso,
        Cancelada,
        Terminada
    }

    [Table("tarea")]
    public class Tarea
    {
        public int ID { get; set; }
        public int? CasoID { get; set; }
        public string? Descripcion { get; set; }
        public TipoEstadoTarea? Estado { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime ? FechaFin { get; set; } // Nullable para tareas sin finalizar
        public DateTime? DeadLine { get; set; }
        public int? AnalistaID { get; set; } // Owner ID

        //[JsonIgnore]
        //[BindNever]
        //// Relación con Caso
        //public Caso Caso { get; set; }

    }

}
