namespace Fiap_Hackathon.Models
{
    public class AgendarConsultaViewModel
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HoraConsulta { get; set; }
        public string Local { get; set; }
        public string Procedimento { get; set; }
        public string Email { get; set; }
        public int MedicoId { get; set; }
        public int Paciente { get; set; }
        public int ClinicaId { get; set; }

        // Lista de Clínicas para preencher o dropdown
        public List<Clinica> ClinicasDisponiveis { get; set; }

        public List<Medico> MedicosDisponiveis { get; set; }
    }

    public class Clinica
    {
        public int Id { get; set; }  // Chave primária
        public string Nome_Clinica { get; set; }
        public string Endereco { get; set; }
        public string CEP { get; set; }
        public DateTime Data_Cadastro { get; set; }
        public int Ativo { get; set; }
    }

    public class Medico
    {
        public int Id { get; set; }  // Chave primária
        public string Nome { get; set; }
        public string Especialidade { get; set; }
        public string CRM { get; set; }
        public string Email { get; set; }
    }
}
