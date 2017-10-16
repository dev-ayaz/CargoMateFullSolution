var Home = {
    Selectors: {

        AddressPicker: ".placePicker",
    },
    Callbacks: {

    },
    InitEvents: function(){

        $(Home.Selectors.AddressPicker).placepicker({
            placeChanged: function (place) {
                var location = this.getLocation();
                console.log(location);
            }
        }).data("placepicker");
    }
};

jQuery(function () {
    Home.InitEvents();
});