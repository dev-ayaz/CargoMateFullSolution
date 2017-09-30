using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace CargoMate.Web.Admin.Shared
{
    public static class HtmlHelperExtensions
    {
        public static string HasPermission(this HtmlHelper htmlHelper, string permission)
        {
            return SharedFunctions.CheckPermissions(permission).ToString();
        }

        public static string LoggedInUserName(this HtmlHelper htmlHelper)
        {
            var currentUser =
                HttpContext.Current.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>()
                    .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            if (currentUser != null)
            {
                return currentUser.FirstName + " " + currentUser.LastName;
            }

            return string.Empty;
        }

        public static string LoggedInUserPhoto(this HtmlHelper htmlHelper)
        {
            var currentUser =
                HttpContext.Current.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>()
                    .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            return currentUser != null ? currentUser.ImageUrl : string.Empty;
        }

        /// <summary>
        /// This function customize return validation error
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static HtmlString ValidationSummaryBootstrap(this HtmlHelper htmlHelper)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException("htmlHelper");
            }

            if (htmlHelper.ViewData.ModelState.IsValid)
            {
                return new HtmlString(string.Empty);
            }

            return new HtmlString(
                "<div class=\"alert alert-warning\">"
                + htmlHelper.ValidationSummary()
                + "</div>");
        }

        public static HtmlString CustomizedValidateMessage<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            if (helper.ViewData.ModelState.IsValid)
            {
                return new HtmlString(string.Empty);
            }

            var modelState = helper.ViewData.ModelState;

            var validProperty = true;

            var memberExpression = expression.Body as MemberExpression;

            for (int i = 0; i < modelState.Keys.Count; i++)
            {
                if (memberExpression == null || !modelState.Keys.ElementAt(i).Equals(memberExpression.Member.Name))
                {
                    continue;
                }

                if (modelState.Values.ElementAt(i).Errors.Count > 0)
                {
                    validProperty = false;
                }
            }

            if (!validProperty)
            {
                return new HtmlString(
                    "<div class=\"alert alert-danger custom-validation-msg\">"
                    + helper.ValidationMessageFor(expression)
                    + "</div>");

            }

            return new HtmlString(string.Empty);
        }

        /// <summary>
        /// This extension method is used to create ajax action link with provided html or text
        /// </summary>
        /// <param name="ajaxHelper">Ajax helper</param>
        /// <param name="linkText">Html or text for link</param>
        /// <param name="actionName">Action to be called</param>
        /// <param name="controllerName">Controller that contains the action</param>
        /// <param name="routeValues"></param>
        /// <param name="ajaxOptions"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString RawActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var repID = Guid.NewGuid().ToString();
            var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(lnk.ToString().Replace(repID, linkText));
        }

        public static string IsParentActive(this HtmlHelper html, string controller = null, string cssClass = null)
        {

            var currentController = html.ViewContext.ParentActionViewContext.RouteData.GetRequiredString("controller");

            if (string.IsNullOrEmpty(controller))
            {
                return string.Empty;
            }

            return (currentController.IndexOf(controller, StringComparison.Ordinal) >= 0) ? cssClass : String.Empty;
        }

        public static string IsSelectedMenu(this HtmlHelper html, string link = null)
        {
            const string cssClass = "active open";
            var currentAction = (string)html.ViewContext.ParentActionViewContext.RouteData.Values["action"];
            var currentController = (string)html.ViewContext.ParentActionViewContext.RouteData.Values["controller"];

            if (string.IsNullOrEmpty(link))
            {
                return string.Empty;
            }

            try
            {
                var linkParts = link.TrimStart('/').Split('/');

                var action = "Index";
                string controller;

                if (linkParts.Length > 2)
                {
                    action = linkParts[linkParts.Length - 1];
                    controller = linkParts[linkParts.Length - 2];
                }
                else
                {
                    controller = linkParts[linkParts.Length - 1];
                }

                return controller.ToLower().Equals(currentController.ToLower()) && action.ToLower().Equals(currentAction.ToLower()) ?
                    cssClass : String.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        //public static string IsSelected(this HtmlHelper html, string controller = null, string action = null)
        //{
        //    const string cssClass = "active open";
        //    string currentAction = (string)html.ViewContext.ParentActionViewContext.RouteData.Values["action"];
        //    string currentController = (string)html.ViewContext.ParentActionViewContext.RouteData.Values["controller"];

        //    if (String.IsNullOrEmpty(controller))
        //        controller = currentController;

        //    if (String.IsNullOrEmpty(action))
        //        action = currentAction;

        //    return controller.Split(';').Contains(currentController) && action.Split(';').Contains(currentAction) ?
        //        cssClass : String.Empty;
        //}

        public static string RenderRazorViewToString(this Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}