using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd_PGI.Model
{
    public enum TipoEstadoCaso { 
        Activo,
        Inactivo,
        Cancelado,
        Procesado,
        Terminado
    }

    [Table("caso")]
    public class Caso
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; } // Nullable para permitir casos abiertos
        public TipoEstadoCaso Estado { get; set; }

        // Relación con Tareas
        public ICollection<Tarea>? Tareas { get; set; }       
        public ICollection<Maquina>? Maquinas { get; set; }
        public ICollection<Asset>? Assets { get; set; }
        public ICollection<Analisis>? Analisiss { get; set; }

    }

}
