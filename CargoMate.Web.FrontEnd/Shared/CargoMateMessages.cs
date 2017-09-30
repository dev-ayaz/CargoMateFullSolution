using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Shared
{
    public class CargoMateMessages
    {
        public static object ModelError = new
        {
            IsError = true,
            MessageHeader = "Operation Fail",
            Message = "Validate your inputs & try again"
        };

        public static object SuccessResponse = new
        {
            IsError = false,
            MessageHeader = "Operation Success",
            Message = "Operation successfully completed"
        };

        public static object FailureResponse = new
        {
            IsError = true,
            MessageHeader = "Operation Fail",
            Message = "No record affeted"
        };
    }
}