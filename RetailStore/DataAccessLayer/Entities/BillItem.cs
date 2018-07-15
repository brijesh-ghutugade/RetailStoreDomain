using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailStore.DataAccessLayer.Entities
{
    public class BillItem
    {
              
        public int Id { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        
    }
}
