using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Position
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Поле не может быть больше 100 символов")]
        [Display(Name = "Должность")]
        public string Title { get; set; }

        [Required]
        public int Level { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
