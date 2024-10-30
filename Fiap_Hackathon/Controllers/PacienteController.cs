using Microsoft.AspNetCore.Mvc;
using Fiap_Hackathon.Models;
using Microsoft.EntityFrameworkCore;
using Fiap_Hackathon.Service;
using Fiap_Hackathon.Context;

public class PacienteController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ConsultaService _consultaService;
    private readonly ClinicaService _clinicaService;

    public PacienteController(ConsultaService consultaService, ApplicationDbContext context, ClinicaService clinicaService)
    {
        _consultaService = consultaService;
        _context = context;
        _clinicaService = clinicaService;
    }

    [HttpGet]
    public async Task<IActionResult> Paciente()
    {
        var consultas = await GetConsultasAgendadas(); 
        return View(consultas); 
    }

    [HttpGet]
    public ActionResult AgendarConsulta()
    {
        // Redireciona para a tela de agendamento de consulta
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> CancelarConsulta(int id)
    {
        var emailLogado = TempData["EmailUsuarioLogado"] as string ?? HttpContext.Session.GetString("EmailUsuarioLogado");

        if (string.IsNullOrEmpty(emailLogado))
        {
            TempData["ErrorMessage"] = "Usuário não está logado.";
        }

        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == emailLogado);

        var success = await _consultaService.CancelarConsulta(id);

        if (success)
        {
            TempData["SuccessMessage"] = "Consulta reagendada com sucesso!";
            if (usuario.Especialidade != null)
            {
                return RedirectToAction("Medico", "Medico");
            }
            return RedirectToAction("Paciente");
        }

        return RedirectToAction("Paciente"); 
    }

    public async Task<IActionResult> ReagendarConsulta(int id)
    {
        var consulta = await _consultaService.ObterConsultaPorId(id);
        if (consulta == null)
        {
            TempData["ErrorMessage"] = "Consulta não encontrada.";
            return RedirectToAction("Paciente");
        }

        var viewModel = new ReagendarConsultaViewModel
        {
            Id = consulta.Id,
            DataAtual = consulta.Data_Consulta,
            ClinicaId = consulta.Id_Clinica,
            MedicoId = consulta.Id_Medico,
            ClinicasDisponiveis = _clinicaService.ObterTodasClinicas(),
            MedicosDisponiveis =  _consultaService.ObterMedicos(),
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> ReagendarConsulta(ReagendarConsultaViewModel viewModel)
    {
        var emailLogado = TempData["EmailUsuarioLogado"] as string ?? HttpContext.Session.GetString("EmailUsuarioLogado");

        if (string.IsNullOrEmpty(emailLogado))
        {
            TempData["ErrorMessage"] = "Usuário não está logado.";
        }

        DateTime dataHoraConsulta = viewModel.Data.Add(viewModel.HoraConsulta);
        viewModel.Data = dataHoraConsulta;

        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == emailLogado);

        if (!ModelState.IsValid)
        {
            viewModel.ClinicasDisponiveis = _clinicaService.ObterTodasClinicas();
            viewModel.MedicosDisponiveis = _consultaService.ObterMedicos();
            return View(viewModel);
        }

        var success = await _consultaService.ReagendarConsulta(viewModel);

        if (success)
        {
            TempData["SuccessMessage"] = "Consulta reagendada com sucesso!";
            if(usuario.Especialidade != null)
            {
                return RedirectToAction("Medico", "Medico");
            }
            return RedirectToAction("Paciente");
        }

        TempData["ErrorMessage"] = "Não foi possível reagendar a consulta.";
        return View(viewModel);
    }

    private async Task<IEnumerable<ConsultaViewModel>> GetConsultasAgendadas()
    {
        var emailLogado = TempData["EmailUsuarioLogado"] as string ?? HttpContext.Session.GetString("EmailUsuarioLogado");

        if (string.IsNullOrEmpty(emailLogado))
        {
            TempData["ErrorMessage"] = "Usuário não está logado.";
            return Enumerable.Empty<ConsultaViewModel>(); 
        }

        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == emailLogado);

        if (usuario == null)
        {
            TempData["ErrorMessage"] = "Usuário não encontrado.";
            return Enumerable.Empty<ConsultaViewModel>(); 
        }

        var consultas = _consultaService.ObterConsultasPorPaciente(usuario.Id);

        var consultaViewModels = consultas.Select(c => new ConsultaViewModel
        {
            Id = c.Id,
            Data = c.Data_Consulta,
            Local = _clinicaService.ObterClinicaPorId(c.Id_Clinica),
            Procedimento = c.Procedimento,
            NomeMedico = _consultaService.ObterUsuarioPorId(c.Id_Medico),
        });

        return consultaViewModels;
    }
}
