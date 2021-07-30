using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlanetaWebApi.Models;
using PlanetaWebApi.Repositories.Basic;
using PlanetaWebApi.Repositories.Special;

namespace PlanetaWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new CustomJsonConverter());
                });


            services.AddTransient<IRepository<ClientItem>, DClientRepository>(
                provider => new DClientRepository(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddTransient<IRepository<SubnetItem>, DSubnetRepository>(
                provider => new DSubnetRepository(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddTransient<IClientSubnetsRepository, DClientSubnetsRepository>(
                provider => new DClientSubnetsRepository(Configuration["ConnectionStrings:DefaultConnection"]));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
