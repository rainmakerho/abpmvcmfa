using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Volo.Abp.Account.Settings;
using Volo.Abp.Identity;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.Settings;
using Volo.Abp.Account.Web;

namespace Sun.Pages.Account;

public class CustomLoginModel : Volo.Abp.Account.Web.Pages.Account.LoginModel
{
    private readonly IConfiguration _configuration;
    public CustomLoginModel(
        IAuthenticationSchemeProvider schemeProvider,
        IOptions<AbpAccountOptions> accountOptions,
        IOptions<IdentityOptions> identityOptions,
        IdentityDynamicClaimsPrincipalContributorCache identityDynamicClaimsPrincipalContributorCache,
        IConfiguration configuration)
        : base(schemeProvider, accountOptions, identityOptions, identityDynamicClaimsPrincipalContributorCache)
    {
        _configuration = configuration;
    }

    // public override async Task<IActionResult> OnPostAsync(string action)
    // {
    //     await CheckLocalLoginAsync();

    //     ValidateModel();

    //     ExternalProviders = await GetExternalProviders();

    //     EnableLocalLogin = await SettingProvider.IsTrueAsync(AccountSettingNames.EnableLocalLogin);

    //     await ReplaceEmailToUsernameOfInputIfNeeds();

    //     await IdentityOptions.SetAsync();

    //     var result = await SignInManager.PasswordSignInAsync(
    //         LoginInput.UserNameOrEmailAddress,
    //         LoginInput.Password,
    //         LoginInput.RememberMe,
    //         true
    //     );

    //     await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext()
    //     {
    //         Identity = IdentitySecurityLogIdentityConsts.Identity,
    //         Action = result.ToIdentitySecurityLogAction(),
    //         UserName = LoginInput.UserNameOrEmailAddress
    //     });

    //     if (result.RequiresTwoFactor)
    //     {
    //         return await TwoFactorLoginResultAsync();
    //     }

    //     if (result.IsLockedOut)
    //     {
    //         Alerts.Warning(L["UserLockedOutMessage"]);
    //         return Page();
    //     }

    //     if (result.IsNotAllowed)
    //     {
    //         Alerts.Warning(L["LoginIsNotAllowed"]);
    //         return Page();
    //     }

    //     if (!result.Succeeded)
    //     {
    //         Alerts.Danger(L["InvalidUserNameOrPassword"]);
    //         return Page();
    //     }

    //     //TODO: Find a way of getting user's id from the logged in user and do not query it again like that!
    //     var user = await UserManager.FindByNameAsync(LoginInput.UserNameOrEmailAddress) ??
    //                await UserManager.FindByEmailAsync(LoginInput.UserNameOrEmailAddress);

    //     Debug.Assert(user != null, nameof(user) + " != null");

    //     // Clear the dynamic claims cache.
    //     await IdentityDynamicClaimsPrincipalContributorCache.ClearAsync(user.Id, user.TenantId);

    //     if (_configuration.GetValue<bool>("Settings:Abp.Identity.EnableTwoFactorAuthentication"))
    //     {
    //         var hasKey = !string.IsNullOrWhiteSpace(await UserManager.GetAuthenticatorKeyAsync(user));
    //         var mfaEnabled = await UserManager.GetTwoFactorEnabledAsync(user);

    //         if (!hasKey || !mfaEnabled)
    //         {
    //             // 引導使用者進入 MFA 設定流程
    //             return RedirectToPage("/Account/Manage");
    //         }
    //     }

    //     return await RedirectSafelyAsync(ReturnUrl, ReturnUrlHash);
    // }

    protected override Task<IActionResult> TwoFactorLoginResultAsync()
    {
        TempData["remember_me"] = LoginInput.RememberMe;
        return Task.FromResult<IActionResult>(RedirectToPage("./LoginWith2fa"));
    }
}