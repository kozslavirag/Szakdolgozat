using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Szakdolgozat.Models
{
    public class CateringModel
    {
        [Required]
        [Key]
        [Display(Name ="Dátum")]
        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd}")]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name ="Eladási forgalom (millió Ft)")]
        [DisplayFormat(DataFormatString = "{0:N0} Ft")]
        public int SalesVolume { get; set; }
    }
}
