using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Szakdolgozat.Models
{
    [DataContract]
    public class EmployeeModel
    {
        public EmployeeModel()
        {

        }
        public EmployeeModel(int x, int y, string label)
        {
            this.x = x;
            this.y = y;
            this.label = label;

        }
        public EmployeeModel(string label, int y)
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
