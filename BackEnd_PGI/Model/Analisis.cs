using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd_PGI.Model
{
    [Table("analisis")]

    public class Analisis
    {
        public int ID { get; set; }
        public int CasoID { get; set; }
        public DateTime FechaAnalisis { get; set; }
        public string Resultado { get; set; }

    }
}
