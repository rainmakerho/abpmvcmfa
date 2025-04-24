using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Sun.Localization;
using Sun.Pages.Account.Components.ProfileManagementGroup.TwoFactorAuthentication;
using System.Threading.Tasks;
using Volo.Abp.Account.Localization;
using Volo.Abp.Account.Web.Pages.Account.Components.ProfileManagementGroup.Password;
using Volo.Abp.Account.Web.Pages.Account.Components.ProfileManagementGroup.PersonalInfo;
using Volo.Abp.Account.Web.ProfileManagement;
using Volo.Abp.Identity;
using Volo.Abp.Users;

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
