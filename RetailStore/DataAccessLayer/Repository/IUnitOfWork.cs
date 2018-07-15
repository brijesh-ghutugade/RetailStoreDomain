using RetailStore.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailStore.DataAccessLayer.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Bill> BillRepository { get; set; }
        IRepository<BillItem> BillItemRepository { get; }
        IRepository<Product> ProductRepository { get; }
        void Save();
    }
}
