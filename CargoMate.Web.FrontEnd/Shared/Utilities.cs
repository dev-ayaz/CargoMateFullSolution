
using System.Data.Entity.Spatial;

namespace CargoMate.Web.FrontEnd.Shared
{
    public static class Utilities
    {
        public static DbGeography StringtoDbGeography(string location)
        {
            if (string.IsNullOrEmpty(location)){
                return null;
            }
            else if (location.Contains("Point"))
            {
                return DbGeography.FromText(location, 4326);
            }
            return DbGeography.FromText($"POINT({location?.Replace(",", " "),4326 })");
        }
    }
}