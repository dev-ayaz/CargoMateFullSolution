var DriversTable = {
    selectors: {
        TblDriversList: "#tblDriversList"
    },
    services: {

        controller: "Driver",
        actions: {
            Edit: "EditDriver",
            Delete: "DeleteDriver",
            List: "GetDriversList"
        }

    },
    domConfigurations: {
        initTable: function () {
            var utilities = IAC.Utilities;
            var listUrl = [utilities.getSiteRoot(), DriversTable.services.controller, "/", DriversTable.services.actions.List].join("");
            var DriverDetailsUrl = [utilities.getSiteBase(), "/Transporters/Driver/GetDriversList"].join("");

            var actions = [
                {
                    actionName: " View Profile ",
                    icon: "fa fa-info-circle",
                    classes: "btnAViewProfile",
                    dataAttr: ["Id"],
                    hrefParams: [{ Name: "driverId", ValueColumn: "Id" }],
                    href: "DriverProfile?hashcode=09ji78"
                }
            ];

            var dataTable = $(DriversTable.selectors.TblDriversList).DataTable({
                "bServerSide": true,
                "bSortCellsTop": true,
                "ajax": {
                    "url": listUrl,
                    "data": function (data) {
                        var params = IACdataTable.setDataTableFilters(data);
                        //params.Status = $(DriversTable.selectors.searchFilterSelectors.Status).attr("data-status");
                        return params;
                    }
                },
                "bProcessing": false,
                "filter": false, //set to false to Disable filter (search box)
                "drawCallback": function (settings) {

                },
                "columns": [

                    {
                        mRender: function (data, type, row) {
                            var url = row.ImageUrl;

                            var imageUrl = 'assets/img/placeholder.jpg';
                            if (url != null) {
                                imageUrl = url.indexOf("http") > -1 ? url : IAC.ClientAppUrl + "/SystemImages/DriverImages/" + url;
                            }

                            return "<img src='" + imageUrl + "' class='img-circle' style='width: 60px; height: 60px;'>"
                        },
                        "sortable": false
                    },
                    { data: "Name", "sortable": true },
                    { data: "LegalName", "sortable": false },
                    { data: "EmailAddress", "sortable": false },
                    { data: "PhoneNumber", "sortable": false },
                    { data: "Status", "sortable": false, defaultContent: "<i>Not set</i>" },
                    {
                        mRender: function (data, type, row) { return IACdataTable.htmlWrapers.ActionMenu.replace("{li}", IACdataTable.createActionLink(actions, row)); },
                        "sortable": false
                    }
                ],
                "aaSorting": [1, "asc"],
                "createdRow": function (row, data, index) {
                    $("td", row).eq(4).addClass(data.Priority);

                }
            });

        } // end initTable

    } // end domConfigurations
};

jQuery(document).ready(function () {
    DriversTable.domConfigurations.initTable();
});