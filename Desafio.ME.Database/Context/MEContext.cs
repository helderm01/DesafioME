using Desafio.ME.Dominio;
using Microsoft.EntityFrameworkCore;

namespace Desafio.ME.Database.Context
{
    public class MEContext : DbContext
    {
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> Itens { get; set; }

        public MEContext(DbContextOptions<MEContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MEContext).Assembly);
        }
    }
}
