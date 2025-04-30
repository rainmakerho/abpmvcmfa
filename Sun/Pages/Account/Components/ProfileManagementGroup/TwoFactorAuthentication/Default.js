(function ($) {
  $(function () {
    var l = abp.localization.getResource("Sun");
    reloadTwoFactorAuthentication();
    $("#TwoFactorAuthForm").submit(function (e) {
      e.preventDefault();
      if (!$("#TwoFactorAuthForm").valid()) {
        return false;
      }
      const vc = $("#verification-code").val();
      sun.services.twoFactorAuth
        .verifyTwoFactorToken(vc)
        .then(function (result) {
          abp.notify.success(l("EnableMfaSuccess"));
          reloadTwoFactorAuthentication();
        });
    });

    $("#btnDisableMFA").click(function (e) {
      abp.message.confirm(
        l("DisableMfaSuccess", l("DisableMfa")),
        l("Confirm"),
        function (confirmed) {
          if (confirmed) {
            sun.services.twoFactorAuth
              .enableTwoFactor(false)
              .then(function (result) {
                abp.notify.success(l("DisableMfaSuccess"));
                reloadTwoFactorAuthentication();
              });
          }
        }
      );
    });

    function reloadTwoFactorAuthentication() {
      const divEnableMFA = $("#divEnableMFA");
      const divDisableMFA = $("#divDisableMFA");
      divEnableMFA.hide();
      divDisableMFA.hide();
      sun.services.twoFactorAuth.twoFactorTokenStatus().then(function (result) {
        console.log(result);
        if (result) {
          divDisableMFA.show();
        } else {
          divEnableMFA.show();
        }
      });
    }
  });
})(jQuery);
