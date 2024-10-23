namespace Fiap_Hackathon.Models
{
    public class ConsultaViewModel
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HoraConsulta { get; set; }
        public string Local { get; set; }
        public string Procedimento { get; set; }
        public int MedicoId { get; set; }
        public int Paciente { get; set; }
        public int ClinicaId { get; set; }
    }
}
