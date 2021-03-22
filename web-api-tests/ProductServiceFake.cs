using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using web_api.Contracts;
using web_api.Model;

namespace web_api_tests
{
    public class ProductServiceFake : IProductService
    {
        private readonly List<Product> _products;

        public ProductServiceFake()
        {
            _products = new List<Product>()
            {
                new Product() { Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    Name = "Orange Juice", Description="Orange Tree", Price = 5.00M },
                new Product() { Id = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),
                    Name = "Diary Milk", Description="Mad Cow", Price = 4.00M },
                new Product() { Id = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18ad"),
                    Name = "Frozen Pizza", Description="Uncle Mickey's", Price = 12.00M }
            };
        }

        public IEnumerable<Product> GetAllItems()
        {
            return _products;
        }

        public Product Add(Product newItem)
        {
            newItem.Id = Guid.NewGuid();
            _products.Add(newItem);
            return newItem;
        }

        public Product GetById(Guid id)
        {
            return _products.Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public void Remove(Guid id)
        {
            var existing = _products.First(a => a.Id == id);
            _products.Remove(existing);
        }
    }
}
