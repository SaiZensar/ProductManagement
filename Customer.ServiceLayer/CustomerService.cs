using Customer.ServiceContract;
using DataLayer;
using DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Customer.ServiceLayer
{
    public class CustomerService : ICustomerService
    {
        ProdDbContext db;

        public CustomerService(ProdDbContext _db)
        {
            db = _db;
        }

        public Product Details(int id)
        {
            var user = db.Products.Where(e => e.Id == id).SingleOrDefault();
            return user;
        }

        public IEnumerable<Product> List(string prodCode)
        {
            var products = db.Products.Where(p => p.Code.Contains(prodCode)).OrderBy(p => p.Id);
            return products;
        }

        public IEnumerable<Product> ListOfProducts()
        {
            return db.Products.ToList();
        }

        public IEnumerable<Product> Search(string prodSearch)
        {
            var prodquery = from x in db.Products select x;
            if (!string.IsNullOrEmpty(prodSearch))
            {
                prodquery = prodquery.Where(x => x.Name.Contains(prodSearch));
            }
            return prodquery.AsNoTracking().ToList();
        }
    }
}
