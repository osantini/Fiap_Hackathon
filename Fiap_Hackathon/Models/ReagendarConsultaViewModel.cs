namespace Fiap_Hackathon.Models
{
    public class ReagendarConsultaViewModel
    {
        public int Id { get; set; }
        public DateTime DataAtual { get; set; } 
        public DateTime Data { get; set; }
        public TimeSpan HoraConsulta { get; set; }
        public int ClinicaId { get; set; }
        public int MedicoId { get; set; }

        public List<Clinica> ClinicasDisponiveis { get; set; } = new List<Clinica>();
        public List<Medico> MedicosDisponiveis { get; set; } = new List<Medico>();
    }
}
