namespace Fiap_Hackathon.Models
{
    public class Usuario
    {
        public int Id { get; set; }  // Chave primária
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime Data_Cadastro { get; set; }
        public int Tipo { get; set; }
        public int Ativo { get; set; }
        public string? Especialidade { get; set; } // Somente se for Médico
        public string? CRM { get; set; } // Somente se for Médico
    }
}
