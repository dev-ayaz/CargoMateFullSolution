using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CargoMate.Web.Admin.Shared.ActionFilters
{
    public class RbacAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Required Permission Member
        /// </summary>
        private string _requiredPermission = string.Empty;

        public RbacAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RbacAttribute class.
        /// </summary>
        /// <param name="permission">Required Permission String</param>
        public RbacAttribute(string permission)
        {
            this._requiredPermission = permission;
        }

        /// <summary>
        /// On Authorization Method
        /// </summary>
        /// <param name="filterContext">MVC Request Authorization Context</param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            /*Create permission string based on the requested controller 
              name and action name in the format 'controllername-action'*/
            if (string.IsNullOrEmpty(this._requiredPermission))
            {
                _requiredPermission = $"{filterContext.ActionDescriptor.ControllerDescriptor.ControllerName}-{filterContext.ActionDescriptor.ActionName}";
            }
            if (string.IsNullOrEmpty(SessionHandler.RolePermissions))
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }
            // Check if the requesting user has the permission to run the controller's action
            else if (!SharedFunctions.CheckPermissions(this._requiredPermission))
            {

                /*User doesn't have the required permission and is not a SysAdmin, return our 
                    custom '401 Unauthorized' access error. Since we are setting 
                    filterContext.Result to contain an ActionResult page, the controller's 
                    action will not be run.

                    The custom '401 Unauthorized' access error will be returned to the 
                    browser in response to the initial request.*/

                //filterContext.Controller.TempData.Add("RedirectReason", "You are not authorized to access this page.");

                //filterContext.Result = new RedirectResult("~/Area/Administration/Views/Shared/UnAuthorize");

                // filterContext.Result =
                //    new HttpUnauthorizedResult("Rousource your are trying to access is not available or restricted");

                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "UnAuthorize" }));
            }

            /*If the user has the permission to run the controller's action, then 
              filterContext.Result will be uninitialized and executing the controller's 
              action is dependant on whether filterContext.Result is uninitialized.*/
        }

    }
}