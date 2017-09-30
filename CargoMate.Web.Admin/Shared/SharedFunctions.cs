using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.Admin.Shared
{
    public class SharedFunctions
    {
        public static bool CheckPermissions(string permission)
        {
            var permissions = SessionHandler.RolePermissions;

            if (string.IsNullOrEmpty(permissions))
            {
                return false;
            }

            var permissionsList = permissions.Split(',');

            permissionsList = permissionsList.Select(r => r.Replace(Environment.NewLine, string.Empty)).ToArray();

            return permissionsList.Contains(permission.ToLower());
        }



    }
}