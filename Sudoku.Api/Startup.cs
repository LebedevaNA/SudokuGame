using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Sudoku.Api.Extensions;
using Sudoku.Api.Middleware;
using Sudoku.Application.Accounts.Commands.LoginAccount;
using Sudoku.Application.Hubs;
using Sudoku.Application.Infrastructure.Automapper;
using Sudoku.Application.Interfaces;
using Sudoku.Common;
using Sudoku.Persistence;

namespace Sudoku.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options => 
                    options.Filters.Add(new ExceptionFilter()))
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            var authOptions = Configuration.GetSection("Auth").Get<AuthOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false; //в релизе true
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = authOptions.Audience,

                        ValidateLifetime = true,

                        IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });
            
            services.AddCors(options =>
                options.AddDefaultPolicy(
                    builder =>
                    {
                        //builder.AllowAnyOrigin()
                        builder.WithOrigins("http://localhost:40427")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    }));

            services.AddSwaggerGen();

            services.AddMediatR(typeof(LoginAccountCommand).Assembly);
            services.AddCustomServices();
            
            services.AddSignalR();
            
            services.AddAutoMapper(cfg => {
                cfg.AddProfile<ApplicationProfile>();
            });
            
            services.AddDbContext<ISudokuDbContext, SudokuDbContext>(options =>
                options.UseSqlServer("Data Source=cs-cyfral-test;Initial Catalog=testt;user id=sa;Password=Zab%BP@;"));
            using var dbcontext = services.BuildServiceProvider().GetService<ISudokuDbContext>();
                dbcontext.Migrate();

                
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "sudoku.Api");
                c.RoutePrefix = string.Empty;
            });
            
            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapHub<GameStateHub>("/gameStateHub");
            });
        }
    }
}