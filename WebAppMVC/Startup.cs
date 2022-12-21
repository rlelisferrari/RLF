using ApiCatalogo.Logging;
using ApiCatalogo.Repository;
using DATA.Contexts;
using DATA.Repositories;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebAppMVC
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
            services.AddControllersWithViews();

            //services.AddDbContext<AppDbContext>(
            //    options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("SGOOConnection")));

            services.AddDbContext<AppDbContext>(
                options =>
                    options.UseMySql(Configuration.GetConnectionString("SGOOConnection")));

            services.AddControllers();

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            //services.AddScoped(typeof(IEquipamentoRepository), typeof(EquipamentoRepository));
            //services.AddScoped(typeof(IOrdemRepository), typeof(OrdemRepository));
            //services.AddScoped(typeof(ITipoOrdemRepository), typeof(TipoOrdemRepository));

            //services.AddScoped<EquipamentoService>();
            //services.AddScoped<OrdemService>();
            //services.AddScoped<TipoOrdemService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            loggerFactory.AddProvider(
                new CustomLoggerProvider(
                    new CustomLoggerProviderConfiguration
                    {
                        LogLevel = LogLevel.Information
                    }));

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllerRoute(
                        "default",
                        "{controller=Home}/{action=Index}/{id?}");
                });
        }
    }
}