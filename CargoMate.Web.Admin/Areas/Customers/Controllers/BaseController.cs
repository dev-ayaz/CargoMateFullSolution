using CargoMate.DataAccess.Contracts;
using System.Web.Mvc;


namespace CargoMate.Web.Admin.Areas.Customers.Controllers
{
    public class BaseController : Controller
    {
        public IUnitOfWork UnitOfWork { get; }

        public BaseController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}