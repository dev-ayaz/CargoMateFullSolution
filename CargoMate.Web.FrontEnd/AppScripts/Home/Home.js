var Home = {
    Selectors: {
        PickUpAddress: "#PickUpAddress",
        PickUpLocation: "#PickUpLocation",
        DropUpAddress: "#DropUpAddress",
        DropUpLocation: "#DropUpLocation"
    },
    Callbacks: {
        getGeoAddress: function (place) {
            var geoAddress = {};
            var addressElements = [];
            geoAddress.locality = "";
            geoAddress.country = "";
            $.map(place.address_components, function (elem) {
                var geoElement = {};
                geoElement.Name = elem.long_name;
                geoElement.Types = [];
                $.map(elem.types, function (type) {
                    geoElement.Types.push(type);
                    if (type == "locality") {
                        geoAddress.locality = elem.long_name;
                    }
                    if (type == "country") {
                        geoAddress.country = elem.long_name;
                    }
                });

                addressElements.push(geoElement);
            });
            geoAddress.addressElements = addressElements;
            geoAddress.basePath = place.formatted_address;
            geoAddress.fullAddress = place.formatted_address;
            geoAddress.shortAddress = place.address_components[0].long_name;

            console.log(geoAddress);
            return geoAddress;
        }
    },
    InitEvents: function () {

        // pickup address google placepicker
        $(Home.Selectors.PickUpAddress).placepicker({
            placeChanged: function (place) {

                sessionStorage.setItem("PickUpAddress", Home.Callbacks.getGeoAddress(place));
                var location = this.getLocation();
                $(Home.Selectors.PickUpLocation).val(location.latitude + "," + location.longitude);
            }
        }).data("placepicker");

        // dropup address google placepicker
        $(Home.Selectors.DropUpAddress).placepicker({

            placeChanged: function (place) {
                sessionStorage.setItem("DropUpAddress", Home.Callbacks.getGeoAddress(place));
                var location = this.getLocation();
                $(Home.Selectors.DropUpLocation).val(location.latitude + "," + location.longitude);
            }
        }).data("placepicker");
    }
};

jQuery(function () {
    sessionStorage.setItem("DropUpAddress", null);
    sessionStorage.setItem("PickUpAddress", null);
    Home.InitEvents();
});