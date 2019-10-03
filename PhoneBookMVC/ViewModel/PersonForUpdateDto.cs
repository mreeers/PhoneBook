using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookMVC.ViewModel
{
    public class PersonForUpdateDto
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "Поле не может быть больше 100 символов")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        
        [StringLength(100, ErrorMessage = "Поле не может быть больше 100 символов")]
        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }
        
        [StringLength(100, ErrorMessage = "Поле не может быть больше 100 символов")]
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Display(Name = "ФИО")]
        public string FullName { get { return $"{SecondName} {FirstName} {MiddleName}"; } }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Отдел")]
        public int DepartmentId { get; set; }
        
        [Display(Name = "Должность")]
        public int PositionId { get; set; }

        public int PhoneId { get; set; }

        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

    }
}
