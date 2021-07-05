using CarServiceApp.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarServiceApp.ViewModels
{
    public class CarAndCustomerViewModel
    {
       // public IdentityUser userObj { get; set; }
        public IdentityUser UserObj { get; set; }
        public IEnumerable<Car> Cars { get; set; } 
    }
}
