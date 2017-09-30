using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CargoMate.DataAccess.Contracts;

namespace CargoMate.Web.FrontEnd.BusinessLayer.Repositories
{
    public class CustomerRepository : BaseRepository
    {
        public CustomerRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }


    }
}
