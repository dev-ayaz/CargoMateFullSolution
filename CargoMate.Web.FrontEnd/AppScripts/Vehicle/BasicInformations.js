var Vehicles = {
    Selectors: {
        ConfigurationDropDown: "#ConfigurationId",
        TypedropDown: "#TypeId",
        CapacityDropDown: "#CapacityId",
        MakeDropDown: "#MakeId",
        ModelDropDown: "#ModelId",
        YearDropDown: "#YearId",
        PayLoadTypesDropDown: "#PayLoadTypes",
        TripTypesDropDown: "#TripTypes"
    },
    Services: {

        Controller: "Vehicle",
        Actions: {
            ConfigurationList: "ConfigurationList",
            CapacitiesList: "CapacitiesList",
            ModelList: "ModelList",
            YearsList: "YearsList",
            PayLoadTypeList: "PayLoadTypeList",
            TriptypesList: "TriptypesList"
        }
    },
    CallBacks: {
        InsertSuccess: function (response) {
            CargoMateAlerts.actionAlert(response.MessageHeader, response.Message, response.IsError);
        },

        ConfigurationList: function ($this) {

            var $configurationDropDown = $this.closest("form").find(Vehicles.Selectors.ConfigurationDropDown);

            $configurationDropDown.val(null).trigger("change").empty();

            var options = "<option value=''>Please Select a Configuration </option>";

            var url = [RequestHandler.getSiteRoot(), Vehicles.Services.controller, "/", Vehicles.Services.Actions.ConfigurationList].join("");

            var typeId = $this.val();

            if (typeId) {

                $.getJSON(url, { typeId: typeId }, function (data) {

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

            var url = [RequestHandler.getSiteRoot(), Vehicles.Services.controller, "/", Vehicles.Services.Actions.PayLoadTypeList].join("");

            var typeId = $this.val();

            if (typeId) {

                $.getJSON(url, { typeId: typeId }, function (data) {

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

            var $capacityDropDown = $this.closest("form").find(Vehicles.Selectors.CapacityDropDown);

            $capacityDropDown.val(null).trigger("change").empty();

            var options = "<option value=''>Please Select a Capacity </option>";

            var url = [RequestHandler.getSiteRoot(), Vehicles.Services.controller, "/", Vehicles.Services.Actions.CapacitiesList].join("");

            var typeId = $this.val();

            if (typeId) {

                $.getJSON(url, { typeId: typeId }, function (data) {

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

            var $modelDropDown = $this.closest("form").find(Vehicles.Selectors.ModelDropDown);

            $modelDropDown.val(null).trigger("change").empty();

            var options = "<option value=''>Please Select a Model </option>";

            var url = [RequestHandler.getSiteRoot(), Vehicles.Services.controller, "/", Vehicles.Services.Actions.ModelList].join("");

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

            var $yearDropDown = $this.closest("form").find(Vehicles.Selectors.YearDropDown);

            $yearDropDown.val(null).trigger("change").empty();

            var options = "<option value=''>Please Select a Year </option>";

            var url = [RequestHandler.getSiteRoot(), Vehicles.Services.controller, "/", Vehicles.Services.Actions.YearsList].join("");

            var modelId = $this.val();

            if (modelId) {

                $.getJSON(url, { modelId: modelId }, function (data) {

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

            var url = [RequestHandler.getSiteRoot(), Vehicles.Services.controller, "/", Vehicles.Services.Actions.TriptypesList].join("");

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
        $(Vehicles.Selectors.TypedropDown + ":not(.bound)").addClass("bound").change(function() {

            Vehicles.CallBacks.ConfigurationList($(this));
            Vehicles.CallBacks.CapacityList($(this));
            Vehicles.CallBacks.PayLoadTypesList($(this));
        });
        $(Vehicles.Selectors.MakeDropDown + ":not(.bound)").addClass("bound").change(function () {

            Vehicles.CallBacks.ModelsList($(this));
        });
        $(Vehicles.Selectors.ModelDropDown + ":not(.bound)").addClass("bound").change(function () {

            Vehicles.CallBacks.YearList($(this));
            Vehicles.CallBacks.TripTypeList($(this));
        });
    }
}