using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Szakdolgozat.Models
{
    public class UnemployedModel
    {
        [Required]
        [Key]
        [Display(Name = "Dátum")]
        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd}")]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Munkanélküliek száma")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int NumberofUnemployed { get; set; }
        [Required]
        [Display(Name = "Férfiak száma")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int MaleUnemployed { get; set; }
        [Required]
        [Display(Name = "Nők száma")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int FemaleUnemployed { get; set; }
    }
}
