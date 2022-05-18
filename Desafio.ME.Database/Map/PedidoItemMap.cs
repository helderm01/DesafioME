using Desafio.ME.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.ME.Database.Map
{
    public class PedidoItemMap : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.ToTable(nameof(PedidoItem));
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Descricao)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.PrecoUnitario)
                .IsRequired();

            builder.Property(c => c.Quantidade)
                .IsRequired();
        }
    }
}
