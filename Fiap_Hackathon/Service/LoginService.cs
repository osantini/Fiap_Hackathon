using Fiap_Hackathon.Context;
using Fiap_Hackathon.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Fiap_Hackathon.Service
{
    public class LoginService
    {
        private readonly ApplicationDbContext _context;

        public LoginService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, string ErrorMessage, Usuario Usuario)> LoginAsync(string email, string senha)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);

            if (usuario == null)
            {
                return (false, "Email ou senha incorretos.", null);
            }

            return (true, string.Empty, usuario);
        }
    }
}
