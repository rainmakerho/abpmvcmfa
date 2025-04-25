using Microsoft.AspNetCore.Identity;
using Volo.Abp.Uow;
using IdentityUser = Volo.Abp.Identity.IdentityUser;


namespace Sun.Middlewares;

public class EnforceMfaMiddleware
{
    private readonly RequestDelegate _next;

    public EnforceMfaMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var path = context.Request.Path.Value?.ToLower();

            // 排除登入、MFA 設定頁等
            if (!path.StartsWith("/account/login") &&
                !path.StartsWith("/account/manage") &&
                !path.StartsWith("/abp") &&
                !path.StartsWith("/api") &&
                !path.StartsWith("/account/logout"))
            {
                var userManager = context.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
                var user = await userManager.GetUserAsync(context.User);
                var isMfaEnabled = await userManager.GetTwoFactorEnabledAsync(user);
                var unitOfWorkManager = context.RequestServices.GetRequiredService<IUnitOfWorkManager>();
                var hasAuthenticatorKey = false;
                using (var uow = unitOfWorkManager.Begin(requiresNew: true)) {
                    hasAuthenticatorKey = !string.IsNullOrWhiteSpace(await userManager.GetAuthenticatorKeyAsync(user));
                }
                if (!isMfaEnabled || !hasAuthenticatorKey)
                {
                    context.Response.Redirect("/Account/Manage");
                    return;
                }
            }
        }

        await _next(context);
    }
}

