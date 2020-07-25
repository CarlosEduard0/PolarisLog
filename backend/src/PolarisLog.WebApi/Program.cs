using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PolarisLog.Infra;
using PolarisLog.Infra.CrossCutting.Identity.Context;
using PolarisLog.WebApi.Extensions;

namespace PolarisLog.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .MigrateDatabase<ApplicationDbContext>()
                .MigrateDatabase<Context>()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}