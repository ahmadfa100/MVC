using Microsoft.AspNetCore.Mvc;
using Task6.Data;
using Task6.Models;

namespace Task6.Controllers
{
    public class SupplierController : Controller
    {

        private readonly ApplicationDbContext _context;

        public SupplierController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var suppliers = _context.Suppliers.ToList();

            return View(suppliers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}
