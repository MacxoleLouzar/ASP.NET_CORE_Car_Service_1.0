using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarServiceApp.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name ="VIN")]
        public string Vin { get; set; }
        [Required]
        [Display(Name = "Make")]
        public string make { get; set; }
        [Required]
        [Display(Name = "Model")]
        public string model { get; set; }
        [Required]
        [Display(Name = "Style")]
        public string style { get; set; }
        [Required]
        [Display(Name = "Color")]
        public string color { get; set; }

        public int Year { get; set; }
        public double Miles { get; set; }

        //foriegn key
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual IdentityUser IdentityUser { get; set; }
    }
}
