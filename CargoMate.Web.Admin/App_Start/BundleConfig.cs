// ┌────────────────────────────────────────────────────────────────────┐ \\
// │ Bundel Config For App Resources By dev_Ayaz                        │ \\
// ├────────────────────────────────────────────────────────────────────┤ \\
// │ Copyright © 2011-2017 Muhammad Ayaz (dev_ayaz@yahoo.com)           │ \\
// │ Copyright © 2011-2017 Muhammad Ayaz (dev_ayaz@yahoo.com)           │ \\
// ├────────────────────────────────────────────────────────────────────┤ \\
// │                                                                    │ \\
// └────────────────────────────────────────────────────────────────────┘ \\

using CargoMate.Web.Admin.Shared;
using System.Web;
using System.Web.Optimization;

namespace CargoMate.Web.Admin
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = WebConfigKeys.IsProduction.ToLower().Equals("true");

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/assets/global/plugins/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
                "~/Scripts/jquery.unobtrusive*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                "~/Scripts/respond.js"));

            //bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
            //    "~/Scripts/Vendors/plugins/sweetalert/*.js",
            //    "~/Scripts/Vendors/plugins/bootstrap-datepicker/*.js"));




            bundles.Add(new ScriptBundle("~/bundles/scripts/AppCommonScripts").Include(
                "~/Scripts/AppCommonScripts/CargoMateApp.js",
                "~/Scripts/AppCommonScripts/Utilities.js",
                "~/Scripts/AppCommonScripts/AppAlerts.js",
                "~/Scripts/AppCommonScripts/link-activator.js",
                "~/Scripts/AppCommonScripts/ajax-interceptor.js",
                "~/Scripts/AppCommonScripts/IAC-datatable.js",
                "~/Scripts/AppCommonScripts/has-permission.js"));


            bundles.Add(new StyleBundle("~/bundles/bootstrap").Include(
                "~/assets/global/plugins/bootstrap/css/bootstrap.css",
                "~/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.css"));

            bundles.Add(new StyleBundle("~/bundles/bootstrap-rtl").Include(
                "~/assets/global/plugins/bootstrap/css/bootstrap-rtl.css",
                "~/assets/global/plugins/bootstrap-switch/css/bootstrap-switch-rtl.css"));


            bundles.Add(new StyleBundle("~/bundles/css/layout").Include(
                "~/assets/global/css/components.css",
                "~/assets/global/css/plugins.css",
                "~/assets/layouts/layout4/css/layout.css",
                "~/assets/layouts/layout4/css/themes/light.css",
                "~/assets/layouts/layout4/css/custom.css",
                "~/assets/global/plugins/morris/morris.css"));

            bundles.Add(new StyleBundle("~/bundles/css/layout-rtl").Include(
                "~/assets/global/css/components-rtl.css",
                "~/assets/global/css/plugins-rtl.css",
                "~/assets/layouts/layout4/css/layout-rtl.css",
                "~/assets/layouts/layout4/css/themes/light-rtl.css",
                "~/assets/layouts/layout4/css/custom-rtl.css",
                "~/assets/global/plugins/morris/morris.css"));

            bundles.Add(new StyleBundle("~/bundles/css/fonts").Include(
                "~/assets/global/plugins/font-awesome/css/font-awesome.css",
                "~/assets/global/plugins/simple-line-icons/simple-line-icons.css"));
        }
    }
}
