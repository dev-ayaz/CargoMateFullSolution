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
            get
            {
                if (HttpContext.Current.Session[SessionKeys.UserId] != null)
                {
                    return HttpContext.Current.Session[SessionKeys.UserId].ToString();

                }

                return null;
            }
            set { HttpContext.Current.Session[SessionKeys.UserId] = value; }
        }

        public static Enums.UserType? UserType
        {
            get
            {
                if (HttpContext.Current.Session[SessionKeys.UserType] != null)
                {
                    return (Enums.UserType)HttpContext.Current.Session[SessionKeys.UserType];

                }

                return null;
            }
            set { HttpContext.Current.Session[SessionKeys.UserType] = value; }
        }

        public static string UserEmail
        {
            get
            {
                if (HttpContext.Current.Session[SessionKeys.UserEmail] != null)
                {
                    return HttpContext.Current.Session[SessionKeys.UserEmail].ToString();

                }

                return null;
            }
            set { HttpContext.Current.Session[SessionKeys.UserEmail] = value; }
        }

        public static string UserProfile
        {
            get
            {
                if (HttpContext.Current.Session[SessionKeys.UserProfile] != null)
                {
                    return HttpContext.Current.Session[SessionKeys.UserProfile].ToString();

                }

                return null;
            }
            set { HttpContext.Current.Session[SessionKeys.UserProfile] = value; }
        }

        public static string UserImage
        {
            get
            {
                if (HttpContext.Current.Session[SessionKeys.UserImage] != null)
                {
                    return HttpContext.Current.Session[SessionKeys.UserImage].ToString();

                }

                return null;
            }
            set { HttpContext.Current.Session[SessionKeys.UserImage] = value; }
        }

        public static string UserName
        {
            get
            {
                if (HttpContext.Current.Session[SessionKeys.UserName] != null)
                {
                    return HttpContext.Current.Session[SessionKeys.UserName].ToString();

                }

                return null;
            }
            set { HttpContext.Current.Session[SessionKeys.UserName] = value; }
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