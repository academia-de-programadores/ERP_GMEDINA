using ERP_GMEDINA.Attribute;
using System.Web.Mvc;

namespace ERP_GMEDINA.Controllers
{
    public class HomeController : Controller
    {
        [SessionManager("Home/Index")]
        public ActionResult Index()
        {
            return View();
        }
    
        public ActionResult Minor()
        {
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }
    }
}