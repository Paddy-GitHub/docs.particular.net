using Microsoft.AspNet.Mvc;

namespace MvcSample.Web
{
    public class SendAndBlockController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Title = "SendAndBlock";
            return View();
        }

        [HttpPost]
        public ActionResult Index(string textField)
        {
            ViewBag.Title = "SendAndBlock";
            return View();
        }
    }
}