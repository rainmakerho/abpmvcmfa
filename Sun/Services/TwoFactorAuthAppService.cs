using Microsoft.AspNetCore.Identity;
using Volo.Abp;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace Sun.Services;

public class TwoFactorAuthAppService : SunAppService
{
    private readonly UserManager<Volo.Abp.Identity.IdentityUser> _userManager;

    public TwoFactorAuthAppService(UserManager<Volo.Abp.Identity.IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    private Task<IdentityUser> GetUser()
    {
        return _userManager.FindByEmailAsync(CurrentUser.Email);
    }

    public async Task<bool> TwoFactorTokenStatus()
    {
        var user = await GetUser();
        return user.TwoFactorEnabled;
    }

    public async Task EnableTwoFactorAsync(bool isEnable)
    {
        var user = await GetUser();
        if (user == null)
        {
            throw new UserFriendlyException(L["UserNotExist"]);
        }

        // Use the UserManager's SetTwoFactorEnabledAsync method to enable two-factor authentication
        var result = await _userManager.SetTwoFactorEnabledAsync(user, isEnable);
        if (!result.Succeeded)
        {
            throw new UserFriendlyException(L["EnableMfaFail", string.Join(",", result.Errors.Select(err => err.ToString()))]);
        }
        await _userManager.UpdateAsync(user);
    }

    public async Task<bool> VerifyTwoFactorToken(string code)
    {

        var user = await GetUser();
        if (user == null)
        {
            throw new UserFriendlyException(L["UserNotExist"]);
        }

        var isValid = await _userManager.VerifyTwoFactorTokenAsync(
            user, TokenOptions.DefaultAuthenticatorProvider, code);
        if (!isValid)
        {
            throw new UserFriendlyException(L["VerificationCodeError"]);
        }

        await EnableTwoFactorAsync(true);
        return isValid;
    }
}