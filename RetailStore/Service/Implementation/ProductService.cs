using RetailStore.DataAccessLayer.Entities;
using RetailStore.DataAccessLayer.Repository;
using RetailStore.DTO;
using RetailStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailStore.Service.Implementation
{
    public class ProductService : IProductService
    {
        IUnitOfWork unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }

        public void CreateProduct(ProductDto productDto)
        {
            VerifyIfProductExists(productDto.BarCode);
            Product product = new Product
            {
                BarCode = productDto.BarCode,
                Name = productDto.Name,
                Category = productDto.Category,
                Rate = productDto.Rate
            };
            unitOfWork.ProductRepository.Insert(product);
            unitOfWork.Save();
        }

        public void DeleteProduct(long id)
        {
            VerifyIfProductExists(id);           
            Product product = unitOfWork.ProductRepository.GetById(id);
            if (product != null)
            {
                unitOfWork.ProductRepository.Delete(product);
                unitOfWork.Save();
            }
        }

        public IQueryable<Product> GetAllProducts()
        {
            return unitOfWork.ProductRepository.GetAll();
        }

        public Product GetProductById(long id)
        {
            VerifyIfProductExists(id);
            return unitOfWork.ProductRepository.GetById(id);

        }

        public void UpdateProduct(ProductDto productDto, long id)
        {
            VerifyIfProductExists(id);
            Product product = new Product
            {
                BarCode = productDto.BarCode,
                Name = productDto.Name,
                Category = productDto.Category,
                Rate = productDto.Rate
            };
            unitOfWork.ProductRepository.Edit(product);
            unitOfWork.Save();

        }

        public void VerifyIfProductExists(string barCode)
        {
            IEnumerable<Product> products = unitOfWork.ProductRepository.FindBy(p => p.BarCode == barCode);
            if (products != null && products.Count() == 0)
                throw new ApplicationException($"A Product with bar code {barCode} exists in Product master");
        }

        public void VerifyIfProductExists(long id)
        {
            IEnumerable<Product> products = unitOfWork.ProductRepository.FindBy(p => p.Id == id);
            if (products != null && products.Count() == 0)
                throw new ApplicationException($"A Product with Id {id} exists in Product master");
        }

              
    }
}
