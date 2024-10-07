using Microsoft.AspNetCore.Mvc;
using SistemaHackathon.Models;

public class PacienteController : Controller
{
    [HttpGet]
    public ActionResult HomePaciente()
    {
        var consultas = GetConsultasAgendadas(); // Função fictícia para obter as consultas
        return View(consultas);
    }

    [HttpGet]
    public ActionResult AgendarConsulta()
    {
        // Redireciona para a tela de agendamento de consulta
        return View();
    }

    [HttpGet]
    public ActionResult CancelarConsulta(int id)
    {
        // Lógica para cancelar a consulta
        return RedirectToAction("HomePaciente");
    }

    [HttpGet]
    public ActionResult ReagendarConsulta(int id)
    {
        // Lógica para reagendar a consulta
        return View();
    }

    private IEnumerable<ConsultaViewModel> GetConsultasAgendadas()
    {
        // Simulação de dados
        return new List<ConsultaViewModel>
        {
            new ConsultaViewModel { Id = 1, Data = DateTime.Now.AddDays(3), Local = "Clínica X", Procedimento = "Consulta de rotina", Medico = "Dr. João" },
            new ConsultaViewModel { Id = 2, Data = DateTime.Now.AddDays(7), Local = "Hospital Y", Procedimento = "Exame de sangue", Medico = "Dra. Maria" }
        };
    }
}
