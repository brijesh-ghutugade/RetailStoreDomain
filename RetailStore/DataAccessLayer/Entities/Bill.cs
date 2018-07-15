using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailStore.DataAccessLayer.Entities
{
    public class Bill
    {
        public int Id { get; set; }
        public virtual IList<BillItem> BillItems { get; set; }
        public double TotalValue { get; set; }
        public int NoOfItems { get; set; }
    }
}
