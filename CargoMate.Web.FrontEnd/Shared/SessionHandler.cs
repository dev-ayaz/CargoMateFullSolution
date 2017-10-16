using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Shared
{
    public class SessionHandler
    {
        public static string UserId
        {
            get { return HttpContext.Current.Session[SessionKeys.UserId]?.ToString(); }
            set { HttpContext.Current.Session[SessionKeys.UserId] = value; }
        }

        public static Enums.UserType? UserType
        {
            get { return HttpContext.Current.Session[SessionKeys.UserType] != null ? (Enums.UserType?)(Enums.UserType)HttpContext.Current.Session[SessionKeys.UserType] : null; }
            set { HttpContext.Current.Session[SessionKeys.UserType] = value; }
        }

        public static string UserEmail
        {
            get { return HttpContext.Current.Session[SessionKeys.UserEmail] != null ? HttpContext.Current.Session[SessionKeys.UserEmail].ToString() : null; }
            set { HttpContext.Current.Session[SessionKeys.UserEmail] = value; }
        }

        public static string UserProfile
        {
            get { return HttpContext.Current.Session[SessionKeys.UserProfile]?.ToString(); }
            set { HttpContext.Current.Session[SessionKeys.UserProfile] = value; }
        }

        public static string UserImage
        {
            get { return HttpContext.Current.Session[SessionKeys.UserImage]?.ToString(); }
            set { HttpContext.Current.Session[SessionKeys.UserImage] = value; }
        }

        public static string UserName
        {
            get { return HttpContext.Current.Session[SessionKeys.UserName]?.ToString(); }
            set { HttpContext.Current.Session[SessionKeys.UserName] = value; }
        }

        public static string CultureCode
        {
            get { return HttpContext.Current.Session[SessionKeys.CultureCode] != null ? HttpContext.Current.Session[SessionKeys.CultureCode].ToString() : "en-US"; }
            set { HttpContext.Current.Session[SessionKeys.CultureCode] = value; }
        }

        public static long? VehicleId
        {
            get
            {
                if (HttpContext.Current.Session[SessionKeys.VehicleId] != null)
                {
                    return Convert.ToInt64(HttpContext.Current.Session[SessionKeys.VehicleId]);

                }

                return null;
            }
            set
            {
                HttpContext.Current.Session[SessionKeys.VehicleId] = value;
            }
        }
    }
}