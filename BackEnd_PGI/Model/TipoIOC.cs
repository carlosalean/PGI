using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd_PGI.Model
{
    public enum BuscarEn
    {
        OpenIA,
        AliendVauld,
        VirusTotal,
    }

    public class TipoIOC
    {
        public int? ID { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Icono { get; set; }
        public BuscarEn? BuscarEn { get; set; }

    }
}
