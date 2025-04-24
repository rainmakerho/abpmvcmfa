using Microsoft.AspNetCore.Identity;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Sun.Pages;

public class IndexModel : AbpPageModel
{
    private readonly UserManager<Volo.Abp.Identity.IdentityUser> _userManager;
    public IndexModel(UserManager<Volo.Abp.Identity.IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public List<string> TokenProviders { get; set; }

    public void OnGet()
    {
        TokenProviders = _userManager.Options.Tokens.ProviderMap.Keys.ToList();
    }

}