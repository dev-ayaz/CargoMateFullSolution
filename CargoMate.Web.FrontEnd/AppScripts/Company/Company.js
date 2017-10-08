var Company = {
    Selectors: {
        AddressPicker: "#Address",
        Phone: "#PhoneNumber",
        Location: "#Location",
        CountryName: "#CountryName",
        Imageinput: "#CompanyLogo",
        CompanyLogo: "#Logo"

    },
    Callbacks: {

        InsertSuccess:function(response) {
            CargoMateAlerts.actionAlert(response.MessageHeader, response.Message, response.IsError);
        },

        InitAddressPicker: function () {
            $(Company.Selectors.AddressPicker).placepicker({
                placeChanged: function (place) {

                    var location = this.getLocation();

                    var filteredArray = place.address_components.filter(function (addressComponent) {
                        return addressComponent.types.includes("country");
                    });

                    var country = filteredArray.length ? filteredArray[0].long_name : "";

                    $(Company.Selectors.Location).val(location.latitude + "," + location.longitude);

                    $(Company.Selectors.CountryName).val(country);
                }
            }).data('placepicker');
        },
        InitPhoneNumber: function () {
            $(Company.Selectors.Phone).intlTelInput({
                // whether or not to allow the dropdown
                allowDropdown: true,

                // if there is just a dial code in the input: remove it on blur, and re-add it on focus
                autoHideDialCode: false,

                // add a placeholder in the input with an example number for the selected country
                autoPlaceholder: "polite",

                // modify the auto placeholder
                customPlaceholder: null,

                // append menu to a specific element
                dropdownContainer: "",

                // don't display these countries
                excludeCountries: [],

                // format the input value during initialisation
                formatOnDisplay: true,

                // geoIp lookup function
                geoIpLookup: function (callback) {
                    $.get("http://ipinfo.io", function () { }, "jsonp").always(function (resp) {
                        var countryCode = (resp && resp.country) ? resp.country : "";
                        callback(countryCode);
                    });
                },

                // initial country
                initialCountry: "",

                // don't insert international dial codes
                nationalMode: false,

                // number type to use for placeholders
                placeholderNumberType: "MOBILE",

                // display only these countries
                onlyCountries: [],

                // the countries at the top of the list. defaults to united states and united kingdom
                preferredCountries: ["sa", "ae", "pk"],

                // display the country dial code next to the selected flag so it's not part of the typed number
                separateDialCode: false,

                // specify the path to the libphonenumber script to enable validation/formatting
                utilsScript: ""
            });
        }
    },
    initEvents: function () {
        $(Company.Selectors.Imageinput + ":not(.bound)").addClass("bound").change(function () {

            var imageInput = $(this);

            if (this.files && this.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {

                    $(imageInput.closest("form").find(Company.Selectors.CompanyLogo)).val(e.target.result);
                }

                reader.readAsDataURL(this.files[0]);
            }
        });
        Company.Callbacks.InitAddressPicker();
        Company.Callbacks.InitPhoneNumber();
    }
};

jQuery(document).ready(function () {
    Company.initEvents();
});