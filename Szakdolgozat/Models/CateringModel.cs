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
        public CateringModel()
        {

        }
        public CateringModel(int x, int y, string label)
        {
            this.x = x;
            this.y = y;
            this.label = label;

        }
        //private readonly DataContext _context;

        [DataMember(Name = "x")]
        public Nullable<int> x = null;

        [DataMember(Name = "y")]
        public Nullable<int> y = null;

        [DataMember(Name = "label")]
        public string label = "";

        [Required]
        [Key]
        [Display(Name ="Dátum")]
        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd}")]
        [DataMember(Name = "Date")]
        public DateTime Date { get; set; }
        
        [Required]
        [Display(Name ="Eladási forgalom (millió Ft)")]
        [DisplayFormat(DataFormatString = "{0:N0} Ft")]
        [DataMember(Name = "SalesVolume")]
        public int SalesVolume { get; set; }
        



    }
}
