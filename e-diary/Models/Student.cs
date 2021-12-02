using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_diary.Models
{
    public class Student
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
        [MaxLength(15, ErrorMessage = "Your Phone Number is too long!")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        [MaxLength(50, ErrorMessage = "Your Email is too long!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your Group!")]
        //make it uppercase
        public char Group { get; set; }

        public bool GetsScholarship { get; set; }

        //public ICollection<Result> results { get; set; }

    }
}
