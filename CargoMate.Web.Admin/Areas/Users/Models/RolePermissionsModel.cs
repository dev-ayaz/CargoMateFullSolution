using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.Admin.Areas.Users.Models
{
    public class RolePermissionsModel
    {
        public string RoleId { get; set; }

        public string MenuActions { get; set; }
    }
}