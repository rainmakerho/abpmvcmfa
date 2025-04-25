using Volo.Abp.Application.Services;
using Sun.Localization;

namespace Sun.Services;

/* Inherit your application services from this class. */
public abstract class SunAppService : ApplicationService
{
    protected SunAppService()
    {
        LocalizationResource = typeof(SunResource);
        
    }
}