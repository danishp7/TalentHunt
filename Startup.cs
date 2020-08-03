using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using TalentHunt.Data;
using TalentHunt.Helpers;
using TalentHunt.Models;

namespace TalentHunt
{
    public class Startup
    {
        public IConfiguration _config { get; }
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // for quick refresh in browser on changing views 
            services.AddControllersWithViews();

            // add sql server connection
            services.AddDbContext<TalentHuntContext>(
                opt => opt.UseSqlServer(_config.GetConnectionString("TalentHuntConnectionString")));

            // add automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // remove auto referencing loop
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            // add repositories
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ISharedRepository, SharedRepository>();
            services.AddScoped<IVacancyRepository, VacancyRepository>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();

            // add authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            // add cors
            services.AddCors();

            // add login path as I dont have account controller
            services.ConfigureApplicationCookie(opt => {
                opt.LoginPath = "/api/auth/login";
            });

            // add identity
            services.AddIdentity<User, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
                cfg.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_.0123456789-@";

                cfg.Password = new PasswordOptions
                {
                    RequireDigit = true,
                    RequiredLength = 5,
                    RequiredUniqueChars = 1,
                    RequireLowercase = true,
                    RequireUppercase = true,
                    RequireNonAlphanumeric = false
                };
                cfg.Lockout = new LockoutOptions
                {
                    DefaultLockoutTimeSpan = new TimeSpan(0, 0, 5, 0),
                    MaxFailedAccessAttempts = 5
                };
            }).AddEntityFrameworkStores<TalentHuntContext>();

            // register application settings
            services.Configure<ApplicationSettings>(_config.GetSection("ApplicationSettings"));
            
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
                // .UseHsts();
            }

            app.UseCors(c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
