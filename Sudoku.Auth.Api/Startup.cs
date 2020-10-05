using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sudoku.Application.Accounts.Commands.LoginAccount;
using Sudoku.Application.Infrastructure.Automapper;
using Sudoku.Application.Interfaces;
using Sudoku.Auth.Api.Extensions;
using Sudoku.Common;
using Sudoku.Persistence;

namespace Sudoku.Auth.Api
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

            var authOptionsConfiguration = Configuration.GetSection("Auth");
            services.Configure<AuthOptions>(authOptionsConfiguration);

            services.AddCors(options =>
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    }));

            services.AddSwaggerGen();
            
            services.AddDbContext<ISudokuDbContext, SudokuDbContext>(options =>
                options.UseSqlServer("Data Source=cs-cyfral-test;Initial Catalog=testt;user id=sa;Password=Zab%BP@;"));

            using (var dbcontext = services.BuildServiceProvider().GetService<ISudokuDbContext>())
            {
                dbcontext.Migrate();
            }

            services.AddMediatR(typeof(LoginAccountCommand).Assembly);
            services.AddCustomServiecs();
            services.AddAutoMapper(cfg => {
                cfg.AddProfile<ApplicationProfile>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "sudoku.Auth.Api");
                c.RoutePrefix = string.Empty;
            });
            app.UseRouting();

            app.UseCors();

            app.UseEndpoints(endpoints => { endpoints.MapControllerRoute("default", "api/{controller}/{action}/{id?}"); });
        }
    }
}