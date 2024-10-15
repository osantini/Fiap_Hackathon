using Fiap_Hackathon.Context;
using Fiap_Hackathon.Models;

namespace Fiap_Hackathon.Service
{
    public class ConsultaService
    {
        private readonly ApplicationDbContext _context;

        public ConsultaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AgendarConsulta(Consulta consulta, out string mensagemErro)
        {
            try
            {
                _context.Consultas.Add(consulta);
                _context.SaveChanges();
                mensagemErro = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                mensagemErro = ex.Message;
                return false;
            }
        }

        public List<Consulta> ObterConsultasPorPaciente(int pacienteId)
        {
            return _context.Consultas
                           .Where(c => c.Id_Usuario == pacienteId)
                           .ToList();
        }

        public List<Consulta> ObterConsultasPorMedico(int medicoId)
        {
            return _context.Consultas
                           .Where(c => c.Id_Medico == medicoId)
                           .ToList();
        }
    }
}
