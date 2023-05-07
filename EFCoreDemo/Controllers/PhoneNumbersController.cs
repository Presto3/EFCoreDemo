using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFCoreDemo.Models;
using System.Net.Mail;

namespace EFCoreDemo.Controllers
{
    public class PhoneNumbersController : Controller
    {
        private readonly PatientsContext _context;

        public PhoneNumbersController(PatientsContext context)
        {
            _context = context;
        }

        // GET: PhoneNumbers
        public async Task<IActionResult> Index()
        {
            var patientsContext = _context.PhoneNumbers.Include(p => p.Patient);
            return View(await patientsContext.ToListAsync());
        }

        // GET: PhoneNumbers Details by Id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PhoneNumbers == null)
            {
                return NotFound();
            }

            var phoneNumber = await _context.PhoneNumbers
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.PhoneId == id);
            if (phoneNumber == null)
            {
                return NotFound();
            }

            return View(phoneNumber);
        }

        // GET: PhoneNumbers Add new
        public IActionResult Create(int? id)
        {
            ViewData["PatientId"] = id;
            return View();
        }

        // POST: PhoneNumbers Insert into db
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientId,PhoneNumber1,PhoneType")] PhoneNumber phoneNumber)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phoneNumber);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Patients", new { id = phoneNumber.PatientId });
            }
           
            return View(phoneNumber);
        }

        // GET: PhoneNumbers Edit by Id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PhoneNumbers == null)
            {
                return NotFound();
            }

            var phoneNumber = await _context.PhoneNumbers.FindAsync(id);
            if (phoneNumber == null)
            {
                return NotFound();
            }
          
            return View(phoneNumber);
        }

        // POST: PhoneNumbers update db
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhoneId,PatientId,PhoneNumber1,PhoneType")] PhoneNumber phoneNumber)
        {
            if (id != phoneNumber.PhoneId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phoneNumber);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneNumberExists(phoneNumber.PhoneId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Patients", new { id = phoneNumber.PatientId });
            }
            return View(phoneNumber);
        }

        // GET: PhoneNumbers Delete by Id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PhoneNumbers == null)
            {
                return NotFound();
            }

            var phoneNumber = await _context.PhoneNumbers
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.PhoneId == id);
            if (phoneNumber == null)
            {
                return NotFound();
            }

            return View(phoneNumber);
        }

        // POST: PhoneNumbers Delete from db
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PhoneNumbers == null)
            {
                return Problem("Entity set 'PatientsContext.PhoneNumbers'  is null.");
            }
            var phoneNumber = await _context.PhoneNumbers.FindAsync(id);
            if (phoneNumber != null)
            {
                _context.PhoneNumbers.Remove(phoneNumber);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Patients", new { id = phoneNumber.PatientId });
        }

        private bool PhoneNumberExists(int id)
        {
          return (_context.PhoneNumbers?.Any(e => e.PhoneId == id)).GetValueOrDefault();
        }
    }
}
