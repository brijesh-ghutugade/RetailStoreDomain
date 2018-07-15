using RetailStore.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailStore.Service.Interfaces
{
    interface IBillService
    {
        Bill AddProductToBill(long billId, string productBarCode, int quantity);

        void CalculateBillAmount(Bill bill);

        double CalculateValueForItem(BillItem billItem);

        void CreateBill(Bill bill);

        void DeleteBill(Bill id);

        IEnumerable<Bill> GetAllBills();

        Bill GetBillById(long id);

        Bill RemoveProductFromBill(long billId, string barCode);

        void VerifyBillExists(long id);

        void VerifyIfProductExists(string barCode);

    }
}
