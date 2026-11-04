
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UniversityCafeteria2.Models;

namespace UniversityCafeteria2.Controllers
{
    public class SupplyRequestController : Controller
    {
        private static List<ConfirmedRequest> requests = new();
        private static int id2 = 1;

        private List<Product> GetDraft()
        {
            string json = HttpContext.Session.GetString("Products") ?? "[]";

            return JsonSerializer.Deserialize<List<Product>>(json)
                   ?? new List<Product>();
        }

        public IActionResult Index()
        {
            ViewBag.Priority =
                Request.Cookies["PreferredPriority"] ?? "Normal";

            return View();
        }

        [HttpPost]
        public IActionResult AddItem(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.ProductName))
            {
                return BadRequest("Product name is required");
            }

            if (product.Quantity <= 0 || product.UnitPrice <= 0)
            {
                return BadRequest("Quantity and price must be greater than zero");
            }

            List<Product> products = GetDraft();

            product.TotalCost = product.Quantity * product.UnitPrice;

            products.Add(product);

            HttpContext.Session.SetString(
                "Products",
                JsonSerializer.Serialize(products)
            );

            return RedirectToAction("Index");
        }

        private ConfirmedRequest CalculateRequest(List<Product> products)
        {
            ConfirmedRequest request = new ConfirmedRequest();

            request.Products = products;
            request.ProductCount = products.Count;

            decimal subtotal = 0;
            string priority = "Normal";

            foreach (Product product in products)
            {
                product.TotalCost = product.Quantity * product.UnitPrice;

                subtotal += product.TotalCost;

                if (product.Priority == "Emergency")
                {
                    priority = "Emergency";
                }
                else if (product.Priority == "Urgent" &&
                         priority != "Emergency")
                {
                    priority = "Urgent";
                }
            }

            decimal preparationFee = 0;

            if (products.Count > 0)
            {
                switch (priority)
                {
                    case "Normal":
                        preparationFee = 2;
                        break;

                    case "Urgent":
                        preparationFee = 5;
                        break;

                    case "Emergency":
                        preparationFee = 10;
                        break;
                }
            }

            decimal discount = 0;

            if (subtotal > 200)
            {
                discount = subtotal * 0.15m;
            }
            else if (subtotal > 100)
            {
                discount = subtotal * 0.10m;
            }

            decimal finalTotal = subtotal + preparationFee - discount;

            request.Priority = priority;
            request.Subtotal = subtotal;
            request.PreparationFee = preparationFee;
            request.Discount = discount;
            request.FinalTotal = finalTotal;

            return request;
        }

        public IActionResult Review()
        {
            List<Product> products = GetDraft();

            ConfirmedRequest request = CalculateRequest(products);

            return View(request);
        }

        [HttpPost]
        public IActionResult ConfirmRequest()
        {
            List<Product> products = GetDraft();

            if (products.Count == 0)
            {
                TempData["Error"] = "Cannot confirm an empty request.";

                return RedirectToAction("Review");
            }

            ConfirmedRequest request = CalculateRequest(products);

            request.RequestId = id2++;

            request.Status = "Confirmed";

            requests.Add(request);

            HttpContext.Session.Remove("Products");

            TempData["Message"] = "Request confirmed successfully.";

            return RedirectToAction("MyRequests");
        }

        [HttpPost]
        public IActionResult ClearDraft()
        {
            HttpContext.Session.Remove("Products");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SavePreference(string priority)
        {
            if (priority != "Normal" &&
                priority != "Urgent" &&
                priority != "Emergency")
            {
                return BadRequest("Invalid priority");
            }

            Response.Cookies.Append("PreferredPriority", priority,
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                });

            TempData["Message"] =
                $"Your preferred request priority is {priority}.";

            return RedirectToAction("Index");
        }

        public IActionResult MyRequests()
        {
            return View(requests);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
