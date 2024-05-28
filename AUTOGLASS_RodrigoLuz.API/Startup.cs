using AUTOGLASS_RodrigoLuz.Application.Services;
using AUTOGLASS_RodrigoLuz.Domain.Interfaces.Repositories;
using AUTOGLASS_RodrigoLuz.Domain.Interfaces.Services;
using AUTOGLASS_RodrigoLuz.Domain.Mappers;
using AUTOGLASS_RodrigoLuz.Infra;
using AUTOGLASS_RodrigoLuz.Infra.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AUTOGLASS_RodrigoLuz.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AUTOGLASS_RodrigoLuz.API", Version = "v1" });
            });

            services.AddDbContext<Context>(option =>
            option.UseSqlServer(Configuration.GetSection("DatabaseSetting:ConnectionString").Value));
            services.AddTransient<Context>();

            services.AddTransient(typeof(IProdutoService), typeof(ProdutoService));

            services.AddTransient(typeof(IProdutoRepository), typeof(ProdutoRepository));

            services.AddAutoMapper(typeof(EntityToDtoMapping));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AUTOGLASS_RodrigoLuz.API v1"));
            }

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}

