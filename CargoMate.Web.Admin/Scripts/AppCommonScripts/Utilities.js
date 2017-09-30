var permissionsStore;
IAC.Utilities = {
    services: {
        controller: {
            DashboardController: "Dashboard"
        },
        actions: {
            permissions: "Permissions"
        }
    },
    getSiteBase: function () {

        var siteRoot = [
           window.location.protocol,
           "//",
           window.location.hostname
        ].join("");

        if (window.location.port !== "") {
            siteRoot += ":" + window.location.port;
        };

        return siteRoot;
    },

    getSiteRoot: function () {

        var siteRoot = [
            window.location.protocol,
            "//",
            window.location.hostname
        ].join("");

        if (window.location.port !== "") {
            siteRoot += ":" + window.location.port;
        };

        siteRoot += "/" + window.location.pathname.split("/").splice(1, 1);

        if (siteRoot.lastIndexOf("/") !== siteRoot.length - 1) {
            siteRoot += "/";
        };

        return siteRoot;
    },

    getQueryString: function (field, url) {

        debugger;

        field = field.replace(/[\[\]]/g, "\\$&");
        var regex = new RegExp("[?&]" + field + "(=([^&#]*)|&|#|$)"),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return "";
        return decodeURIComponent(results[2].replace(/\+/g, " "));

        //var query = $.url(url).attr("query");

        //if (query.indexOf("?") < 0) {
        //    query = "?" + query;
        //}

        //return $.url(query).param(field);
    },


    formMethods: {

        Post: "POST",
        Get: "GET"
    },

    postToController: function (url, formMethod, data, callback) {

        $.ajax({
            url: url,
            type: formMethod,
            data: data,
            success: function (response) {
                callback(response);
            },
            error: function (err, exception) {
                console.log("Failed to post to server " + err.responseText);
            }
        });

    },

    getFromAction: function (url, data, callback) {

        $.get(url, function (result) {
            callback(result);

        }, function (err) {

            console.log(err.responseBody);
        });
    },
    reintializeJqueryValidations: function (form) {
        $(form).removeData("validator");
        $(form).removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(form);
    },
    getPermissions: function () {
        var url = [utils.getSiteRoot(), utils.services.controller.DashboardController, "/", utils.services.actions.permissions].join("");
        $.ajax({
            url: url,
            dataType: "json",
            async: false,
            data: {},
            success: function (data) {
                console.log(data);
                permissionsStore = data;
            }
        });
    },
    resetJqueryValidations: function ($form) {

        $form.find("[data-valmsg-summary=true]")
            .removeClass("validation-summary-errors")
            .addClass("validation-summary-valid")
            .find("ul").empty();

        //reset unobtrusive field level, if it exists
        $form.find("[data-valmsg-replace]")
            .removeClass("field-validation-error")
            .addClass("field-validation-valid")
            .empty();

        $form.find(":input").removeClass("text-danger");

        $(".has-error").addClass("has-success").removeClass("has-error");
        
    },
    resetForm: function (form) {

        $("select").not(".dataTables_length select , #CultureCode ").val("").trigger("change");
        utils.reintializeJqueryValidations($(form));
        utils.resetJqueryValidations($(form));
        $(form)[0].reset(); 
      

    },
    urlParam : function (name) {
        var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
        if (results === null) {
            return null;
        }
        else {
            return decodeURI(results[1]) || 0;
        }
    },
    select2Ajax: function ($this,controller,action) {
        var utilities = IAC.Utilities;
        var url = [utilities.getSiteRoot(), controller, "/", action].join("");


        $($this).select2({

            //minimumInputLength: 1,
            ajax: {
                dropdownAutoWidth: true,
                width: false,
                theme: "bootstrap",
                url: url,
                dataType: "json",
                type: "POST",
                data: function (item) {
                    return {
                        term: item.term||'a',
                        page: item.page || 1
                    };
                },
                processResults: function (data, params) {

                    params.page = params.page || 1;
                    var mappedData = $.map(data, function (obj) {
                        obj.text = obj.Name;
                        obj.id = obj.id;
                        return obj;
                    });
                    return {
                        results: mappedData,
                        pagination: {
                            more: (params.page * 30) < data.TotalCount
                        }
                    };
                }

            }
        });
    }
};

var utils = IAC.Utilities;