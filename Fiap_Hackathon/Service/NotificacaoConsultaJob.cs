namespace Fiap_Hackathon.Service
{
    public class NotificacaoConsultaJob : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly ConsultaService _consultaService;
        private readonly EmailService _emailService;

        public NotificacaoConsultaJob(ConsultaService consultaService, EmailService emailService)
        {
            _consultaService = consultaService;
            _emailService = emailService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var agora = DateTime.Now;
            var horarioEnvio = new DateTime(agora.Year, agora.Month, agora.Day, 9, 0, 0);

            if (agora > horarioEnvio)
                horarioEnvio = horarioEnvio.AddDays(1);

            var intervalo = horarioEnvio - agora;
            _timer = new Timer(ExecutarJob, null, intervalo, TimeSpan.FromDays(1));
            return Task.CompletedTask;
        }

        private async void ExecutarJob(object state)
        {
            var consultas = _consultaService.ObterConsultasParaAmanha();

            foreach (var consulta in consultas)
            {
                var usuario = _consultaService.ObterNomeEmailPorId(consulta.Id_Usuario);
                var mensagem = $"Olá {usuario.Nome}, você tem uma consulta marcada para {consulta.Data_Consulta: dd/MM/yyyy} às {consulta.Data_Consulta: HH:mm}.";
                await _emailService.EnviarEmailAsync(usuario.Email, "Lembrete de Consulta", mensagem);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
