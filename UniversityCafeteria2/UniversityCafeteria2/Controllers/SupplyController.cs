using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Day3.Controllers
{
    public class SupplyController : Controller
    {
        public IActionResult Index()
        {
           
            List<string> categories = new List<string> { "Drinks", "Food", "Snacks", "Hot Drinks", "Cold Drinks" };
            ViewBag.Categories = categories;
            return View();
        }

        public IActionResult Products()
        {
            ViewData["Page_Title"] = "Cafeteria Products";
            ViewData["Manager_Name"] = "Ahmad Ali";
            ViewData["Number_of_Products"] = 5;

            string[] arr = { "Water","Orange Juice","Coffee","Sandwich","Chocolate"
};

            ViewData["Products"] = arr;

            return View();
        }

        public IActionResult Suppliers()
        {
            ViewBag.Page_Title = "Our Suppliers";
            ViewBag.Number_of_Suppliers = 3;

            string[] suppliers = { "Fresh Food Company", "Happy Drinks Company", "Daily Supplies Company" };
            ViewBag.Suppliers = suppliers;

            return View();
        }
        [HttpGet]
        public IActionResult CreateRequest()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SubmitRequest()
        {


            string employeeName = Request.Form["EmployeeName"].ToString();
            string productName = Request.Form["ProductName"].ToString();
            string quantity = Request.Form["Quantity"].ToString();
            string requiredDate = Request.Form["RequiredDate"].ToString();

            if (string.IsNullOrWhiteSpace(employeeName))
            {
                ViewBag.Error = "Employee Name cannot be empty!";
                return View();

            }
            ViewBag.EmployeeName = employeeName;
                ViewBag.ProductName = productName;
                ViewBag.Quantity = quantity;
                ViewBag.RequiredDate = requiredDate;

              

            TempData["Message"]  = $"Request Submitted Successfully :),  Employee: {employeeName}, " +
                $"Product: {productName}, Quantity: {quantity}, " +
                $"Required Date: {requiredDate}";

            return RedirectToAction("About");
        }
        public IActionResult About()
        {
            return View();
        }

    }
}
