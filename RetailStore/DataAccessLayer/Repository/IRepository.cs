using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RetailStore.DataAccessLayer.Repository
{
    public interface IRepository<T>

    {
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        T GetById(long id);

        IQueryable<T> GetAll();

        void Edit(T entity);

        void Insert(T entity);

        void Delete(T entity);

    }
}
