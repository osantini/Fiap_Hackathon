using Fiap_Hackathon.Context;
using Microsoft.AspNetCore.Mvc;
using Fiap_Hackathon.Models;
using System.Linq;
using Fiap_Hackathon.Service;



namespace Fiap_Hackathon.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly ConsultaService _consultaService;
        private readonly ClinicaService _clinicaService;

        public ConsultaController(ConsultaService consultaService, ClinicaService clinicaService)
        {
            _consultaService = consultaService;
            _clinicaService = clinicaService;
        }

        public IActionResult AgendarConsulta()
        {
            var model = new AgendarConsultaViewModel
            {
                ClinicasDisponiveis = _clinicaService.ObterTodasClinicas()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AgendarConsulta(Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                if (_consultaService.AgendarConsulta(consulta, out string mensagemErro))
                {
                    TempData["MensagemSucesso"] = "Consulta agendada com sucesso!";
                    return RedirectToAction("AgendarConsulta");
                }
                else
                {
                    TempData["MensagemErro"] = mensagemErro;
                }
            }
            return View(consulta);
        }
    }
}
