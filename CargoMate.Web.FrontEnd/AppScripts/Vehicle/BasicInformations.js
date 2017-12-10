var Vehicles = {
    Selectors: {
        VehicleConfigurationDropDown: "#VehicleConfigurationId",
        VehicleTypeDropDown: "#VehicleTypeId",
        VehicleCapacityDropDown: "#VehicleCapacityId",
        VehicleMakeDropDown: "#VehicleMakeId",
        VehicleModelDropDown: "#VehicleModelId",
        VehicleModelYearDropDown: "#VehicleModelYearId",
        PayLoadTypesDropDown: "#PayLoadTypeIds",
        TripTypesDropDown: "#TripTypeIds",
        ImageUrl: "#RegistrationImage",
        ImageSrc: "#ImageSrc",
        CompanyDrpDown:"InsuranceCompanyId"
    },
    Services: {

        Controller: "Vehicle",
        Actions: {
            MakeAutoComplete:"MakeAutoComplete",
            VehicleConfigurationsAutoComplete: "VehicleConfigurationsAutoComplete",
            VehicleCapacitiesAutoComplete: "VehicleCapacitiesAutoComplete",
            ModelsAutoComplete: "ModelsAutoComplete",
            ModelYearAutoComplete: "ModelYearAutoComplete",
            PayloadTypesAutoComplete: "PayloadTypesAutoComplete",
            CompaniesDropDownSource:"CompaniesDropDownSource"
        }
    },
    CallBacks: {
        InsertSuccess: function (response) {
            CargoMateAlerts.actionAlert(response.MessageHeader, response.Message, response.IsError);
        },
        VehicleMakes: function ($this) {
 
            var $makeDropDown = $this.closest("form").find(Vehicles.Selectors.VehicleMakeDropDown);

            $makeDropDown.val(null).trigger("change").empty();

            var options = "<option value=''>Please select vehicle make </option>";

            var url = [RequestHandler.getSiteBase(), "/", Vehicles.Services.Controller, "/", Vehicles.Services.Actions.MakeAutoComplete].join("");

            var typeId = $this.val();

            if (typeId) {

                $.getJSON(url, { vehicletypeId: typeId }, function (data) {

                    $.each(data,
                        function (i, item) {
                            options += "<option value='" + item.Value + "'> " + item.Text + "</option>";
                        });

                    $makeDropDown.html(options);
                });
            }
            else {
                $makeDropDown.html(options);
            }
        },
        ConfigurationList: function ($this) {

            var $configurationDropDown = $this.closest("form").find(Vehicles.Selectors.VehicleConfigurationDropDown);

            $configurationDropDown.val(null).trigger("change").empty();

            var options = "<option value=''>Please Select a Configuration </option>";

            var url = [RequestHandler.getSiteBase(), "/", Vehicles.Services.Controller, "/", Vehicles.Services.Actions.VehicleConfigurationsAutoComplete].join("");

            var typeId = $this.val();

            if (typeId) {

                $.getJSON(url, { vehicletypeId: typeId }, function (data) {

                    $.each(data,
                        function (i, item) {
                            options += "<option value='" + item.Value + "'> " + item.Text + "</option>";
                        });

                    $configurationDropDown.html(options);
                });
            }
            else {
                $configurationDropDown.html(options);
            }
        },
        PayLoadTypesList: function ($this) {

            var $payLoadTypeDropDown = $this.closest("form").find(Vehicles.Selectors.PayLoadTypesDropDown);

            $payLoadTypeDropDown.val(null).trigger("change").empty();

            var options = "<option value=''>Please Select Pay Load Types </option>";

            var url = [RequestHandler.getSiteBase(), "/", Vehicles.Services.Controller, "/", Vehicles.Services.Actions.PayloadTypesAutoComplete].join("");

            var typeId = $this.val();

            if (typeId) {

                $.getJSON(url, { vehicletypeId: typeId }, function (data) {

                    $.each(data,
                        function (i, item) {
                            options += "<option value='" + item.Value + "'> " + item.Text + "</option>";
                        });

                    $payLoadTypeDropDown.html(options);
                });
            }
            else {
                $payLoadTypeDropDown.html(options);
            }
        },
        CapacityList: function ($this) {

            var $capacityDropDown = $this.closest("form").find(Vehicles.Selectors.VehicleCapacityDropDown);

            $capacityDropDown.val(null).trigger("change").empty();

            var options = "<option value=''>Please Select a Capacity </option>";

            var url = [RequestHandler.getSiteBase(), "/", Vehicles.Services.Controller, "/", Vehicles.Services.Actions.VehicleCapacitiesAutoComplete].join("");

            var typeId = $this.val();

            if (typeId) {

                $.getJSON(url, { vehicletypeId: typeId }, function (data) {

                    $.each(data,
                        function (i, item) {
                            options += "<option value='" + item.Value + "'> " + item.Text + "</option>";
                        });

                    $capacityDropDown.html(options);
                });
            }
            else {
                $capacityDropDown.html(options);
            }
        },
        ModelsList: function ($this) {

            var $modelDropDown = $this.closest("form").find(Vehicles.Selectors.VehicleModelDropDown);

            $modelDropDown.val(null).trigger("change").empty();

            var options = "<option value=''>Please Select a Model </option>";

            var url = [RequestHandler.getSiteBase(), "/", Vehicles.Services.Controller, "/", Vehicles.Services.Actions.ModelsAutoComplete].join("");

            var makeId = $this.val();

            if (makeId) {

                $.getJSON(url, { makeId: makeId }, function (data) {

                    $.each(data,
                        function (i, item) {
                            options += "<option value='" + item.Value + "'> " + item.Text + "</option>";
                        });

                    $modelDropDown.html(options);
                });
            }
            else {
                $modelDropDown.html(options);
            }
        },
        YearList: function ($this) {

            var $yearDropDown = $this.closest("form").find(Vehicles.Selectors.VehicleModelYearDropDown);

            $yearDropDown.val(null).trigger("change").empty();

            var options = "<option value=''>Please Select a Year </option>";

            var url = [RequestHandler.getSiteBase(), "/", Vehicles.Services.Controller, "/", Vehicles.Services.Actions.ModelYearAutoComplete].join("");

            var modelId = $this.val();

            if (modelId) {

                $.getJSON(url, { modelId: modelId }, function (data) {
                    debugger;
                    $.each(data,
                        function (i, item) {
                            options += "<option value='" + item.Value + "'> " + item.Text + "</option>";
                        });

                    $yearDropDown.html(options);
                });
            }
            else {
                $yearDropDown.html(options);
            }
        },
        TripTypeList: function ($this) {

            var $tripTypesDropDown = $this.closest("form").find(Vehicles.Selectors.TripTypesDropDown);

            $tripTypesDropDown.val(null).trigger("change").empty();

            var options = "<option value=''>Please Select Trip Types </option>";

            var url = [RequestHandler.getSiteBase(), "/", Vehicles.Services.Controller, "/", Vehicles.Services.Actions.TriptypesList].join("");

            var modelId = $this.val();

            if (modelId) {

                $.getJSON(url, { modelId: modelId }, function (data) {

                    $.each(data,
                        function (i, item) {
                            options += "<option value='" + item.Value + "'> " + item.Text + "</option>";
                        });

                    $tripTypesDropDown.html(options);
                });
            }
            else {
                $tripTypesDropDown.html(options);
            }
        }
    },
    InitEvents: function () {

        RequestHandler.select2Ajax(Vehicles.Selectors.CompanyDrpDown, Vehicles.Services.Controller, Vehicles.Services.Actions.CompaniesDropDownSource);

        $(Vehicles.Selectors.ImageSrc + ":not(.bound)").addClass("bound").change(function () {

            var imageInput = $(this);

            if (this.files && this.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {

                    $(imageInput.closest("form").find(Vehicles.Selectors.ImageUrl)).val(e.target.result);
                }

                reader.readAsDataURL(this.files[0]);
            }
        });

        $(Vehicles.Selectors.VehicleTypeDropDown + ":not(.bound)").addClass("bound").change(function () {

            Vehicles.CallBacks.VehicleMakes($(this));
            Vehicles.CallBacks.ConfigurationList($(this));
            Vehicles.CallBacks.CapacityList($(this));
            Vehicles.CallBacks.PayLoadTypesList($(this));
        });
        $(Vehicles.Selectors.VehicleMakeDropDown + ":not(.bound)").addClass("bound").change(function () {

            Vehicles.CallBacks.ModelsList($(this));
        });
        $(Vehicles.Selectors.VehicleModelDropDown + ":not(.bound)").addClass("bound").change(function () {

            Vehicles.CallBacks.YearList($(this));
        });
    }
}

jQuery(document).ready(function () {
    Vehicles.InitEvents();
});