using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TalentHunt.Data;
using TalentHunt.Helpers;
using TalentHunt.Models;

namespace TalentHunt
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    // get the required parameters for the method
                    var _user = services.GetRequiredService<UserManager<User>>();
                    var _role = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var _ctx = services.GetRequiredService<TalentHuntContext>();

                    // add the migrations
                    _ctx.Database.Migrate();

                    // now seed the default user
                    SeedData.SeedUserData(_user, _ctx, _role).Wait();

                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "an error occured during migratuons");
                }
            };

            host.Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
