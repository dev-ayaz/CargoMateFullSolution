using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using CargoMate.Web.Admin.Areas.Users.Controllers;
using CargoMate.DataAccess.Contracts;
using CargoMate.Web.Admin.Areas.Users.Models;
using CargoMate.Web.Admin.Shared;
using CargoMate.DataAccess.Models.Users;
using CargoMate.Web.Admin.Models;

namespace CargoMate.Web.Admin.Areas.Users.Controllers
{
    public class UsersController : BaseController
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

        public UsersController(IUnitOfWork unitOfWork) :
        base(unitOfWork)
        {

        }

        // GET: Administration/Users
        public ActionResult Index()
        {
            var roles = UnitOfWork.Roles.GetAll().ToList();

            var userModel = new UserViewModel
            {
                Roles = roles
            };

            return View(userModel);
        }

        public JsonResult UsersList(SearchFilter filter)
        {

            var total = UnitOfWork.Users.Count();

            var pageNumber = filter.PageLenght > 0 ? (filter.Start / filter.PageLenght) + 1 : 1;

            IOrderedQueryable<User> OrderByExpression(IQueryable<User> r) => r.OrderByDescending(u =>u.Id );

            List<User> extraNetUsers;
            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                var searchTerm = filter.SearchTerm.ToLower();
                extraNetUsers = UnitOfWork.Users.GetAll(pageNumber, filter.PageLenght, u =>
                u.FirstName.ToLower().Contains(searchTerm)
                || u.LastName.ToLower().Contains(searchTerm)
                || u.Email.ToLower().Contains(searchTerm)
                || u.PhoneNumber.Contains(searchTerm),
                    OrderByExpression
                , "UserRoles,UserRoles.Role1").ToList();
            }
            else
            {
                extraNetUsers = UnitOfWork.Users.GetAll(pageNumber, filter.PageLenght, null, OrderByExpression, "UserRoles,UserRoles.Role1").ToList();
            }

            return Json(new
            {
                sEcho = filter.Draw,
                iTotalRecords = total,
                iTotalDisplayRecords = extraNetUsers.Count(),
                aaData = extraNetUsers.Select(r => new
                {
                    r.Id,
                    r.FirstName,
                    r.LastName,
                    r.Email,
                    r.PhoneNumber,
                    CreationDate= r.CreationDate?.ToString("dd/MMMM/yyyy"),
                    r.IsActive,
                    UserRole = r.UserRoles.FirstOrDefault()?.Role?.Name,
                    r.ImageUrl

                }).ToList()
            },
                JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var roles = UnitOfWork.Roles.GetAll().ToList();

            var userModel = new UserViewModel
            {
                Roles = roles
            };

            return PartialView("Partials/_Add", userModel);
        }


        [HttpPost]
        public async Task<ActionResult> Create(UserViewModel adminUser)
        {
            
            if (!ModelState.IsValid)
            {
                return Json(AlertMessages.ModelError);
            }

            if (UnitOfWork.Users.GetWhere(u => u.Email == adminUser.Email).Any())
            {
                return Json( new {
                    IsError = true,
                    Title = "Operation Fail",
                    Message = "This email is aleady taken."
                });
            }

            var user = new ApplicationUser
            {
                UserName = adminUser.Email,
                FirstName = adminUser.FirstName,
                LastName = adminUser.LastName,
                Email = adminUser.Email,
                PhoneNumber = adminUser.PhoneNumber,
                CreationDate = DateTime.Now,
                ImageUrl = ImageUploader.SaveImageFromBase64(adminUser.UserImage,SessionKeys.UserImagesPath),
                IsActive = true

            };

            try
            {
                var result = await UserManager.CreateAsync(user, adminUser.Password);

                if (!result.Succeeded)
                {
                    return Json(AlertMessages.FailureResponse);
                }

                var role = UnitOfWork.Roles.GetWhere(r => r.Id == adminUser.RoleId).FirstOrDefault();

                if (role != null)
                {
                    result = UserManager.AddToRole(user.Id, role.Name);
                }

                return Json(result.Succeeded? AlertMessages.SuccessResponse : AlertMessages.FailureResponse);
            }
            catch (Exception ex)
            {
                return Json(AlertMessages.FailureResponse);
            }

        }


        [HttpGet]
        public ActionResult Edit(string id)
        {
            var user = UnitOfWork.Users.GetWhere(a => a.Id == id, "UserRoles").FirstOrDefault();

            if (user == null)
            {
                return PartialView("Partials/_Edit");
            }



            var roleId = user.UserRoles.FirstOrDefault()?.RoleId;

            var roles = UnitOfWork.Roles.GetAll().ToList();

            var result = new UserViewModel
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                RoleId = roleId,
                Roles = roles,
                UserImage = user.ImageUrl,
                IsActive = user.IsActive.Value
            };

            return PartialView("Partials/_Edit", result);
        }



        [HttpPost]

        public ActionResult Edit(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return Json(AlertMessages.ModelError);
            }

            if (UnitOfWork.Users.GetWhere(u => u.Email == user.Email && u.Id != user.UserId).Any())
            {
                return Json("This email is aleady taken.");
            }

            var dbUser = UnitOfWork.Users.GetWhere(a => a.Id == user.UserId).FirstOrDefault();

            if (dbUser == null)
            {
                return Json(AlertMessages.ModelError);
            }

            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.Email = user.Email;
            dbUser.UserName = user.Email;
            dbUser.PhoneNumber = user.PhoneNumber;
            if (!string.IsNullOrEmpty(user.UserImage))
            {
                dbUser.ImageUrl = ImageUploader.SaveImageFromBase64(user.UserImage,SessionKeys.UserImagesPath);
            }
          
            dbUser.IsActive = user.IsActive;

            if (!string.IsNullOrEmpty(user.Password))
            {
                dbUser.PasswordHash = UserManager.PasswordHasher.HashPassword(user.Password);
            }


              var userRole = UnitOfWork.UserRoles.GetWhere(r => r.UserId == user.UserId, "Role,Role1").FirstOrDefault();

            if (userRole != null)
            {
                if (!userRole.RoleId.Equals(user.RoleId))
                {
                    // new role 
                    var role = UnitOfWork.Roles.GetWhere(r => r.Id == user.RoleId).FirstOrDefault();
                    if (role != null)
                    {
                        UserManager.AddToRole(user.UserId, role.Name);
                        UserManager.RemoveFromRole(user.UserId, userRole.Role.Name);
                    }
                }
            }
            else
            {
                if (user.RoleId != null)
                {
                    UnitOfWork.UserRoles.Insert(new UserRole
                    {
                        UserId = user.UserId,
                        RoleId = user.RoleId
                    });
                }
            }

            UnitOfWork.Users.Update(dbUser);

            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse);
        }


        [HttpGet]
        public ActionResult Profile(string id)
        {
            return View();
        }
    }
}