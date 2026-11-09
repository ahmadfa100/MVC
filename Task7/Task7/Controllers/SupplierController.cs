using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Task7.Data;
using Task7.Models;

namespace Task7.Controllers
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
            var suppliers = _context.Supplier.Include(s => s.Products).ToList();
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
            _context.Supplier.Add(supplier);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var supplier = _context.Supplier.FirstOrDefault(s => s.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }
        [HttpPost]
        public IActionResult Edit(Supplier supplier)
        {
            _context.Supplier.Update(supplier);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var supplier = _context.Supplier.Include(s => s.Products).FirstOrDefault(s => s.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var supplier = _context.Supplier.FirstOrDefault(s => s.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }
            _context.Supplier.Remove(supplier);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
