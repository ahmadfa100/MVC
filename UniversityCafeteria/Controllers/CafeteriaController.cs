using Microsoft.AspNetCore.Mvc;

namespace UniversityCafeteria.Controllers
{
    public class CafeteriaController : Controller
    {
        // GET: CafeteriaController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Products()
        {
            return View();
        }
        public ActionResult Suppliers()
        {
            return View();
        }
        public ActionResult SupplyStatus()
        {
            return View();
        }

    }
}
