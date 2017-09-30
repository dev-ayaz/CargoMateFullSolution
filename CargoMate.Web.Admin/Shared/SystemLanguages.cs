using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CargoMate.Web.Admin.Shared
{
    public static class SystemLanguages
    {
        public static List<SelectListItem> Languages = new List<SelectListItem>
        {
            new SelectListItem
            {
                Text = @"English",
                Value = @"en-US"
            },
            new SelectListItem
            {
                Text = @"Arabic",
                Value = @"ar-SA"
            }

        };
    }
}