using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Szakdolgozat.Models
{
    [DataContract]
    public class TourismModel
    {
        public TourismModel()
        {

        }
        public TourismModel(int x, int y, string label)
        {
            this.x = x;
            this.y = y;
            this.label = label;

        }

        public TourismModel(string label, int y)
        {
            this.label = label;
            this.y = y;
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
        [Display(Name = "Külföldi utazások száma (ezer fő)")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int TripForeignGuest { get; set; }
        [Required]
        [Display(Name = "Külföldi vendégéjszakák száma (ezer nap)")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int NightForeignGuest { get; set; }
        [Required]
        [Display(Name = "Külföldi vendégek költése (millió Ft)")]
        [DisplayFormat(DataFormatString = "{0:N0} Ft")]
        public int SpendForeignGuest { get; set; }
        [Required]
        [Display(Name = "Belföldi utazások száma (ezer fő)")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int TripHungarianGuest { get; set; }
        [Required]
        [Display(Name = "Belföldi vendégéjszakák száma (ezer nap)")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int NightHungarianGuest { get; set; }
        [Required]
        [Display(Name = "Belföldi vendégek költése (millió Ft)")]
        [DisplayFormat(DataFormatString = "{0:N0} Ft")]
        public int SpendHungarianGuest { get; set; }


    }
}
