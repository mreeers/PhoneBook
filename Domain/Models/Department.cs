using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Отдел")]
        public string Title { get; set; }

        [DefaultValue(false)]
        public bool Lock { get; set; }

        [Required]
        public int Level { get; set; }

        public ICollection<Person> People { get; set; }

        public override string ToString()
        {
            return Title;
        }


    }
}
