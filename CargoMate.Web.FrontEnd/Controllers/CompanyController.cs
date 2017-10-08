using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CargoMate.DataAccess.Contracts;
using CargoMate.Web.FrontEnd.Models.CustomerViewModels;
using CargoMate.Web.FrontEnd.Shared;
using CargoMate.DataAccess.Models.Customers;
using System.Data.Entity.Spatial;

namespace CargoMate.Web.FrontEnd.Controllers
{
    public class CompanyController : BaseController
    {
        public CompanyController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        // GET: Company
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(CompanyFormModel companyForm)
        {
            if (!ModelState.IsValid)
            {
                return Json(CargoMateMessages.ModelError);
            }

            UnitOfWork.Companies.Insert(new Company
            {
                Address = companyForm.Address,
                CountryId = companyForm.CountryId,
                CrNumber = companyForm.CrNumber,
                Location = Utilities.StringtoDbGeography(companyForm.Location),
                Logo = CargoMateImageHandler.SaveImageFromBase64(companyForm.Logo, GlobalProperties.CustomerImagesFolder),
                Name = companyForm.Name,
                PhoneNumber = companyForm.PhoneNumber,
                WebSiteUrl = companyForm.WebSiteUrl

            });

            return Json(UnitOfWork.Commit() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse);
        }
    }
}