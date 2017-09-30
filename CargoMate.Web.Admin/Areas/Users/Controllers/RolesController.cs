using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using CargoMate.DataAccess.Contracts;
using CargoMate.Web.Admin.Areas.Users.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using CargoMate.Web.Admin.Shared;
using WebGrease.Css.Extensions;
using CargoMate.DataAccess.Models.Users;
using CargoMate.Web.Admin;

namespace CargoMate.Web.Admin.Areas.Users.Controllers
{
    public class RolesController : BaseController
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

        public RolesController(IUnitOfWork unitOfWork) :
            base(unitOfWork)
        {

        }


        // GET: Administration/Roles

        public ActionResult Index()
        {
            var roles = UnitOfWork.Roles.GetAll();

            var result = roles.Select(r => new RoleViewModel
            {
                Id = r.Id,
                RoleName = r.Name,
                RoleDescription = r.Description
            }).ToList();

            return View(result);
        }


        [HttpPost]
        public JsonResult CreateRole(RoleViewModel role)
        {
            if (!ModelState.IsValid)
            {
                return Json(AlertMessages.ModelError);
            }

            var status =   RoleManager.Create(new IdentityRole
            {
                Name = role.RoleName
                   
            }).Succeeded;

            return Json(status ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse);
        }

        public ActionResult RolesList()
        {
            var roles = UnitOfWork.Roles.GetAll();

            var result = roles.Select(r => new RoleViewModel
            {
                Id = r.Id,
                RoleName = r.Name,
                RoleDescription = r.Description
            }).ToList();

            return View("Partials/_RolesList", result);
        }

        [HttpGet]
        public ActionResult EditRole(string id)
        {
            var role = UnitOfWork.Roles.GetWhere(a => a.Id == id).Select(r => new RoleViewModel()
            {
                Id = r.Id,
                RoleName = r.Name

            }).FirstOrDefault();


            return PartialView("Partials/_AddRole", role);
        }



        [HttpPost]
        public JsonResult EditRole(RoleViewModel role)
        {
            if (!ModelState.IsValid)
            {
                return Json(AlertMessages.ModelError);
            }

            var status = RoleManager.Update(new IdentityRole
            {
                Id = role.Id,
                Name = role.RoleName
            }).Succeeded;


            return Json(status ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse);
        }

        public JsonResult DeleteRole(string id)
        {
            var role = UnitOfWork.Roles.GetWhere(a => a.Id == id).FirstOrDefault();
            if (role == null)
            {
                return Json(AlertMessages.ModelError, JsonRequestBehavior.AllowGet);
            }
            var status = RoleManager.Delete(RoleManager.FindByName(role.Name)).Succeeded;

            return Json(status?AlertMessages.SuccessResponse:AlertMessages.FailureResponse , JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Permissions(string roleId)
        {
            return View();
        }


        [HttpPost]
        public ActionResult Permissions(RolePermissionsModel permissions)
        {
            if (string.IsNullOrEmpty(permissions?.MenuActions))
            {
                return Json(new { Success = false });

            }

            var menuActionsList = permissions.MenuActions.Split(',')
                .Where(p => p.Contains("Action_"))
                .Select(r => r.Replace("Action_", "")).ToArray();


            var actions = Array.ConvertAll(menuActionsList, int.Parse);


            var existingPermission = UnitOfWork.RolePermissions.GetWhere(p => p.RoleId == permissions.RoleId);

            actions.ForEach(a =>
            {

                if (!existingPermission.Any(p => p.MenuActionId == a))
                {
                    UnitOfWork.RolePermissions.Insert(new RolePermission
                    {
                        RoleId = permissions.RoleId,
                        MenuActionId = a
                    });
                }

            });

            // delete all removed permissions

            var removedPermission =
                existingPermission.Where(p => !actions.Any(action => action == p.MenuActionId)).ToList();

            removedPermission.ForEach(r =>
            {
                UnitOfWork.RolePermissions.Delete(r);
            });

            var status = UnitOfWork.Commit() > 0;

            if (!status)
            {
                return Json(AlertMessages.FailureResponse);
            }

            // update logged in user roles permission 
            var currentUserRoles = ((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value).ToList();

            // ReSharper disable once InvertIf
            if (currentUserRoles.Contains(permissions.RoleId))
            {
                var userId = User.Identity.GetUserId();
                var roleId = RoleManager.FindByNameAsync(UserManager.GetRoles(userId).FirstOrDefault())?.Result.Id;

                var rolePermissions = UnitOfWork.RolePermissions.GetWhere(p => p.RoleId == roleId,
                    "MenuAction,MenuAction.Menu,MenuAction.Action");

                var permissionsList = rolePermissions.Select(p => new
                {
                    permission = (p.MenuAction.Menu.MenuKey + "-" + p.MenuAction.Action.Key).ToLower()

                }).Select(p => p.permission);

                var permissionsString = string.Join(",", permissionsList.ToArray());

                SessionHandler.RolePermissions = permissionsString;
            }

            return Json(AlertMessages.SuccessResponse);
        }


        [HttpGet]
        public ActionResult Menus(string roleId)
        {
            var menus = UnitOfWork.Menus.GetWhere(m => m.ParentId == null, "SubMenus,SubMenus.Action");

            //var userId = User.Identity.GetUserId();

            //var roleId = RoleManager.FindByNameAsync(UserManager.GetRoles(userId).FirstOrDefault())?.Result.Id;

            var rolePermission = UnitOfWork.RolePermissions.GetWhere(p => p.RoleId == roleId);

            var result = menus.Select(m => new
            {
                id = m.Id,
                text = m.Title,
                state = new
                {
                    opened = true
                },
                children = m.SubMenus.Select(s => new
                {
                    id = "Menu_" + s.Id,
                    text = s.Title,
                    type = "file",
                    children = s.MenuActions.Select(a => new
                    {
                        id = "Action_" + a.Id,
                        text = a.Action.Name,
                        type = "action",
                        state = new
                        {
                            opened = false,
                            selected = rolePermission.Any(p => p.MenuActionId == a.Id)
                        }
                    })
                }),
            });

            return Json(result.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}