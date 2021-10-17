using AutoMapper;
using Domain.Repository;
using FluentValidation.AspNetCore;
using Infrastructure.Database;
using Infrastructure.Database.Repository;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Presentation.Features.Commands.CreatePerson;
using Presentation.Filters;
using Presentation.Handlers;
using Presentation.Helpers;
using Presentation.Middlewares;
using Services;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Presentation
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

            services.AddControllers(optns => optns.Filters.Add<ValidationFilter>())
                .AddFluentValidation(cnfg =>
                {
                    cnfg.RegisterValidatorsFromAssemblyContaining<CreatePersonCommandValidator>();
                })
                .ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Presentation", Version = "v1" });
            });
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContextPool<ApplicationDbContext>(optns =>
            {
                // optns.UseSqlServer(connectionString);
                optns.UseSqlite(Configuration.GetConnectionString("DefaultConnectionSQLite"));
            });
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Presentation v1"));
            }

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:19328")
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
            app.UseRouting();

            // app.UseMiddleware<AuthUserMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
