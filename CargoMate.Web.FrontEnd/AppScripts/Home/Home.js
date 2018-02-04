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
                geoElement.name = elem.long_name;
                geoElement.types = [];
                $.map(elem.types, function (type) {
                    geoElement.types.push(type);
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
                var location = this.getLocation();
                var VehicleQuery = JSON.parse(sessionStorage.getItem("VehicleQuery"));
                VehicleQuery.pickupAddress = Home.Callbacks.getGeoAddress(place);
                VehicleQuery.pickupLocation = "POINT(" + location.latitude + " " + location.longitude + ")"
                console.log(VehicleQuery);
                sessionStorage.setItem("VehicleQuery", JSON.stringify(VehicleQuery));


                $(Home.Selectors.PickUpLocation).val(location.latitude + "," + location.longitude);

            }
        }).data("placepicker");

        // dropup address google placepicker
        $(Home.Selectors.DropUpAddress).placepicker({

            placeChanged: function (place) {

                var location = this.getLocation();
                var VehicleQuery = JSON.parse(sessionStorage.getItem("VehicleQuery"));
                VehicleQuery.dropoffAddress = Home.Callbacks.getGeoAddress(place);
                VehicleQuery.dropoffLocation = "POINT(" + location.latitude + " " + location.longitude + ")"
                console.log(VehicleQuery);
                sessionStorage.setItem("VehicleQuery", JSON.stringify(VehicleQuery));

                $(Home.Selectors.DropUpLocation).val(location.latitude + "," + location.longitude);


            }
        }).data("placepicker");
    }
};

jQuery(function () {

    var Vquery = {
        queryId: 1,
        customer: {},
        selectedVehicle: {},
        // selectedVehicleLocation: null,
        //vehicleParaPack: {
        //    vehicleType: {},
        //    vehicleConfig: {},
        //    vehicleCapacity: {}
        //},
        pickupAddress: {},
        dropoffAddress: {},
        //searchAddress: {},
        pickupLocation: null,
        dropoffLocation: null,
        distance: 5.1,
        time: 6.1,
        jobWorth: 7.1,
        priceSurge: 8.1,
        insuredNeeded: false,
        fixedRate: false,
        driverJobAccessed: false,
        insuranceAmount: 12.1,
        payloadTypeList: [{ image: "", name: "container, dry goods,boxes",typeId:5}],
        tripType: {id:1,name:'Local'},
        payloadWeight: 13.1,
        payloadWeightUnit: null,
        //payloadQty: 15,
        //payloadQtyUnit: 0,
        // recipientName: null,
        //recipientPhone: null,
        //recipientId: null,
        recipientNotified: false,
        //jobStatusMap: {},
        jobStatus: "JOB_INVOKE_DRIVER",
        nationalityPref: {},
        codeQRReceiver: null,
        codeQRSupplier: null,
        payloadSealCode: null,
        payloadSealed: false,
        codeQRReceiverVerified: false,
        codeQRSupplierVerified: false,
        driverDocVerified: false,
        vehicleDocVerified: false,
        payloadConditionRating: 29.1,
        // payloadConditionRemarks: null
    };

    if (!sessionStorage.getItem("VehicleQuery"))
    {
        sessionStorage.setItem("VehicleQuery", JSON.stringify(Vquery));
    }
    Home.InitEvents();
});