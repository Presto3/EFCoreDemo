using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFCoreDemo.Models;
using System.Net;

namespace EFCoreDemo.Controllers
{
    public class EmailAddressesController : Controller
    {
        private readonly PatientsContext _context;

        public EmailAddressesController(PatientsContext context)
        {
            _context = context;
        }

        // GET: EmailAddresses
        public async Task<IActionResult> Index()
        {
            var patientsContext = _context.EmailAddresses.Include(e => e.Patient);
            return View(await patientsContext.ToListAsync());
        }

        // GET: EmailAddresses Details by ID
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmailAddresses == null)
            {
                return NotFound();
            }

            var emailAddress = await _context.EmailAddresses
                .Include(e => e.Patient)
                .FirstOrDefaultAsync(m => m.EmailId == id);
            if (emailAddress == null)
            {
                return NotFound();
            }

            return View(emailAddress);
        }

        // GET: EmailAddresses Add New
        public IActionResult Create(int? id)
        {
            ViewData["PatientId"] = id;
            return View();
        }

        // POST: EmailAddresses Add New
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientId,Email")] EmailAddress emailAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emailAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Patients", new { id = emailAddress.PatientId });
            }
            
            return View(emailAddress);
        }

        // GET: EmailAddresses Edit by ID
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmailAddresses == null)
            {
                return NotFound();
            }

            var emailAddress = await _context.EmailAddresses.FindAsync(id);
            if (emailAddress == null)
            {
                return NotFound();
            }
            
            return View(emailAddress);
        }

        // POST: EmailAddresses Update db
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmailId,PatientId,Email")] EmailAddress emailAddress)
        {
            if (id != emailAddress.EmailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emailAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailAddressExists(emailAddress.EmailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Patients", new { id = emailAddress.PatientId });
            }
            
            return View(emailAddress);
        }

        // GET: EmailAddresses Delete by ID
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmailAddresses == null)
            {
                return NotFound();
            }

            var emailAddress = await _context.EmailAddresses
                .Include(e => e.Patient)
                .FirstOrDefaultAsync(m => m.EmailId == id);
            if (emailAddress == null)
            {
                return NotFound();
            }

            return View(emailAddress);
        }

        // POST: EmailAddresses Delete from db
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmailAddresses == null)
            {
                return Problem("Entity set 'PatientsContext.EmailAddresses'  is null.");
            }
            var emailAddress = await _context.EmailAddresses.FindAsync(id);
            if (emailAddress != null)
            {
                _context.EmailAddresses.Remove(emailAddress);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Patients", new { id = emailAddress.PatientId });
        }

        private bool EmailAddressExists(int id)
        {
          return (_context.EmailAddresses?.Any(e => e.EmailId == id)).GetValueOrDefault();
        }
    }
}
