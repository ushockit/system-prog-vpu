using Application;
using Application.Hubs;
using AutoMapper;
using Domain.Repository;
using Infrastructure.Database;
using Infrastructure.Database.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Services;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Helpers;
using WebApplication1.Middlewares;
using WebApplication1.Services;
using WebApplication1.Services.Abstract;
using WebApplication1.Services.Impl;
using WebApplication1.Utils;

namespace WebApplication1
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
            services.AddLocalization(optns =>
            {
                optns.ResourcesPath = "Resources";
            });
            services.AddControllersWithViews()
                .AddViewLocalization();
            services.AddSession();

            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IWebPeopleService, WebPeopleService>();

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContextPool<ApplicationDbContext>(optns =>
            {
                optns.UseSqlServer(connectionString);
            });
            services.AddSingleton<AppUtils>();
            services.AddTransient<IStringLocalizer, DefaultStringLocalizer>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddApplicationServices();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            AppUtils utils
            )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(utils.DefaultCultureName),
                SupportedCultures = utils.AvailableCultures,
                SupportedUICultures = utils.AvailableCultures,
            });
            // app.UseMiddleware<LocalizationMiddleware>();
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chathub");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
