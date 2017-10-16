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
            get => HttpContext.Current.Session[SessionKeys.UserId] != null ? HttpContext.Current.Session[SessionKeys.UserId].ToString() : null;
            set => HttpContext.Current.Session[SessionKeys.UserId] = value;
        }

        public static Enums.UserType? UserType
        {
            get => HttpContext.Current.Session[SessionKeys.UserType] != null ? (Enums.UserType?) (Enums.UserType) HttpContext.Current.Session[SessionKeys.UserType]: null;
            set => HttpContext.Current.Session[SessionKeys.UserType] = value;
        }

        public static string UserEmail
        {
            get => HttpContext.Current.Session[SessionKeys.UserEmail] != null ? HttpContext.Current.Session[SessionKeys.UserEmail].ToString() : null;
            set => HttpContext.Current.Session[SessionKeys.UserEmail] = value;
        }

        public static string UserProfile
        {
            get => HttpContext.Current.Session[SessionKeys.UserProfile] != null ? HttpContext.Current.Session[SessionKeys.UserProfile].ToString() : null;
            set => HttpContext.Current.Session[SessionKeys.UserProfile] = value;
        }

        public static string UserImage
        {
            get => HttpContext.Current.Session[SessionKeys.UserImage] != null ? HttpContext.Current.Session[SessionKeys.UserImage].ToString() : null;
            set => HttpContext.Current.Session[SessionKeys.UserImage] = value;
        }

        public static string UserName
        {
            get => HttpContext.Current.Session[SessionKeys.UserName] != null ? HttpContext.Current.Session[SessionKeys.UserName].ToString() : null;
            set => HttpContext.Current.Session[SessionKeys.UserName] = value;
        }

        public static string CultureCode
        {
            get => HttpContext.Current.Session[SessionKeys.CultureCode] != null ? HttpContext.Current.Session[SessionKeys.CultureCode].ToString() : "en-US";
            set => HttpContext.Current.Session[SessionKeys.CultureCode] = value;
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
            set => HttpContext.Current.Session[SessionKeys.VehicleId] = value;
        }
    }
}