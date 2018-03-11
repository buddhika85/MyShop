using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        IRepository<ProductCategory> context;

        public ProductCategoryManagerController(IRepository<ProductCategory> productContext)
        {
            context = productContext;
        }

        // GET: ProductManager
        [HttpGet]
        public ActionResult Index()
        {
            var categories = context.Collection();
            return View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var category = new ProductCategory();
            return View(category);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategoryToAdd)
        {
            if (ModelState.IsValid)
            {
                context.Insert(productCategoryToAdd);
                context.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return View(productCategoryToAdd);
            }
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var productCategoryToEdit = context.Find(id);
            if (productCategoryToEdit != null)
            {
                return View(productCategoryToEdit);
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult Edit(string id, ProductCategory productCategoryEdited)
        {
            var productCategory = context.Find(id);
            if (productCategory != null)
            {
                if (ModelState.IsValid)
                {
                    productCategory.Category = productCategoryEdited.Category;                                       
                    context.Update(productCategory);
                    context.Commit();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(productCategoryEdited);
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
            var productCategoryToDelete = context.Find(id);
            if (productCategoryToDelete != null)
            {
                return View(productCategoryToDelete);
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
            var productCategoryToDelete = context.Find(id);
            if (productCategoryToDelete != null)
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