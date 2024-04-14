#region Using

using System;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using WebApp.Models;
using WebApp.Services;

#endregion

namespace WebApp.Controllers
{
  [Authorize]
  public class AccountController : Controller
  {
    // TODO: This should be moved to the constructor of the controller in combination with a DependencyResolver setup
    // NOTE: You can use NuGet to find a strategy for the various IoC packages out there (i.e. StructureMap.MVC5)
    //private readonly UserManager _manager = UserManager.Create();
    private ApplicationUserManager _userManager;
    public ApplicationUserManager UserManager
        {
            get => this._userManager ?? this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => this._userManager = value;
        }
    private ApplicationSignInManager _signInManager;

    public ApplicationSignInManager SignInManager
        {
            get => this._signInManager ?? this.HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            private set => this._signInManager = value;
        }
    private readonly ICompanyService _companyService;
    public AccountController(ICompanyService companyService) => this._companyService = companyService;

    // GET: /account/forgotpassword
    [AllowAnonymous]
    public ActionResult ForgotPassword()
    {
      // We do not want to use any existing identity information
      this.EnsureLoggedOut();

      return this.View();
    }

    // GET: /account/login
    [AllowAnonymous]

    public ActionResult Login(string returnUrl)
    {
      // We do not want to use any existing identity information
      this.EnsureLoggedOut();

      // Store the originating URL so we can attach it to a form field

      return this.View(new AccountLoginModel { ReturnUrl = returnUrl });
    }

    // POST: /account/login
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Login(AccountLoginModel viewModel)
    {
      // Ensure we have a valid viewModel to work with
      if (!this.ModelState.IsValid)
      {
        return this.View(viewModel);
      }

      // Verify if a user exists with the provided identity information

      // If a user was found
      if (await this.UserManager.FindByEmailAsync(viewModel.Email) != null)
      {
        switch (await this.SignInManager.PasswordSignInAsync(( await this.UserManager.FindByEmailAsync(viewModel.Email) ).UserName, viewModel.Password, viewModel.RememberMe, shouldLockout: false))
        {
          case SignInStatus.Success:
            // Then create an identity for it and sign it in
            await this.SignInAsync(await this.UserManager.FindByEmailAsync(viewModel.Email), viewModel.RememberMe);
            // If the user came from a specific page, redirect back to it
            return this.RedirectToLocal(viewModel.ReturnUrl);
          case SignInStatus.LockedOut:
            return this.View("Lockout");
          case SignInStatus.RequiresVerification:
          case SignInStatus.Failure:
          default:
            this.ModelState.AddModelError("", "Invalid login attempt.");
            return this.View(viewModel);
        }
      }

      // No existing user was found that matched the given criteria
      this.ModelState.AddModelError("", "Invalid username or password.");

      // If we got this far, something failed, redisplay form
      return this.View(viewModel);
    }

    // GET: /account/error
    [AllowAnonymous]
    public ActionResult Error()
    {
      // We do not want to use any existing identity information
      this.EnsureLoggedOut();

      return this.View();
    }

    // GET: /account/register
    [AllowAnonymous]

    public ActionResult Register()
    {
      // We do not want to use any existing identity information
      this.EnsureLoggedOut();
      var data = this._companyService.Queryable().Select(x => new ListItem() { Value = x.Id.ToString(), Text = x.Name });

      this.ViewBag.companylist = data;
      var model = new AccountRegistrationModel
      {
        CompanyCode = data.FirstOrDefault() != null ? data.FirstOrDefault().Value : "",
        CompanyName = data.FirstOrDefault() != null ? data.FirstOrDefault().Text : ""
      };

      return this.View(model);
    }

    // POST: /account/register
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Register(AccountRegistrationModel viewModel)
    {
      var data = this._companyService.Queryable().Select(x => new ListItem() { Value = x.Id.ToString(), Text = x.Name });
      this.ViewBag.companylist = data;

      // Ensure we have a valid viewModel to work with
      if (!this.ModelState.IsValid)
      {
        return this.View(viewModel);
      }

      // Try to create a user with the given identity
      try
      {
        // Prepare the identity with the provided information
        var user = new ApplicationUser
        {
          UserName = viewModel.Username,
          FullName = viewModel.Lastname + "." + viewModel.Firstname,
          CompanyCode = viewModel.CompanyCode,
          CompanyName = viewModel.CompanyName,
          Email = viewModel.Email,
          AccountType = 0
        };
        var result = await this.UserManager.CreateAsync(user, viewModel.Password);

        // If the user could not be created
        if (!result.Succeeded)
        {
          // Add all errors to the page so they can be used to display what went wrong
          this.AddErrors(result);

          return this.View(viewModel);
        }

        // If the user was able to be created we can sign it in immediately
        // Note: Consider using the email verification proces
        await this.SignInAsync(user, true);

        return this.RedirectToLocal();
      }
      catch (DbEntityValidationException ex)
      {
        // Add all errors to the page so they can be used to display what went wrong
        this.AddErrors(ex);

        return this.View(viewModel);
      }
    }

    // POST: /account/Logout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Logout()
    {
      // First we clean the authentication ticket like always
      FormsAuthentication.SignOut();
      this.SignInManager.SignOut();
      // Second we clear the principal to ensure the user does not retain any authentication
      this.HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

      // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
      // this clears the Request.IsAuthenticated flag since this triggers a new request
      return this.RedirectToLocal();
    }
    public new ActionResult Profile => this.View();
    private ActionResult RedirectToLocal(string returnUrl = "")
    {
      // If the return url starts with a slash "/" we assume it belongs to our site
      // so we will redirect to this "action"
      if (!returnUrl.IsNullOrWhiteSpace() && this.Url.IsLocalUrl(returnUrl))
      {
        return this.Redirect(returnUrl);
      }

      // If we cannot verify if the url is local to our host we redirect to a default location
      return this.RedirectToAction("index", "home");
    }

    private void AddErrors(DbEntityValidationException exc)
    {
      foreach (var error in exc.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors.Select(validationError => validationError.ErrorMessage)))
      {
        this.ModelState.AddModelError("", error);
      }
    }

    private void AddErrors(IdentityResult result)
    {
      // Add all errors that were returned to the page error collection
      foreach (var error in result.Errors)
      {
        this.ModelState.AddModelError("", error);
      }
    }

    private void EnsureLoggedOut()
    {
      // If the request is (still) marked as authenticated we send the user to the logout action
      if (this.Request.IsAuthenticated)
        this.Logout();
    }
    private async Task SignInAsync(ApplicationUser user, bool isPersistent) =>
        // Clear any lingering authencation data
        //FormsAuthentication.SignOut();
        //this.SignInManager.SignOut();

        // Create a claims based identity for the current user

        // Write the authentication cookie
        FormsAuthentication.SetAuthCookie(( await this.SignInManager.CreateUserIdentityAsync(user) ).Name, isPersistent);
    [HttpGet]
    public ActionResult SetCulture(string lang)
    {
      switch (lang.Trim())
      {
        case "en":
          CultureInfo.CurrentCulture = new CultureInfo("en-US");
          CultureInfo.CurrentUICulture = new CultureInfo("en-US");
          break;
        case "cn":
          CultureInfo.CurrentCulture = new CultureInfo("zh-CN");
          CultureInfo.CurrentUICulture = new CultureInfo("zh-CN");
          break;
        case "tw":
          CultureInfo.CurrentCulture = new CultureInfo("zh-TW");
          CultureInfo.CurrentUICulture = new CultureInfo("zh-TW");
          break;
      }

      var cookie = new HttpCookie("culture", lang)
      {
        Expires = DateTime.Now.AddYears(1)
      };
      this.Response.Cookies.Add(cookie);
      return this.Json(new { success = true }, JsonRequestBehavior.AllowGet);
    }

    // GET: /account/lock
    [AllowAnonymous]
    public ActionResult Lock() => this.View();
  }
}