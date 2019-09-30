using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class PeopleInTheDepartment
    {
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<Person> People { get; set; }

    }
}
