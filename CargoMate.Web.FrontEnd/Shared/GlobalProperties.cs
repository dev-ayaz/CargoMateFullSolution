using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CargoMate.Web.FrontEnd.Shared
{
    public static class GlobalProperties
    {
        public static List<SelectListItem> Languges = new List<SelectListItem>
            {
                new SelectListItem {Text = @"English US", Value = "en-US"},
                new SelectListItem {Text = @"Arabic", Value = "ar-SA"}
            };

        public static string CustomerImagesPath = "http://cm.ayti.com.pk/SystemImages/CustomerImages/";
        public static string DriverImagesPath = "http://cm.ayti.com.pk/SystemImages/DriverImages/";
        public static string DriverDocumentsPath = "http://cm.ayti.com.pk/SystemImages/DriverDocumentsImages/";
        public static string VehicleImagesPath = "http://cm.ayti.com.pk/SystemImages/VehicleImages/";
        public static string UserImagesPath = "http://cm.ayti.com.pk/SystemImages/UserImages/";
        public static string BasicDataImagesPath = "http://ayti.com.pk/";

        public static string DriverImagesFolder = $"{HttpContext.Current.Server.MapPath(@"\")}SystemImages/DriverImages";

        public static string DriverDocumentsFolder = $"{HttpContext.Current.Server.MapPath(@"\")}SystemImages/DriverDocumentsImages";

        public static string CustomerImagesFolder = $"{HttpContext.Current.Server.MapPath(@"\")}SystemImages/CustomerImages";

        public static string VehicleImagesFolder = $"{HttpContext.Current.Server.MapPath(@"\")}SystemImages/VehicleImages";

        public static string UserImagesFolder = $"{HttpContext.Current.Server.MapPath(@"\")}SystemImages/UserImages";
    }
}
