using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarServiceApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;

namespace CarServiceApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UsersController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(string opt, string search)
        {
            var user = _db.Users.ToList();
            if(opt == "email" && search != null)
            {
                user = _db.Users.Where(u => u.Email.ToLower().Contains(search.ToLower())).ToList();
            }
            else if (opt == "name" && search != null)
            {
                user = _db.Users.Where(u => u.UserName.ToLower().Contains(search.ToLower())).ToList();
            }
            else
                if (opt == "phone" && search != null)
            {
                user = _db.Users.Where(u => u.PhoneNumber.ToLower().Contains(search.ToLower())).ToList();
            }
            return View(user);
        }
        //Details
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _db.Users.SingleOrDefaultAsync(m => m.Id==id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        //Edit
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _db.Users.SingleOrDefaultAsync(m => m.Id==id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        //POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, IdentityUser users)
        {
            if (id != users.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Update(users);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }
        //Delete
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _db.Users.SingleOrDefaultAsync(m => m.Id==id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveUser(string id)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Id==id);
            _db.Users.Remove(user);
            return RedirectToAction(nameof(Index));
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
