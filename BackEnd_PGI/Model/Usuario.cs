using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd_PGI.Model
{
    public class Usuario
    {
        public int ID { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public int IdRol { get; set; }

        // Relación con Rol
        public Rol Rol { get; set; }
    }
}
