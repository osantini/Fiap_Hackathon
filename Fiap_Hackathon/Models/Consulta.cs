namespace Fiap_Hackathon.Models
{
    public class Consulta
    {
        public int Id { get; set; }
        public DateTime Data_Consulta { get; set; }
        public string Procedimento { get; set; }
        public int Id_Usuario { get; set; }
        public int Id_Medico { get; set; }
        public int Status { get; set; }
        public int Id_Clinica { get; set; }

    }
}
