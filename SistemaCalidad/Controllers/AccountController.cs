#region Using

using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EnviarCorreo;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NumberGenerate;
using SendMails.methods;
using ElGasSeguimientoWeb.Data;
using ElGasSeguimientoWeb.Extensions;
using ElGasSeguimientoWeb.Models;
using ElGasSeguimientoWeb.Services;
using ElGasSeguimientoWeb.Utils;

#endregion

namespace ElGasSeguimientoWeb.Controllers
{
    [Authorize(Policy ="Administracion")]
    [Route("[controller]/[action]")]
    [Layout("_AuthLayout")]
    public class AccountController : Controller
    {
        private readonly IEmailSender _emailSender;
        public IConfiguration Configuration { get; }
        private readonly ILogger _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext db;

        [TempData]
        public string ErrorMessage { get; set; }

        public AccountController(IConfiguration configuration, ApplicationDbContext context,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            db = context;
            Configuration = configuration;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            // Something failed, redisplay form
            if (!ModelState.IsValid)
                return View(model);

            // Check if the user exists in the data store
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user!=null)
            {
                var password = PasswordUtil.Password + GenerateNumber.Generate().ToString();
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, password);
                user.EmailConfirmed = false;
                await _userManager.UpdateAsync(user);
                var mensaje = "Recuperar contraseña"
                               + "\n \n Hola Señor(a): " + user.Name + " " + user.LastName
                               + "\n \n Le informamos que se ha reseteado su contraseña."
                               + "\n \n Nuevas credenciales de ingreso al sistema."
                               + "\n \n Usuario:  " + user.Email
                               + "\n \n Contraseña temporal: " + password
                               + "\n \n Click en el siguiente enlace para acceder al sistema" + "\n \n"
                               + Configuration.GetSection("EmailLink").Value
                               + Configuration.GetSection("EmailFooter").Value;

                Mail mail = new Mail
                {

                    Body = mensaje
                                     ,
                    EmailTo = user.Email
                                     ,
                    NameTo = "Name To"
                                     ,
                    Subject = "Recuperar contraseña",
                };

                //execute the method Send Mail or SendMailAsync
                var a = await Emails.SendEmailAsync(mail);
                return RedirectToAction(nameof(ForgotPasswordConfirmation));

            }
            ModelState.AddModelError(string.Empty, "El correo electrónico ingresado no existe.");
            return View(model);

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true

                var user= await db.Users.Where(x => x.Email == model.Email).FirstOrDefaultAsync();
                if (user!=null)
                {

                var usurio=await  _userManager.FindByEmailAsync(model.Email);

                 var existe=await  _signInManager.CheckPasswordSignInAsync(usurio, model.Password,lockoutOnFailure:false);
                if (existe.Succeeded)
                {
                        var IsConfirmed =await db.Users.Where(x => x.EmailConfirmed == false && x.Email==model.Email).FirstOrDefaultAsync();
                        if (IsConfirmed!=null)
                        {
                            return RedirectToAction(nameof(AccountController.ConfirmAccount), "Account",new { email=usurio.Email});
                        }

                }
                
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                     
                    _logger.LogInformation("User logged in.");
                    return RedirectToLocal(returnUrl);
                }
               
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToAction(nameof(Lockout));
                }
                }
                ModelState.AddModelError(string.Empty, "Intento de inicio de sesión no válido.");
                return View(model);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]

        public IActionResult ConfirmAccount(string email,string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new ConfirmAccount {Email=email});
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmAccount( ConfirmAccount confirmAccount)
        {
            if (ModelState.IsValid)
            {
                var user =await _userManager.FindByEmailAsync(confirmAccount.Email);
                if (user!=null)
                {
                   var changePassword= await _userManager.ChangePasswordAsync(user, confirmAccount.CurrentPassword, confirmAccount.Password);

                    if (changePassword.Succeeded)
                    {
                        var CurrentUser = await _userManager.FindByEmailAsync(confirmAccount.Email);
                        CurrentUser.EmailConfirmed = true;
                        await _userManager.UpdateAsync(CurrentUser);
                        var result = await _signInManager.PasswordSignInAsync(confirmAccount.Email, confirmAccount.Password, true, lockoutOnFailure: false);
                        if (result.Succeeded)
                        {
                            _logger.LogInformation("User logged in.");
                            this.TempData["Mensaje"] = $"{Mensaje.MensajeSatisfactorio}|{"Bienvenido"}";
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                    }
                    ModelState.AddModelError(string.Empty, "Intento de inicio de sesión no válido.");
                    return View(confirmAccount);

                }
                ModelState.AddModelError(string.Empty, "Intento de inicio de sesión no válido.");
                return View(confirmAccount);

            }
            ModelState.AddModelError(string.Empty, "Intento de inicio de sesión no válido.");
            return View(confirmAccount);
        }



        public async Task<IActionResult> Index()
        {
          
            try
            {
               var lista = await db.Users.ToListAsync();
               return View(lista);
            }
            catch (Exception)
            {
                TempData["Mensaje"] = $"{Mensaje.Error}|{Mensaje.ErrorListado}";
                return View();
            }
           
        }

       

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
       
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                this.TempData["Mensaje"] = $"{Mensaje.MensajeSatisfactorio}|{"Bienvenido"}";
                return Redirect(returnUrl);
            }
            this.TempData["Mensaje"] = $"{Mensaje.MensajeSatisfactorio}|{"Bienvenido"}";
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
