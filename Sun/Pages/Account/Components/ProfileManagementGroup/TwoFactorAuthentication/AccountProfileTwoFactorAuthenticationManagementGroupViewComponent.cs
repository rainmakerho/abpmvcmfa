using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;
using Volo.Abp.Users;
using IdentityUser = Volo.Abp.Identity.IdentityUser;


namespace Sun.Pages.Account.Components.ProfileManagementGroup.TwoFactorAuthentication;

public class AccountProfileTwoFactorAuthenticationManagementGroupViewComponent : AbpViewComponent
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ICurrentUser _currentUser;
    private readonly IConfiguration _configuration;
    public AccountProfileTwoFactorAuthenticationManagementGroupViewComponent(
        UserManager<IdentityUser> userManager,
        ICurrentUser currentUser,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _currentUser = currentUser;
        _configuration = configuration;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        
        var user = await _userManager.FindByEmailAsync(_currentUser.Email);

        var key = await _userManager.GetAuthenticatorKeyAsync(user);
        if (string.IsNullOrEmpty(key))
        {
            await _userManager.ResetAuthenticatorKeyAsync(user);
            key = await _userManager.GetAuthenticatorKeyAsync(user);
        }

        var appName = _configuration["App:Name"] ?? "AbpApp";
        var otpauthUri = $"otpauth://totp/{appName}:{user.Email}?secret={key}&issuer={appName}&digits=6";

        var qrGenerator = new QRCodeGenerator();
        var qrCodeData = qrGenerator.CreateQrCode(otpauthUri, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new PngByteQRCode(qrCodeData);
        var qrCodeBytes = qrCode.GetGraphic(10);
        var base64 = Convert.ToBase64String(qrCodeBytes);

        var base64Image = $"data:image/png;base64,{base64}";

        return View("~/Pages/Account/Components/ProfileManagementGroup/TwoFactorAuthentication/Default.cshtml", new TwoFactorAuthModel
        {
            Key = key,
            OtpUri = otpauthUri,
            IsEnabled = user.TwoFactorEnabled,
            QrCodeBase64 = base64Image
        });
    }

    public class TwoFactorAuthModel
    {
        public string Key { get; set; }
        public string OtpUri { get; set; }
        public bool IsEnabled { get; set; }
        public string QrCodeBase64 { get; set; }
    }
}
