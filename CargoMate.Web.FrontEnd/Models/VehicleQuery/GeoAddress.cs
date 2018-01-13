using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CargoMate.Web.FrontEnd.Models.VehicleQuery
{
    public class GeoAddress
    {
        public GeoAddress()
        {
            TypesOrder = new string[]{"country", "postal_code", "administrative_area_level_1",
            "administrative_area_level_2", "locality", "sublocality", "route"};
        }

        public List<GeoElement> AddressElements { get; set; }

        private string[] TypesOrder { get; set; }



        public string SubLocality
        {
            get
            {
                ReArrangeElements();
                for (int i = 0;i< AddressElements.Count; i++)
                { 
                    if (AddressElements[i].Types.Contains("sublocality"))
                        return AddressElements[i].Name;
                }
                return null;
            }
        }
        public string Locality
        {
            get
            {
                ReArrangeElements();
                for (int i = 0; i < AddressElements.Count; i++)
                {
                    if (AddressElements[i].Types.Contains("locality"))
                        return AddressElements[i].Name;
                }
                return null;
            }
        }
        public string Country
        {
            get
            {
                ReArrangeElements();
                for (int i = 0; i < AddressElements.Count; i++)
                {
                    if (AddressElements[i].Types.Contains("country"))
                        return AddressElements[i].Name;
                }
                return null;
            }
        }
        public string FullAddress
        {
            get
            {
                ReArrangeElements();
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < AddressElements.Count; i++)
                {
                    builder.Append(", " + AddressElements[i].Name);
                }
                return builder.ToString().Substring(2);
            }
        }
        public string ShortAddress
        {
            get
            {
                ReArrangeElements();
                StringBuilder builder = new StringBuilder();
                for (int i =0;i< AddressElements.Count; i++)
                {
                    builder.Append(", " + AddressElements[i].Name);
                }
                return builder.ToString().Substring(2);
            }
        }
        public string BasePath
        {
            get
            {
                ReArrangeElements();
                StringBuilder DBpathBuilder = new StringBuilder();
                foreach (GeoElement element in AddressElements)
                {
                    DBpathBuilder.Append(element.Name + "/");
                    if (element.Types.Contains("sublocality"))
                    { break; }
                }
                return DBpathBuilder.ToString();
            }
        }
        private void ReArrangeElements()
        {
            //List<GeoElement> tempAddressElements = new List<GeoElement>();
            //foreach (string type in TypesOrder)
            //{
            //    foreach (GeoElement element in AddressElements)
            //    {
            //        if (element.Types.Contains(type))
            //            tempAddressElements.Add(element);
            //    }
            //}
            //AddressElements = tempAddressElements;
        }
    }
}