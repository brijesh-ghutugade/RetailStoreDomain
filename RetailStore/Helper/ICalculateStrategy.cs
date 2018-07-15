using RetailStore.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailStore.Helper
{
    public interface ICalculateStrategy
    {
        double CalculateValue(BillItem billItem);
    }
}
