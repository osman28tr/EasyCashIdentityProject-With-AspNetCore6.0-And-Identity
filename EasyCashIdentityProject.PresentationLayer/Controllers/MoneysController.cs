using AutoMapper;
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
		private readonly IMapper _mapper;

        public MoneysController(UserManager<AppUser> userManager, ICustomerAccountProcessService customerAccountProcessService,IMapper mapper)
        {
            _userManager = userManager;
            _customerAccountProcessService = customerAccountProcessService;
			_mapper = mapper;
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

			CustomerAccountProcess customerAccountProcess = _mapper.Map<CustomerAccountProcess>(sendMoneyDto);
	
			customerAccountProcess.ReceiverId = receiverAccountNumberId;
			customerAccountProcess.SenderId = senderAccountNumberId;

			_customerAccountProcessService.TInsert(customerAccountProcess);
			return RedirectToAction("Index", "Deneme");
		}
	}
}
