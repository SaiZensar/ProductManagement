using Customer.ServiceContract;
using DataLayer;
using DataLayer.Services;
using DomainModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Controllers
{

    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IConfiguration _configuration;
        string apiurl;
        private readonly ProdDbContext _db;
        private ICustomerService customerService;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductController(ILogger<ProductController> logger, ProdDbContext db, IConfiguration configuration, IWebHostEnvironment hostEnvironment, ICustomerService _cs)
        {
            _logger = logger;
            _db = db;
            _configuration = configuration;
            customerService = _cs;
            apiurl = _configuration.GetValue<string>("WebAPIBaseUrl");
            webHostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var prod = new List<Product>();
            using (HttpClient client = new HttpClient())
            {
                using (var response = await client.GetAsync(apiurl))
                /* using (var response = await client.GetAsync(apiurl))*/
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    prod = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }
            }
            return View(prod);
        }
        [HttpGet]
        public IActionResult Index(string ProdSearch)
        {
            ViewData["GetProductDetails"] = ProdSearch;

            var prodquery = customerService.Search(ProdSearch);
            return View(prodquery);
        }

        public ViewResult Create() => View();


        [HttpPost]
        public IActionResult Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                Product product = new Product
                {
                    Id = model.Id,
                    Name = model.Name,
                    Code = model.Code,
                    Available = model.Available,
                    Price = model.Price,
                    Rating = model.Rating,
                    Image = uniqueFileName,
                    Description = model.Description,
                };

                _db.Products.Add(product);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        private string UploadedFile(ProductViewModel model)
        {
            string uniqueFileName = null;

            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        //[HttpDelete]
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiurl);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync($"{apiurl}/{id}");
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }

        }
        public async Task<IActionResult> Edit(int id)
        {
            var prodData = new Product();

            using (HttpClient client = new HttpClient())
            {

                using (var response = await client.GetAsync($"{apiurl}/{id}"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    prodData = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }
            return View(prodData);
        }


        [HttpPost]

        public async Task<IActionResult> Edits(int id, Product product)
        {
            var prodData = new Product();
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8,
                    "application/json");
                using (var response = await client.PutAsync($"{apiurl}/{id}", content))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    prodData = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            var prod = new Product();
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync($"{apiurl}/{id}"))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        prod = JsonConvert.DeserializeObject<Product>(apiResponse);
                    }
                    else
                    {
                        //var noResponse = response.StatusCode.ToString();
                        //return View(noResponse);
                        ViewBag.StatusCode = response.StatusCode;
                    }
                }
            }
            return View(prod);



        }
        [HttpPost]
        public IActionResult Edit(int id,Product product)
        {
            var res = _db.Products.FirstOrDefault(e => e.Id == id);
            
                res.Id = product.Id;
                res.Name = product.Name;
                res.Code = product.Code;
                res.Rating = product.Rating;
                res.Description = product.Description;
                res.Available = product.Available;
                res.Price = product.Price;
                _db.SaveChanges();
                return RedirectToAction("Index");
            
        }

    }
}