var VehicleModel = {
    selectors: {
        AddVehicleModelForm: "#AddVehicleModelForm",
        tblVehicleModelssList: "#tblVehicleModelssList",
        btnDeletModel: ".DeletModel",
        EditCapicityForm: "#EditCapicityForm",
        btnVehicleModelEdit: ".VehicleModelEdit",
        EditVehicleModelFormContent: "#EditVehicleModelFormContent",
        EditVehicleModelForm: "#EditVehicleModelForm",
        modalEditVehicleModel: "#modalEditVehicleModel",
        ImagePreView: "#ImagePreviewr",
        ImageUrl: "#ImageUrl",
        ImageInput: "#ImageInput"
    },
    services: {
        controller: "Vehicle",
        actions: {
            ModelList: "ModelList",
            DeletModel: "DeletModel",
            editVehicleModel: "EditVehicleModel"

        }
    },
    callbacks: {
        insertSuccess: function (result) {
            AppAlerts.actionAlert(result);
            if (!result.IsError) {
                $(VehicleModel.selectors.AddVehicleModelForm)[0].reset();
                var url = [IAC.Utilities.getSiteRoot(), VehicleModel.services.controller, "/", VehicleModel.services.actions.ModelList].join("");

                IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, {}, function (response) {

                    $(VehicleModel.selectors.tblVehicleModelssList).html(response);
                    VehicleModel.initEvents();
                });
            }
        },
        updateSuccess: function (result) {
            AppAlerts.actionAlert(result);
            if (!result.IsError) {
                var url = [IAC.Utilities.getSiteRoot(), VehicleModel.services.controller, "/", VehicleModel.services.actions.ModelList].join("");

                IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, {}, function (response) {

                    $(VehicleModel.selectors.tblVehicleModelssList).html(response);
                    $(VehicleModel.selectors.modalEditVehicleModel).modal("hide");
                    VehicleModel.initEvents();
                });
            }
        },
        deleteVehicleModel: function ($this) {

            var modelId = $this.attr("data-modelid");

            var url = [IAC.Utilities.getSiteRoot(), VehicleModel.services.controller, "/", VehicleModel.services.actions.DeletModel].join("");
            IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, { modelId: modelId }, function (result) {
                AppAlerts.actionAlert(result);
                if (!result.IsError) {
                    $this.closest("tr").remove();
                }
            });
        },
        editVehicleModel: function ($this) {

            var modelId = $this.attr("data-modelid");

            var url = [IAC.Utilities.getSiteRoot(), VehicleModel.services.controller, "/", VehicleModel.services.actions.editVehicleModel].join("");
            IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, { modelId: modelId }, function (result) {
                $(VehicleModel.selectors.EditVehicleModelFormContent).html(result);
                $(VehicleModel.selectors.modalEditVehicleModel).modal("toggle");
                $("select").select2({
                    dropdownAutoWidth: true,
                    width: false
                });
            });

        }
    },
    initEvents: function () {

        $(VehicleModel.selectors.btnDeletModel).click(function () {
            var $this = $(this);
            AppAlerts.confirm(function (isConfirm) {
                if (isConfirm) {
                    VehicleModel.callbacks.deleteVehicleModel($this);
                }

            });
        });

        $(VehicleModel.selectors.btnVehicleModelEdit).click(function () {
            var $this = $(this);
            VehicleModel.callbacks.editVehicleModel($this);
        });

       
        $(VehicleModel.selectors.ImageInput + ":not(.bound)").addClass("bound").change(function () {

            var imageInput = $(this);

            if (this.files && this.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {

                    $(imageInput.closest("form").find(VehicleModel.selectors.ImageUrl)).val(e.target.result);

                    $(VehicleModel.selectors.ImagePreView).attr("src", e.target.result);
                }

                reader.readAsDataURL(this.files[0]);
            }
        });
    }
};