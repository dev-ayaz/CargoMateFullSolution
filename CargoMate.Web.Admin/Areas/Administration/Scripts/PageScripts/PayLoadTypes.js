var PayLoadTypes = {
    selectors: {
        AddPayLoadtypeForm: "#AddPayLoadtypeForm",
        tblPayLoadTypeList: "#tblPayLoadTypeList",
        btnDeletePayLoadTypes: ".PayLoadDelete",
        btnPayLoadTypeEdit: ".PayLoadEdit",
        UpdatePayLoadTypeForm: "#UpdatePayLoadTypeForm",
        EditPayLoadTypeContent: "#EditPayLoadTypeContent",
        modalEditPayLoadType: "#modalEditPayLoadType",
        ImagePreView: "#ImagePreviewr",
        ImageUrl: "#ImageUrl",
        ImageInput: "#ImageInput"
    },
    services: {
        controller: "Vehicle",
        actions: {
            PayLoadTypeList: "PayLoadTypeList",
            DeletePayLoadTypes: "DeletePayLoadTypes",
            PayLoadEdit: "PayLoadEdit"

        }
    },
    callbacks: {
        insertSuccess: function (result) {
            AppAlerts.actionAlert(result);
            if (!result.IsError) {
                $(PayLoadTypes.selectors.AddPayLoadtypeForm)[0].reset();
                var url = [IAC.Utilities.getSiteRoot(), PayLoadTypes.services.controller, "/", PayLoadTypes.services.actions.PayLoadTypeList].join("");

                IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, {}, function (response) {

                    $(PayLoadTypes.selectors.tblPayLoadTypeList).html(response);
                    PayLoadTypes.initEvents();
                });
            }
        },
        updateSuccess: function (result) {
            AppAlerts.actionAlert(result);
            if (!result.IsError) {
                var url = [IAC.Utilities.getSiteRoot(), PayLoadTypes.services.controller, "/", PayLoadTypes.services.actions.PayLoadTypeList].join("");

                IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, {}, function (response) {

                    $(PayLoadTypes.selectors.tblPayLoadTypeList).html(response);
                    PayLoadTypes.initEvents();
                    $(PayLoadTypes.selectors.modalEditPayLoadType).modal("toggle");
                });
            }
            },
        deletePayloadType: function ($this) {

            var payloadId = $this.attr("data-payloadid");

            var url = [IAC.Utilities.getSiteRoot(), PayLoadTypes.services.controller, "/", PayLoadTypes.services.actions.DeletePayLoadTypes].join("");
            IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, { payloadId: payloadId }, function (result) {
                AppAlerts.actionAlert(result);
                if (!result.IsError) {
                    $this.closest("tr").remove();
                }
            });


        },
        EditPayLoadType: function ($this) {

            var payloadId = $this.attr("data-payloadid");

            var url = [IAC.Utilities.getSiteRoot(), PayLoadTypes.services.controller, "/", PayLoadTypes.services.actions.PayLoadEdit].join("");
            IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, { payloadId: payloadId }, function (result) {
                $(PayLoadTypes.selectors.EditPayLoadTypeContent).html(result);
                $(PayLoadTypes.selectors.modalEditPayLoadType).modal("toggle");
                $("select").select2({
                    dropdownAutoWidth: true,
                    width: false
                });
            });
        }
    },
    initEvents: function () {


        $(PayLoadTypes.selectors.btnDeletePayLoadTypes).click(function () {
            var $this = $(this);
            AppAlerts.confirm(function (isConfirm) {
                if (isConfirm) {
                    PayLoadTypes.callbacks.deletePayloadType($this);
                }

            });
        });

        $(PayLoadTypes.selectors.btnPayLoadTypeEdit).click(function () {
            var $this = $(this);
            PayLoadTypes.callbacks.EditPayLoadType($this);
        });


        $(document).on("change", PayLoadTypes.selectors.ImageInput, function () {

            var imageInput = $(this);

            if (this.files && this.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {

                    $(imageInput.closest("form").find(PayLoadTypes.selectors.ImageUrl)).val(e.target.result);

                    $(PayLoadTypes.selectors.ImagePreView).attr("src", e.target.result);
                }

                reader.readAsDataURL(this.files[0]);
            }
        });

    }
};