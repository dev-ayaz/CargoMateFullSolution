$(document).on('ready', function (e) {


    //firebase.database().ref('jobVehicle/' + 99).set({
    //    "queryId": 1,
    //    "customer": {},
    //    "selectedVehicle": {
    //        "pickUpAddress": "sample string 1",
    //        "dropUpAddress": "sample string 2",
    //        "pickUpLocation": "sample string 3",
    //        "dropUpLocation": "sample string 4",
    //        "date": "2018-01-08T00:25:24.3393713+03:00",
    //        "capacityId": 1,
    //        "vehicleTypeId": 1,
    //        "vehicleCapacities": [
    //            {
    //                "disabled": true,
    //                "group": {
    //                    "disabled": true,
    //                    "name": "sample string 2"
    //                },
    //                "selected": true,
    //                "text": "sample string 3",
    //                "value": "sample string 4"
    //            },
    //            {
    //                "disabled": true,
    //                "group": {
    //                    "disabled": true,
    //                    "name": "sample string 2"
    //                },
    //                "selected": true,
    //                "text": "sample string 3",
    //                "value": "sample string 4"
    //            }
    //        ],
    //        "vehicleTypes": [
    //            {
    //                "disabled": true,
    //                "group": {
    //                    "disabled": true,
    //                    "name": "sample string 2"
    //                },
    //                "selected": true,
    //                "text": "sample string 3",
    //                "value": "sample string 4"
    //            },
    //            {
    //                "disabled": true,
    //                "group": {
    //                    "disabled": true,
    //                    "name": "sample string 2"
    //                },
    //                "selected": true,
    //                "text": "sample string 3",
    //                "value": "sample string 4"
    //            }
    //        ]
    //    },
    //    "selectedVehicleLocation": "sample string 2",
    //    "vehicleParaPack": {
    //        "vehicleType": {
    //            "id": 1,
    //            "name": "sample string 1",
    //            "description": "sample string 2",
    //            "imageUrl": "sample string 3",
    //            "isEquipment": true
    //        },
    //        "vehicleConfig": {
    //            "id": 1,
    //            "name": "sample string 1",
    //            "description": "sample string 2",
    //            "imageUrl": "sample string 3"
    //        },
    //        "vehicleCapacity": {
    //            "id": 1,
    //            "name": "sample string 1",
    //            "capacity": 1,
    //            "length": 1,
    //            "palletNumber": 1
    //        }
    //    },
    //    "pickupAddress": {
    //        "addressElements": [
    //            {
    //                "name": "sample string 1",
    //                "types": [
    //                    "sample string 1",
    //                    "sample string 2"
    //                ]
    //            },
    //            {
    //                "name": "sample string 1",
    //                "types": [
    //                    "sample string 1",
    //                    "sample string 2"
    //                ]
    //            }
    //        ],
    //        "subLocality": null,
    //        "locality": null,
    //        "country": null,
    //        "fullAddress": "sample string 1, sample string 1",
    //        "shortAddress": "sample string 1, sample string 1",
    //        "basePath": "sample string 1/sample string 1/"
    //    },
    //    "dropoffAddress": {
    //        "addressElements": [
    //            {
    //                "name": "sample string 1",
    //                "types": [
    //                    "sample string 1",
    //                    "sample string 2"
    //                ]
    //            },
    //            {
    //                "name": "sample string 1",
    //                "types": [
    //                    "sample string 1",
    //                    "sample string 2"
    //                ]
    //            }
    //        ],
    //        "subLocality": null,
    //        "locality": null,
    //        "country": null,
    //        "fullAddress": "sample string 1, sample string 1",
    //        "shortAddress": "sample string 1, sample string 1",
    //        "basePath": "sample string 1/sample string 1/"
    //    },
    //    "searchAddress": {
    //        "addressElements": [
    //            {
    //                "name": "sample string 1",
    //                "types": [
    //                    "sample string 1",
    //                    "sample string 2"
    //                ]
    //            },
    //            {
    //                "name": "sample string 1",
    //                "types": [
    //                    "sample string 1",
    //                    "sample string 2"
    //                ]
    //            }
    //        ],
    //        "subLocality": null,
    //        "locality": null,
    //        "country": null,
    //        "fullAddress": "sample string 1, sample string 1",
    //        "shortAddress": "sample string 1, sample string 1",
    //        "basePath": "sample string 1/sample string 1/"
    //    },
    //    "pickupLocation": "sample string 3",
    //    "dropoffLocation": "sample string 4",
    //    "distance": 5.1,
    //    "time": 6.1,
    //    "jobWorth": 7.1,
    //    "priceSurge": 8.1,
    //    "insuredNeeded": true,
    //    "fixedRate": true,
    //    "driverJobAccessed": true,
    //    "insuranceAmount": 12.1,
    //    "payloadTypeList": [
    //        {
    //            "id": 1,
    //            "name": "sample string 1",
    //            "imageUrl": "sample string 2"
    //        },
    //        {
    //            "id": 1,
    //            "name": "sample string 1",
    //            "imageUrl": "sample string 2"
    //        }
    //    ],
    //    "tripType": {
    //        "id": 1,
    //        "name": "sample string 1",
    //        "imageUrl": "sample string 2"
    //    },
    //    "payloadWeight": 13.1,
    //    "payloadWeightUnit": "sample string 14",
    //    "payloadQty": 15,
    //    "payloadQtyUnit": "sample string 16",
    //    "recipientName": "sample string 17",
    //    "recipientPhone": "sample string 18",
    //    "recipientId": "sample string 19",
    //    "recipientNotified": true,
    //    "jobStatusMap": {
    //        "sample string 1": 2,
    //        "sample string 3": 4
    //    },
    //    "jobStatus": 1,
    //    "nationalityPref": {
    //        "id": 1,
    //        "name": "sample string 1",
    //        "countryCode": "sample string 2",
    //        "currencyLong": "sample string 3",
    //        "currencyCode": "sample string 4",
    //        "currencySymbol": "sample string 5",
    //        "phonCode": "sample string 6",
    //        "flag": "sample string 7"
    //    },
    //    "codeQRCustomer": "sample string 21",
    //    "codeQRRecipient": "sample string 22",
    //    "payloadSealCode": "sample string 23",
    //    "payloadSealed": true,
    //    "codeQRCustomerVerified": true,
    //    "codeQRRecipientVerified": true,
    //    "driverDocVerified": true,
    //    "vehicleDocVerified": true,
    //    "payloadConditionRating": 29.1,
    //    "payloadConditionRemarks": "sample string 30"
    //});

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
                        vehicleId: key,
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
            '<a href="' + item.url_point + '" class="btn_infobox"  style="margin-right:5px">Details</a>' +
            '<button onclick="gotoInvokingPage(' + item.vehicleId + ')"  class="btn_infobox" >Invoke</button>' +
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

function gotoInvokingPage(vehicleId) {
    alert(RequestHandler.getQueryString("VehicleTypeId"));
    if (window.UserId) {

        window.location = RequestHandler.getSiteRoot() + "/DriverInvoke?vehicleId=" + vehicleId + "&VehicleTypeId=" + RequestHandler.getQueryString("VehicleTypeId") + "&CapacityId=" + RequestHandler.getQueryString("CapacityId") 
    }
    else {
        alert("login is require to invoke driver");
    }
}