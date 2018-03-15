using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using MyShop.Core.ViewModels;
using MyShop.Core.Contracts;
using System.IO;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoriesContext)
        {
            context = productContext;
            productCategories = productCategoriesContext;
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
            var productManagerViewModel = new ProductManagerViewModel();
            productManagerViewModel.Product = new Product();
            productManagerViewModel.Categories = productCategories.Collection();
            return View(productManagerViewModel);
        }

        [HttpPost]
        public ActionResult Create(ProductManagerViewModel productToAdd, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        productToAdd.Product.Image = productToAdd.Product.Id + Path.GetExtension(file.FileName);
                        file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToAdd.Product.Image);
                    }
                    context.Insert(productToAdd.Product);
                    context.Commit();
                    return RedirectToAction("Index");
                }
                else
                {                    
                    return View(productToAdd);
                }
            }
            catch (Exception ex)
            {

                throw;
            }            
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var productManagerViewModel = new ProductManagerViewModel();
            var productToEdit = context.Find(id);
            if (productToEdit != null)
            {
                productManagerViewModel.Product = productToEdit;
                productManagerViewModel.Categories = productCategories.Collection();
                return View(productManagerViewModel);
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult Edit(string id, ProductManagerViewModel productEdited, HttpPostedFileBase file)
        {
            var product = context.Find(id);
            if (product != null)
            { 
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        productEdited.Product.Image = productEdited.Product.Id + Path.GetExtension(file.FileName);
                        file.SaveAs(Server.MapPath("//Content//ProductImages//") + productEdited.Product.Image);
                    }
                    product.Name = productEdited.Product.Name;
                    product.Price = productEdited.Product.Price;
                    product.Image = productEdited.Product.Image;
                    product.Description = productEdited.Product.Description;
                    product.Category = productEdited.Product.Category;
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