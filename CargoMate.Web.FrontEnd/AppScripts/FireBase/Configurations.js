
var config = {
    apiKey: "AIzaSyAAgsBhSzZ3WgVmEbTOMJG4dDpZRKZS12M",
    authDomain: "prologix-5cc3c.firebaseapp.com",
    databaseURL: "https://prologix-5cc3c.firebaseio.com",
    storageBucket: "prologix-5cc3c.appspot.com",
    messagingSenderId: "729428235723"
};
firebase.initializeApp(config);



firebase.auth().onAuthStateChanged(function (user) {

    if (user) {
        $(".login_menu").addClass("hide");
        $("#logout_menu").removeClass("hide");
    } else {
        $(".login_menu").removeClass("hide");
        $("#logout_menu").addClass("hide");
    }
});


$("#btnlogout").click(function () {
    firebaseUtilFunc.logout();
});

var firebaseUtilFunc = {

    logout: function () {
        window.firebase.auth().signOut().then(function (user) {
            $(".login_menu").removeClass("hide");
            $("#logout_menu").addClass("hide");
            window.location.href = RequestHandler.getSiteBase();
        }, function (error) {
            console.log(error);
        });
    },
    signInWithEmailAndPassword: function (email, password) {
        return window.firebase.auth().signInWithEmailAndPassword(email, password).then(function (response) {
            return { result: response, IsError: false };
        }).catch(function (error) {
            return { result: error.message, IsError: true };
        });
    },
    createUserWithEmailAndPassword: function (email, password) {
        return window.firebase.auth().createUserWithEmailAndPassword(email, password).then(function (response) {
            return { result: response, IsError: false };
        }).catch(function (error) {
            return { result: error.message, IsError: true };
        });
    },
    signiInWithFacebook: function () {

        var facebbokProvider = new window.firebase.auth.FacebookAuthProvider();
        facebbokProvider.addScope("user_birthday");
        facebbokProvider.setCustomParameters({
            'display': "popup"
        });

        return window.firebase.auth().signInWithPopup(facebbokProvider).then(function (response) {
            return { result: response.user, IsError: false };
        }).catch(function (error) {
            return { result: error.message, IsError: true };
        });
    },

    signInwithGoogle: function () {

        var googleProvider = new window.firebase.auth.GoogleAuthProvider();
        googleProvider.addScope("profile");
        googleProvider.addScope("email");
        googleProvider.setCustomParameters({
            "login_hint": "user@example.com",
            'display': "popup"
        });

        return window.firebase.auth().signInWithPopup(googleProvider).then(function (response) {

            return { result: response.user, IsError: false };
        }).catch(function (error) {
            return { result: error.message, IsError: true };
        });
    }
}