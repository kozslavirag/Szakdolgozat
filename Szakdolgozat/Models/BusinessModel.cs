using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Szakdolgozat.Models
{
    public class BusinessModel
    {
        [Required]
        [Key]
        [Display(Name = "Dátum")]
        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd}")]
        public DateTime Date { get; set;}
        [Required]
        [Display(Name = "Internetes forgalom (millió Ft)")]
        [DisplayFormat(DataFormatString ="{0:N0} Ft")]
        public int OnlineBusiness { get; set; }
        [Required]
        [Display(Name = "Kiskereskedelmi forgalom (millió Ft)")]
        [DisplayFormat(DataFormatString = "{0:N0} Ft")]
        public int Retail { get; set; }
    }
}
