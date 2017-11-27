using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CargoMate.Web.Admin.Shared;
using CargoMate.DataAccess.Models.Customers;
using CargoMate.DataAccess.Contracts;
using CargoMate.Web.Admin.Areas.Customers.Controllers;
using CargoMate.Web.Admin.Areas.Customers.Models;

namespace CargoMate.Web.Admin.Areas.Customers.Controllers
{
    public class CustomerController : BaseController
    {

        public CustomerController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        // GET: Customers/Customer
        public ActionResult Index()
        {
            return View();
        }

        // GET: Customers/Customer/CustomersList
        public ActionResult CustomersList()
        {
            return View();
        }

        public JsonResult GetCustomersList(SearchFilter searchFilter)
        {

            var total = UnitOfWork.DriverPersonalInfos.Count();

            var pageNumber = searchFilter.PageLenght > 0 ? (searchFilter.Start / searchFilter.PageLenght) + 1 : 1;

            IOrderedQueryable<Customer> OrderByExpression(IQueryable<Customer> r) => r.OrderByDescending(m => m.Id);

            var driverList = UnitOfWork.Customers.GetAll(pageNumber, searchFilter.PageLenght, null, OrderByExpression, "CustomerStatus.LocalizedCustomerStatuses").ToList()
                             .Select(c => new CustomerDisplayViewModel
                             {
                                 Id = c.CustomerId,
                                 DateOfBirth = c.DateOfBirth.ToString(),
                                 EmailAddress = c.EmailAddress,
                                 ImageUrl = c.ImageUrl,
                                 Name = c.Name,
                                 PhoneNumber = c.PhoneNumber,
                                 Address = c.Address,
                                 Status = c.CustomerStatus?.LocalizedCustomerStatuses?.FirstOrDefault(st => st.CultureCode == SessionHandler.CultureCode)?.Name
                             }).ToList();



            return Json(new
            {
                sEcho = searchFilter.Draw,
                iTotalRecords = total,
                iTotalDisplayRecords = driverList.Count,
                aaData = driverList
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult CustomerProfile(string hashcode, string customerId)
        {

            var customer = UnitOfWork.Customers.GetWhere(c => c.CustomerId == customerId, "CustomerStatus.LocalizedCustomerStatuses, Company ")
                         .ToList()
                         .Select(c => new CustomerProfileDisplayViewModel
                         {
                             Id = c.CustomerId,
                             Name = c.Name,
                             PhoneNumber = c.PhoneNumber,
                             EmailAddress = c.EmailAddress,
                             ImageUrl = c.ImageUrl,
                             DateOfBirth = c.DateOfBirth?.ToShortDateString(),
                             Gender = c.Gender,
                             Address = c.Address,
                             Location = c.Location?.ToString(),
                             Rating = c.Rating,
                             IsPhoneVerified = c.IsPhoneVerified,
                             CompanyName = c.Company?.Name,
                             Status = c.CustomerStatus?.LocalizedCustomerStatuses.FirstOrDefault(cs => cs.CultureCode == SessionHandler.CultureCode)?.Name,


                         }).FirstOrDefault();

            return View(customer);
        }

    }
}