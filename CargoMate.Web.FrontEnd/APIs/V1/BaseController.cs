using CargoMate.DataAccess.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CargoMate.Web.FrontEnd.APIs.V1
{
    [RoutePrefix("api/v1")]
    public class BaseController : ApiController
    {
        public IUnitOfWork UnitOfWork { get; }

        public BaseController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
