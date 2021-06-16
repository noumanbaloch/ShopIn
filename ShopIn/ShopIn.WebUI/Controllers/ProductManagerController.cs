﻿using ShopIn.Core;
using ShopIn.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopIn.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository _context;
        public ProductManagerController()
        {
            _context = new ProductRepository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            IEnumerable<Product> products = _context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            _context.Insert(product);
            _context.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string Id)
        {
            Product product = _context.Find(Id);

            if(product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, string Id)
        {
            Product productToEdit = _context.Find(Id);
            if(productToEdit == null)
            {
                return HttpNotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(product);
            }


            productToEdit.Category = product.Category;
            productToEdit.Description = product.Description;
            productToEdit.Image = product.Image;
            productToEdit.Name = product.Name;
            productToEdit.Price = product.Price;

            _context.Update(productToEdit);
            _context.Commit();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string Id)
        {
            Product productToDelete = _context.Find(Id);
            if(productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = _context.Find(Id);

            if(productToDelete == null)
            {
                return HttpNotFound();
            }

            _context.Delete(Id);

            _context.Commit();

            return RedirectToAction("Index");
        }
    }
}