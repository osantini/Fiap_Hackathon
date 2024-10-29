using Fiap_Hackathon.Context;
using Fiap_Hackathon.Models;
using Fiap_Hackathon.Service;
using Fiap_Hackathon.Services;
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
        public async Task<IActionResult> CadastrarClinica(ClinicaViewModel clinica)
        {
            var (success, errors) = await _clinicaService.CadastrarClinica(clinica);

            if (success)
            {
                TempData["SuccessMessage"] = "Clinica cadastrada com sucesso!";
                return RedirectToAction("Medico", "Medico");
            }
            else
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }

            return View(clinica);
        }
    }
}
