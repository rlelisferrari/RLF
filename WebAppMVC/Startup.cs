using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DATA.Contexts;
using DATA.Repositories;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            services.AddDbContext<AppDbContext>(
                options =>
                    options.UseSqlServer(Configuration.GetConnectionString("SGOOConnection")));

            services.AddControllers();

            services.AddScoped(typeof(IEquipamentoRepository), typeof(EquipamentoRepository));
            services.AddScoped(typeof(IOrdemRepository), typeof(OrdemRepository));
            services.AddScoped(typeof(ITipoOrdemRepository), typeof(TipoOrdemRepository));

            services.AddScoped<EquipamentoService>();
            services.AddScoped<OrdemService>();
            services.AddScoped<TipoOrdemService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
