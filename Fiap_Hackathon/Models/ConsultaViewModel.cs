namespace Fiap_Hackathon.Models
{
    public class ConsultaViewModel
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HoraConsulta { get; set; }
        public string Local { get; set; }
        public string Procedimento { get; set; }
        public int Medico { get; set; }
        public string NomeMedico { get; set; }
        public string Paciente { get; set; }
        public int ClinicaId { get; set; }
    }
}
