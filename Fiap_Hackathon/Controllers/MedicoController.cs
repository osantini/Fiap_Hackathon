using Microsoft.AspNetCore.Mvc;
using Fiap_Hackathon.Models;
using Fiap_Hackathon.Service;
using Microsoft.EntityFrameworkCore;
using Fiap_Hackathon.Context;

public class MedicoController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ConsultaService _consultaService;
    private readonly ClinicaService _clinicaService;

    public MedicoController(ConsultaService consultaService, ApplicationDbContext context, ClinicaService clinicaService)
    {
        _consultaService = consultaService;
        _context = context;
        _clinicaService = clinicaService;
    }

    [HttpGet]
    public async Task<IActionResult> Medico()
    {
        var consultas = await GetConsultasAgendadasParaMedico();
        return View(consultas);
    }

    private async Task<IEnumerable<ConsultaViewModel>> GetConsultasAgendadasParaMedico()
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

        var consultas = _consultaService.ObterConsultasPorMedico(usuario.Id);

        var consultaViewModels = consultas.Select(c => new ConsultaViewModel
        {
            Id = c.Id,
            Data = c.Data_Consulta,
            Local = _clinicaService.ObterClinicaPorId(c.Id_Clinica),
            Procedimento = c.Procedimento,
            Paciente = _consultaService.ObterUsuarioPorId(c.Id_Usuario),
        });

        return consultaViewModels;
    }
}
