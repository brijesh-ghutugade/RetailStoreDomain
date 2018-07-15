using RetailStore.DataAccessLayer.Entities;
using RetailStore.DataAccessLayer.Repository;
using RetailStore.Helper;
using RetailStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailStore.Service.Implementation
{
    public class BillService : IBillService
    {

        IUnitOfWork unitOfWork;
        IProductService productService;

        public BillService(IUnitOfWork unitOfWork,IProductService productService)
        {
            this.unitOfWork = unitOfWork;
            this.productService = productService;

        }

        public Bill AddProductToBill(long billId, string productBarCode, int quantity)
        {
            Bill bill = unitOfWork.BillRepository.GetById(billId);
            VerifyIfProductExists(productBarCode);
            Product product = unitOfWork.ProductRepository.FindBy(p => p.BarCode == productBarCode).ToList().FirstOrDefault();

           
            BillItem billItem = new BillItem();
            billItem.Product = product;
            billItem.Quantity = quantity;
            if (bill.BillItems != null)
            {
                BillItem existingBillItem = bill.BillItems.Where(b => b.Product.BarCode == productBarCode).FirstOrDefault();
                if (existingBillItem == null)
                    bill.BillItems.Add(billItem);
                else
                {
                    existingBillItem.Quantity++;
                }
            }
            else
            {
                bill.BillItems = new List<BillItem>();
                bill.BillItems.Add(billItem);
            }
            unitOfWork.BillItemRepository.Insert(billItem);
            unitOfWork.BillRepository.Edit(bill);
            unitOfWork.Save();
            return bill;
        }

        public void CalculateBillAmount(Bill bill)
        {
            double totalValue = 0;
            foreach (var item in bill.BillItems)
            {
                totalValue += CalculateValueForItem(item);
            }
            bill.TotalValue = totalValue;
            unitOfWork.BillRepository.Edit(bill);
            unitOfWork.Save();
        }

        public double CalculateValueForItem(BillItem billItem)
        {
            ProductValueCalculator productValueCalculator;
            double productValue = 0;
            switch (billItem?.Product?.Category)
            {

                case Category.A:
                    productValueCalculator = new ProductValueCalculator(new ProductCatAStrategy());                 
                    break;
                case Category.B:
                    productValueCalculator = new ProductValueCalculator(new ProductCatBStrategy());                   
                    break;
                case Category.C:
                    productValueCalculator = new ProductValueCalculator(new ProductCatCStrategy());                 
                    break;
                default:
                    productValueCalculator = new ProductValueCalculator(new ProductCatCStrategy());
                    break;
            }
            productValue = productValueCalculator.CalculateValue(billItem);
            return productValue;
        }

        public void CreateBill(Bill bill)
        {
            unitOfWork.BillRepository.Insert(bill);
            unitOfWork.Save();
        }

        public void DeleteBill(Bill bill)
        {
            unitOfWork.BillRepository.Delete(bill);
            unitOfWork.Save();
        }

        public IEnumerable<Bill> GetAllBills()
        {
            return unitOfWork.BillRepository.GetAll();
        }

        public Bill GetBillById(long id)
        {
            return unitOfWork.BillRepository.GetById(id);
        }

        public Bill RemoveProductFromBill(long billId, string barCode)
        {
            Bill bill = unitOfWork.BillRepository.GetById(billId);

            VerifyIfProductExists(barCode);
            if (bill.BillItems != null)
            {
                BillItem billItem = bill.BillItems.Where(i => i.Product.BarCode == barCode).FirstOrDefault();
                if (billItem != null)
                    bill.BillItems.Remove(billItem);
            }
            else
            {
                throw new ApplicationException("There are no bill items in the bill.");
            }

            unitOfWork.BillRepository.Edit(bill);
            unitOfWork.Save();
            return bill;

        }

        public void VerifyBillExists(long id)
        {
            Bill bill = unitOfWork.BillRepository.GetById(id);
            if (bill == null)
            {
                throw new ApplicationException("Bill with id " + id + " not found");
            }
        }

        public void VerifyIfProductExists(string barCode)
        {
            this.productService.VerifyIfProductExists(barCode);
        }

    }
}
