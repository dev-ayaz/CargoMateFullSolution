var Customers = {
    Selectors: {

        IsCompanyDropDown: "#IsCompany",
        CompanyItemDiv: "#div-Company",
        AddressPicker: "#Address",
        Phone: "#PhoneNumber",
        Location: "#Location",
        ImageSrc: "#ImageSrc",
        ImageUrl: "#ImageUrl",
        CustomerId: "#CustomerId",
        Name: "#Name",
        EmailAddress: "#EmailAddress",
        CompanyDrpDown: "#CompanyId"
        
    },
    Services: {

        Controller: "Customer",
        Actions: {
            CheckCustoer: "IsCustomerExists",
            CompaniesDropDownSource: "CompaniesDropDownSource"
        }
    },
    CallBacks: {
        IsCustomerExists: function (user) {


            var url = [RequestHandler.getSiteBase(), "/", Customers.Services.Controller, "/", Customers.Services.Actions.CheckCustoer].join("");

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
                window.location.href = RequestHandler.getSiteBase();
            }
        }
    },
    InitEvents: function () {


     
      

        $(Customers.Selectors.ImageUrl + ":not(.bound)").addClass("bound").change(function () {

            var imageInput = $(this);

            if (this.files && this.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {

                    $(imageInput.closest("form").find(Customers.Selectors.ImageSrc)).val(e.target.result);
                }

                reader.readAsDataURL(this.files[0]);
            }
        });

        $(Customers.Selectors.IsCompanyDropDown + ":not(.bound)").addClass("bound").change(function () {
            if ($(this).val() === "true") {
                $(Customers.Selectors.CompanyItemDiv).removeClass("hide");
                RequestHandler.select2Ajax(Customers.Selectors.CompanyDrpDown, Customers.Services.Controller, Customers.Services.Actions.CompaniesDropDownSource);
            } else {
                $(Customers.Selectors.CompanyItemDiv).addClass("hide");
            }
            
        });

        $(Customers.Selectors.AddressPicker).placepicker({
            placeChanged: function (place) {

                var location = this.getLocation();
                $(Customers.Selectors.Location).val(location.latitude + "," + location.longitude);
            }
        }).data('placepicker');

        $(Customers.Selectors.Phone).intlTelInput({

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

jQuery(document).ready(function () {
    Customers.InitEvents();
});