using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovementTechnology.ViewModels;
using MovementTechnology.Models;
using Microsoft.AspNetCore.Identity;
using MovementTechnology.Data;

namespace MovementTechnology.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
 
    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }
 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        model.ReturnUrl = Request.Headers.Referer;
        if (ModelState.IsValid)
        {
            var result = _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false).Result;
            if (result.Succeeded)
            {
                // проверяем, принадлежит ли URL приложению
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }
        }
        return View(model);
    }
 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        // удаляем аутентификационные куки
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if(ModelState.IsValid)
        {
            User user = new User { Email = model.Email, UserName = model.Username, LastName = model.LastName, FirstName = model.FirstName, MiddleName = model.MiddleName};
            // добавляем пользователя
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // установка куки
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
        return View(model);
    }
    
    [Authorize]
    public IActionResult Index()
    {
        if (!_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("Login", "Account");
        }
        return View();
    }
}