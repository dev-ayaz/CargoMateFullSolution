var CargoMateAlerts = {

    success: function (title, msg, html, autoHide) {

        swal({
            title: title,
            text: msg,
            timer: 2000,
            type: "success",
            showConfirmButton: false
        });

    },

    error: function (title, msg, html, autoHide) {

        swal({
            title: title,
            text: msg,
            timer: 2000,
            type: "error",
            showConfirmButton: false
        });

    },

    successConfirm: function (title, msg, confirmButtonText, callback) {

        swal({
            title: title,
            text: msg,
            type: "success",
            showCancelButton: false,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: confirmButtonText || "Ok",
            closeOnConfirm: false
        },
            function () {

                callback();
            });

    },
    inlineAlert: function (msg, type, parent) {

        var $container = $('<div/>').addClass('alert').addClass('alert-' + type);

        $container.append($('<button/>').addClass('close').attr('data-close', 'alert'));

        $container.append($('<span/>').text(msg));

        $(parent).find('.alert').remove();

        $(parent).prepend($container);
    },
    // boostrap alert message
    showInlineAlert: function (msg, type, parent) {

        var $label = $('<label/>').addClass('alert').addClass('alert-' + type).css({ "width": "100%" });

        $label.text(msg);

        $(parent).prepend($label);
    },
    // toaster notification message
    actionAlert: function (title, msg, isError) {

        toastr.options = {
            "debug": false,
            "positionClass": "toast-top-right",
            "onclick": null,
            "fadeIn": 300,
            "fadeOut": 1000,
            "timeOut": 5000,
            "extendedTimeOut": 1000
        };


        if (isError) {

            toastr.error(msg, title);
        } else {

            toastr.success(msg, title);
        }
    },
    confirm: function (callback) {

        swal({
            title: "Are you sure?",
            text: "You will not be able to recover this record!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: true,
            allowOutsideClick: false
        }, callback);




    }


};