console.log("2FA JS loaded"); // 在 Default.js 開頭加這行

(function ($) {
    $(function () {
        var l = abp.localization.getResource("AbpAccount");

        $('#TwoFactorAuthForm').submit(function (e) {
            e.preventDefault();
            alert('submit'); 
            if (!$('#TwoFactorAuthForm').valid()) {
                return false;
            }

            var input = $('#TwoFactorAuthForm').serializeFormToObject(false);
            abp.notify.success(l('PersonalSettingsSaved'));
            //volo.abp.account.profile.update(input).then(function (result) {
            //    abp.notify.success(l('PersonalSettingsSaved'));
            //    updateConcurrencyStamp();
            //});
        });
    });

   
})(jQuery);