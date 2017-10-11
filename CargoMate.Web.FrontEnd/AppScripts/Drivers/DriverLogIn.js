var DriverLogin = {

    selectors: {
        signInWithFacebookButton: "#signInWithFacebook",
        signInWithGoogleButton: "#signInWithGoogle",
        DriverLoginForm: ".DriverLoginForm",
        EmailAddress: "input[id=Email]",
        Password: "input[id=Password]"
    },
    services: {
        controller: "Driver",
        actions: {
            CheckDriver: "IsDriverExists",
            Register:"Register"

        }
    },
    callbacks: {
        loginDriver: function (form, e) {
            if ($(form).valid()) {
                e.preventDefault();
                firebaseUtilFunc.signInWithEmailAndPassword($(form).find(DriverLogin.selectors.EmailAddress).val(), $(form).find(DriverLogin.selectors.Password).val()).then(function (response) {
                    if (response.IsError) {
                        CargoMateAlerts.actionAlert("Error", response.result, true);
                    } else {
                        CustomerLogin.callbacks.IsDriverExists(response.result);
                    }
                });
                return true;
            }
            return false;
        },
        IsDriverExists: function (user) {


            var url = [RequestHandler.getSiteBase(), "/", DriverLogin.services.controller, "/", DriverLogin.services.actions.CheckDriver].join("");

            RequestHandler.postToController(url, RequestHandler.formMethods.Get, { userId: user.uid }, function (response) {


                if (response.RedirectUrl && response.IsExist) {

                    window.location.href = response.RedirectUrl;
                }
                else if (!response.IsExist) {
                    DriverLogin.callbacks.registerDriver(user);
                }
            });
        },
        registerDriver: function (user) {


            var url = [RequestHandler.getSiteBase(), "/", DriverLogin.services.controller, "/", DriverLogin.services.actions.Register].join("");

            var userModel = { Id: user.uid, Name: user.displayName, Email: user.email, Phone: user.phoneNumber, ImageUrl: user.photoURL };


            $.ajax({
                type: "POST",
                url: url,
                traditional: true,
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(userModel),
                success: function (data) {
                    CargoMateAlerts.actionAlert(data.MessageHeader, data.Message, data.IsError);
                    location.href = "/Driver/Edit?driverId=" + user.uid;
                },
                error: function (data) { console.log(data) }
            });

        }


    },
    initevents: function () {
        $(DriverLogin.selectors.signInWithFacebookButton + ":not(.bound)").addClass("bound").click(function () {
            var user = firebaseUtilFunc.signiInWithFacebook();
            console.log(user);
        });
        $(DriverLogin.selectors.signInWithGoogleButton + ":not(.bound)").addClass("bound").click(function () {
            debugger;
            firebaseUtilFunc.signInwithGoogle().then(function (response) {
                if (response.IsError) {
                    CargoMateAlerts.actionAlert("Error", response.result, true);
                } else {
                    DriverLogin.callbacks.IsDriverExists(response.result);
                }
            });
        });

        $(DriverLogin.selectors.DriverLoginForm).submit(function (e) {

            //changed by tanzil
            var form = DriverLogin.callbacks.loginDriver($(this), e);
            return false;
        });


    }
}

jQuery(document).ready(function () {
    DriverLogin.initevents();
});