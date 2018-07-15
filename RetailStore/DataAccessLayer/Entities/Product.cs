using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailStore.DataAccessLayer.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BarCode { get; set; }
        public double Rate { get; set; }
        public Category Category { get; set; }


    }

    public enum Category
    {
        A = 1,
        B = 2,
        C = 3

    }
}
