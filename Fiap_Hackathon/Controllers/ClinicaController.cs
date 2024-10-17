using Fiap_Hackathon.Context;
using Fiap_Hackathon.Models;
using Fiap_Hackathon.Service;
using Microsoft.AspNetCore.Mvc;

namespace Fiap_Hackathon.Controllers
{
    public class ClinicaController : Controller
    {
        private readonly ClinicaService _clinicaService;

        public ClinicaController(ClinicaService clinicaService)
        {
            _clinicaService = clinicaService;
        }

        [HttpGet]
        public IActionResult CadastrarClinica()
        {
            return View(new ClinicaViewModel());
        }

        [HttpPost]
        public IActionResult CadastrarClinica(Clinica clinica)
        {
            if (ModelState.IsValid)
            {
                if (_clinicaService.CadastrarClinica(clinica, out string mensagemErro))
                {
                    TempData["MensagemSucesso"] = "Clínica cadastrada com sucesso!";
                    return RedirectToAction("CadastrarClinica");
                }
                else
                {
                    TempData["MensagemErro"] = mensagemErro;
                }
            }
            return View(clinica);
        }
    }
}
