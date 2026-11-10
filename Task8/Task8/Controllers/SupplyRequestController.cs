using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task8.Data;
using Task8.Models;

namespace Task8.Controllers
{
    [Authorize]
    public class SupplyRequestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserFromIdentity> _userManager;

        public SupplyRequestController(ApplicationDbContext context, UserManager<UserFromIdentity> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> isAdmin()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return false;

            return await _userManager.IsInRoleAsync(user, "Admin") ||
                   await _userManager.IsInRoleAsync(user, "SuperAdmin");
        }

        public async Task<IActionResult> Index()
        {
            if (await isAdmin())
            {
                var requests = _context.supllyRequests.ToList();
                return View(requests);
            }

            var userId = _userManager.GetUserId(User);

            var myRequests = _context.supllyRequests
                .Where(r => r.UserId == userId)
                .ToList();

            return View(myRequests);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var products = _context.products.ToList();
            return View(products);
        }

        [HttpPost]
        public IActionResult Create(List<RequestItem> items)
        {
            var request = new SupplyRequest
            {
                RequestDate = DateTime.Now,
                Status = StatusValues.Pending,
                UserId = _userManager.GetUserId(User),
                Items = new List<RequestItem>()
            };

            foreach (var item in items) { 

                if (item.Quantity == 0)
                    continue;

                var product = _context.products.Find(item.productId);

                

                request.Items.Add(new RequestItem
                {
                    productId = product.Id,
                    Quantity = item.Quantity,
                    Price = (double)product.Price
                });

                request.TotalPrice += product.Price * (decimal)item.Quantity;
            }

            

            _context.supllyRequests.Add(request);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var request = _context.supllyRequests
                .Include(r => r.User)
                .Include(r => r.Items)
                .ThenInclude(i => i.product)
                .FirstOrDefault(r => r.Id == id);

            if (request == null)
                return NotFound();

            if (!await isAdmin() &&
                request.UserId != _userManager.GetUserId(User))
                return Forbid();

            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!await isAdmin())
                return Forbid();

            var request = _context.supllyRequests.Find(id);

            if (request == null)
                return NotFound();

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, StatusValues status)
        {
            if (!await isAdmin())
                return Forbid();

            var request = _context.supllyRequests.Find(id);

            if (request == null)
                return NotFound();

            if (!Enum.IsDefined(status))
                return BadRequest();

            request.Status = status;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await isAdmin())
                return Forbid();

            var request = _context.supllyRequests.Find(id);

            if (request == null)
                return NotFound();

            var items = _context.requestItems
                .Where(i => i.supplyRequestId == id)
                .ToList();

            _context.requestItems.RemoveRange(items);
            _context.supllyRequests.Remove(request);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}