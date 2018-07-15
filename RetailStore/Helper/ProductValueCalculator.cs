using RetailStore.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailStore.Helper
{
    public class ProductValueCalculator
    {
        private ICalculateStrategy strategy;
        public ProductValueCalculator(ICalculateStrategy strategy)
        {
            this.strategy = strategy;
        }

       public double CalculateValue(BillItem billItem)
        {
            return this.strategy.CalculateValue(billItem);
        }
    }
}
