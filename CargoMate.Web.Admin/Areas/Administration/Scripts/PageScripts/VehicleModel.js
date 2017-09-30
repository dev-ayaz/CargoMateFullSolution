var VehicleModel = {
    selectors: {
        AddVehicleModelForm: "#AddVehicleModelForm",
        tblVehicleModelssList: "#tblVehicleModelssList",
        btnDeletModel: ".DeletModel",
        EditCapicityForm: "#EditCapicityForm",
        btnVehicleModelEdit: ".VehicleModelEdit",
        EditVehicleModelFormContent: "#EditVehicleModelFormContent",
        EditVehicleModelForm: "#EditVehicleModelForm",
        modalEditVehicleModel: "#modalEditVehicleModel"
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

        $(VehicleModel.selectors.AddVehicleModelForm).submit(function (e) {

            e.preventDefault();
            var action = $(VehicleModel.selectors.AddVehicleModelForm).attr("action");
            var formData = new FormData($(VehicleModel.selectors.AddVehicleModelForm).get(0));
            console.log(formData);

            $.ajax({
                type: "POST",
                url: action,
                data: formData,
                dataType: "json",
                contentType: false,
                processData: false,
                success: function (data) {
                    VehicleModel.callbacks.insertSuccess(data);
                }
            });
            return false;
        });

        $(VehicleModel.selectors.EditVehicleModelForm).submit(function (e) {

            e.preventDefault();
            var action = $(VehicleModel.selectors.EditVehicleModelForm).attr("action");
            var formData = new FormData($(VehicleModel.selectors.EditVehicleModelForm).get(0));
            console.log(formData);

            $.ajax({
                type: "POST",
                url: action,
                data: formData,
                dataType: "json",
                contentType: false,
                processData: false,
                success: function (data) {
                    VehicleModel.callbacks.updateSuccess(data);
                }
            });
            return false;
        });

    }
};