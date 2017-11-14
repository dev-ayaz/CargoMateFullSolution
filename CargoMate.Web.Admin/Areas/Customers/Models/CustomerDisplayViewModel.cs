using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.Admin.Areas.Customers.Models
{
    public class CustomerDisplayViewModel
    {
        public string Id { get; set; }

        public string DateOfBirth { get; set; }

        public string ImageUrl { get; set; }

        public string EmailAddress { get; set; }

        public string Address { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Status { get; set; }
    }
}