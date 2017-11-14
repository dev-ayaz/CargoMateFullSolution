var DriverRegistration = {

    selectors: {
        signUpWithFacebookButton: "#signUpWithFacebook",
        signUpWithGoogleButton: "#signUpWithGoogle",
        DriverRegistrationForm: "#DriverRegistrationForm",
        EmailAddress: "#DriverRegistrationForm input[id=Email]",
        Password: "#DriverRegistrationForm input[id=Password]"
    },
    services: {
        controller: "Driver",
        actions: {
            Register: "Register"
            
           
        }
    },
    callbacks: {
        registerDriver: function (user) {

         
            var url = [RequestHandler.getSiteBase(), "/", DriverRegistration.services.controller, "/", DriverRegistration.services.actions.Register].join("");

            var userModel = { Id: user.uid, Name: user.displayName , Email: user.email, Phone : user.phoneNumber,ImageUrl:user.photoURL };


            $.ajax({
                type: "POST",
                url: url,
                traditional: true,
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(userModel),
                success: function(data) {
                    CargoMateAlerts.actionAlert(data.MessageHeader, data.Message, data.IsError);
                    location.href = "/Driver/Edit?userId=" + user.uid;
                },
                error: function (data) { console.log(data) }
            });
           
        }

    },
    initevents: function () {
        $(DriverRegistration.selectors.signUpWithFacebookButton + ":not(.bound)").addClass("bound").click(function () {
            var user = firebaseUtilFunc.signiInWithFacebook();
            console.log(user);
        });
        $(DriverRegistration.selectors.signUpWithGoogleButton + ":not(.bound)").addClass("bound").click(function () {
            firebaseUtilFunc.signInwithGoogle().then(function (response) {
                if (response.IsError) {
                    CargoMateAlerts.actionAlert("Error", response.result, true);
                } else {
                    DriverRegistration.callbacks.registerDriver(response.result);
                }
            });
        });

        $(DriverRegistration.selectors.DriverRegistrationForm).submit(function (e) {

            e.preventDefault();
           // e.stopImmediatePropagation()

            firebaseUtilFunc.createUserWithEmailAndPassword($(DriverRegistration.selectors.EmailAddress).val(), $(DriverRegistration.selectors.Password).val()).then(function (response) {

               
                if (response.IsError) {
                    CargoMateAlerts.actionAlert("Error", response.result, true);
                }
                else {
                  
                    DriverRegistration.callbacks.registerDriver(response.result);
                }
            });
            return false;
        });

       
    }
}
jQuery(document).ready(function () {
    DriverRegistration.initevents();
});