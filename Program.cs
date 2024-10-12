using CsvReader.Classes;
using System.Data.SqlClient;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        string csvFilePath = @"C:\yourFile.csv";
        string connectionString = "Server=yourServer;Database=yourDatabase;User Id=yourUser;Password=yourPassword;";

        try
        {
            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<CandidatoAreaInteresse>().ToList();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    foreach (var record in records)
                    {
                        int? idAreaInteresse2 = record.IdAreaInteresse2 == "NULL" ? (int?)null : int.Parse(record.IdAreaInteresse2);
                        int? idAreaInteresse3 = record.IdAreaInteresse3 == "NULL" ? (int?)null : int.Parse(record.IdAreaInteresse3);

                        string query = "INSERT INTO ProjetoCandidatoAreaInteresse " +
                                       "(IdProjetoCandidato, IdAreaInteresse1, IdAreaInteresse2, IdAreaInteresse3, DataAlteracao, DataCriacao, IdUsuarioAlteracao, IdUsuarioCriacao, Ativo) " +
                                       "VALUES (@IdProjetoCandidato, @IdAreaInteresse1, @IdAreaInteresse2, @IdAreaInteresse3, @DataAlteracao, @DataCriacao, @IdUsuarioAlteracao, @IdUsuarioCriacao, @Ativo)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@IdProjetoCandidato", record.IdProjetoCandidato);
                            cmd.Parameters.AddWithValue("@IdAreaInteresse1", record.IdAreaInteresse1);
                            cmd.Parameters.AddWithValue("@IdAreaInteresse2", idAreaInteresse2 ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@IdAreaInteresse3", idAreaInteresse3 ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@DataAlteracao", record.DataAlteracao);
                            cmd.Parameters.AddWithValue("@DataCriacao", record.DataCriacao);
                            cmd.Parameters.AddWithValue("@IdUsuarioAlteracao", record.IdUsuarioAlteracao);
                            cmd.Parameters.AddWithValue("@IdUsuarioCriacao", record.IdUsuarioCriacao);
                            cmd.Parameters.AddWithValue("@Ativo", record.Ativo);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    Console.WriteLine("Dados inseridos com sucesso!");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: " + ex.Message);
        }
    }
}
