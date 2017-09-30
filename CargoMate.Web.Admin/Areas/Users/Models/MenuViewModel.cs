using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.Admin.Areas.Users.Models
{
    public class MenuViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public string JsFunction { get; set; }

        /// <summary>
        /// This key is used for permission purpose
        /// </summary>
        public string MenuKey { get; set; }

        public string Icon { get; set; }

        public bool IsSidebarMenu { get; set; }


        public List<MenuViewModel> SubMenus { get; set; }
    }
}