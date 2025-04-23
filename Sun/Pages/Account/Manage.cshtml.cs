using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Volo.Abp.Account.Web.Pages.Account;
using Volo.Abp.Account.Web.ProfileManagement;

namespace Sun.Pages.Account
{
    public class CustomManageModel : ManageModel
    {
        public CustomManageModel(IOptions<ProfileManagementPageOptions> options) : base(options)
        {
        }
 
    }
}
