using EFCoreDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EFCoreDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PatientsContext patientsContext;

        public HomeController(ILogger<HomeController> logger, PatientsContext patientsContext)
        {
            _logger = logger;
            this.patientsContext=patientsContext;
        }

        public IActionResult Index()
        {
            var patient = patientsContext.Patients.ToList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}