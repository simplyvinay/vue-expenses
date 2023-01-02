using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using vue_expenses_api.Infrastructure;
using vue_expenses_api.Infrastructure.Security;

namespace vue_expenses_api;

public class Program
{
    public static async Task Main(
        string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<ExpensesContext>();
            var passwordHasher = services.GetRequiredService<IPasswordHasher>();
            context.Database.Migrate();
            await DataSeeder.SeedData(context, passwordHasher);
        }

        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(
        string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
}