using Microsoft.EntityFrameworkCore;
using Fiap_Hackathon.Models;

namespace Fiap_Hackathon.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; } // Tabela de Usuários
    }
}
