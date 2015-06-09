using Microsoft.AspNet.Mvc;

namespace MvcSample.Web
{
    public class HomeController : Controller
    {
        public IActionResult SendLinks()
        {
            return View();
        }

    }
}