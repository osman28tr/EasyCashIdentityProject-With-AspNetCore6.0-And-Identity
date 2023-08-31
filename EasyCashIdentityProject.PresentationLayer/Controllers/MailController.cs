using EasyCashIdentityProject.EntityLayer.Concrete;
using EasyCashIdentityProject.PresentationLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
	public class MailController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		public MailController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}
		[HttpGet]
		public IActionResult Index()
		{
			var mail = TempData["Mail"];
			ViewBag.v = mail;
			//confirmMailViewModel.Mail = mail.ToString();
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Index(ConfirmMailViewModel confirmMailViewModel)
		{
			var user = await _userManager.FindByEmailAsync(confirmMailViewModel.Mail);
			if(user.ConfirmCode == confirmMailViewModel.ConfirmCode)
			{
				return RedirectToAction("Index", "Profile");
			}
			return View();
		}
	}
}
