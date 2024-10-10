using System.ComponentModel.DataAnnotations;

namespace Fiap_Hackathon.Models
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public DateTime DataCadastro { get; set; }

        public int Ativo { get; set; }

        [Required(ErrorMessage = "O tipo de usuário é obrigatório.")]
        public string Tipo { get; set; } // 1 Médico ou  0 Paciente

        // Campos extras apenas se for médico
        public string Especialidade { get; set; }
        public string CRM { get; set; }
    }
}