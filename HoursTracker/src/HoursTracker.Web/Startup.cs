using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoursTracker.Core.Campuses;
using HoursTracker.Core.Careers;
using HoursTracker.Core.Classes;
using HoursTracker.Core.Professors;
using HoursTracker.Core.Projecthours;
using HoursTracker.Core.Students;
using HoursTracker.Data.Contexts;
using HoursTracker.Data.Repositories;
using HoursTracker.Data.Repositories.Campuses;
using HoursTracker.Data.Repositories.Careers;
using HoursTracker.Data.Repositories.Classes;
using HoursTracker.Data.Repositories.Professors;
using HoursTracker.Data.Repositories.Projecthours;
using HoursTracker.Data.Repositories.Students;
using HoursTracker.Domain.Aggregates.Campuses;
using HoursTracker.Domain.Aggregates.Careers;
using HoursTracker.Domain.Aggregates.Classes;
using HoursTracker.Domain.Aggregates.Professors;
using HoursTracker.Domain.Aggregates.Projecthours;
using HoursTracker.Domain.Aggregates.Students;
using HoursTracker.Domain.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
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
            services.AddDbContext<HoursTrackerContext>(options =>
                options
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();

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

            services.AddScoped<IProjecthourRepository, ProjecthourRepository>();
            services.AddScoped<IProjecthourService, ProjecthourService>();
            services.AddScoped<IStudentRepository, StudentsRepository>();
            services.AddScoped<IStudentService, StudentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var options = new DbContextOptionsBuilder<HoursTrackerContext>()
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options;

            using var context = new HoursTrackerContext(options);
            context.Database.EnsureCreated();
            
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