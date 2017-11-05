var VehicleConfigurations = {
    selectors: {
        AddVehicleConfigurationForm: "#AddVehicleConfigurationForm",
        tblVehicleConfigurationList: "#tblVehicleConfigurationList",
        btnVehicleConfigurationDelete: ".VehicleConfigurationDelete",
        btnVehicleConfigurationEdit: ".VehicleConfigurationEdit",
        EditVehicleConfigurationFormContent: "#EditVehicleConfigurationFormContent",
        modalEditVehicleConfiguration: "#EditVehicleConfigurationModal",
        UpdateVehicleConfigurationForm: "#UpdateVehicleConfigurationForm",
        ImagePreView: "#ImagePreviewr",
        ImageUrl: "#ImageUrl",
        ImageInput: "#ImageInput"
       
    },
    services: {
        controller: "Vehicle",
        actions: {
            VehicleConfigurationList: "VehicleConfigurationList",
            DeleteVehicleConfiguration: "DeleteVehicleConfiguration",
            UpdateVehicleConfiguration: "UpdateVehicleConfiguration",
            EditVehicleConfiguration: "EditVehicleConfiguration"
        }
    },
    callbacks: {
        insertSuccess: function (result) {
            AppAlerts.actionAlert(result);
            if (!result.IsError) {
                $(VehicleConfigurations.selectors.AddVehicleConfigurationForm)[0].reset();
                var url = [IAC.Utilities.getSiteRoot(), VehicleConfigurations.services.controller, "/", VehicleConfigurations.services.actions.VehicleConfigurationList].join("");

                IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, {}, function (response) {

                    $(VehicleConfigurations.selectors.tblVehicleConfigurationList).html(response);
                    $(VehicleConfigurations.selectors.modalEditVehicleConfiguration).modal("hide");
                    VehicleConfigurations.initEvents();
                });
            }
        },
        deleteVehicleConfiguration: function ($this) {

            var configurationId = $this.attr("data-configurationId");

            var url = [IAC.Utilities.getSiteRoot(), VehicleConfigurations.services.controller, "/", VehicleConfigurations.services.actions.DeleteVehicleConfiguration].join("");
            IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, { configurationId: configurationId }, function (result) {
                AppAlerts.actionAlert(result);
                if (!result.IsError) {
                    $this.closest("tr").remove();
                }
            });
        },
        editVehicleConfiguration: function ($this) {

            var configurationId = $this.attr("data-configurationid");

            var url = [IAC.Utilities.getSiteRoot(), VehicleConfigurations.services.controller, "/", VehicleConfigurations.services.actions.EditVehicleConfiguration].join("");
            IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, { configurationId: configurationId }, function (result) {
                $(VehicleConfigurations.selectors.EditVehicleConfigurationFormContent).html(result);
                $(VehicleConfigurations.selectors.modalEditVehicleConfiguration).modal("toggle");
                $("select").select2({
                    dropdownAutoWidth: true,
                    width: false
                });
            });

           
        }
    },
    initEvents: function () {

        $(VehicleConfigurations.selectors.btnVehicleConfigurationDelete).click(function () {
            var $this = $(this);
            AppAlerts.confirm(function (isConfirm) {
                if (isConfirm) {
                    VehicleConfigurations.callbacks.deleteVehicleConfiguration($this);
                }

            });
        });
        $(VehicleConfigurations.selectors.btnVehicleConfigurationEdit).click(function () {
            var $this = $(this);
            VehicleConfigurations.callbacks.editVehicleConfiguration($this);
        });


        $(document).on("change", VehicleConfigurations.selectors.ImageInput, function () {

            var imageInput = $(this);

            if (this.files && this.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {

                    $(imageInput.closest("form").find(VehicleConfigurations.selectors.ImageUrl)).val(e.target.result);

                    $(VehicleConfigurations.selectors.ImagePreView).attr("src", e.target.result);
                }

                reader.readAsDataURL(this.files[0]);
            }
        });
    }
};