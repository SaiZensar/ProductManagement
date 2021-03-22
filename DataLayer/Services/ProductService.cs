using DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Services
{
    public class ProductService : IProductData
    {
        private readonly ProdDbContext _db;

        public ProductService(ProdDbContext db)
        {
            _db = db;

        }

        public IEnumerable<Product> Products
        {
            get
            {
                return _db.Products.Include(c => c);
            }
        }


        public void Add(Product product)
        {

            _db.Products.Add(product);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var prod = _db.Products.Find(id);
            _db.Products.Remove(prod);
            _db.SaveChanges();
        }
        public IEnumerable<Product> GetAll()
        {
            var prods = _db.Products.ToList();
            return prods;
        }

        public Product GetById(int id)
        {
            return _db.Products.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Product> GetByName(string name)
        {
            return _db.Products.Where(r => r.Name.Contains(name)).ToList();
        }


        public void Update(Product product)
        {
            var existingProduct = _db.Products.Where(p => p.Id == product.Id).FirstOrDefault();
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Code = product.Code;
            existingProduct.Description = product.Description;
            _db.SaveChanges();
        }
    }
}