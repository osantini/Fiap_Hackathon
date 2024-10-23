using Microsoft.AspNetCore.Mvc;
using Fiap_Hackathon.Models;

public class MedicoController : Controller
{
    [HttpGet]
    public ActionResult HomeMedico()
    {
        var consultas = GetConsultasAgendadasParaMedico(); // Função fictícia para obter as consultas
        return View(consultas);
    }

    private IEnumerable<ConsultaViewModel> GetConsultasAgendadasParaMedico()
    {
        // Simulação de dados para consultas agendadas
        return new List<ConsultaViewModel>
        {
            new ConsultaViewModel { Id = 1, Data = DateTime.Now.AddDays(1), Paciente = 1, Local = "Clínica X", Procedimento = "Consulta de rotina" },
            new ConsultaViewModel { Id = 2, Data = DateTime.Now.AddDays(3), Paciente = 2, Local = "Hospital Y", Procedimento = "Exame de sangue" }
        };
    }
}
