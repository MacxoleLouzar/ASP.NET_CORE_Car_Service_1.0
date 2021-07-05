using CarServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarServiceApp.ViewModels
{
    public class CarAndServiceViewModel
    {
        public int CarId { get; set; }
        public string Vin { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string style { get; set; }
        public int Year { get; set; }
        public string UserId { get; set; }


        public Service NewServiceObj { get; set; }
        public IEnumerable<Service> PastserviceObj { get; set; }
        public List<ServiceType> ServiceTypesObj { get; set; }
    }
}
