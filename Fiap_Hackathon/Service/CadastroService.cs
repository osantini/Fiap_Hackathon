using Fiap_Hackathon.Repositories;
using Fiap_Hackathon.Models;
using Fiap_Hackathon.Service;
using Fiap_Hackathon.Context;

namespace Fiap_Hackathon.Services
{
    public class CadastroService
    {
        private readonly ApplicationDbContext _context;
        private readonly ValidationService _validationService;

        public CadastroService(ApplicationDbContext context, ValidationService validationService)
        {
            _context = context;
            _validationService = validationService;
        }

        public async Task<(bool success, List<string> errors)> CadastrarUsuario(UsuarioViewModel usuarioViewModel)
        {
            // Validação do usuário
            var (isValid, validationErrors) = await _validationService.ValidateUser(usuarioViewModel);
            if (!isValid)
            {
                return (false, validationErrors);
            }

            // Se a validação for bem-sucedida, continuar com o cadastro
            var usuario = new Usuario
            {
                Nome = usuarioViewModel.Nome,
                Email = usuarioViewModel.Email,
                Senha = usuarioViewModel.Senha,
                Data_Cadastro = DateTime.Now,
                Tipo = usuarioViewModel.Tipo == "Médico" ? 1 : 0,
                Ativo = 1,
                Especialidade = usuarioViewModel.Tipo == "Médico" ? usuarioViewModel.Especialidade : null,
                CRM = usuarioViewModel.Tipo == "Médico" ? usuarioViewModel.CRM : null
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return (true, new List<string>());
        }
    }
}
