using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Szakdolgozat.Models
{
    public class PriceModel
    {
        [Required]
        [Key]
        [Display(Name = "Dátum")]
        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd}")]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Alap élelmiszer árak")]
        [DisplayFormat(DataFormatString = "{0:N0} Ft")]
        public int StapleFoodPrice { get; set; }
        [Required]
        [Display(Name = "Benzin ár")]
        [DisplayFormat(DataFormatString = "{0:N0} Ft")]
        public int PetrolPrice { get; set; }
        [Required]
        [Display(Name = "Gázolaj ár")]
        [DisplayFormat(DataFormatString = "{0:N0} Ft")]
        public int GasOilPrice { get; set; }

    }
}
