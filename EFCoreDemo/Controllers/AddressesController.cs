using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo.Controllers
{
    public class AddressesController : Controller
    {
        private readonly PatientsContext _context;

        public AddressesController(PatientsContext context)
        {
            _context = context;
        }

        // GET: Addresses
        //public async Task<IActionResult> Index()
        //{
         //   var patientsContext = _context.Addresses.Include(a => a.Patient);
        //    return View(await patientsContext.ToListAsync());
        //}

        // GET: Addresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Addresses == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses
               
                .FirstOrDefaultAsync(m => m.AddressId == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // GET: Addresses/Create
        public IActionResult Create(int? id)
        {

            ViewData["PatientId"] = id;
            return View();
        }

        // POST: Addresses Create New
        // 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddressId,PatientId,Address1,City,State,ZipCode")] Address address)
        {
            
                _context.Add(address);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Patients", new {id=address.PatientId});
            
        }



        // GET: Addresses Edit with ID
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Addresses == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            
            return View(address);
        }

        // POST: Addresses Update to db
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddressId,PatientId,Address1,City,State,ZipCode")] Address address)
        {
            if (id != address.AddressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ViewBag.YesMV = "Model Valid";
                try
                {
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.AddressId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            
            return RedirectToAction("Details", "Patients", new { id = address.PatientId });
           
        }

        // GET: Addresses Delete by ID
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Addresses == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses
               
                .FirstOrDefaultAsync(m => m.AddressId == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Addresses Delete from db
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            if (_context.Addresses == null)
            {
                return Problem("Entity set 'PatientsContext.Addresses'  is null.");
            }
            var address = await _context.Addresses.FindAsync(id);
            if (address != null)
            {
                
               _context.Addresses.Remove(address);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Patients", new { id = address.PatientId});
        }

        private bool AddressExists(int id)
        {
            return _context.Addresses.Any(e => e.AddressId == id);
        }
    }
}
