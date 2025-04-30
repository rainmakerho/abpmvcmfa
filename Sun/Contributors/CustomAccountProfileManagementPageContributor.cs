
using Microsoft.Extensions.Localization;
using Sun.Localization;
using Sun.Pages.Account.Components.ProfileManagementGroup.TwoFactorAuthentication;
using Volo.Abp.Account.Web.ProfileManagement;

namespace Sun.Contributors;

public class CustomAccountProfileManagementPageContributor : IProfileManagementPageContributor
{
    public async Task ConfigureAsync(ProfileManagementPageCreationContext context)
    {
        var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<SunResource>>();

        context.Groups.Add(
            new ProfileManagementPageGroup(
                "Volo.Abp.Account.TwoFactorAuthentication",
                l["ProfileTab:TwoFactorAuthentication"],
                typeof(AccountProfileTwoFactorAuthenticationManagementGroupViewComponent)
            )
        );
    }
}