using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailStore.Service.Interfaces
{
    interface IBillItemService
    {
        void VerifyIfBillItemExists(long id);
    }
}
