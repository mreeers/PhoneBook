using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;


namespace Domain.Models
{
    /// <summary>
    /// Заполняет таблицу данными, если она пустая
    /// </summary>
    class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                if (context.People.Any() || context.Phones.Any() || context.Departments.Any() || context.Phones.Any())
                {
                    return;
                }

                context.Departments.AddRange(new Department
                {
                    Title = "Администрация",
                    Level = 1,
                });

                context.Positions.AddRange(new Position
                {
                    Title = "Директор учреждения",
                    Level = 1
                }); ;

                context.Phones.AddRange(new Phone
                {
                    PhoneNumber = "52-29-01"
                });

                context.People.AddRange(new Person
                {
                    FirstName = "Даниил",
                    SecondName = "Конев",
                    MiddleName = "Владимирович",
                    Email = "test@test.ru",
                }) ;
                context.SaveChanges();
            }
        }

    }
}
