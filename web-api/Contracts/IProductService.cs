using System;
using System.Collections.Generic;
using web_api.Model;

namespace web_api.Contracts
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllItems();
        Product Add(Product newItem);
        Product GetById(Guid id);
        void Remove(Guid id);
    }
}
