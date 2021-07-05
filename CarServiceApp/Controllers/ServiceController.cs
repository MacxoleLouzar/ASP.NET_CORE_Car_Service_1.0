using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarServiceApp.Data;
using CarServiceApp.Models;
using CarServiceApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarServiceApp.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ServiceController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int carId)
        {
            var car = _db.cars.FirstOrDefault(c => c.id == carId);
            var model = new CarAndServiceViewModel
            {
                CarId = car.id,
                make = car.make,
                Vin = car.Vin,
                style = car.style,
                Year = car.Year,
                model = car.model,
                UserId = car.UserId,
                ServiceTypesObj = _db.serviceTypes.ToList(),
                PastserviceObj = _db.services.Where(s => s.CarId == carId).OrderByDescending(s => s.dateAdded),
            };
            return View(model);
        }

        public IActionResult Create(int carId)
        {
            var car = _db.cars.FirstOrDefault(c => c.id == carId);
            var model = new CarAndServiceViewModel 
            {
                CarId = car.id,
               make = car.make,
               Vin = car.Vin,
               style = car.style,
               Year = car.Year,
               model = car.model,
               UserId = car.UserId,
                ServiceTypesObj = _db.serviceTypes.ToList(),
                PastserviceObj = _db.services.Where(s => s.CarId == carId).OrderByDescending(s => s.dateAdded).Take(5)
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (CarAndServiceViewModel model)
        {
            if(ModelState.IsValid)
            {
                model.NewServiceObj.CarId = model.CarId;
                model.NewServiceObj.dateAdded = DateTime.Now;
                _db.Add(model.NewServiceObj);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Create), new { carId = model.CarId });
            }
            var car = _db.cars.FirstOrDefault(c => c.id == model.CarId);
            var newmodel = new CarAndServiceViewModel
            {

                CarId = car.id,
                make = car.make,
                Vin = car.Vin,
                style = car.style,
                Year = car.Year,
                model = car.model,
                UserId = car.UserId,
                

                ServiceTypesObj = _db.serviceTypes.ToList(),
                PastserviceObj = _db.services.Where(s => s.CarId == model.CarId).OrderByDescending(s => s.dateAdded).Take(5)
            };
            return View(newmodel);
        }

        public async Task<IActionResult> Delete (int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var service = await _db.services.Include(m => m.car).Include(s => s.ServiceType).SingleOrDefaultAsync(x => x.id == id);
            if(service == null)
            {
                return NotFound();
            }
            return View(service);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(Service model)
        {
            var serviceId = model.id;
            var carId = model.CarId;
            var service = await _db.services.SingleOrDefaultAsync(m => m.id == serviceId);
            if(service == null)
            {
                return NotFound();
            }
            _db.services.Remove(service);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Create), new { CarId = carId });
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                _db.Dispose();
            }
        }
    }

}
