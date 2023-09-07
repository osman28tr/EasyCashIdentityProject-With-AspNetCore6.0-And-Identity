using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
    public class AccountProcessController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
