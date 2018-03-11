using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
                productCategories = new List<ProductCategory>();
        }

        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory ProductCategory)
        {
            productCategories.Add(ProductCategory);
        }

        public void Update(ProductCategory ProductCategory)
        {
            ProductCategory categoryToUpdate = productCategories.Find(p => p.Id == ProductCategory.Id);
            if (categoryToUpdate != null)
            {
                categoryToUpdate = ProductCategory;
            }
            else
            {
                throw new Exception("ProductCategory not found");
            }
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory productCategory = productCategories.Find(p => p.Id == Id);
            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("ProductCategory not found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable<ProductCategory>();
        }

        public void Delete(string Id)
        {
            ProductCategory categoryToDelete = productCategories.Find(p => p.Id == Id);
            if (categoryToDelete != null)
            {
                productCategories.Remove(categoryToDelete);
            }
            else
            {
                throw new Exception("ProductCategory not found");
            }
        }
    }
}
