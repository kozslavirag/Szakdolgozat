using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Szakdolgozat.Models
{
    public class EmployeeModel
    {
        [Required]
        [Key]
        [Display(Name = "Dátum")]
        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd}")]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Foglalkoztatottak száma")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int NumberofEmployee { get; set; }
        [Required]
        [Display(Name = "Férfiak száma")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int MaleEmployee { get; set; }
        [Required]
        [Display(Name = "Nők száma")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int FemaleEmployee { get; set; }
    }
}
