using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;

        public ProductManagerController()
        {
            context = new ProductRepository();
        }

        // GET: ProductManager
        [HttpGet]
        public ActionResult Index()
        {
            var products = context.Collection();
            return View(products);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var product = new Product();
            return View(product);
        }

        [HttpPost]
        public ActionResult Create(Product productToAdd)
        {
            if (ModelState.IsValid)
            {
                context.Insert(productToAdd);
                context.Commit();
                return RedirectToAction("Index", "ProductManager");
            }
            else
            {
                return View(productToAdd);
            }
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var productToEdit = context.Find(id);
            if (productToEdit != null)
            {
                return View(productToEdit);
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult Edit(string id, Product productEdited)
        {
            var product = context.Find(id);
            if (product != null)
            { 
                if (ModelState.IsValid)
                {
                    product.Name = productEdited.Name;
                    product.Price = productEdited.Price;
                    product.Image = productEdited.Image;
                    product.Description = productEdited.Description;
                    product.Category = productEdited.Category;
                    context.Update(product);
                    context.Commit();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(productEdited);
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            var productToDelete = context.Find(id);
            if (productToDelete != null)
            {
                return View(productToDelete);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            var productToDelete = context.Find(id);
            if (productToDelete != null)
            {
                context.Delete(id);
                context.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}