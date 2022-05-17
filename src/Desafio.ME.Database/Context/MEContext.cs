using Desafio.ME.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Desafio.ME.Database.Context
{
    public class MEContext : DbContext
    {
        private static bool tabelasCriadas;
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> Itens { get; set; }

        public MEContext(DbContextOptions<MEContext> options) : base(options)
        {
            Database.EnsureCreated();

            if (!tabelasCriadas)
            {
                var criador = (RelationalDatabaseCreator)Database.GetService<IDatabaseCreator>();

                criador.CreateTables();

                tabelasCriadas = true;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MEContext).Assembly);
        }
    }
}
