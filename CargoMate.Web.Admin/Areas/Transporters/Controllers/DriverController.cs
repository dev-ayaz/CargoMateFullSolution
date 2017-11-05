using CargoMate.Web.Admin.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CargoMate.DataAccess.Contracts;
using CargoMate.DataAccess.Models.Transporters;
using CargoMate.Web.Admin.Areas.Transpoters.Models;

namespace CargoMate.Web.Admin.Areas.Transpoters.Controllers
{
    public class DriverController : BaseController
    {
        public DriverController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        // GET: Transpoters/Driver
        public ActionResult Index()
        {
            return View();
        }

        // GET: Transpoters/Driver/DriversList
        public ActionResult DriversList()
        {
            return View();
        }

        public JsonResult GetDriversList(SearchFilter searchFilter)
        {

            var total = UnitOfWork.DriverPersonalInfos.Count();

            var pageNumber = searchFilter.PageLenght > 0 ? (searchFilter.Start / searchFilter.PageLenght) + 1 : 1;

            IOrderedQueryable<DriverPersonalInfo> OrderByExpression(IQueryable<DriverPersonalInfo> r) => r.OrderByDescending(m => m.Id);

            var driverList = UnitOfWork.DriverPersonalInfos.GetAll(pageNumber, searchFilter.PageLenght, null, OrderByExpression, "DriverStatus.LocalizedDriverStatuses").ToList()
                             .Select(d => new DriverDisplayViewModel
                             {
                                 Id = d.Id,
                                 DateOfBirth = d.DateOfBirth?.ToShortDateString(),
                                 EmailAddress = d.EmailAddress,
                                 ImageUrl = d.ImageUrl,
                                 LegalName = d.LegalName,
                                 Name = d.Name,
                                 PhoneNumber = d.PhoneNumber,
                                 Status = d.DriverStatus?.LocalizedDriverStatuses?.FirstOrDefault(st => st.CultureCode == SessionHandler.CultureCode)?.Name
                             }).ToList();

           

            return Json(new
            {
                sEcho = searchFilter.Draw,
                iTotalRecords = total,
                iTotalDisplayRecords = driverList.Count,
                aaData = driverList
            }, JsonRequestBehavior.AllowGet);

        }
    }
}