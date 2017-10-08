
RequestHandler = {
    services: {
    
    },
    getSiteBase: function() {

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

    getSiteRoot: function() {

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

    getQueryString: function(name, url) {
        if (!url) url = window.location.href;
        name = name.replace(/[\[\]]/g, "\\$&");
        var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, " "));
    },


    formMethods: {
        Post: "POST",
        Get: "GET"
    },

    postToController: function(url, formMethod, data, callback) {

        $.ajax({
            url: url,
            type: formMethod,
            data: data,
            success: function(response) {
                callback(response);
            },
            error: function(err, exception) {
                console.log("Failed to post to server " + err.responseText);
            }
        });

    },
    postMultiPartForm: function(form, callback) {

        var action = $(form).attr("action");
        var formData = new FormData($(form).get(0));
        $.ajax({
            type: "POST",
            url: action,
            data: formData,
            dataType: "json",
            contentType: false,
            processData: false,
            success: function(data) {
                callback(data);

            },
            error: function(jqXHR, textStatus, errorThrown) {

            }
        });

    },

    getFromAction: function(url, data, callback) {

        $.get(url, function(result) {
            callback(result);

        }, function(err) {

            console.log(err.responseBody);
        });
    },
    reintializeJqueryValidations: function() {
        $("form").removeData("validator");
        $("form").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse("form");
    },
    select2Ajax: function ($this, controller, action) {
        debugger;
        var utilities = RequestHandler;
        var url = [utilities.getSiteBase(),"/", controller, "/", action].join("");


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
                        term: item.term || 'a',
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
