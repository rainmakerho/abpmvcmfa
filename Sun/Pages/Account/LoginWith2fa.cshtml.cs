using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Sun.Localization;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Account.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace Sun.Pages.Account;

public class LoginWith2faModel : AbpPageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;

    public LoginWith2faModel(SignInManager<IdentityUser> signInManager)
    {
        LocalizationResourceType = typeof(SunResource);
        _signInManager = signInManager;

    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public void OnGet()
    {
        Input.RememberMe = TempData.ContainsKey("remember_me") && Convert.ToBoolean(TempData["remember_me"]);
    }
    public async Task<IActionResult> OnPostAsync()
    {
        ValidateModel();
        var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

        if (user == null)
        {
            Alerts.Warning(L["UserLogoutOrSessionExpired"]);
            return RedirectToPage("/Login");
        }

        var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(
            Input.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty),
            Input.RememberMe,
            Input.RememberMachine
        );

        if (result.Succeeded)
        {
            return RedirectToPage("/Index");
        }

        if (result.IsLockedOut)
        {

            Alerts.Warning(L["UserLockedOutMessage"]);
            return Page();
        }

        Alerts.Warning(L["VerificationCodeError"]);
        return Page();
    }


    public class InputModel
    {
        [Required]
        [StringLength(7, MinimumLength = 6)]
        public string TwoFactorCode { get; set; }

        public bool RememberMachine { get; set; }

        public bool RememberMe { get; set; }
    }
}