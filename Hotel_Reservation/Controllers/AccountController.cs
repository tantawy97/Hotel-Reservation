using Hotel_Reservation.Models;
using Hotel_Reservation.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hotel_Reservation.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View(new LoginViewModel());

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(loginViewModel.Email);
                user = (user != null) ? user : await userManager.FindByEmailAsync(loginViewModel.Email);
                if (user != null)
                {
                    var passwordChecked = await userManager.CheckPasswordAsync(user, loginViewModel.Password);
                    if (passwordChecked)
                    {
                        var response = await signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                        if (response.Succeeded)
                        {
                           
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            TempData["Error"] = "Failed..!!";
            return View(loginViewModel);
        }



        public IActionResult Register()
        {
            return View(new RegistrationViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationViewModel registrationViewModel)
        {
            if (ModelState.IsValid)
            {
                var userByEmail = await userManager.FindByEmailAsync(registrationViewModel.Email);

                if (userByEmail == null)
                {
                    var newUser = new User()
                    {UserName = registrationViewModel.Name,
                    PasswordHash=registrationViewModel.Password,
                    PhoneNumber = registrationViewModel.PhoneNumber,
                    NationalId=registrationViewModel.NationalityId,
                    Nationality=registrationViewModel.Nationality,
                     Email = registrationViewModel.Email                    };
                    var response = await userManager.CreateAsync(newUser, registrationViewModel.Password);
                    if (response.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newUser, "User");
                        await signInManager.SignInAsync(newUser, true);
                    }

                    return RedirectToAction("Index", "Home");
                }

                if (userByEmail != null)
                    TempData["Error"] = "Email Already Exists";
                return View(registrationViewModel);
            }
            TempData["Error"] = "Login Error, Please Try again";
            return View(registrationViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
