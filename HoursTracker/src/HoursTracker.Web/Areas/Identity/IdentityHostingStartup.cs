﻿using System;
using HoursTracker.Web.Areas.Identity.Data;
using HoursTracker.Web.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(HoursTracker.Web.Areas.Identity.IdentityHostingStartup))]
namespace HoursTracker.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //builder.ConfigureServices((context, services) => {
            //    services.AddDbContext<IdentityContext>(options =>
            //        options.UseSqlServer(
            //            context.Configuration.GetConnectionString("DefaultConnection")));

            //    services.AddDefaultIdentity<User>()
            //        .AddEntityFrameworkStores<IdentityContext>();
            //});
        }
    }
}