using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp.Account.Web.Pages.Account.Components.ProfileManagementGroup.PersonalInfo;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;

namespace Sun.Pages.Account.Components.ProfileManagementGroup.TwoFactorAuthentication;

public class AccountProfileTwoFactorAuthenticationManagementGroupViewComponent : AccountProfilePersonalInfoManagementGroupViewComponent
{
    public AccountProfileTwoFactorAuthenticationManagementGroupViewComponent(IProfileAppService profileAppService)
        : base(profileAppService)
    {
    }
}
