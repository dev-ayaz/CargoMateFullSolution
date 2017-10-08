using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CargoMate.DataAccess.Contracts;
using CargoMate.Web.FrontEnd.Shared;
using CargoMate.DataAccess.Models.Customers;
using CargoMate.Web.FrontEnd.Models;
using CargoMate.Web.FrontEnd.Models.CustomerViewModels;
using System.Data.Entity.SqlServer;
using System.Globalization;
using System.Data.Entity.Spatial;

namespace CargoMate.Web.FrontEnd.Controllers
{
    public class CustomerController : BaseController
    {
        public CustomerController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult IsCustomerExists(string userId)
        {
            var customer = UnitOfWork.Customers.GetWhere(c => c.CustomerId == userId).FirstOrDefault();

            if (customer == null)
            {
                return Json(new { IsExists = false, RedirectUrl = Url.Action("Register", "Customer", new { UId = userId }) }, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(customer.Address) || string.IsNullOrEmpty(customer.Name) ||
                                   string.IsNullOrEmpty(customer.PhoneNumber))
            {
                return Json(new { IsExists = true, RedirectUrl = Url.Action("EditCustomer", "Customer", new { customerId = userId }) }, JsonRequestBehavior.AllowGet);
            }

            Session[SessionKeys.CustomerId] = userId;
            return Json(new { IsExists = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Register(RegisterWithFireBase customerForm)
        {
            if (!ModelState.IsValid)
            {
                return Json(CargoMateMessages.ModelError);
            }
            if (!UnitOfWork.Customers.GetWhere(c => c.CustomerId == customerForm.Id).Any())
            {


                UnitOfWork.Customers.Insert(new Customer
                {
                    EmailAddress = customerForm.Email,
                    CustomerId = customerForm.Id,
                    ImageUrl = customerForm.ImageUrl,
                    Name = customerForm.Name,
                    PhoneNumber = customerForm.Phone,

                });

                return
                    Json(UnitOfWork.Commit() > 0
                        ? CargoMateMessages.SuccessResponse
                        : CargoMateMessages.FailureResponse);
            }
            return Json(new
            {
                IsError = true,
                MessageHeader = "Operation Fail",
                Message = "Account Already Exists"
            });
        }

        public ActionResult EditCustomer(string customerId)
        {
            var customer = UnitOfWork.Customers.GetWhere(c => c.CustomerId == customerId).ToList().Select(c => new CustomerFormModel
            {
                Address = c.Address,
                CompanyId = c.CompanyId,
                CustomerId = c.CustomerId,
                Name = c.Name,
                Id = c.Id,
                ImageSrc = c.ImageUrl,
                PhoneNumber = c.PhoneNumber??string.Empty,
                DateOfBirth = c.DateOfBirth?.ToString(),
                EmailAddress = c.EmailAddress,
                Location = c.Location?.ToString(),
                Gender=c.Gender
            }).FirstOrDefault();

            return View(customer);

        }

        [HttpPost]
        public JsonResult EditCustomer(CustomerFormModel customerForm)
        {
            if (!ModelState.IsValid)
            {
                return Json(CargoMateMessages.ModelError);
            }

            var location = customerForm.Location.Contains("Point") ? customerForm.Location : (!string.IsNullOrEmpty(customerForm.Location) ? $"POINT({customerForm.Location?.Replace(",", " "),4326 })": string.Empty);
            UnitOfWork.Customers.Update(new Customer
            {
                Id = customerForm.Id,
                Address = customerForm.Address,
                EmailAddress = customerForm.EmailAddress,
                DateOfBirth = Convert.ToDateTime(customerForm.DateOfBirth, CultureInfo.InvariantCulture),
                CustomerId = customerForm.CustomerId,
                CompanyId = customerForm.CompanyId,
                Gender = customerForm.Gender,
                ImageUrl = CargoMateImageHandler.SaveImageFromBase64(customerForm.ImageSrc, GlobalProperties.CustomerImagesFolder),
                Name = customerForm.Name,
                PhoneNumber = customerForm.PhoneNumber,
                Location = !string.IsNullOrEmpty(location)?DbGeography.FromText(location,4326) : null

            });

            return
                Json(UnitOfWork.Commit() > 0
                    ? CargoMateMessages.SuccessResponse
                    : CargoMateMessages.FailureResponse);


        }

        [HttpPost]
        public JsonResult CompaniesDropDownSource(string term)
        {
            var companies = UnitOfWork.Companies.GetWhere(c => c.Name.Contains(term))
                .Select(c => new
                {
                    id = c.Id,
                    Name = c.Name
                }).Take(10)
                .ToList();

            return Json(companies, JsonRequestBehavior.AllowGet);
        }


    }
}