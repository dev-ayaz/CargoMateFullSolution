var Roles = {

    Selectors: {

        divRolesList: "#divRolesList",
        RolesForm: "#RolesForm",
        modalEditRole: "#modalEditRole",
        divEditRole: "#DivEditRole",
        RoleEditButton: ".RoleEdit",
        RoleDeleteButton:".RoleDelete"
    },

    Services: {

        controller: "Roles",

        actions: {

            List: "RolesList",
            Edit: "EditRole",
            Delete: "DeleteRole"

        }
    },

    Callbacks: {
        InsertSuccess: function (response) {

            AppAlerts.actionAlert(response);

            if (!response.IsError) {

                IAC.Utilities.resetForm(Roles.Selectors.RolesForm);

                Roles.Callbacks.ReloadRolesList();

            }
        },
        UpdateSuccess: function (response) {

            AppAlerts.actionAlert(response);

            if (!response.IsError) {

                Roles.Callbacks.ReloadRolesList();

                $(Roles.Selectors.modalEditRole).modal("hide");

            }
        },
        ReloadRolesList:function() {
            var utilities = IAC.Utilities;
            var url = [utilities.getSiteRoot(), Roles.Services.controller, "/", Roles.Services.actions.List].join("");

            utilities.postToController(url, utilities.formMethods.Get, {}, function (rolesList) {

                $(Roles.Selectors.divRolesList).html(rolesList);

                Roles.InitEvents();
            });
        },
        Edit: function ($this) {

            var roleId = $this.attr("data-id");

            var utilities = IAC.Utilities;
            var url = [utilities.getSiteRoot(), Roles.Services.controller, "/", Roles.Services.actions.Edit].join("");

            utilities.postToController(url, utilities.formMethods.Get, { id: roleId }, function (role) {

                $(Roles.Selectors.divEditRole).html(role);

                $(Roles.Selectors.modalEditRole).modal("show");

                Roles.InitEvents();
            });
        },
        Delete: function ($this) {

            var roleId = $this.attr("data-id");

            var utilities = IAC.Utilities;

            var url = [utilities.getSiteRoot(), Roles.Services.controller, "/", Roles.Services.actions.Delete].join("");

            utilities.postToController(url, utilities.formMethods.Get, { id: roleId }, function (response) {

                AppAlerts.actionAlert(response);

                if (!response.IsError) {

                    $this.closest("tr").remove();
                }

                Roles.InitEvents();
            });
        }
    },
    InitEvents: function () {


        $(Roles.Selectors.RoleEditButton + ":not(.bound)").addClass("bound").click(function () {
            var $this = $(this);

            Roles.Callbacks.Edit($this);

        });

        $(Roles.Selectors.RoleDeleteButton + ":not(.bound)").addClass("bound").click(function () {

            var $this = $(this);

            AppAlerts.confirm(function (isConfirm) {
                if (isConfirm) {
                    Roles.Callbacks.Delete($this);
                }
            });
          
        });



    }
};