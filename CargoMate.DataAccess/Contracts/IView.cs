using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Contracts
{
    public interface IView<T> where T : class
    {
        IQueryable<T> Retrieve(int pageNumber = 0, int pageSize = 0,
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        IQueryable<T> Retrieve();
    }
}
