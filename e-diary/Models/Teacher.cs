using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_diary.Models
{
    public class Teacher
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter your First Name!")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Your First Name is too long!")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Please enter your Last Name!")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Your Last Name is too long!")]
        public string LName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }

        public double Rating { get; set; }

        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
    }
}
