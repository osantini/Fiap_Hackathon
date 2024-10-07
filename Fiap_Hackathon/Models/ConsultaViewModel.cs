namespace SistemaHackathon.Models
{
    public class ConsultaViewModel
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Local { get; set; }
        public string Procedimento { get; set; }
        public string Medico { get; set; }
        public string Paciente { get; set; }
    }
}
