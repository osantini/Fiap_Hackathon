using Fiap_Hackathon.Context;
using Fiap_Hackathon.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> CancelarConsulta(int consultaId)
        {
            var consulta = await _context.Consultas.FindAsync(consultaId);

            if (consulta == null)
            {
                return false; 
            }

            _context.Consultas.Remove(consulta); 
            await _context.SaveChangesAsync(); 

            return true; 
        }

        public async Task<Consulta> ObterConsultaPorId(int consultaId)
        {
            return await _context.Consultas.FindAsync(consultaId);
        }

        public async Task<bool> ReagendarConsulta(ReagendarConsultaViewModel viewModel)
        {
            var consulta = await _context.Consultas.FindAsync(viewModel.Id);
            if (consulta == null)
            {
                return false; 
            }

            consulta.Data_Consulta = viewModel.Data;

            await _context.SaveChangesAsync(); 

            return true; 
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

        public string ObterUsuarioPorId(int Id)
        {
            return _context.Usuarios
                   .Where(c => c.Id == Id)
                   .Select(c => c.Nome)
                   .FirstOrDefault(); 
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

        public List<Consulta> ObterConsultasParaAmanha()
        {
            var amanha = DateTime.Now.Date.AddDays(1);
            return _context.Consultas
                           .Where(c => c.Data_Consulta == amanha)
                           .ToList();
        }

        public UsuarioDTO ObterNomeEmailPorId(int id)
        {
            return _context.Usuarios
                   .Where(c => c.Id == id)
                   .Select(c => new UsuarioDTO
                   {
                       Nome = c.Nome,
                       Email = c.Email
                   })
                   .FirstOrDefault();
        }
    }
}
