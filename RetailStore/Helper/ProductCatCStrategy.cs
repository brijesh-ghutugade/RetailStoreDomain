using RetailStore.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailStore.Helper
{
    public class ProductCatCStrategy : ICalculateStrategy
    {
        public double CalculateValue(BillItem billItem)
        {
            return billItem.Quantity * billItem.Product.Rate;
        }
    }
}
