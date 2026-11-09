using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task7.Data;
using Task7.Models;

namespace Task7.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var orders = _context.Order.Include(o => o.Employee).ToList();
            return View(orders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Employees = _context.Employee.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Order order)
        {
            _context.Order.Add(order);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var order = _context.Order.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            ViewBag.Employees = _context.Employee.ToList();
            return View(order);
        }
        [HttpPost]
        public IActionResult Edit(Order order)
        {
            _context.Order.Update(order);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var order = _context.Order.Include(o => o.Employee).FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var order = _context.Order.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            _context.Order.Remove(order);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
