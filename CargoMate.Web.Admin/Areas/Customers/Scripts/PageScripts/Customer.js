var CustomersTable = {
    selectors: {
        TblCustomersList: "#tblCustomersList"
    },
    services: {

        controller: "Customer",
        actions: {
            List: "GetCustomersList"
        }

    },
    domConfigurations: {
        initTable: function () {
            var utilities = IAC.Utilities;
            var listUrl = [utilities.getSiteRoot(), CustomersTable.services.controller, "/", CustomersTable.services.actions.List].join("");

            var actions = [
                {
                    actionName: " View Profile ",
                    icon: "fa fa-info-circle",
                    classes: "btnAViewProfile",
                    dataAttr: ["Id"],
                    hrefParams: [{ Name: "customerId", ValueColumn: "Id" }],
                    href: "CustomerProfile?hashcode=09ji78"
                }
            ];

            var dataTable = $(CustomersTable.selectors.TblCustomersList).DataTable({
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
                                imageUrl = url.indexOf("http") > -1 ? url : IAC.ClientAppUrl + "/SystemImages/CustomerImages/" + url;
                            }

                            return "<img src='" + imageUrl + "' class='img-circle' style='width: 60px; height: 60px;'>"
                        },
                        "sortable": false
                    },
                    { data: "Name", "sortable": true },
                    { data: "EmailAddress", "sortable": false },
                    { data: "PhoneNumber", "sortable": false },
                    { data: "Address", "sortable": false },
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
    CustomersTable.domConfigurations.initTable();
});