using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;


namespace Domain.Models
{
    class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
            {

                if (context.UserAdmins.Any())
                {
                    return;
                }
                context.UserAdmins.AddRange(new Models.Admin { UserName = "admin", Password= "admin" });
                context.SaveChanges();

            }
        }

    }
}
