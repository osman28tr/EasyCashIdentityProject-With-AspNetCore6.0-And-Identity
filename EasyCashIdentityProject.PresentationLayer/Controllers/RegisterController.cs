using EasyCashIdentityProject.DTO.Dtos.AppUserDtos;
using EasyCashIdentityProject.EntityLayer.Concrete;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(AppUserCreateDto appUserCreateDto)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                string code;
                code = random.Next(100000, 1000000).ToString();
                AppUser appUser = new AppUser()
                {
                    UserName = appUserCreateDto.Username,
                    Name = appUserCreateDto.Name,
                    Surname = appUserCreateDto.Surname,
                    Email = appUserCreateDto.Mail,
                    City = "asd",
                    District = "bbb",
                    ImageUrl = "ccc",
                    ConfirmCode = code
                };
                var result = await _userManager.CreateAsync(appUser, appUserCreateDto.Password);
                if (result.Succeeded)
                {
                    MimeMessage mimeMessage = new MimeMessage();
                    MailboxAddress mailboxAddressFrom = new MailboxAddress("Easy Cash Admin", "test12328t@gmail.com");
                    MailboxAddress mailboxAddressTo = new MailboxAddress("User", appUser.Email);

                    mimeMessage.From.Add(mailboxAddressFrom);
                    mimeMessage.To.Add(mailboxAddressTo);

                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = "Kayıt işlemini gerçekleştirmek için onay kodunuz:" + code;
                    mimeMessage.Body = bodyBuilder.ToMessageBody();

                    mimeMessage.Subject = "Easy Cash Onay Kodu";

                    SmtpClient client = new SmtpClient();
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("test12328t@gmail.com", "tdpbimjqzczhlgvc");
                    client.Send(mimeMessage);
                    client.Disconnect(true);
                    TempData["Mail"] = appUserCreateDto.Mail;
                    return RedirectToAction("Index", "Mail");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View();
        }
    }
}
