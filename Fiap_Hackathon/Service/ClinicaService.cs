using Fiap_Hackathon.Context;
using Fiap_Hackathon.Models;

namespace Fiap_Hackathon.Service
{
    public class ClinicaService
    {
        private readonly ApplicationDbContext _context;

        public ClinicaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CadastrarClinica(Clinica clinica, out string mensagemErro)
        {
            try
            {
                _context.Clinicas.Add(clinica);
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

        public List<Clinica> ObterTodasClinicas()
        {
            return _context.Clinicas.ToList();
        }
    }
}
