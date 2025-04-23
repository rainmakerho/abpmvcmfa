using Microsoft.Extensions.Localization;
using Sun.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Sun;

[Dependency(ReplaceServices = true)]
public class SunBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<SunResource> _localizer;

    public SunBrandingProvider(IStringLocalizer<SunResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}