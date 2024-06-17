using Microsoft.AspNetCore.Mvc;

namespace HeartyBeat.Controllers
{
    public class TrackerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
