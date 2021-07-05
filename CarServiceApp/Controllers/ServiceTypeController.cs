using CarServiceApp.Data;
using CarServiceApp.Models;
using CarServiceApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CarServiceApp.Controllers
{
    public class ServiceTypeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ServiceTypeController(ApplicationDbContext db)
        {
            _db = db;
        }
       //[Authorize(Roles =SD.AdminUser)]
        public IActionResult Index()
        {
            return View(_db.serviceTypes.ToList());
        }
        //Get
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceType serviceType)
        {
            if (ModelState.IsValid)
            {
                _db.serviceTypes.Add(serviceType);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(serviceType);
        }
        //Details

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var serviceType = await _db.serviceTypes.SingleOrDefaultAsync(m => m.id == id);
            if(serviceType == null)
            {
                return NotFound();
            }
            return View(serviceType);
        }

        //Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var serviceType = await _db.serviceTypes.SingleOrDefaultAsync(m => m.id == id);
            if (serviceType == null)
            {
                return NotFound();
            }
            return View(serviceType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ServiceType serviceType)
        {
            if(id != serviceType.id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                _db.Update(serviceType);
                await _db.SaveChangesAsync();

              return RedirectToAction(nameof(Index));
            }
            return View(serviceType);
        }
            //Delete
            public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var serviceType = await _db.serviceTypes.SingleOrDefaultAsync(m => m.id == id);
            if (serviceType == null)
            {
                return NotFound();
            }
            return View(serviceType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveServiceType(int id)
        {
            var servicetype = await _db.serviceTypes.SingleOrDefaultAsync(m => m.id == id);
            _db.serviceTypes.Remove(servicetype);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
