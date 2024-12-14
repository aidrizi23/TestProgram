using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestProgram.Data;
using TestProgram.Models.Account;

namespace TestProgram.Controllers;

public class AccountController : Controller
{
    
    private readonly UserManager<Teacher> _userManager;
    private readonly SignInManager<Teacher> _signInManager;
    
    public AccountController(UserManager<Teacher> userManager, SignInManager<Teacher> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    // login method
    public IActionResult Login()
    {
        LoginViewModel model = new LoginViewModel();
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
      
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        
        return View(model);
    }
}