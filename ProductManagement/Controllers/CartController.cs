using DataLayer;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductManagement.Models;
using ProductManagement.Global;

namespace ProductManagement.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ProdDbContext _pdb;
        public CartController(ApplicationDbContext db, ProdDbContext pdb)
        {
            _db = db;
            _pdb = pdb;
        }

        public ViewResult ShowCart()
        {
            var cartItems = _db.ShoppingCarts.Where(user => user.ApplicationUserId == User.Identity.Name).ToList();
            foreach (var item in cartItems)
            {
                item.Product = _pdb.Products.Where(r => r.Id == item.ProductId).FirstOrDefault();
            }

            return View(cartItems);
        }
        public ActionResult AddToCart(int id)
        {


            var item = _db.ShoppingCarts.Where(user => user.ApplicationUserId == User.Identity.Name && user.ProductId == id).FirstOrDefault();

            if (item != null)
            {
                item.Count = item.Count + 1;
            }
            else
            {
                var addcartitem = new ShoppingCart()
                {
                    ApplicationUserId = User.Identity.Name,
                    ProductId = id

                };
                _db.ShoppingCarts.Add(addcartitem);
                GlobalVariables.cartItemCount = GlobalVariables.cartItemCount + 1;
            }
            _db.SaveChanges();
            
            return RedirectToAction("ShowCart");
            // return View();
        }

        public ActionResult CartItemCount(int id, string type)
        {

            var cartItems = _db.ShoppingCarts.Where(user => user.ApplicationUserId == User.Identity.Name && user.Id == id).FirstOrDefault();
            if (type == "add")
                cartItems.Count = cartItems.Count + 1;
            else
            {
                if (cartItems.Count == 1) return RedirectToAction("ShowCart");
                cartItems.Count = cartItems.Count - 1;
            }

            _db.SaveChanges();
            return RedirectToAction("ShowCart");
        }
        public ActionResult DeleteItem(int id)
        {
            var cartItems = _db.ShoppingCarts.Where(user => user.ApplicationUserId == User.Identity.Name && user.Id == id).FirstOrDefault();
            if (cartItems != null)
            {
                var data = _db.ShoppingCarts.Remove(cartItems);
                _db.SaveChanges();
                GlobalVariables.cartItemCount = GlobalVariables.cartItemCount - 1;
            }
            return RedirectToAction("ShowCart");
        }
        public ActionResult Summary()
        {
            string userId = User.Identity.Name;
            var currentUser = _db.Users.FirstOrDefault(x => x.Id == userId);
            //ViewBag.UserPhoneNumber = currentUser.PhoneNumber;
            var allCartitem = _db.ShoppingCarts.Where(user => user.ApplicationUserId == User.Identity.Name).ToList();
            foreach (var item in allCartitem)
            {
                item.Product = _pdb.Products.Where(r => r.Id == item.ProductId).FirstOrDefault();
            }
            return View(allCartitem);
        }
        public ActionResult Payment()
        {
            return View();
        }
        public ActionResult ClearCartItems()//To clear cart
        {
            var cartItems = _db.ShoppingCarts.Where(user => user.ApplicationUserId == User.Identity.Name).ToList();
            if (cartItems != null)
            {
                foreach (var v in cartItems)
                {
                    var data = _db.ShoppingCarts.Remove(v);
                    _db.SaveChanges();
                    GlobalVariables.cartItemCount = GlobalVariables.cartItemCount - 1;
                }
            }
            return RedirectToAction("Payment");
        }
    }
}