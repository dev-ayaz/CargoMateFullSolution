var VehicleImages = {
    selectors: {
        VehicleImagesDropZone: "#VehicleImagesDropZone",
        ImagesPath: [utils.getSiteBase(), "/SystemImages/VehicleImages/"].join("")

    },

    services: {
        controller: "Vehicle",
        actions: {
            Delete: "VehicleImagesDelete",
            Select: "VehicleImagesGet"
        }
    },


    callbacks: {

        initDropZone: function () {
            Dropzone.autoDiscover = false;

            var deleteUrl = [RequestHandler.getSiteRoot(), VehicleImages.services.controller, "/", VehicleImages.services.actions.Delete].join("");
            var selectUrl = [RequestHandler.getSiteRoot(), VehicleImages.services.controller, "/", VehicleImages.services.actions.Select].join("");

            var vehicleImagesDropZone = new Dropzone(VehicleImages.selectors.VehicleImagesDropZone, {
                addRemoveLinks: true,
                maxFiles: 10,
                dictMaxFilesExceeded: "Maximum upload limit reached",
                acceptedFiles: "image/*",
                dictInvalidFileType: "upload only JPG/PNG",
                init: function () {

                    thisDropZone = this;
                    thisDropZone.on("removedfile", function (file) {
                        $.post(deleteUrl, { name: file.name }, function (data) {
                            console.log(data);
                        });
                    });

                    $.get(selectUrl, function (data) {
                        $.each(data, function (key, value) {
                            var mockFile = { name: value, size: 1000 };
                            thisDropZone.options.addedfile.call(thisDropZone, mockFile);
                            thisDropZone.createThumbnailFromUrl(mockFile, [RequestHandler.getSiteBase(), "/SystemImages/VehicleImage/"].join("") + value, function () {
                                thisDropZone.emit("complete", mockFile);
                            });

                        });

                    });
                }

            });
        }
    },


    InitEvents: function () {
        VehicleImages.callbacks.initDropZone();
    }

}

jQuery(document).ready(function () {
    VehicleImages.InitEvents();
});