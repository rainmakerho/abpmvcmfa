using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp.Account.Settings;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.Security.Claims;
using Volo.Abp.Settings;
using Volo.Abp.Validation;
using IdentityUser = Volo.Abp.Identity.IdentityUser;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Sun.Pages.Account;

public class LoginWith2faModel : AbpPageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;

    public LoginWith2faModel(SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();



    public async Task<IActionResult> OnPostAsync()
    {
        ValidateModel();
        var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

        if (user == null)
        {
            Alerts.Warning("使用者已登出或會話過期");
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
            Alerts.Warning("帳號已被鎖定");
            return Page();
        }

        Alerts.Warning("驗證碼錯誤");
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
