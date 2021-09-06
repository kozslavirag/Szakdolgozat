using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Szakdolgozat.Models
{
    [DataContract]
    public class IndustryModel
    {
        public IndustryModel()
        {

        }

        public IndustryModel(int x, int y, string label)
        {
            this.x = x;
            this.y = y;
            this.label = label;

        }

        [DataMember(Name = "x")]
        public Nullable<int> x = null;

        [DataMember(Name = "y")]
        public Nullable<int> y = null;

        [DataMember(Name = "label")]
        public string label = "";

        [Required]
        [Key]
        [Display(Name = "Dátum")]
        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd}")]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Értékesítési összeg (milliárd Ft)")]
        [DisplayFormat(DataFormatString = "{0:N0} Ft")]
        public int SalesAmount { get; set; }
    }
}
