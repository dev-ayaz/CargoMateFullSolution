using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.Admin.Areas.Customers.Models
{
    public class CustomerProfileDisplayViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CustomerId { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string ImageUrl { get; set; }

        public string DateOfBirth { get; set; }

        public bool? Gender { get; set; }

        public string Address { get; set; }

        public string Location { get; set; }

        public decimal? Rating { get; set; }

        public string CompanyName { get; set; }

        public bool? IsPhoneVerified { get; set; }

        public string CustomerStatusId { get; set; }

        public string Status { get; set; }
    }
}