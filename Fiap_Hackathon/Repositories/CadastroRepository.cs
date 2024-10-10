using Fiap_Hackathon.Context;
using Fiap_Hackathon.Models;

namespace Fiap_Hackathon.Repositories
{
    public class CadastroRepository
    {
        private readonly ApplicationDbContext _context;

        public CadastroRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IncluirUsuario(Usuario usuario)
        {
            try
            {
                _context.Usuarios.Add(usuario); 
                await _context.SaveChangesAsync(); 
                return true;
            }
            catch
            {
                // Tratar erro conforme necessário
                return false;
            }
        }
    }
}
