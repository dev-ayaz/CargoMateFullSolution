var AjaxInterceptor = function() {

    $(document)

        .ajaxStart(function () {
            App.blockUI({
                animate: true,
                overlayColor: 'none'
            });
            //App.blockUI({
            //    boxed: true
            //});
        })

        .ajaxStop(function() {
            App.unblockUI();
        });

}();

