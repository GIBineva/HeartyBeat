using Microsoft.AspNetCore.Mvc;

namespace HeartyBeat.Controllers
{
    public class HealthyTipsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
