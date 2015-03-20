using System.Web.Mvc;

namespace RiseOfVikings.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}