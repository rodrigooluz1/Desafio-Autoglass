using System;
using AUTOGLASS_RodrigoLuz.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUTOGLASS_RodrigoLuz.Infra.Mapping
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.DescricaoFornecedor).IsRequired().HasMaxLength(255);

            builder.Property(f => f.CNPJFornecedor);

        }
    }
}

