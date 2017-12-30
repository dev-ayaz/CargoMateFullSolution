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
                return Json(new { IsExists = true, RedirectUrl = Url.Action("EditCustomer", "Customer", new { userId = userId }) }, JsonRequestBehavior.AllowGet);
            }

            SetSession(customer);

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

        public ActionResult EditCustomer(string userId)
        {
            var customer = UnitOfWork.Customers.GetWhere(c => c.CustomerId == userId).ToList().Select(c => new CustomerFormModel
            {
                Address = c.Address,
                CompanyId = c.CompanyId,
                CustomerId = c.CustomerId,
                Name = c.Name,
                Id = c.Id,
                ImageSrc = c.ImageUrl,
                PhoneNumber = c.PhoneNumber??string.Empty,
                DateOfBirth = c.DateOfBirth?.Date,
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

            UnitOfWork.Customers.Update(new Customer
            {
                Id = customerForm.Id.Value,
                Address = customerForm.Address,
                EmailAddress = customerForm.EmailAddress,
                DateOfBirth = customerForm.DateOfBirth,
                CustomerId = customerForm.CustomerId,
                CompanyId = customerForm.CompanyId,
                Gender = customerForm.Gender,
                ImageUrl = CargoMateImageHandler.SaveImageFromBase64(customerForm.ImageSrc, GlobalProperties.UserImagesFolder),
                Name = customerForm.Name,
                PhoneNumber = customerForm.PhoneNumber,
                Location = Utilities.StringtoDbGeography(customerForm.Location) 

            });

            SetSession(UnitOfWork.Customers.GetWhere(c=>c.CustomerId==customerForm.CustomerId).FirstOrDefault());

            return Json(UnitOfWork.Commit() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse);


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

        public ActionResult CustomerSignIn() {
            return View();
        }

        public void SetSession(Customer customer)
        {
            SessionHandler.UserId = customer.CustomerId;
            SessionHandler.UserType = Enums.UserType.Customer;
            SessionHandler.UserName = customer.Name;
            SessionHandler.UserImage = customer.ImageUrl;
            SessionHandler.UserEmail = customer.EmailAddress;
            SessionHandler.UserProfile = "Customer/EditCustomer?userId=" + customer.CustomerId;
        }
    }
}