var VehicelModelYear = {
    selectors: {
        AddVehicelModelYearForm: "#AddVehicelModelYearForm",
        tblModelYearList: "#tblModelYearList",
        btnDeleteVehicelModelYear: ".DeleteVehicelModelYear",
        btnVehicleModelEdit: ".VehicleModelEdit",
        EditVehicleModelYearFormContent: "#EditVehicleModelYearFormContent",
        EditVehicleModelYearForm: "#EditVehicleModelYearForm",
        modalEditVehicleModelYear: "#modalEditVehicleModelYear",
        ImagePreView: "#ImagePreviewr",
        ImageUrl: "#ImageUrl",
        ImageInput: "#ImageInput"
    },
    services: {
        controller: "Vehicle",
        actions: {
            VehicleModelYearList: "VehicleModelYearList",
            DeleteVehicelModelYear: "DeleteVehicelModelYear",
            EditVehicleModelYear: "EditVehicleModelYear"

        }
    },
    callbacks: {
        insertSuccess: function (result) {
            AppAlerts.actionAlert(result);
            if (!result.IsError) {
                $(VehicelModelYear.selectors.AddVehicelModelYearForm)[0].reset();
                var url = [IAC.Utilities.getSiteRoot(), VehicelModelYear.services.controller, "/", VehicelModelYear.services.actions.VehicleModelYearList].join("");

                IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, {}, function (response) {

                    $(VehicelModelYear.selectors.tblModelYearList).html(response);
                    VehicelModelYear.initEvents();
                });
            }
        },
        updateSuccess: function (result) {
            AppAlerts.actionAlert(result);
            if (!result.IsError) {
                var url = [IAC.Utilities.getSiteRoot(), VehicelModelYear.services.controller, "/", VehicelModelYear.services.actions.VehicleModelYearList].join("");

                IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, {}, function (response) {

                    $(VehicelModelYear.selectors.tblModelYearList).html(response);
                    $(VehicelModelYear.selectors.modalEditVehicleModelYear).modal("hide");
                    VehicelModelYear.initEvents();
                });
            }
        },
        deleteVehicleModelYear: function ($this) {

            var modelYearId = $this.attr("data-modelyearid");
            debugger;
            var url = [IAC.Utilities.getSiteRoot(), VehicelModelYear.services.controller, "/", VehicelModelYear.services.actions.DeleteVehicelModelYear].join("");
            IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, { modelYearId: modelYearId }, function (result) {
                AppAlerts.actionAlert(result);
                if (!result.IsError) {
                    $this.closest("tr").remove();
                }
            });

            
        },
        EditVehicleModelYear: function ($this) {

            var modelYearId = $this.attr("data-modelyearid");

            var url = [IAC.Utilities.getSiteRoot(), VehicelModelYear.services.controller, "/", VehicelModelYear.services.actions.EditVehicleModelYear].join("");
            IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, { modelYearId: modelYearId }, function (result) {
                $(VehicelModelYear.selectors.EditVehicleModelYearFormContent).html(result);
                $(VehicelModelYear.selectors.modalEditVehicleModelYear).modal("toggle");
                $("select").select2({
                    dropdownAutoWidth: true,
                    width: false
                });
            });
        }
    },
    initEvents: function () {


        $(VehicelModelYear.selectors.btnDeleteVehicelModelYear).click(function () {
            var $this = $(this);
            AppAlerts.confirm(function (isConfirm) {
                if (isConfirm) {
                    VehicelModelYear.callbacks.deleteVehicleModelYear($this);
                }

            });
        });

        $(VehicelModelYear.selectors.btnVehicleModelEdit).click(function () {
            var $this = $(this);
            VehicelModelYear.callbacks.EditVehicleModelYear($this);
        });

        $(document).on("change", VehicelModelYear.selectors.ImageInput, function () {

            var imageInput = $(this);

            if (this.files && this.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {

                    $(imageInput.closest("form").find(VehicelModelYear.selectors.ImageUrl)).val(e.target.result);

                    $(VehicelModelYear.selectors.ImagePreView).attr("src", e.target.result);
                }

                reader.readAsDataURL(this.files[0]);
            }
        });
        
    }
};