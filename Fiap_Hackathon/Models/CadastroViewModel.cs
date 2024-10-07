namespace SistemaHackathon.Models
{
    public class CadastroViewModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Tipo { get; set; }
        public string Especialidade { get; set; } // Somente se for Médico
        public string CRM { get; set; } // Somente se for Médico
    }
}
