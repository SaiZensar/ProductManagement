using DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Services
{
    public interface IProductData
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> Products { get; }
        Product GetById(int id);
        IEnumerable<Product> GetByName(string name);
        void Add(Product product);
        void Delete(int id);
        void Update(Product product);
    }
}
