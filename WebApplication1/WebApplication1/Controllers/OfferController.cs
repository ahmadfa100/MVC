using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class OfferController : Controller
    {
        static List<NewOffer> offers = List<NewOffer>{
            new NewOffer{ Id=1, Category="Electronics", Supply="In Stock",
                OfferName="Smartphone", Quantity=50,
                UnitPrice=299.99
        }
    }
};
        public IActionResult Index()
        {

            return View();
        }
    }
}
