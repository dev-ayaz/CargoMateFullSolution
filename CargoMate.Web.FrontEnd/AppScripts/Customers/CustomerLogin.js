var CustomerLogin = {

    selectors: {
        signInWithFacebookButton: "#customerSignInWithFacebook",
        signInWithGoogleButton: "#customerSignInWithGoogle",
        CustomerLoginForm: ".CustomerLoginForm",
        EmailAddress: "input[id=Email]",
        Password: "input[id=Password]",
        logoutMenu: "#logout_menu",
        loginMenu:".login_menu"
    },
    services: {
        controller: "Customer",
        actions: {
            CheckCustomer: "IsCustomerExists",
            Register:"Register"

        }
    },
    callbacks: {
        loginCustomer: function (form, e) {
            if ($(form).valid()) {
                e.preventDefault();
                firebaseUtilFunc.signInWithEmailAndPassword($(form).find(CustomerLogin.selectors.EmailAddress).val(), $(form).find(CustomerLogin.selectors.Password).val()).then(function (response) {
                    if (response.IsError) {
                        CargoMateAlerts.actionAlert("Error", response.result, true);
                    } else {
                        CustomerLogin.callbacks.IsCustomerExists(response.result);
                    }
                });
                return true;
            }
            return false;
        },
        registerCustomer: function (user) {


            var url = [RequestHandler.getSiteBase(), "/", CustomerLogin.services.controller, "/", CustomerLogin.services.actions.Register].join("");

            var userModel = { Id: user.uid, Name: user.displayName, Email: user.email, Phone: user.phoneNumber, ImageUrl: user.photoURL };


            $.ajax({
                type: "POST",
                url: url,
                traditional: true,
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(userModel),
                success: function (data) {
                    CargoMateAlerts.actionAlert(data.MessageHeader, data.Message, data.IsError);
                    location.href = "/Customer/EditCustomer?customerId=" + user.uid;
                },
                error: function (data) { console.log(data) }
            });

        },
        IsCustomerExists: function (user) {

            var url = [RequestHandler.getSiteBase(), "/", CustomerLogin.services.controller, "/", CustomerLogin.services.actions.CheckCustomer].join("");

            RequestHandler.postToController(url, RequestHandler.formMethods.Get, { userId: user.uid }, function (response) {

                if (response.RedirectUrl && response.IsExists) {

                    window.location.href = response.RedirectUrl;
                }
                else if (!response.IsExists) {
                    CustomerLogin.callbacks.registerCustomer(user);
                }
                else {
                    CargoMateAlerts.actionAlert("Success", "Successfully login !", false);
                }
            });
        }


    },
    initevents: function () {
        $(CustomerLogin.selectors.signInWithFacebookButton + ":not(.bound)").addClass("bound").click(function () {
            var user = firebaseUtilFunc.signiInWithFacebook();
            console.log(user);
        });
        $(CustomerLogin.selectors.signInWithGoogleButton + ":not(.bound)").addClass("bound").click(function () {
            firebaseUtilFunc.signInwithGoogle().then(function (response) {
                if (response.IsError) {
                    CargoMateAlerts.actionAlert("Error", response.result, true);
                } else {
                    CustomerLogin.callbacks.IsCustomerExists(response.result);
                }
            });
        });

        $(CustomerLogin.selectors.CustomerLoginForm).submit(function (e) {
            //changed by tanzil
            var form = CustomerLogin.callbacks.loginCustomer($(this), e);
            return false;
        });


    }
}

jQuery(document).ready(function () {
    CustomerLogin.initevents();
});