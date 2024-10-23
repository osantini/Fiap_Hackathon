using Fiap_Hackathon.Context;
using Fiap_Hackathon.Models;

namespace Fiap_Hackathon.Service
{
    public class ConsultaService
    {
        private readonly ApplicationDbContext _context;
        private readonly ValidationService _validationService;

        public ConsultaService(ApplicationDbContext context, ValidationService validationService)
        {
            _context = context;
            _validationService = validationService;
        }

        public async Task<(bool success, List<string> errors)> AgendarConsulta(AgendarConsultaViewModel consultaViewModel)
        {
            var (isValid, validationErrors) = await _validationService.ValidateConsulta(consultaViewModel);
            if (!isValid)
            {
                return (false, validationErrors);
            }

            var consulta = new Consulta
            {
                Data_Consulta = consultaViewModel.Data,
                Procedimento = consultaViewModel.Procedimento,
                Id_Usuario = consultaViewModel.Paciente,
                Id_Medico = consultaViewModel.MedicoId,
                Id_Clinica = consultaViewModel.ClinicaId,
                Status = 1
            };

            _context.Consultas.Add(consulta);
            await _context.SaveChangesAsync();

            return (true, new List<string>());
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

        public List<Medico> ObterMedicos()
        {
            return _context.Usuarios
                           .Where(u => u.CRM != null)
                           .Select(u => new Medico
                           {
                               Id = u.Id,
                               Nome = u.Nome,
                               CRM = u.CRM,
                               Email = u.Email
                           })
                           .ToList();
        }
    }
}
