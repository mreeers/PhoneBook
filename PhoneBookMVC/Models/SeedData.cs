using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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

                    context.Departments.AddRange(Departments.Select(c => c.Value));
                    context.Positions.AddRange(Positions.Select(s => s.Value));
                    context.AddRange(
                        new Person
                        {
                            Department = department["Администрация"],
                            FirstName = "Конев",
                            SecondName = "Даниил",
                            MiddleName = "Владимирович",
                            Position = position["Директор"]
                        },
                        new Person
                        {
                            Department = department["Администрация"],
                            FirstName = "Зыков",
                            SecondName = "Павел",
                            MiddleName = "Михайлович",
                            Position = position["Заместитель директора учреждения"]
                        },
                        new Person
                        {
                            Department = department["Отдел системного программирования"],
                            FirstName = "Семенов",
                            SecondName = "Артем",
                            MiddleName = "Витальевич",
                            Position = position["Начальник отдела"]
                        },
                        new Person
                        {
                            Department = department["Отдел прикладного программирования"],
                            FirstName = "Арсланов",
                            SecondName = "Нил",
                            MiddleName = "Нильевич",
                            Position = position["Начальник отдела"]
                        });
                context.SaveChanges();
            }
            
        }
        private static Dictionary<string, Department> department;
        public static Dictionary<string, Department> Departments
        {
            get
            {
                if (department == null)
                {
                    var list = new Department[]
                    {
                        new Department { Level = 1, Title = "Администрация", Id=1 },
                        new Department { Level = 1, Title = "Отдел системного программирования", Id=2},
                        new Department { Level = 1, Title = "Отдел общегородских систем", Id=3},
                        new Department { Level = 1, Title = "Отдел систем документооборота", Id=4},
                        new Department { Level = 1, Title = "Отдел прикладного программирования", Id=5 },
                        new Department { Level = 1, Title = "Отдел системного администрирования", Id=6 },
                        new Department { Level = 1, Title = "Отдел администрирования баз данных", Id=7 },
                        new Department { Level = 1, Title = "Отдел информационной безопасности", Id=8 }
                    };
                    department = new Dictionary<string, Department>();
                    foreach (Department el in list)
                    {
                        department.Add(el.Title, el);
                    }
                }
                return department;
            }


        }

        private static Dictionary<string, Position> position;
        public static Dictionary<string, Position> Positions
        {
            get
            {
                if (position == null)
                {
                    var list = new Position[]
                    {
                        new Position {Level = 1, Title = "Директор", Id=1 },
                        new Position {Level = 1, Title = "Заместитель директора учреждения", Id=2 },
                        new Position {Level = 1, Title = "Начальник отдела", Id=3 },
                        new Position {Level = 1, Title = "Ведущий инженер-программист", Id=4 },
                        new Position {Level = 1, Title = "Инженер-программист 1 категории", Id=5 }
                    };
                    position = new Dictionary<string, Position>();
                    foreach (Position s in list)
                    {
                        position.Add(s.Title, s);
                    }
                }
                return position;
            }
        }
    }
}

