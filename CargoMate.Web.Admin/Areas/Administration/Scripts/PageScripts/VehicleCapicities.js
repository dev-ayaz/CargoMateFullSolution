var VehicleCapicities = {
    selectors: {
        AddVehicleCapicityForm: "#AddVehicleCapicityForm",
        tblVehicleCapicitiesList: "#tblVehicleCapicitiesList",
        btnVehicleCapicityDelete: ".VehicleCapicityDelete",
        EditCapicityForm: "#EditCapicityForm",
        btnVehicleCapicityEdit: ".VehicleCapicityEdit",
        EditVehicleCapicityFormContent: "#EditVehicleCapicityFormContent",
        EditVehicleCapicityForm: "#EditVehicleCapicityForm",
        modalEditVehicleCapicity: "#modalEditVehicleCapicity"
    },
    services: {
        controller: "Vehicle",
        actions: {
            VehicleCapicitiesList: "VehicleCapacitiesList",
            VehicleCapicityDelete: "VehicleCapacityDelete",
            EditVehicleCapicity: "EditVehicleCapacity"
          
        }
    },
    callbacks: {
        insertSuccess: function (result) {
            AppAlerts.actionAlert(result);
            if (!result.IsError) {
                $(VehicleCapicities.selectors.AddVehicleCapicityForm)[0].reset();
                var url = [IAC.Utilities.getSiteRoot(), VehicleCapicities.services.controller, "/", VehicleCapicities.services.actions.VehicleCapicitiesList].join("");

                IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, {}, function (response) {

                    $(VehicleCapicities.selectors.tblVehicleCapicitiesList).html(response);
                    VehicleCapicities.initEvents();
                });
            }
        },
        updateSuccess: function (result) {
            AppAlerts.actionAlert(result);
            if (!result.IsError) {
                var url = [IAC.Utilities.getSiteRoot(), VehicleCapicities.services.controller, "/", VehicleCapicities.services.actions.VehicleCapicitiesList].join("");

                IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, {}, function (response) {

                    $(VehicleCapicities.selectors.tblVehicleCapicitiesList).html(response);
                    $(VehicleCapicities.selectors.modalEditVehicleCapicity).modal("hide");
                    VehicleCapicities.initEvents();
                });
            }
        },
        deleteVehicleCapicity: function ($this) {

            var capicityId = $this.attr("data-capicityid");

            var url = [IAC.Utilities.getSiteRoot(), VehicleCapicities.services.controller, "/", VehicleCapicities.services.actions.VehicleCapicityDelete].join("");
            IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, { capacityId: capicityId }, function (result) {
                AppAlerts.actionAlert(result);
                if (!result.IsError) {
                    $this.closest("tr").remove();
                }
            });
        },
        editVehicleCapicity: function ($this) {

            var capicityId = $this.attr("data-capicityid");

            var url = [IAC.Utilities.getSiteRoot(), VehicleCapicities.services.controller, "/", VehicleCapicities.services.actions.EditVehicleCapicity].join("");
            IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, { capacityId: capicityId }, function (result) {
                $(VehicleCapicities.selectors.EditVehicleCapicityFormContent).html(result);
                $(VehicleCapicities.selectors.modalEditVehicleCapicity).modal("toggle");
                $("select").select2({
                    dropdownAutoWidth: true,
                    width: false
                });
            });
           
        }
    },
    initEvents: function () {

        $(VehicleCapicities.selectors.btnVehicleCapicityDelete).click(function () {
            var $this = $(this);
            AppAlerts.confirm(function (isConfirm) {
                if (isConfirm) {
                    VehicleCapicities.callbacks.deleteVehicleCapicity($this);
                }

            });
        });

        $(VehicleCapicities.selectors.btnVehicleCapicityEdit).click(function () {
            var $this = $(this);
            VehicleCapicities.callbacks.editVehicleCapicity($this);
        });
        
    }
};