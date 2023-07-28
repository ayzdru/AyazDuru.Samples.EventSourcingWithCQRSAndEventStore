using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Behaviours;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Interfaces;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.IoC;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Entities;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Interfaces;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Infrastructure.Data;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Infrastructure.Interceptors;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Infrastructure.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Infrastructure.IoC
{
    public static class ConfigureServicesDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            services.AddTransient<IEmailSender, EmailService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<ApplicationUser>(o => o.SignIn.RequireConfirmedAccount = false)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped<EntitySaveChangesInterceptor>();
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            if (webHostEnvironment.IsDevelopment())
            {
                services.AddScoped<ApplicationDbContextInitialiser>();
            }
            services.AddApplication();
            return services;
        }
    }
}
