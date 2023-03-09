using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain
{

    public class PruebaIQDataContext : DbContext
    {
        public PruebaIQDataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Auditoria> auditorias { get; set; }
        public DbSet<Usuario> usuarios { get; set; }

    }
}