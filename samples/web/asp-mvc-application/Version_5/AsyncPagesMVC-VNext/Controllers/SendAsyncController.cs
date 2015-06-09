using Microsoft.AspNet.Mvc;

namespace MvcSample.Web
{
    public class SendAsyncController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Title = "SendAsync";
            return View();
        }

        [HttpPost]
        public ActionResult Index(string textField)
        {
            ViewBag.Title = "SendAsync";
            return View();
        }

    }
}