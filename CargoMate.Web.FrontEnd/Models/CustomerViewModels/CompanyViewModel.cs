using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Models.CustomerViewModels
{
    public class CompanyViewModel
    {
    }

    public class CompanyFormModel
    {

        public long Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string PhoneNumber { get; set; }

        public long? CrNumber { get; set; }

        public string Logo { get; set; }

        public string WebSiteUrl { get; set; }

        public long? CountryId { get; set; }

        public string Address { get; set; }
    }

    public class CompanyDisplayModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string PhoneNumber { get; set; }

        public long? CrNumber { get; set; }

        public string Logo { get; set; }

        public string WebSiteUrl { get; set; }

        public string Country { get; set; }

        public string Address { get; set; }
    }
}