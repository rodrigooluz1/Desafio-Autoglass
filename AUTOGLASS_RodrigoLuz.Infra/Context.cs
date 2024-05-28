
using AUTOGLASS_RodrigoLuz.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AUTOGLASS_RodrigoLuz.Infra
{
	public class Context: DbContext
	{
		public Context()
		{
        }

        DbSet<Produto> produto { get; set; }
        DbSet<Fornecedor> fornecedor { get; set; }

        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        }
    }
}

