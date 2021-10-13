using LMS_Lexicon.Api.Data.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Api.Data.Data;

namespace LMS_Lexicon.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<LMS_LexiconApiContext>();

                context.Database.EnsureDeleted();
                context.Database.Migrate();

                
                var config = services.GetRequiredService<IConfiguration>();
//                var adminPW = config["AdminPW"];

                try
                {
                    SeedData.InitializeAcync(services).Wait();
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
