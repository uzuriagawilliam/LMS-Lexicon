using LMS_Lexicon.Data.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace LMS_Lexicon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<LmsDbContext>();

                context.Database.EnsureDeleted();
                context.Database.Migrate();

                //dotnet user-secrets set "AdminPW" "BytMig123!"
                var config = services.GetRequiredService<IConfiguration>();
                //var userPW = config["UserPW"];
                var userPW = "PassWord";


                try
                {
                    SeedData.InitAsync(context, services, userPW).Wait();
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
            host.Run();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
