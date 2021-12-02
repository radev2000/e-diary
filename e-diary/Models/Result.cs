using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_diary.Models
{
    public class Result
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int? StudentID { get; set; }
        public virtual Student Student { get; set; }

        [Required]
        public int? SubjectID { get; set; }
        public virtual Subject Subject { get; set; }


        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [MaxLength(150, ErrorMessage = "Your Comment is too long!")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "You need to give a Grade!")]
        public double Grade { get; set; }
    }
}
