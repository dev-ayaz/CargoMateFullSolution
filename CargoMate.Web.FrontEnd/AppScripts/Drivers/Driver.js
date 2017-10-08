var Driver = {
    Selectors: {

        AddressPicker: "#Address",
        Phone: "#PhoneNumber",
        Location: "#Location",
        ImageSrc: "#ImageSrc",
        ImageUrl: "#ImageUrl",
        DriverId: "#DriverId",
        Name: "#Name",
        EmailAddress: "#EmailAddress"

    },
    Services: {

        Controller: "Driver",
        Actions: {
            CheckCustoer: "IsCustomerExists"
        }
    },
    CallBacks: {
        IsCustomerExists: function (user) {


            var url = [RequestHandler.getSiteBase(), "/", Driver.Services.Controller, "/", Driver.Services.Actions.CheckCustoer].join("");

            RequestHandler.postToController(url, RequestHandler.formMethods.Get, { userId: user.uid }, function (response) {

                if (!response.IsExists) {

                    window.location.href = response.RedirectUrl;
                } else {
                    window.location.href = RequestHandler.getSiteBase();
                }
            });
        },
        InsertSuccess: function (response) {
            CargoMateAlerts.actionAlert(response.MessageHeader, response.Message, response.IsError);

            if (!response.IsError) {
               // window.location.href = RequestHandler.getSiteBase();
            }
        }
    },
    InitEvents: function () {





        $(Driver.Selectors.ImageUrl + ":not(.bound)").addClass("bound").change(function () {

            var imageInput = $(this);

            if (this.files && this.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {

                    $(imageInput.closest("form").find(Driver.Selectors.ImageSrc)).val(e.target.result);
                }

                reader.readAsDataURL(this.files[0]);
            }
        });


        $(Driver.Selectors.AddressPicker).placepicker({
            placeChanged: function (place) {

                var location = this.getLocation();
                $(Driver.Selectors.Location).val(location.latitude + "," + location.longitude);
            }
        }).data('placepicker');

        $(Driver.Selectors.Phone).intlTelInput({

            allowDropdown: true,
            autoHideDialCode: false,
            autoPlaceholder: "polite",
            customPlaceholder: null,
            dropdownContainer: "",
            excludeCountries: [],
            formatOnDisplay: true,
            geoIpLookup: function (callback) {
                $.get("http://ipinfo.io", function () { }, "jsonp").always(function (resp) {
                    var countryCode = (resp && resp.country) ? resp.country : "";
                    callback(countryCode);
                });
            },

            initialCountry: "",
            nationalMode: false,
            placeholderNumberType: "MOBILE",
            onlyCountries: [],
            preferredCountries: ["sa", "ae", "pk"],
            separateDialCode: false,
            utilsScript: ""
        });
    }
}