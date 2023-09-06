using EasyCashIdentityProject.DTO.Dtos.AppUserDtos;
using EasyCashIdentityProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public AccountsController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            AppUserEditDto appUserEditDto = new AppUserEditDto
            {
                Name = values.Name,
                Surname = values.Surname,
                District = values.District,
                City = values.City,
                Email = values.Email,
                PhoneNumber = values.PhoneNumber,
                ImageUrl = values.ImageUrl,
            };
            return View(appUserEditDto);
        }
        [HttpPost]
        public async Task<IActionResult> Index(AppUserEditDto appUserEditDto)
        {
            if (appUserEditDto.Password == appUserEditDto.ConfirmPassword)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                user.PhoneNumber = appUserEditDto.PhoneNumber;
                user.Surname = appUserEditDto.Surname;
                user.City = appUserEditDto.City;
                user.District = appUserEditDto.District;
                user.Name = appUserEditDto.Name;
                user.ImageUrl = "test";
                user.Email = appUserEditDto.Email;
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, appUserEditDto.Password);
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}
//password: 123456aA*
