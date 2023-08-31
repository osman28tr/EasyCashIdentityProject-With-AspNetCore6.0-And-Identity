using EasyCashIdentityProject.PresentationLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
	public class MailController : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index(int id)
		{
			var mail = TempData["Mail"];
			return View();
		}
		[HttpPost]
		public IActionResult Index(ConfirmMailViewModel confirmMailViewModel)
		{
			return View();
		}
	}
}
