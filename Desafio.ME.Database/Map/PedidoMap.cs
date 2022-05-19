using Desafio.ME.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.ME.Database.Map
{
    public class PedidoMap : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable(nameof(Pedido));
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NumeroPedido)
                .HasMaxLength(15)
                .IsRequired();

            builder.HasMany(c => c.ItensDoPedido)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(c => c.ItensDoPedido)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            builder.HasIndex(c => c.NumeroPedido).IsUnique();
        }
    }
}
