using System;
using System.Collections.Generic;
using web_api.Contracts;
using web_api.Model;

namespace web_api.Services
{
    public class ProductServices : IProductService
    {
        public Product Add(Product newItem)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAllItems()
        {
            throw new NotImplementedException();
        }

        public Product GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
