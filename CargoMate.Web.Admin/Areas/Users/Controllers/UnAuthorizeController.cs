using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CargoMate.DataAccess.Contracts;
using CargoMate.Web.Admin.Areas.Users.Controllers;

namespace CargoMate.Web.Admin.Areas.Users.Controllers
{
    public class UnAuthorizeController : BaseController
    {
        // GET: Users/UnAuthorize
        public ActionResult Index()
        {
            return View();
        }

        public UnAuthorizeController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}