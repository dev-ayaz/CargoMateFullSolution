var Length = {
    selectors: {
        AddLengthForm: "#AddLengthForm",
        tblLengthList: "#tblLengthList",
        btnLengthDelete: ".LengthDelete",
        btnLengthEdit: ".LengthEdit",
        EditLengthFormContent: "#EditLengthFormContent",
        EditLengthForm: "#UpdateLengthForm",
        modalEditLength: "#EditLengthModal"
    },
    services: {
        controller: "Vehicle",
        actions: {
            LengthList: "LengthList",
            DeleteLength: "DeleteLength",
            EditLength: "EditLength"

        }
    },
    callbacks: {
        insertSuccess: function (result) {
            AppAlerts.actionAlert(result);
            if (!result.IsError) {
                $(Length.selectors.AddLengthForm)[0].reset();
                var url = [IAC.Utilities.getSiteRoot(), Length.services.controller, "/", Length.services.actions.LengthList].join("");

                IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, {}, function (response) {

                    $(Length.selectors.tblLengthList).html(response);
                    Length.initEvents();
                });
            }
        },
        updateSuccess: function (result) {
            AppAlerts.actionAlert(result);
            if (!result.IsError) {
                var url = [IAC.Utilities.getSiteRoot(), Length.services.controller, "/", Length.services.actions.LengthList].join("");

                IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, {}, function (response) {

                    $(Length.selectors.tblLengthList).html(response);
                    $(Length.selectors.modalEditLength).modal("hide");
                    Length.initEvents();
                });
            }
        },
        deleteLength: function ($this) {

            var lengthId = $this.attr("data-id");

            var url = [IAC.Utilities.getSiteRoot(), Length.services.controller, "/", Length.services.actions.DeleteLength].join("");
            IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, { lengthId: lengthId }, function (result) {
                AppAlerts.actionAlert(result);
                if (!result.IsError) {
                    $this.closest("tr").remove();
                }
            });
        },
        EditLength: function ($this) {

            var LengthId = $this.attr("data-id");

            var url = [IAC.Utilities.getSiteRoot(), Length.services.controller, "/", Length.services.actions.EditLength].join("");
            IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, { LengthId: LengthId }, function (result) {
                $(Length.selectors.EditLengthFormContent).html(result);
                $(Length.selectors.modalEditLength).modal("toggle");
                $("select").select2({
                    dropdownAutoWidth: true,
                    width: false
                });
            });

        }
    },
    initEvents: function () {

        $(Length.selectors.btnLengthDelete).click(function () {
            var $this = $(this);
            AppAlerts.confirm(function (isConfirm) {
                if (isConfirm) {
                    Length.callbacks.deleteLength($this);
                }

            });
        });

        $(Length.selectors.btnLengthEdit).click(function () {
            var $this = $(this);
            Length.callbacks.EditLength($this);
        });

    }
};