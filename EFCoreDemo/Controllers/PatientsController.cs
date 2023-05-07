using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFCoreDemo.Models;

namespace EFCoreDemo.Controllers
{
    public class PatientsController : Controller
    {
        private readonly PatientsContext _context;

        public PatientsController(PatientsContext context)
        {
            _context = context;
        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {
              return View(await _context.Patients.ToListAsync());
        }

        // GET: Patients Details by ID
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .Include(m => m.Addresses)
                .Include(p => p.EmailAddresses)
                .Include(q => q.PhoneNumbers)
                .FirstOrDefaultAsync(m => m.PatientId == id);
                
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients Add New
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients Insert into db

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Birthdate,Gender,DateAdded")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(patient);
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients Edit by Id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients Update db

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientId,FirstName,LastName,Birthdate,Gender,DateAdded")] Patient patient)
        {
            if (id != patient.PatientId)
            {
                return NotFound();

            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.PatientId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            ViewBag.SuccessMessage = patient.FirstName + " " + patient.LastName +" has been updated.";
            return View(patient);
        }

        // GET: Patients Delete by Id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients Delete from db
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patients == null)
            {
                return Problem("Entity set 'PatientsContext.Patients'  is null.");
            }
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
          return _context.Patients.Any(e => e.PatientId == id);
        }
    }
}
