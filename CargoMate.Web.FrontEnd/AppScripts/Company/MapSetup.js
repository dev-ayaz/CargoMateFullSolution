var markercount = 0;
var markersArray = [];

var pos;
var map;

var CompanyMap = {
    selectors: {
        mapDiv: "googleMap",
        infoWindoContent: "<h5>Current Location</h5>",
   

    },

    services: {
        controller: "",
        userLocationApi: "http://ip-api.com/json",
        actions: {}
    },


    callbacks: {
        showMarker: function (pinlocation) {
            debugger;
            if (markersArray) {
                for (var i = 0; i < markersArray.length; i++) {
                    markersArray[i].setMap(null);
                }
                markersArray.length = 0;
            }

            var marker = new window.google.maps.Marker({
                position: pinlocation,
                map: map,
                animation: window.google.maps.Animation.BOUNCE
            });


            markersArray.push(marker);

            $(Company.Selectors.Location).val(pinlocation.lat() + "," + pinlocation.lng());

      
            markercount = 1;

            
        },
        initMap: function () {
            var mapDiv = document.getElementById(CompanyMap.selectors.mapDiv);
            map = new window.google.maps.Map(mapDiv, {
                zoom: 13,
                center: pos
            });

            var infowindow = new window.google.maps.InfoWindow({
                content: "<h5>Current Location</h5>"
            });

            infowindow.setPosition(pos);
            infowindow.open(map, null);

            window.google.maps.event.addListener(map, "click", function (event) {

                CompanyMap.callbacks.showMarker(event.latLng);
            });
            //place a marker on Map

        }
    },


    initEvents: function () {

        $.get(CompanyMap.services.userLocationApi, function (data) {
            pos = new window.google.maps.LatLng(data.lat, data.lon);
            CompanyMap.callbacks.initMap();
        });


    }

}

