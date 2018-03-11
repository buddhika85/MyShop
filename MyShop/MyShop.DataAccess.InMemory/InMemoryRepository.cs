using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;
            if (items == null)
                items = new List<T>();
        }

        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T item)
        {
            items.Add(item);
        }

        public void Update(T item)
        {
            T itemToUpdate = items.Find(p => p.Id == item.Id);
            if (itemToUpdate != null)
            {
                itemToUpdate = item;
            }
            else
            {
                throw new Exception("Item not found");
            }
        }

        public T Find(string Id)
        {
            T item = items.Find(p => p.Id == Id);
            if (item != null)
            {
                return item;
            }
            else
            {
                throw new Exception("Item not found");
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable<T>();
        }

        public void Delete(string Id)
        {
            T itemToDelete = items.Find(p => p.Id == Id);
            if (itemToDelete != null)
            {
                items.Remove(itemToDelete);
            }
            else
            {
                throw new Exception("Item not found");
            }
        }
    }
}
