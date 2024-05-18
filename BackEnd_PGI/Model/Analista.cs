using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd_PGI.Model
{
    [Table("analista")]

    public class Analista
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        // Relación con Tareas
        public ICollection<Tarea>? Tareas { get; set; }
    }
}
