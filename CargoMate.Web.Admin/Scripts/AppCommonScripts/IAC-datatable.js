
var IACdataTable = {
    services: {
        controller: "Account",
        actions: {
            Login: "Login"
        }
    },
    htmlWrapers: {
        ActionMenu: "<div class='btn-group'>" +
        "<a class='btn btn-sm green dropdown-toggle' href='javascript:;' data-toggle='dropdown' aria-expanded='false'>" +
        "<i class='fa fa-cog'></i>" +
        "  Actions  " +
        "<i class='fa fa-angle-down'></i>" +
        "</a>" +
        "<ul class='dropdown-menu' role='menu'>" +
        "{li}" +
        "</ul>" +
        "</div>"
    },

    createActionLink: function (actions, row) {

        var options = "";
        $.map(actions, function (item) {

            var $div = $("<div />");
            var actionLink = $("<a />").addClass(item.classes).html("<i class='" + item.icon + "'></i>" + item.actionName);

            $.map(item.dataAttr, function (attr) {
                $(actionLink).attr("data-" + attr, row[attr]);
            });

            if (item.href) {

                var href = item.href;

                $.map(item.hrefParams, function (ele) {

                    href += "&" + ele.Name + "=" + row[ele.ValueColumn];

                });

                $(actionLink).attr("href", href);

                
            }

            if (item.target) {
                $(actionLink).attr("target", item.target);
            }
            options += $div.append($("<li />").append(actionLink)).html();

        });

        return options;

    },
    setDataTableFilters: function (data) {
        var obj = {};
        obj.PageLenght = data.length;
        obj.OrderDirection = data.order[0].dir === "asc" ? 2 : 1;
        obj.Start = data.start;
        obj.SearchTerm = data.search.value;
        obj.Draw = data.draw;
        obj.OrderBy = data.columns[data.order[0].column].name;
        return obj;
    }

}