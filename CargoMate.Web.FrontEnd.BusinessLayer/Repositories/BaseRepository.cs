using CargoMate.DataAccess.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.Web.FrontEnd.BusinessLayer.Repositories
{
    public class BaseRepository
    {
        public IUnitOfWork UnitOfWork { get; }

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
