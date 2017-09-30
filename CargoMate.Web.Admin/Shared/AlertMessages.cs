using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.Admin.Shared
{
    public static class AlertMessages
    {
        public static object ModelError = new
        {
            IsError = true,
            Title = "Operation Fail",
            Message = "Validate your inputs & try again"
        };

        public static object SuccessResponse = new
        {
            IsError = false,
            Title = "Operation Success",
            Message = "Operation successfully completed"
        };

        public static object  FailureResponse = new
        {
            IsError = true,
            Title = "Operation Fail",
            Message = "No record affeted"
        };

        public static object RangeConflict = new
        {
            IsError = true,
            Title = "Operation Fail",
            Message = "Range conflict with existings values"
        };
        
    }
}