using System.Web.Mvc;

namespace AjdemeSi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        { 
            if (User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home", new { area = "Administration" });
            else if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Driver"))
                    return RedirectToAction("Routes", "Index");
                else
                    return RedirectToAction("Index", "Ride");
            }
            else
                return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}