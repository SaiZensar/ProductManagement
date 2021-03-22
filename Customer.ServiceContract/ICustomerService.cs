using DomainModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Customer.ServiceContract
{
    public interface ICustomerService
    {
        Product Details(int id);

        IEnumerable<Product> Search(string prodSearch);

        IEnumerable<Product> List(string prodCode);

        IEnumerable<Product> ListOfProducts();
    }
}
