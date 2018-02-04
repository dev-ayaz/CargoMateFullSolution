using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CargoMate.DataAccess.Contracts;
using CargoMate.DataAccess.Models.Customers;
using CargoMate.Web.FrontEnd.Shared;
using CargoMate.Web.FrontEnd.Models.CustomerViewModels;
using System.Data.Entity.Spatial;
using System.Globalization;
using System.Linq.Expressions;

namespace CargoMate.Web.FrontEnd.APIs.V1
{
    /// <summary>
    /// 
    /// </summary>
    /// 

    public class CustomerEndPointController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        public CustomerEndPointController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        /// <summary>
        /// Add New Customer
        /// </summary>
        /// <param name="customerForm"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public HttpResponseMessage AddCustomer(CustomerFormModel customerForm)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.Ambiguous, ModelState);
            }

            if (!customerForm.Id.HasValue)
            {
                var customer = new Customer
                {
                    CustomerId = customerForm.CustomerId,
                    Address = customerForm.Address,
                    Name = customerForm.Name,
                    CompanyId = customerForm.CompanyId,
                    DateOfBirth = customerForm.DateOfBirth,
                    EmailAddress = customerForm.EmailAddress,
                    Gender = customerForm.Gender,
                    PhoneNumber = customerForm.PhoneNumber,
                   // Location = DbGeography.FromText(customerForm.Location),
                    ImageUrl = CargoMateImageHandler.SaveImageFromBase64(customerForm.ImageSrc, GlobalProperties.CustomerImagesFolder)
                };

                UnitOfWork.Customers.Insert(customer);
                UnitOfWork.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, GetCustomerById(customer.Id));
            }

            var saveCustomer = UnitOfWork.Customers.GetWhere(c => c.Id == customerForm.Id).FirstOrDefault();
            saveCustomer.Address = customerForm.Address;
            saveCustomer.CompanyId = customerForm.CompanyId;
            saveCustomer.DateOfBirth = customerForm.DateOfBirth;
            saveCustomer.EmailAddress = customerForm.EmailAddress;
            saveCustomer.Gender = customerForm.Gender;
            saveCustomer.ImageUrl = CargoMateImageHandler.SaveImageFromBase64(customerForm.ImageSrc, GlobalProperties.CustomerImagesFolder);
           // saveCustomer.Location = DbGeography.FromText(customerForm.Location);
            saveCustomer.Name = customerForm.Name;
            saveCustomer.PhoneNumber = customerForm.PhoneNumber;

            UnitOfWork.Customers.Update(saveCustomer);
            UnitOfWork.Commit();
            return Request.CreateResponse(HttpStatusCode.OK, GetCustomerById(saveCustomer.Id));

        }

        private CustomerDisplayModel GetCustomerById(long? customerId)
        {
            return UnitOfWork.Customers.GetWhere(c => c.Id == customerId, "Company")
                   .ToList().Select(c => new CustomerDisplayModel
                   {
                       Id = c.Id,
                       Name = c.Name,
                       Address = c.Address,
                       CustomerId = c.CustomerId,
                       DateOfBirth = c.DateOfBirth?.Date,
                       EmailAddress = c.EmailAddress,
                       Gender = c.Gender,
                       ImageSrc = c.ImageUrl,
                       PhoneNumber = c.PhoneNumber,
                       Company = c.Company==null?null: new CompanyShortViewModel
                       {
                           Id = c.Company?.Id,
                           Name = c.Company?.Name,
                           Location = c.Company?.Location.ToString(),
                           Logo =  c.Company?.Logo
                       }

                   }).FirstOrDefault();
        }
        [HttpGet]
        public CustomerDisplayModel GetCustomerByFilter(string customerId = "", string emailAddress = "", string phoneNumber = "")
        {
            return UnitOfWork.Customers.GetWhere(CustomersFilter(customerId, emailAddress, phoneNumber), "Company")
                  .ToList().Select(c => new CustomerDisplayModel
                  {
                      Id = c.Id,
                      Name = c.Name,
                      Address = c.Address,
                      CustomerId = c.CustomerId,
                      DateOfBirth = c.DateOfBirth?.Date,
                      EmailAddress = c.EmailAddress,
                      Gender = c.Gender,
                      ImageSrc =  c.ImageUrl,
                      PhoneNumber = c.PhoneNumber,
                      Company =c.Company==null?null: new CompanyShortViewModel
                      {
                          Id = c.Company?.Id,
                          Name = c.Company?.Name,
                          Location = c.Company?.Location?.ToString(),
                          Logo = c.Company?.Logo
                      }

                  }).FirstOrDefault();
        }

        [HttpPost]
        public HttpResponseMessage AddCompany(CompanyFormModel companyForm)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.Ambiguous, ModelState);
            }

            UnitOfWork.Companies.Insert(new Company
            {
                Address = companyForm.Address,
                CountryId = companyForm.CountryId,
                CrNumber = companyForm.CrNumber,
                Location = DbGeography.FromText(companyForm.Location),
                Logo = CargoMateImageHandler.SaveImageFromBase64(companyForm.Logo, GlobalProperties.CustomerImagesFolder),
                Name = companyForm.Name,
                PhoneNumber = companyForm.PhoneNumber,
                WebSiteUrl = companyForm.WebSiteUrl
            });

            return Request.CreateResponse(HttpStatusCode.OK, UnitOfWork.Commit());
        }

        [HttpGet]
        public CompanyDisplayModel GetCompanyByFilter(long? companyId, long? crNumber, string phoneNumber, string cultureCode = "en-US")
        {
            return UnitOfWork.Companies.GetWhere(CompaniesFilter(companyId, crNumber, phoneNumber), "Countries.LocalizedCountries")
                   .ToList().Select(c => new CompanyDisplayModel
                   {
                       Id = c.Id,
                       Address = c.Address,
                       Country = c.Country.LocalizedCountries?.FirstOrDefault(lc => lc.CultureCode == cultureCode)?.Name,
                       CrNumber = c.CrNumber,
                       Location = c.Location.ToString(),
                       Logo = c.Logo,
                       Name = c.Name,
                       PhoneNumber = c.PhoneNumber,
                       WebSiteUrl = c.WebSiteUrl
                   }).FirstOrDefault();
        }

        private Expression<Func<Customer, bool>> CustomersFilter(string customerId, string emailAddress, string phoneNumber)
        {
            var predicate = PredicateBuilder.True<Customer>();


            if (!string.IsNullOrWhiteSpace(customerId))
            {
                predicate = predicate.And(c => c.CustomerId == customerId);
            }
            if (!string.IsNullOrWhiteSpace(emailAddress))
            {
                predicate = predicate.And(c => c.EmailAddress == emailAddress);
            }
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                predicate = predicate.And(c => c.PhoneNumber == phoneNumber);
            }

            return predicate;
        }

        private Expression<Func<Company, bool>> CompaniesFilter(long? companyId, long? crNumber, string phoneNumber)
        {
            var predicate = PredicateBuilder.True<Company>();


            if (companyId.HasValue)
            {
                predicate = predicate.Or(c => c.Id == companyId);
            }
            if (crNumber.HasValue)
            {
                predicate = predicate.Or(c => c.CrNumber == crNumber);
            }
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                predicate = predicate.Or(c => c.PhoneNumber == phoneNumber);
            }

            return predicate;
        }

    }
}
