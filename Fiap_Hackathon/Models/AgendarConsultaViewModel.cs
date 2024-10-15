namespace Fiap_Hackathon.Models
{
    public class AgendarConsultaViewModel
    {
        public DateTime DataConsulta { get; set; }
        public string Procedimento { get; set; }
        public int ClinicaId { get; set; }  // ID da clínica selecionada

        // Lista de Clínicas para preencher o dropdown
        public List<Clinica> ClinicasDisponiveis { get; set; }
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
}
