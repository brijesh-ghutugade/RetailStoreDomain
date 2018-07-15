using Microsoft.EntityFrameworkCore;
using RetailStore.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailStore.DataAccessLayer.Repository
{
    public partial class UnitOfWork : IUnitOfWork
    {
        private DbContext _context;
        private IRepository<Bill> _billRepository;  
        public IRepository<Bill> BillRepository
        {
            get
            {

                if (_billRepository == null)
                    _billRepository = new Repository<Bill>(_context);
                return _billRepository;
            }
            set
            {
                if (value != null)
                    _billRepository = value;
            }
        }

        private IRepository<BillItem>_billItemRepository;
        public IRepository<BillItem> BillItemRepository
        {
            get
            {

                if (_billItemRepository == null)
                    _billItemRepository = new Repository<BillItem>(_context);
                return _billItemRepository;
            }
        }

        private IRepository<Product> _productRepository;
        public IRepository<Product> ProductRepository
        {
            get
            {

                if (_productRepository == null)
                    _productRepository = new Repository<Product>(_context);
                return _productRepository;
            }
        }

     
        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this._context.Dispose();
                }
            }

            this.disposed = true;
        }

    }
}
