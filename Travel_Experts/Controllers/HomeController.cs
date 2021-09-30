using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using Travel_Experts.Models;

namespace Travel_Experts.Controllers
{
    public class HomeController : Controller
    {
        private readonly TravelExpertsContext _context;

        public HomeController(TravelExpertsContext context)
        {
            _context = context;
        }

        // GET: Packages
        public async Task<IActionResult> Index()
        {
            var packages = await _context.Packages.Include(i => i.Bookings).ToListAsync();

            // Find packages selections
            ViewData["PkgName"] = new SelectList(_context.Packages, "PkgName", "PkgName");
            ViewData["PkgStartDate"] = new SelectList(_context.Packages, "PkgStartDate", "PkgStartDate");
            ViewData["PkgDesc"] = new SelectList(_context.TripTypes, "TripTypeId", "Ttname");

            return View(packages);           
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
