using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS_Lexicon.Data;


namespace LMS_Lexicon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<LmsDbContext>();

                //context.Database.EnsureDeleted();
                //context.Database.Migrate();

                //dotnet user-secrets set "AdminPW" "BytMig123!"
                var config = services.GetRequiredService<IConfiguration>();
                //var userPW = config["UserPW"];

                try
                {
                    SeedData.InitAsync(context, services).Wait();
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
