namespace BackEnd_PGI.Model
{
    public class Maquina
    {
        public int ID { get; set; }
        public int CasoID { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        public ICollection<Asset>? Assets { get; set; }

    }
}
