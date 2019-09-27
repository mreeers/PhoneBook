using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Phone
    {
        [Key]
        public int Id { get; set; }

        [StringLength(4)]
        [Display(Name = "Внутренний номер")]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-.●]?([0-9]{2})")]
        public string ShortPhoneNumber { get; set; }


        [StringLength(15)]
        [Display(Name = "Телефон")]
        [Required]
        public string PhoneNumber { get; set; }

        [DefaultValue(false)]
        public bool Lock { get; set; }


        public ICollection<Person> People;
    }
}
