var CustomerRegistration = {

    selectors: {
        signUpWithFacebookButton: "#signUpWithFacebook",
        signUpWithGoogleButton: "#signUpWithGoogle",
        CustomerRegistrationForm: "#CustomerRegistrationForm",
        EmailAddress: "#CustomerRegistrationForm input[id=Email]",
        Password: "#CustomerRegistrationForm input[id=Password]"
    },
    services: {
        controller: "Customer",
        actions: {
            Register: "Register"
            
           
        }
    },
    callbacks: {
        registerCustomer: function (user) {


            var url = [RequestHandler.getSiteBase(), "/", CustomerRegistration.services.controller, "/", CustomerRegistration.services.actions.Register].join("");

            var userModel = { Id: user.uid, Name: user.displayName , Email: user.email, Phone : user.phoneNumber,ImageUrl:user.photoURL };


            $.ajax({
                type: "POST",
                url: url,
                traditional: true,
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(userModel),
                success: function(data) {
                    CargoMateAlerts.actionAlert(data.MessageHeader, data.Message, data.IsError);
                    location.href = "/Customer/EditCustomer?userId=" + user.uid;
                },
                error: function (data) { console.log(data) }
            });
           
        }

    },
    initevents: function () {
        $(CustomerRegistration.selectors.signUpWithFacebookButton + ":not(.bound)").addClass("bound").click(function () {
            var user = firebaseUtilFunc.signiInWithFacebook();
            console.log(user);
        });
        $(CustomerRegistration.selectors.signUpWithGoogleButton + ":not(.bound)").addClass("bound").click(function () {
            firebaseUtilFunc.signInwithGoogle().then(function (response) {
                if (response.IsError) {
                    CargoMateAlerts.actionAlert("Error", response.result, true);
                } else {
                    CustomerRegistration.callbacks.registerCustomer(response.result);
                }
            });
        });

        $(CustomerRegistration.selectors.CustomerRegistrationForm).submit(function (e) {
            var a = $(CustomerRegistration.selectors.EmailAddress).val();

            e.preventDefault();
            firebaseUtilFunc.createUserWithEmailAndPassword($(CustomerRegistration.selectors.EmailAddress).val(), $(CustomerRegistration.selectors.Password).val()).then(function (response) {
                if (response.IsError) {
                    CargoMateAlerts.actionAlert("Error", response.result, true);
                }
                else {
                    CustomerRegistration.callbacks.registerCustomer(response.result);
                }
            });
            return false;
        });

       
    }
}

jQuery(document).ready(function () {
    CustomerRegistration.initevents();
});