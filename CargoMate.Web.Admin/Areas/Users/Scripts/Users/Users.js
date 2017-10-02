
var Users = {

    selectors: {
            UsersTable: "#UsersTable",

            EditButton: ".btnEdit",
            Imageinput: "#Imageinput",
            UserImage: "#UserImage",
            AddUserForm: "#UserForm",
            modalEditUser: "#modalEditUser",
            DivEditUser: "#DivEditUser",
            UserImagePreview:"#UserImagePreview"
        },

    services: {

        controller: "Users",
        actions: {
            List: "UsersList",
            Edit:"Edit"

        }

    },

    Callbacks: {

        InsertSuccess: function (response) {
            AppAlerts.actionAlert(response);

            if (!response.IsError) {

                IAC.Utilities.resetForm(Users.selectors.AddUserForm);
                $(Users.selectors.UsersTable).DataTable().draw();
            }
        }, 
        UpdateSuccess: function (response) {

            AppAlerts.actionAlert(response);

            if (!response.IsError) {

                $(Users.selectors.modalEditUser).modal("hide");

                $(Users.selectors.UsersTable).DataTable().draw();

                Users.InitEvents();
              

            }
        },
        EditUser: function ($this) {

            var userId = $this.attr("data-id");

            var utilities = IAC.Utilities;

            var url = [utilities.getSiteRoot(), Users.services.controller, "/", Users.services.actions.Edit].join("");

            utilities.postToController(url, utilities.formMethods.Get, { id: userId }, function (request) {

                $(Users.selectors.DivEditUser).html(request);

                $(Users.selectors.modalEditUser).modal("show");

                Users.InitEvents();
            });
        }

    },

    InitEvents: function () {

        $(Users.selectors.Imageinput + ":not(.bound)").addClass("bound").change(function () {

            var imageInput = $(this);

            if (this.files && this.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {

                    $(imageInput.closest("form").find(Users.selectors.UserImage)).val(e.target.result);

                    $(Users.selectors.UserImagePreview).attr("src", e.target.result);
                }

                reader.readAsDataURL(this.files[0]);
            }
        });

        $(Users.selectors.ResetRequestFormButton + ":not(.bound)").addClass("bound").click(function () {
            
            IAC.Utilities.resetForm($(Users.selectors.AddUserForm));
        });

        $(Users.selectors.EditButton + ":not(.bound)").addClass("bound").click(function () {

            Users.Callbacks.EditUser($(this));
        });
          
    },
    domConfigurations: {



        initTable: function () {
            var utilities = IAC.Utilities;
            var listUrl = [utilities.getSiteRoot(), Users.services.controller, "/", Users.services.actions.List].join("");


            var actions = [

                {
                    actionName: " Edit",
                    icon: "fa fa-pencil",
                    classes: "btnEdit",
                    dataAttr: ["Id"]

                }
                ,
                {
                    actionName: "Delete",
                    icon: "fa fa fa-times",
                    classes: "btnDelete",
                    dataAttr: ["Id"]

                }
            ];

            var dataTable = $(Users.selectors.UsersTable).dataTable({
                "bServerSide": true,
                "ajax": {
                    "url": listUrl,
                    "data": function (data) {
                        var params = IACdataTable.setDataTableFilters(data);
                        return params;
                    }
                },
                "bProcessing": false,
                "filter": true, //set to false to Disable filter (search box)
                "initComplete": function (settings, json) {
                    $(Users.selectors.UsersTable + "_filter input").unbind();
                    $(Users.selectors.UsersTable + "_filter input").bind("keyup", function (e) {
                        if (e.keyCode === 13) {
                            dataTable.fnFilter(this.value);
                        }
                    });

                },
                "drawCallback": function (settings) {
                    Users.InitEvents();
                },
                "columns": [
                    {
                        mRender: function (data, type, row, meta) {
                            return meta.row + meta.settings._iDisplayStart + 1;

                        },
                        "sortable": false, "autoWidth": false
                    },
                    {
                        mRender: function (data, type, row, meta) {

                            var imageUrl = row.ImageUrl || "avatar.png";

                            return "<img src='../../../../" + imageUrl +"' class='img-circle' style='width:50px;height:50px'/>";

                    },
                    "sortable": false, "autoWidth": false
                },
                    { data: "FirstName", "sortable": true },
                    { data: "LastName", name: "HouseMeterNumber" },
                    { data: "Email", "sortable": false },
                    { data: "PhoneNumber", "sortable": false },
                    { data: "CreationDate", "sortable": true },
                    { data: "UserRole", "sortable": false, defaultContent: "<i>Not set</i>"},
                    { data: "IsActive", name: "IsActive" },
                    {
                        mRender: function (data, type, row) { return IACdataTable.htmlWrapers.ActionMenu.replace("{li}", IACdataTable.createActionLink(actions, row)); },
                        "sortable": false, "autoWidth": false
                    }
                ],
                "aaSorting": [0, "asc"]
            });

        }
    }
}