using Fiap_Hackathon.Context;
using Microsoft.AspNetCore.Mvc;
using Fiap_Hackathon.Models;
using System.Linq;
using Fiap_Hackathon.Service;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;



namespace Fiap_Hackathon.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly ConsultaService _consultaService;
        private readonly ClinicaService _clinicaService;
        private readonly ApplicationDbContext _context;

        public ConsultaController(ConsultaService consultaService, ClinicaService clinicaService, ApplicationDbContext context)
        {
            _consultaService = consultaService;
            _clinicaService = clinicaService;
            _context = context;
        }

        public IActionResult AgendarConsulta()
        {
            var model = new AgendarConsultaViewModel
            {
                ClinicasDisponiveis = _clinicaService.ObterTodasClinicas(),
                MedicosDisponiveis = _consultaService.ObterMedicos()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AgendarConsulta(AgendarConsultaViewModel consulta)
        {
            DateTime dataHoraConsulta = consulta.Data.Add(consulta.HoraConsulta);
            consulta.Data = dataHoraConsulta;

            var paciente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == consulta.Email);
            if (paciente == null)
            {
                ModelState.AddModelError(string.Empty, "Paciente não encontrado.");
                return View(consulta);
            }

            consulta.Paciente = paciente.Id;

            var clinica = await _context.Clinicas.FindAsync(consulta.ClinicaId);
            if (clinica == null)
            {
                ModelState.AddModelError(string.Empty, "Clínica não encontrada.");
                return View(consulta);
            }

            consulta.Local = clinica.Nome_Clinica;

            var (success, errors) = await _consultaService.AgendarConsulta(consulta);

            if (success)
            {
                TempData["SuccessMessage"] = "Consulta Agendada com sucesso!";
                return RedirectToAction("Paciente", "Paciente");
            }
            else
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }

            return View(consulta);
        }
    }
}
