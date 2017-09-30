using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Models
{
    public class GeoAddressFormModel
    {
        public long CountryId { get; set; }

        public DbGeography Location { get; set; }

        public string Locality { get; set; }

        public string SubLocality { get; set; }
    }

    public class GeoAddressDisplayModel
    {
        public string CountryName { get; set; }

        public DbGeography Location { get; set; }

        public string Locality { get; set; }

        public string SubLocality { get; set; }
    }

}