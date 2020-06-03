using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoursTracker.Core.Campuses;
using HoursTracker.Core.Careers;
using HoursTracker.Core.Classes;
using HoursTracker.Core.Professors;
using HoursTracker.Data.Contexts;
using HoursTracker.Data.Repositories;
using HoursTracker.Data.Repositories.Campuses;
using HoursTracker.Data.Repositories.Careers;
using HoursTracker.Data.Repositories.Classes;
using HoursTracker.Data.Repositories.Professors;
using HoursTracker.Domain.Aggregates.Campuses;
using HoursTracker.Domain.Aggregates.Careers;
using HoursTracker.Domain.Aggregates.Classes;
using HoursTracker.Domain.Aggregates.Professors;
using HoursTracker.Domain.Contracts;
using HoursTracker.Web.Areas.Identity.Data;
using HoursTracker.Web.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HoursTracker.Web
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
            services
                .AddDbContext<HoursTrackerContext>(options =>
                    options
                        .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services.AddRazorPages();

            ConfigureDependencies(services);
        }

        private static void ConfigureDependencies(IServiceCollection services)
        {
            services.AddScoped<DbContext, HoursTrackerContext>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(EfRepository<>));

            services.AddScoped<IProfessorRepository, ProfessorRepository>();
            services.AddScoped<IProfessorService, ProfessorService>();

            services.AddScoped<ICampusRepository, CampusRepository>();
            services.AddScoped<ICampusService, CampusService>();
            services.AddScoped<ICareerRepository, CareerRepository>();
            services.AddScoped<ICareerService, CareerService>();
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<IClassService, ClassService>();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}")
                .RequireAuthorization();
                endpoints.MapRazorPages();
            });
        }
    }
}