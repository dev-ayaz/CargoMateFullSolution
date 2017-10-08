var activateLink = function() {

    $(function() {
        
        //Get the current page link
        var currentPageLink = document.location.href;

        var root = RequestHandler.getSiteBase();

        //Search your menu for a linkURL that is similar to the active pageURL
        $(".nav-item a").each(function () {

           $(".nav-item a").removeClass("active");
           var linkLoop = [root, $(this).attr("href")].join('');

            if (linkLoop === currentPageLink) {
                var foundUrl = $(this).attr("href");

                var $activeLink = $('a[href="' + foundUrl + '"]');

                $activeLink.closest('.nav-item').addClass('active');

                var parentMenu = $activeLink.closest('.nav-item').parent().closest('.nav-item');

                if (parentMenu.length) {

                    parentMenu.addClass('active');

                    parentMenu.find('.arrow').addClass('open');
                }
            } else {


                $activeLink = $('a[href*="' + window.location.href.split("/")[4] + '"]');
               // $activeLink.closest('.nav-item').addClass('active');

                  parentMenu = $activeLink.closest('.nav-item').parent().closest('.nav-item');

                if (parentMenu.length) {

                    parentMenu.addClass('active');

                    parentMenu.find('.arrow').addClass('open');
                }
            }
        });

    });

}();