using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd_PGI.Model
{
    [Table("rol")]
    public class Rol
    {
        public int ID { get; set; }
        public string? NombreRol { get; set; }

        // Relación con Usuarios
        public ICollection<Usuario>? Usuarios { get; set; }
    }
}
