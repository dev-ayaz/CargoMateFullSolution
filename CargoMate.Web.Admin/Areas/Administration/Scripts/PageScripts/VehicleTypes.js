var VehicleTypes = {
    selectors: {
        AddVehicleTypeForm: "#AddVehicleTypeForm",
        tblVehicletypeList: "#tblVehicletypeList",
        btnVehicletypeDelete: ".VehicletypeDelete",
        AddVehicleTypeFormContent: "#AddVehicleTypeFormContent",
        btnVehicletypeEdit: ".VehicletypeEdit",
        modalAddNewVehicleType: "#modalAddNewVehicleType",
        modalEditVehicleType: "#modalEditVehicleType",
        EditVehicleTypeFormContent: "#EditVehicleTypeFormContent",
        EditVehicleTypeForm: "#EditVehicleTypeForm",
        ImagePreView: "#ImagePreviewr",
        ImageUrl: "#ImageUrl",
        ImageInput:"#ImageInput"

        
    },
    services: {
        controller: "Vehicle",
        actions: {
            AddVehicleType: "AddVehicleType",
            VehicletypeList: "VehicletypeList",
            DeleteVehicleType: "DeleteVehicleType",
            EditVehicleType: "EditVehicleType"
        }
    },
    callbacks: {
        insertSuccess: function (result) {
            AppAlerts.actionAlert(result);
            if (!result.IsError) {
                $(VehicleTypes.selectors.AddVehicleTypeForm)[0].reset();
                var url = [IAC.Utilities.getSiteRoot(), VehicleTypes.services.controller, "/", VehicleTypes.services.actions.VehicletypeList].join("");

                IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, {}, function (response) {

                    $(VehicleTypes.selectors.tblVehicletypeList).html(response);
                    
                    VehicleTypes.initEvents();
                });

 
            }
        },
        updateSuccess:function(result) {
              AppAlerts.actionAlert(result);
            if (!result.IsError) {
                var url = [IAC.Utilities.getSiteRoot(), VehicleTypes.services.controller, "/", VehicleTypes.services.actions.VehicletypeList].join("");

                IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, {}, function(response) {

                    $(VehicleTypes.selectors.tblVehicletypeList).html(response);

                    VehicleTypes.initEvents();
                });

                $(VehicleTypes.selectors.modalEditVehicleType).modal("hide");
            }
        },
        deleteVehicleType: function ($this) {
            
            var typeId = $this.attr("data-typeid");

            var url = [IAC.Utilities.getSiteRoot(), VehicleTypes.services.controller, "/", VehicleTypes.services.actions.DeleteVehicleType].join("");
            IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, { typeId: typeId }, function (result) {
                AppAlerts.actionAlert(result);
                if (!result.IsError) {
                    $this.closest("tr").remove();
                }
            });
        },
        editVehicleType: function ($this) {

            var typeId = $this.attr("data-typeid");

            var url = [IAC.Utilities.getSiteRoot(), VehicleTypes.services.controller, "/", VehicleTypes.services.actions.EditVehicleType].join("");
            IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, { typeId: typeId }, function (result) {
                $(VehicleTypes.selectors.EditVehicleTypeFormContent).html(result);
                $(VehicleTypes.selectors.modalEditVehicleType).modal("toggle");
                $("select").select2({
                    dropdownAutoWidth: true,
                    width: false
                });
            });

           
        }
    },
    initEvents: function () {

        $(VehicleTypes.selectors.btnVehicletypeDelete).click(function () {
            var $this = $(this);
            AppAlerts.confirm(function (isConfirm) {
                if (isConfirm) {
                    VehicleTypes.callbacks.deleteVehicleType($this);
                }
               
            });
        });
        $(VehicleTypes.selectors.btnVehicletypeEdit).click(function () {
            var $this = $(this);
            VehicleTypes.callbacks.editVehicleType($this);
        });

        $(VehicleTypes.selectors.ImageInput + ":not(.bound)").addClass("bound").change(function () {

            var imageInput = $(this);

            if (this.files && this.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {

                    $(imageInput.closest("form").find(VehicleTypes.selectors.ImageUrl)).val(e.target.result);

                    $(VehicleTypes.selectors.ImagePreView).attr("src", e.target.result);
                }

                reader.readAsDataURL(this.files[0]);
            }
        });
    }
};