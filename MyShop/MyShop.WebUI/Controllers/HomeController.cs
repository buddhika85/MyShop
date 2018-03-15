using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoriesContext)
        {
            context = productContext;
            productCategories = productCategoriesContext;
        }
                
        public ActionResult Index(string category = null)
        {
            var productListViewModel = new ProductListViewModel
            {
                Categories = productCategories.Collection()
            };

            if (category == null)
            {
                productListViewModel.Products = context.Collection();
                return View(productListViewModel);
            }

            productListViewModel.Products = context.Collection().Where(p => p.Category == category);
            return View(productListViewModel);
        }

        public ActionResult Details(string Id)
        {
            var product = context.Find(Id);
            if (product != null)
            {
                return View(product);
            }
            else
            {
                return HttpNotFound();
            }
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}