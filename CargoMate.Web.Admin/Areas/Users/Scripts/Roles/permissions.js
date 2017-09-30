var Permissions = {

    

    selectors: {
        permissionsContainer: "#permission-container",
        formElements : {
            
            hdActions: "input[name='MenuActions']"
        }
    },

    services : {
        
        controller: "Roles",
        actions: {
            
            Menus: "Menus"
            
        }
    },

    callbacks: {

        saveSuccess: function(result) {
            AppAlerts.actionAlert(result);
            if (result.Success) {
                //$.jstree._reference($(Permissions.selectors.permissionsContainer)).refresh(-1);               
            } 
        },
        saveFail: function(result) {
            AppAlerts.actionAlert("Operation Failed", "Error while saving permissions", true);
        }
    },
    
    initEvents: function() {

        $(Permissions.selectors.permissionsContainer).jstree({
            'plugins': ["wholerow", "checkbox", "types"],
            'core': {
                "themes": {
                    "responsive": false,
                    expand_selected_onload: false
                },
                "check_callback": true,
                'data': {
                    'url': function(node) {

                        var utilities = IAC.Utilities;

                        var url = [utilities.getSiteRoot(), Permissions.services.controller, "/", Permissions.services.actions.Menus].join('');

                        return url + "?roleId=" + utilities.getQueryString("roleId", window.location.href);
                    },
                    'data': function(node) {
                        return { 'parent': node.id };
                    }
                }
            },
            "types": {
                "default": {
                    "icon": "fa fa-folder icon-state-warning icon-lg"
                },
                "file": {
                    "icon": "fa fa-file icon-state-default icon-lg"
                },
                "action": {
                    "icon": "fa fa-pencil-square icon-state-success icon-lg"
                }
            }
        }).bind("changed.jstree", function(e, data) {

            var selected = data.selected.join();
            $(Permissions.selectors.formElements.hdActions).val(selected);

        });


    }
};