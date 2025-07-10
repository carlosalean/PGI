namespace BackEnd_PGI.Model
{
    public class ThreatDetectionRule
    {
        public int TipoAssetID { get; set; }
        public string[] Keywords { get; set; } = Array.Empty<string>();
    }
}
