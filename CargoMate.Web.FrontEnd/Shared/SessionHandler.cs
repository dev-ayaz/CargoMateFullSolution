﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Shared
{
    public class SessionHandler
    {
        public static long? DriverId
        {
            get
            {
                if (HttpContext.Current.Session[SessionKeys.DriverId] != null)
                {
                    return Convert.ToInt64(HttpContext.Current.Session[SessionKeys.DriverId]);

                }

                return null;
            }
            set { HttpContext.Current.Session[SessionKeys.DriverId] = value; }
        }

        public static string CultureCode
        {
            get
            {
                if (HttpContext.Current.Session[SessionKeys.CultureCode] != null)
                {
                    return HttpContext.Current.Session[SessionKeys.CultureCode].ToString();

                }

                return "en-US";
            }
            set { HttpContext.Current.Session[SessionKeys.CultureCode] = value; }
        }
    }
}