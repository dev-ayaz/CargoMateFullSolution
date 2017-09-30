using CargoMate.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.Admin.Shared
{
    public class SearchFilter
    {
        public int PageLenght { get; set; }
        public int Start { get; set; }
        public Enums.OrderDirection OrderDirection { get; set; }
        public string OrderBy { get; set; }
        public string SearchTerm { get; set; }
        public int Draw { get; set; }
    }
}