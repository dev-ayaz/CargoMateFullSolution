var Home = {
    Selectors: {
        PickUpAddress: "#PickUpAddress",
        PickUpLocation: "#PickUpLocation",
        DropUpAddress: "#DropUpAddress",
        DropUpLocation: "#DropUpLocation"
    },
    Callbacks: {

    },
    InitEvents: function () {

        // pickup address google placepicker
        $(Home.Selectors.PickUpAddress).placepicker({
            placeChanged: function (place) {
                var location = this.getLocation();
                $(Home.Selectors.PickUpLocation).val(location.latitude + "," + location.longitude);
            }
        }).data("placepicker");

        // dropup address google placepicker
        $(Home.Selectors.DropUpAddress).placepicker({
            placeChanged: function (place) {
                var location = this.getLocation();
                $(Home.Selectors.DropUpLocation).val(location.latitude + "," + location.longitude);
            }
        }).data("placepicker");
    }
};

jQuery(function () {
    Home.InitEvents();
});