using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using RetailStore.DataAccessLayer.Repository;
using RetailStore.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Linq;
using RetailStore.Service.Implementation;
using RetailStore.Service.Interfaces;
using System.Linq.Expressions;
using System;

namespace RetailsStoreTests
{
    [TestClass]
    public class BillServiceTests
    {

        private IUnitOfWork _unitOfWork = A.Fake<IUnitOfWork>();
        private IProductService _productService = A.Fake<IProductService>();
        List<Bill> bills = new List<Bill>();
        List<Product> products = new List<Product>();
        static Product product = new Product() { Rate = 500, Name = "RattleSet", BarCode = "ASXHJ", Category = Category.C, Id = 1 };
        static BillItem billItem = new BillItem() { Product = product, Id = 0, Quantity = 1 };
        static List<BillItem> billItems = new List<BillItem>();
        static Bill bill = new Bill() { BillItems = billItems, Id = 1, NoOfItems = 1, TotalValue = 500 };
        BillService billService = null;

        [TestInitialize]
        public void Initialise()
        {
            billService = new BillService(_unitOfWork, _productService);
            products.Add(product);
            IQueryable<Bill> queryablebills = bills.AsQueryable();
            billItems.Add(billItem);
            A.CallTo(() => _unitOfWork.BillRepository.GetAll()).Returns(queryablebills);
            A.CallTo(() => _unitOfWork.BillRepository.GetById(A<long>.Ignored)).Returns(new Bill() { Id = 1, NoOfItems = 0, BillItems = new List<BillItem>(), TotalValue = 0 });
            A.CallTo(() => _unitOfWork.ProductRepository.FindBy(A<Expression<Func<Product, bool>>>.Ignored)).Returns(products.AsQueryable());

        }

        [TestMethod]
        public void AddProduct_Should_Call_BillItemRepositoryInsert_And_BillRepositoryEdit()
        {

            Bill billCreated = billService.AddProductToBill(1, "ASXHJ", 1);
            A.CallTo(() => _unitOfWork.BillRepository.GetById(1)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.BillItemRepository.Insert(A<BillItem>.Ignored)).MustHaveHappened();
            A.CallTo(() => _unitOfWork.ProductRepository.FindBy(A<Expression<Func<Product, bool>>>._)).MustHaveHappened();
            A.CallTo(() => _unitOfWork.BillRepository.Edit(A<Bill>._)).MustHaveHappened();
            Assert.AreEqual(bill.Id, billCreated.Id);
            Assert.AreEqual(billCreated.BillItems[0].Product.BarCode, bill.BillItems[0].Product.BarCode);
        }

        [TestMethod]
        public void CreateBill_Should_Call_BillRepositoryInsert()
        {
            Bill bill = new Bill() { TotalValue = 500 };
            billService.CreateBill(bill);
            A.CallTo(() => _unitOfWork.BillRepository.Insert(A<Bill>.That.IsEqualTo(bill))).MustHaveHappened();
        }

    }
}
