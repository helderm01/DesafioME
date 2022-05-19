using Desafio.ME.Database.Context;
using Desafio.ME.Database.Interfaces;
using Desafio.ME.Database.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Desafio.ME.Handlers.Testes
{
    public class DbFixture
    {
        public DbFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<MEContext>(c => c.UseInMemoryDatabase(databaseName: "METests"));
            serviceCollection.AddTransient<IPedidoRepositorio, PedidoRepositorio>();
            serviceCollection.AddTransient<CriarPedidoHandler>();
            serviceCollection.AddTransient<ExcluirPedidoHandler>();
            serviceCollection.AddTransient<ObterPedidoHandler>();
            serviceCollection.AddTransient<AlterarPedidoHandler>();
            serviceCollection.AddTransient<AlterarStatusHandler>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }
}
