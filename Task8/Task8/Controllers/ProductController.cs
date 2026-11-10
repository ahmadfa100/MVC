using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task8.Data;
using Task8.Models;

namespace Task8.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserFromIdentity> _userManager;

        public ProductController(ApplicationDbContext context, UserManager<UserFromIdentity> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> isAdmin()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                return false;
            }
            return await _userManager.IsInRoleAsync(user, "Admin") ||
                await _userManager.IsInRoleAsync(user, "SuperAdmin");
        }
        public async Task<IActionResult> Index()
        {
            var products = await _context.products.ToListAsync();
            return View(products);
        }
        public IActionResult Details(int id)
        {
            var product = _context.products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (!await isAdmin())
                return Forbid(); //403 , which means you have no previlge 

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (!await isAdmin())
                return Forbid();

           
            _context.products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!await isAdmin())
                return Forbid();

            var product = _context.products.Find(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (!await isAdmin())
                return Forbid();


            var existing = _context.products.FirstOrDefault(p => p.Id == id);

            if (existing == null)
                return NotFound();

            existing.Name = product.Name;
            existing.Description = product.Description;
            existing.Price = product.Price;
            existing.Quantity = product.Quantity;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await isAdmin())
                return Forbid();

            var product = _context.products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            if (_context.requestItems.Any(x => x.productId == id))
                return RedirectToAction("Index");

            _context.products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
