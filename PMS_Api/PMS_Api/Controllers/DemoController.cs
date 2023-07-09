using Microsoft.AspNetCore.Mvc;

namespace PMS_Api.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
