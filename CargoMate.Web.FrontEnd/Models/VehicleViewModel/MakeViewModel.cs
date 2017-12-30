using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Models.VehicleViewModel
{
    public class MakeViewModel
    {
        public long? Id { get; set; }

        public string Name { get; set; }

        public string CountryName { get; set; }

        public string ImageUrl { get; set; }


    }


    public class MakeCompositeViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string CountryName { get; set; }

        public string ImageUrl { get; set; }

        public MakeCountry MakeCountry { get; set; }
    }
    public class MakeCountry
    {
        public long? Id { get; set; }

        public string Name { get; set; }

        public string Flag { get; set; }
    }


    public class MakeTreeStructureViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public MakeCountry MakeCountry { get; set; }

        public List<VehicleModelCompositViewModel> Models { get; set; }
    }
}