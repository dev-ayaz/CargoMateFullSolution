using CargoMate.DataAccess.Contracts;
using CargoMate.Web.Admin.Areas.Users.Controllers;
using CargoMate.Web.Admin.Areas.Users.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CargoMate.Web.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get => _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            private set => _roleManager = value;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        [Authorize]
        public ActionResult Sidebar()
        {
            var userId = User.Identity.GetUserId();
            var roleId = RoleManager.FindByNameAsync(UserManager.GetRoles(userId).FirstOrDefault())?.Result.Id;

            //var rolePermissions = UnitOfWork.RolePermissions.GetWhere(p => p.RoleId == roleId,
            //    "MenuAction,MenuAction.Menu,MenuAction.Menu.SubMenus").Select(p=>p.MenuAction.Menu.ParentId).Distinct().ToList();

            var permittedMenus =
                UnitOfWork.Menus.GetWhere(r => r.MenuActions.Any(m => m.RolePermissions.Any(p => p.RoleId == roleId)))
                    .Select(m => m.Id).ToList();

            //var menus = UnitOfWork.Menus.GetWhere(m => permittedMenus.Contains(m.Id) && , "SubMenus").ToList();

            var menus =
                UnitOfWork.Menus.GetWhere(
                    m => m.ParentId == null && m.SubMenus.Any(s => s.IsSidebarMenu && permittedMenus.Contains(s.Id)),
                    "SubMenus").ToList();

            var result = menus.Select(m => new MenuViewModel
            {
                Id = m.Id,
                Title = m.Title,
                JsFunction = m.JsFunction,
                Link = m.Link,
                MenuKey = m.MenuKey,
                Icon = m.Icon,
                SubMenus = m.SubMenus.Where(s => s.IsSidebarMenu && permittedMenus.Contains(s.Id)).Select(s => new MenuViewModel
                {
                    Title = s.Title,
                    JsFunction = s.JsFunction,
                    Link = s.Link,
                    Icon = s.Icon

                }).ToList()

            }).ToList();

            return View(result);

        }

        public HomeController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}