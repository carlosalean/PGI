using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_PGI.Model
{

    [Table("ioc")]
    public class IOC
    {
        public int ID { get; set; }
        public int TipoIocId { get; set; }
        public int AssetID { get; set; }
        public string? Valor { get; set; }
        public string? Descripcion { get; set; }

        public TipoIOC? TipoIOC { get; set; } 
    }
}
