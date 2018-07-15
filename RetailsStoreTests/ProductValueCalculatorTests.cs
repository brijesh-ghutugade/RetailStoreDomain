using Microsoft.VisualStudio.TestTools.UnitTesting;
using RetailStore.DataAccessLayer.Entities;
using RetailStore.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetailsStoreTests
{
    [TestClass]
    public class ProductValueCalculatorTests
    {
        [TestInitialize]
        public void Initialise()
        {


        }

        [TestMethod]
        public void Should_Add_TenPercentTax_For_CategoryAProducts()
        {
            ProductValueCalculator productValueCalculator = new ProductValueCalculator(new ProductCatAStrategy());
            double productValue = productValueCalculator.CalculateValue(new BillItem() { Product = new Product() { Rate = 100 }, Quantity = 1 });
            Assert.AreEqual(productValue.ToString("F4"), 110.ToString("F4"));
        }


        [TestMethod]
        public void Should_Add_TenPercentTax_For_CategoryBProducts()
        {
            ProductValueCalculator productValueCalculator = new ProductValueCalculator(new ProductCatBStrategy());
            double productValue = productValueCalculator.CalculateValue(new BillItem() { Product = new Product() { Rate = 100 }, Quantity = 1 });
            Assert.AreEqual(productValue.ToString("F4"), 120.ToString("F4"));
        }

        [TestMethod]
        public void Should_Add_TenPercentTax_For_CategoryCProducts()
        {
            ProductValueCalculator productValueCalculator = new ProductValueCalculator(new ProductCatCStrategy());
            double productValue = productValueCalculator.CalculateValue(new BillItem() { Product = new Product() { Rate = 100 }, Quantity = 1 });
            Assert.AreEqual(productValue.ToString("F4"), 100.ToString("F4"));
        }
    }
}
