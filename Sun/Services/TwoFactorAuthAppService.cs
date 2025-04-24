using Microsoft.AspNetCore.Identity;
using Volo.Abp;

namespace Sun.Services;

public class TwoFactorAuthAppService : SunAppService
{
    private readonly UserManager<Volo.Abp.Identity.IdentityUser> _userManager;

    public TwoFactorAuthAppService(UserManager<Volo.Abp.Identity.IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task EnableTwoFactorAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            throw new UserFriendlyException("用戶不存在");
        }

        // Use the UserManager's SetTwoFactorEnabledAsync method to enable two-factor authentication
        var result = await _userManager.SetTwoFactorEnabledAsync(user, true);
        if (!result.Succeeded)
        {
            throw new UserFriendlyException("無法啟用雙重身份驗證");
        }
    }
}
