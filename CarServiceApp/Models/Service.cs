using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarServiceApp.Models
{
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Display(Name = "Miles")]
        public double miles { get; set; }
        [Display(Name = "Service Price")]
        public double price { get; set; }
        [Display(Name = "Service Details")]
        public string Details { get; set; }
        [DataType(DataType.Date)]
        [Display(Name ="Date Of Service")]
        public DateTime dateAdded { get; set; }

        //Foriegn Keys 
        public int servicetypeId { get; set; }
        public int CarId { get; set; }
        [ForeignKey("CarId")]
        public virtual Car car { get; set; }

        [ForeignKey("servicetypeId")]
        public virtual ServiceType ServiceType { get; set; }
    }
}
