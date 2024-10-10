using Fiap_Hackathon.Services;
using Microsoft.AspNetCore.Mvc;
using Fiap_Hackathon.Models;

public class AccountController : Controller
{
    private readonly CadastroService _cadastroService;

    public AccountController(CadastroService cadastroService)
    {
        _cadastroService = cadastroService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (TempData["SuccessMessage"] != null)
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
        }

        return View();
    }

    [HttpPost]
    public ActionResult Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Verificar login e redirecionar se bem-sucedido
        }
        return View(model);
    }

    public ActionResult ForgotPassword()
    {
        return View();
    }

    public ActionResult Cadastro()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Cadastro(UsuarioViewModel usuario)
    {

        var (success, errors) = await _cadastroService.CadastrarUsuario(usuario);

        if (success)
        {
            TempData["SuccessMessage"] = "Cadastro realizado com sucesso! Faça login para continuar.";
            return RedirectToAction("Login");
        }
        else
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }

        return View(usuario);
    }
}
