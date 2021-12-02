using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_diary.Models
{
    public class Subject
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter the Subject's Name!")]
        [StringLength(50, ErrorMessage = "The given Name is too long!")]
        public string Name { get; set; }

        [Required]
        public int TeacherID { get; set; }
        public virtual Teacher Teacher { get; set; }



    }
}
