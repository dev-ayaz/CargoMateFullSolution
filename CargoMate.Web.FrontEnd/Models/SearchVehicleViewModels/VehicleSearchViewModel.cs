using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Models.SearchVehicleViewModels
{
    public class VehicleSearchSumeryViewModel
    {
        public long? VehicleId { get; set; }
        public string DriverName { get; set; }
        public string DriverNationality { get; set; }

        public string Make { get; set; }
        public string MakeImage { get; set; }

        public string Model { get; set; }
        public string ModelImage { get; set; }

        public string Year { get; set; }

        public string Type { get; set; }
        public string TypeDescreption { get; set; }
        public string TypeImage { get; set; }

        public List<string> Images { get; set; }

        public string Configuration { get; set; }

        public string Capacity { get; set; }

        public List<string> TripeTypes { get; set; }

        public List<string> PayLoadTypes { get; set; }

        public List<string> FareTypes { get; set; }

        public decimal? PricePerKM { get; set; }

        public bool? IsInsured { get; set; }

    }

    public class VehiclePopupViewModel
    {
        public long? VehicleId { get; set; }
        public string DriverName { get; set; }
        public string DriverNationality { get; set; }
        public string DriverPhone { get; set; }
        public string Type { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string TypeDescreption { get; set; }
        public string TypeImage { get; set; }
    }
}