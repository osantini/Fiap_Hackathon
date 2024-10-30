using Fiap_Hackathon.Services;
using Microsoft.AspNetCore.Mvc;
using Fiap_Hackathon.Models;
using Fiap_Hackathon.Service;

public class AccountController : Controller
{
    private readonly CadastroService _cadastroService;
    private readonly LoginService _loginService;

    public AccountController(CadastroService cadastroService, LoginService loginService)
    {
        _cadastroService = cadastroService;
        _loginService = loginService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (TempData["ErrorMessage"] != null)
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var (success, errorMessage, usuario) = await _loginService.LoginAsync(email, password);

        TempData["EmailUsuarioLogado"] = usuario.Email;
        HttpContext.Session.SetString("EmailUsuarioLogado", usuario.Email);

        if (success)
        {
            if (usuario.Tipo == 1)
            {
                return RedirectToAction("Medico", "Medico");
            }
            else if (usuario.Tipo == 0)
            {
                return RedirectToAction("Paciente", "Paciente");
            }
        }
        else
        {
            TempData["ErrorMessage"] = errorMessage;
        }

        return RedirectToAction("Login");
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
            TempData["successMessage"] = "Cadastro realizado com sucesso! Faca login para continuar.";
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
