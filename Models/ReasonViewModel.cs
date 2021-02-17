using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkForBHHC.Models
{
    public class ReasonViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255, ErrorMessage = "Reason can only by 255 characters long")]
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
