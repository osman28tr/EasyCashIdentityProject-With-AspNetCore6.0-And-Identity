using EasyCashIdentityProject.BusinessLayer.Abstract;
using EasyCashIdentityProject.DataAccessLayer.Concrete;
using EasyCashIdentityProject.DTO.Dtos.CustomerAccountProcessDtos;
using EasyCashIdentityProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
	public class MoneysController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly ICustomerAccountProcessService _customerAccountProcessService;

        public MoneysController(UserManager<AppUser> userManager, ICustomerAccountProcessService customerAccountProcessService)
        {
            _userManager = userManager;
            _customerAccountProcessService = customerAccountProcessService;
        }

        [HttpGet]
		public IActionResult SendMoney(string mycurrency)
		{
			ViewBag.currency = mycurrency;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SendMoney(SendMoneyDto sendMoneyDto)
		{
			var context = new Context();
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			var receiverAccountNumberId = context.CustomerAccounts.Where(x=>x.CustomerAccountNumber == sendMoneyDto.ReceiverAccountNumber).Select(y=>y.CustomerAccountID).FirstOrDefault();

			var senderAccountNumberId = context.CustomerAccounts.Where(x => x.AppUserId == user.Id).Where(y => y.CustomerAccountCurrency == "Türk Lirası").Select(z => z.CustomerAccountID).FirstOrDefault();

			var values = new CustomerAccountProcess
			{
				ProcessDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
				SenderId = senderAccountNumberId,
				ProcessType = "Havale",
				ReceiverId = receiverAccountNumberId,
				Amount = sendMoneyDto.Amount,
				Description = sendMoneyDto.Description
			};
			_customerAccountProcessService.TInsert(values);
			return RedirectToAction("Index", "Deneme");
		}
	}
}
