using RetailStore.DataAccessLayer.Entities;
using RetailStore.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailStore.Service.Interfaces
{
   public interface IProductService
    {
        void CreateProduct(ProductDto productDto);

        void DeleteProduct(long id);        

        IQueryable<Product> GetAllProducts();

        Product GetProductById(long id);

        void UpdateProduct(ProductDto productDto, long id);

        void VerifyIfProductExists(string barCode);

        void VerifyIfProductExists(long id);

    }
}
