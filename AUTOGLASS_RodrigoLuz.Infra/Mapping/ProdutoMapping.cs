using AUTOGLASS_RodrigoLuz.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUTOGLASS_RodrigoLuz.Infra.Mapping
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Descricao).IsRequired().HasMaxLength(255);

            builder.Property(p => p.Situacao);

            builder.Property(p => p.DataFabricacao);

            builder.Property(p => p.DataValidade);

            builder.HasOne(p => p.Fornecedor)
            .WithMany()
            .HasForeignKey(p => p.IdFornecedor);

            builder.HasOne(p => p.Fornecedor)
               .WithMany()
               .HasForeignKey(p => p.IdFornecedor)
               .OnDelete(DeleteBehavior.Restrict);

        }
    }
}


