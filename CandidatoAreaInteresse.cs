namespace CsvReader.Classes
{
    public class CandidatoAreaInteresse
    {
        public int IdProjetoCandidato { get; set; }
        public int IdAreaInteresse1 { get; set; }
        public string? IdAreaInteresse2 { get; set; }
        public string? IdAreaInteresse3 { get; set; }
        public DateTime DataAlteracao { get; set; }
        public DateTime DataCriacao { get; set; }
        public int IdUsuarioAlteracao { get; set; }
        public int IdUsuarioCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}
