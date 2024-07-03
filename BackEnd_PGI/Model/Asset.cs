using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_PGI.Model
{
    [Table("asset")]
    public class Asset
    {
        public int ID { get; set; }
        public int CasoID { get; set; }
        public int? MaquinaID { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int TipoAssetID { get; set; }
        public string? Ubicacion { get; set; }
        public TipoAsset? TipoAsset { get; set; } 
        public ICollection<IOC>? IOCs { get; set; }

    }
}
