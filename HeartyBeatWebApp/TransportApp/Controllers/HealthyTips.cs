using Microsoft.AspNetCore.Mvc;

namespace HeartyBeat.Controllers
{
    public class HealthyTips : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
