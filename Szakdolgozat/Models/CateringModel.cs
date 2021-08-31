using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Szakdolgozat.Context;
using Szakdolgozat.Controllers;

namespace Szakdolgozat.Models
{
    [DataContract]
    public class CateringModel
    {
        public CateringModel(DateTime Date, int SalesVolume)
        {
            this.Date = Date;
            this.SalesVolume = SalesVolume;

        }
        //private readonly DataContext _context;

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
