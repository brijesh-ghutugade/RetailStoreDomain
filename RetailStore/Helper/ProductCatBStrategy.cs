using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RetailStore.DataAccessLayer.Entities;

namespace RetailStore.Helper
{
    public class ProductCatBStrategy : ICalculateStrategy
    {
       
        public double CalculateValue(BillItem billItem)
        {
            return billItem.Quantity * billItem.Product.Rate * 1.2;
        }
    }
}
