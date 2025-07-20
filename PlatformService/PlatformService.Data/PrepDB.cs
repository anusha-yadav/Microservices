using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Data;

namespace PlatformService.Data
{
    public static class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder,bool isProd)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateAsyncScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(),isProd);
            }
        }

        private static void SeedData(AppDbContext appDbContext,bool isProd)
        {
            if (isProd)
            {
                Console.WriteLine("---> Attempting to apply migrations...");
                try
                {
                    appDbContext.Database.Migrate();
                }
                catch(Exception ex) 
                { 
                    Console.WriteLine("---> Could not apply migrations" + ex.ToString());
                }
            }
            if (!appDbContext.Platforms.Any())
            {
                Console.WriteLine("--> Seeding Data ..");

                appDbContext.Platforms.AddRange(
                    new Platform() { Name = "Dotnet", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Kubernets", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
                );
                appDbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}
