using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Models
{
    public class CountryViewModel
    {
        public long? Id { get; set; }

        public string Name { get; set; }

        public string CountryCode { get; set; }

        public string CurrencyLong { get; set; }

        public string CurrencyCode { get; set; }

        public string CurrencySymbol { get; set; }

        public string PhonCode { get; set; }

        public string Flag { get; set; }
    }
}