﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Models.CustomerViewModels
{
    public class CustomerSearchFilter
    {
        public string CustomerId { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }
    }
}