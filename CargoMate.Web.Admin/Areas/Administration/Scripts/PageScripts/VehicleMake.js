var VehicleMake = {
    selectors: {
        AddVehicleMakeForm: "#AddVehicleMakeForm",
        tblVehicleMakesList: "#tblVehicleMakesList",
        btnVehicleMakeDelete: ".VehicleMakeDelete",
        btnVehicleMakeEdit: ".VehicleMakeEdit",
        EditVehicleMakeFormContent: "#EditVehicleMakeFormContent",
        EditVehicleMakeForm: "#UpdateVehicleMakeForm",
        modalEditVehicleMake: "#modalEditVehicleMake",
        ImagePreView: "#ImagePreviewr",
        ImageUrl: "#ImageUrl",
        ImageInput: "#ImageInput"
    },
    services: {
        controller: "Vehicle",
        actions: {
            MakeList: "MakeList",
            DeletMake: "DeletMake",
            EditVehicleMake: "EditVehicleMake"

        }
    },
    callbacks: {
        insertSuccess: function (result) {
            AppAlerts.actionAlert(result);
            if (!result.IsError) {
                $(VehicleMake.selectors.AddVehicleMakeForm)[0].reset();
                var url = [IAC.Utilities.getSiteRoot(), VehicleMake.services.controller, "/", VehicleMake.services.actions.MakeList].join("");

                IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, {}, function (response) {

                    $(VehicleMake.selectors.tblVehicleMakesList).html(response);
                    VehicleMake.initEvents();
                });
            }
        },
        updateSuccess: function (result) {
            AppAlerts.actionAlert(result);
            if (!result.IsError) {
                var url = [IAC.Utilities.getSiteRoot(), VehicleMake.services.controller, "/", VehicleMake.services.actions.MakeList].join("");

                IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, {}, function (response) {

                    $(VehicleMake.selectors.tblVehicleMakesList).html(response);
                    $(VehicleMake.selectors.modalEditVehicleMake).modal("hide");
                    VehicleMake.initEvents();
                });
            }
        },
        deleteVehicleMake: function ($this) {

            var makeId = $this.attr("data-makeid");

            var url = [IAC.Utilities.getSiteRoot(), VehicleMake.services.controller, "/", VehicleMake.services.actions.DeletMake].join("");
            IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, { makeId: makeId }, function (result) {
                AppAlerts.actionAlert(result);
                if (!result.IsError) {
                    $this.closest("tr").remove();
                }
            });
        },
        EditVehicleMake: function ($this) {

            var makeId = $this.attr("data-makeid");

            var url = [IAC.Utilities.getSiteRoot(), VehicleMake.services.controller, "/", VehicleMake.services.actions.EditVehicleMake].join("");
            IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, { makeId: makeId }, function (result) {
                $(VehicleMake.selectors.EditVehicleMakeFormContent).html(result);
                $(VehicleMake.selectors.modalEditVehicleMake).modal("toggle");
                $("select").select2({
                    dropdownAutoWidth: true,
                    width: false
                });
            });

            
        }
    },
    initEvents: function () {

        $(VehicleMake.selectors.btnVehicleMakeDelete).click(function () {
            var $this = $(this);
            AppAlerts.confirm(function (isConfirm) {
                if (isConfirm) {
                    VehicleMake.callbacks.deleteVehicleMake($this);
                }

            });
        });

        $(VehicleMake.selectors.btnVehicleMakeEdit).click(function () {
            var $this = $(this);
            VehicleMake.callbacks.EditVehicleMake($this);
        });


        $(document).on("change", VehicleMake.selectors.ImageInput, function () {
            var imageInput = $(this);

            if (this.files && this.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {

                    $(imageInput.closest("form").find(VehicleMake.selectors.ImageUrl)).val(e.target.result);

                    $(VehicleMake.selectors.ImagePreView).attr("src", e.target.result);
                }

                reader.readAsDataURL(this.files[0]);
            }
        });

    }
};