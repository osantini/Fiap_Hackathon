using Fiap_Hackathon.Context;
using Fiap_Hackathon.Models;
using Fiap_Hackathon.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Fiap_Hackathon.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ConsultaService _consultaService;

        public HomeController(ConsultaService consultaService, ApplicationDbContext context)
        {
            _consultaService = consultaService;
            _context = context;
        }

        [HttpGet]
        public ActionResult Paciente()
        {
            var consultas = ConsultasAgendadas(); // Função fictícia para obter as consultas
            return View(consultas);
        }

        public async Task<IActionResult> ConsultasAgendadas()
        {
            var emailLogado = TempData["EmailUsuarioLogado"] as string ?? HttpContext.Session.GetString("EmailUsuarioLogado");

            if (string.IsNullOrEmpty(emailLogado))
            {
                TempData["ErrorMessage"] = "Usuário não está logado.";
                return RedirectToAction("Login", "Account");
            }

            // Buscar o usuário pelo e-mail
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == emailLogado);

            if (usuario == null)
            {
                TempData["ErrorMessage"] = "Usuário não encontrado.";
                return RedirectToAction("Login", "Account");
            }

            // Buscar consultas para o usuário
            var consultas = _consultaService.ObterConsultasPorPaciente(usuario.Id);
            return View(consultas);
        }

        [HttpGet]
        public IActionResult Medico()
        {
            return View();
        }

        private readonly ILogger<HomeController> _logger;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
