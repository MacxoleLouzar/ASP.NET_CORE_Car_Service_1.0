using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarServiceApp.Data;
using CarServiceApp.Models;
using CarServiceApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarServiceApp.Controllers
{
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CarsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(string UserId)
        {
            if (UserId == null)
            {
                UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _db.cars.Where(c => c.UserId == UserId);
            }
            var model = new CarAndCustomerViewModel 
            {
                Cars = _db.cars.Where(c => c.UserId == UserId),
                UserObj = _db.Users.FirstOrDefault(u => u.Id == UserId)
            };
            return View(model);
        }
        public IActionResult Create(string userId)
        {
            Car carobj = new Car
            {
                Year = DateTime.Now.Year,
                UserId = userId
            };
           return View(carobj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Car car)
        {
            if(ModelState.IsValid)
            {
                _db.Add(car);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { userId = car.UserId });
            }
            return View(car);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var car = await _db.cars.Include(c => c.IdentityUser).SingleOrDefaultAsync(m => m.id == id);
            if(car == null)
            {
                NotFound();
            }
            return View(car);
        }
        //Edit

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var car = await _db.cars.Include(c => c.IdentityUser).SingleOrDefaultAsync(m => m.id == id);
            if (car == null)
            {
                NotFound();
            }
            return View(car);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, Car car)
        {
            if(id != car.id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                _db.cars.Update(car);
                await _db.SaveChangesAsync();
               return RedirectToAction(nameof(Index), new {userId = car.UserId });
            }
            return View(car);
        }
        //Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var car = await _db.cars.Include(c => c.IdentityUser).SingleOrDefaultAsync(m => m.id == id);
            if (car == null)
            {
                NotFound();
            }
            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> DeleteConfirmed(int id)
        {
            var car = await _db.cars.SingleOrDefaultAsync(c => c.id == id);
            if(car == null)
            {
                return NotFound();
            }
            _db.cars.Remove(car);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { UserId = car.UserId });
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
