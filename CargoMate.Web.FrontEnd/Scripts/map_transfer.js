$(document).on('ready', function (e) {
    //(function (A) {

    //    if (!Array.prototype.forEach)
    //        A.forEach = A.forEach || function (action, that) {
    //            for (var i = 0, l = this.length; i < l; i++)
    //                if (i in this)
    //                    action.call(that, this[i], i, this);
    //        };

    //})(Array.prototype);
    markersData = [];

    var userLocation;
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            userLocation = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };

        }, function (e) {
            console.log(e);
        });
    } else {
        // Browser doesn't support Geolocation
        console.log(e);
    }

    var firebaseRef = firebase.database().ref().child("vehicle").child("location");

    // Create a new GeoFire instance at the random Firebase location
    var vehicleTypeId = RequestHandler.getQueryString('VehicleTypeId');

    var geoFire = new GeoFire(firebaseRef.child(vehicleTypeId ? vehicleTypeId : "1"));

    var geoQuery = geoFire.query({
        center: [21.2854, 39.2376],
        radius: 1000
    });

    var onReadyRegistration = geoQuery.on("ready", function () {
        console.log("GeoQuery has loaded and fired all other events for initial data");
    });

    var onKeyEnteredRegistration = geoQuery.on("key_entered", function (key, location, distance) {

        if (key in markersData) {

            console.log(key + 'key exists');
        }
        else {
            console.log(key + 'new key added');
            markersData.push(key);

            var url = [RequestHandler.getSiteBase(), "/", "Home", "/", "GetVehicleById"].join("");

            RequestHandler.postToController(url, RequestHandler.formMethods.Get, { vehicleId: key }, function (response) {

                if (response) {
                    showMarkers({
                        name: response.DriverName + ' (' + response.DriverNationality + ')',
                        make: response.Make,
                        model: response.Model,
                        year:response.Year,
                        location_latitude: location[0],
                        location_longitude: location[1],
                        map_image_url: response.TypeImage,//'../../../images/VehicleTypes/t4.jpg',
                        name_point: response.Type,
                        description_point: 'Description Goes Here',
                        get_directions_start_address: '',
                        phone: response.DriverPhone,
                        url_point: '#'
                    });
                }

            });


        }

        console.log(key + " entered query at " + location + " (" + distance + " km from center)");
    });

    var onKeyExitedRegistration = geoQuery.on("key_exited", function (key, location, distance) {
        console.log(key + " exited query to " + location + " (" + distance + " km from center)");
    });

    var onKeyMovedRegistration = geoQuery.on("key_moved", function (key, location, distance) {
        console.log(key + " moved within query to " + location + " (" + distance + " km from center)");
    });

    var mapObject, markers = [];


    var mapOptions = {
        zoom: 8,
        center: new google.maps.LatLng(21.2854, 39.2376),
        mapTypeId: google.maps.MapTypeId.ROADMAP,

        mapTypeControl: false,
        mapTypeControlOptions: {
            style: google.maps.MapTypeControlStyle.DROPDOWN_MENU,
            position: google.maps.ControlPosition.LEFT_CENTER
        },
        panControl: false,
        panControlOptions: {
            position: google.maps.ControlPosition.TOP_RIGHT
        },
        zoomControl: true,
        zoomControlOptions: {
            style: google.maps.ZoomControlStyle.LARGE,
            position: google.maps.ControlPosition.TOP_LEFT
        },
        scrollwheel: false,
        scaleControl: false,
        scaleControlOptions: {
            position: google.maps.ControlPosition.TOP_LEFT
        },
        streetViewControl: true,
        streetViewControlOptions: {
            position: google.maps.ControlPosition.LEFT_TOP
        },
        styles: [
            {
                "featureType": "landscape",
                "stylers": [
                    {
                        "hue": "#FFBB00"
                    },
                    {
                        "saturation": 43.400000000000006
                    },
                    {
                        "lightness": 37.599999999999994
                    },
                    {
                        "gamma": 1
                    }
                ]
            },
            {
                "featureType": "road.highway",
                "stylers": [
                    {
                        "hue": "#FFC200"
                    },
                    {
                        "saturation": -61.8
                    },
                    {
                        "lightness": 45.599999999999994
                    },
                    {
                        "gamma": 1
                    }
                ]
            },
            {
                "featureType": "road.arterial",
                "stylers": [
                    {
                        "hue": "#FF0300"
                    },
                    {
                        "saturation": -100
                    },
                    {
                        "lightness": 51.19999999999999
                    },
                    {
                        "gamma": 1
                    }
                ]
            },
            {
                "featureType": "road.local",
                "stylers": [
                    {
                        "hue": "#FF0300"
                    },
                    {
                        "saturation": -100
                    },
                    {
                        "lightness": 52
                    },
                    {
                        "gamma": 1
                    }
                ]
            },
            {
                "featureType": "water",
                "stylers": [
                    {
                        "hue": "#0078FF"
                    },
                    {
                        "saturation": -13.200000000000003
                    },
                    {
                        "lightness": 2.4000000000000057
                    },
                    {
                        "gamma": 1
                    }
                ]
            },
            {
                "featureType": "poi",
                "stylers": [
                    {
                        "hue": "#00FF6A"
                    },
                    {
                        "saturation": -1.0989010989011234
                    },
                    {
                        "lightness": 11.200000000000017
                    },
                    {
                        "gamma": 1
                    }
                ]
            }
        ]
    };
    var marker;
    mapObject = new google.maps.Map(document.getElementById('map'), mapOptions);

    function showMarkers(item) {
        marker = new google.maps.Marker({
            position: new google.maps.LatLng(item.location_latitude, item.location_longitude),
            map: mapObject,
            icon: {
                url: '../images/pins/Airports.png', // url
                scaledSize: new google.maps.Size(43, 55), // scaled size
                origin: new google.maps.Point(0, 0), // origin
                anchor: new google.maps.Point(0, 0) // anchor
            }
        });

        markers.push(marker);
        google.maps.event.addListener(marker, 'click', (function () {
            closeInfoBox();
            getInfoBox(item).open(mapObject, this);
            mapObject.setCenter(new google.maps.LatLng(item.location_latitude, item.location_longitude));
        }));
    }



    function hideAllMarkers() {
        markers.forEach(function (marker) {
            marker.setMap(null);
        });
    };

    function closeInfoBox() {
        $('div.infoBox').remove();
    };

    function getInfoBox(item) {
        return new InfoBox({
            content:
            '<div class="marker_info" id="marker_info" style="padding:10px;">' +
            '<img src="' + item.map_image_url + '" alt="Image"/>' +
            '<h5>' + item.name + '</h3>' +
            '<h3>' + item.name_point + '</h3>' +
            //'<span>' + item.description_point + '</span>' +
            '<div class="marker_tools">' +
            '<h5> ' + item.make+' ' + item.model + ' (' + item.year +')</h5>' +
            '</div>' +
            '<div class="marker_tools">' +
            '<form action="http://maps.google.com/maps" method="get" target="_blank" style="display:inline-block""><input name="saddr" value="' + item.get_directions_start_address + '" type="hidden"><input type="hidden" name="daddr" value="' + item.location_latitude + ',' + item.location_longitude + '"><button type="submit" value="Get directions" class="btn_infobox_get_directions">Directions</button></form>' +
            '<a href="tel://' + item.phone + '" class="btn_infobox_phone">' + item.phone + '</a>' +
            '</div>' +
            '<a href="' + item.url_point + '" class="btn_infobox" >Details</a>' +
            '</div>',
            disableAutoPan: false,
            maxWidth: 0,
            pixelOffset: new google.maps.Size(10, 125),
            closeBoxMargin: '5px -20px 2px 2px',
            closeBoxURL: "http://www.google.com/intl/en_us/mapfiles/close.gif",
            isHidden: false,
            alignBottom: true,
            pane: 'floatPane',
            enableEventPropagation: true
        });


    };

});