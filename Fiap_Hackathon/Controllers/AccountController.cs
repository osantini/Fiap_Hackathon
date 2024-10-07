using Microsoft.AspNetCore.Mvc;
using SistemaHackathon.Models;

public class AccountController : Controller
{
    [HttpGet]
    public ActionResult Login()
    {
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
    public ActionResult Cadastro(CadastroViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Processar o registro e redirecionar
        }
        return View(model);
    }
}
