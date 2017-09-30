var Weight = {
    selectors: {
        AddWeightForm: "#AddWeightForm",
        tblWeightList: "#tblWeightList",
        btnWeightDelete: ".WeightDelete",
        EditCapicityForm: "#EditCapicityForm",
        btnWeightEdit: ".WeightEdit",
        EditWeightFormContent: "#EditWeightFormContent",
        EditWeightForm: "#EditWeightForm",
        modalEditWeight: "#modalEditWeight"
    },
    services: {
        controller: "Vehicle",
        actions: {
            WeightList: "WeightList",
            DeleteWeight: "DeleteWeight",
            EditWeight: "EditWeight"

        }
    },
    callbacks: {
        insertSuccess: function (result) {
            AppAlerts.actionAlert(result);
            if (!result.IsError) {
                $(Weight.selectors.AddWeightForm)[0].reset();
                var url = [IAC.Utilities.getSiteRoot(), Weight.services.controller, "/", Weight.services.actions.WeightList].join("");

                IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, {}, function (response) {

                    $(Weight.selectors.tblWeightList).html(response);
                    Weight.initEvents();
                });
            }
        },
        updateSuccess: function (result) {
            AppAlerts.actionAlert(result);
            if (!result.IsError) {
                var url = [IAC.Utilities.getSiteRoot(), Weight.services.controller, "/", Weight.services.actions.WeightList].join("");

                IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, {}, function (response) {

                    $(Weight.selectors.tblWeightList).html(response);
                    $(Weight.selectors.modalEditWeight).modal("hide");
                    Weight.initEvents();
                });
            }
        },
        deleteWeight: function ($this) {

            var weightId = $this.attr("data-id");

            var url = [IAC.Utilities.getSiteRoot(), Weight.services.controller, "/", Weight.services.actions.DeleteWeight].join("");
            IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, { weightId: weightId }, function (result) {
                AppAlerts.actionAlert(result);
                if (!result.IsError) {
                    $this.closest("tr").remove();
                }
            });
        },
        EditWeight: function ($this) {

            var weightId = $this.attr("data-id");

            var url = [IAC.Utilities.getSiteRoot(), Weight.services.controller, "/", Weight.services.actions.EditWeight].join("");
            IAC.Utilities.postToController(url, IAC.Utilities.formMethods.Get, { weightId: weightId }, function (result) {
                $(Weight.selectors.EditWeightFormContent).html(result);
                $(Weight.selectors.modalEditWeight).modal("toggle");
                $("select").select2({
                    dropdownAutoWidth: true,
                    width: false
                });
            });

        }
    },
    initEvents: function () {

        $(Weight.selectors.btnWeightDelete).click(function () {
            var $this = $(this);
            AppAlerts.confirm(function (isConfirm) {
                if (isConfirm) {
                    Weight.callbacks.deleteWeight($this);
                }

            });
        });

        $(Weight.selectors.btnWeightEdit).click(function () {
            var $this = $(this);
            Weight.callbacks.EditWeight($this);
        });

    }
};