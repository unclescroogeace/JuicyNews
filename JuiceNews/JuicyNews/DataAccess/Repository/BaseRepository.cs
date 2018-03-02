using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BaseRepository<T> where T : BaseEntity
    {
        protected DbContext Context { get; set; }
        protected DbSet<T> Items { get; set; }

        public BaseRepository()
        {
            Context = new MyDbContext();
            Items = Context.Set<T>();
        }

        public T GetById(int id)
        {
            return Items.Where(i => i.Id == id).FirstOrDefault();
        }

        public List<T> GetAll(int page, int pageSize)
        {
            return Items.OrderBy(i => i.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public void Save(T item)
        {
            if (item.Id <= 0)
                Items.Add(item);
            else
                Context.Entry(item).State = EntityState.Modified;

            Context.SaveChanges();
        }

        public void Delete(T item)
        {
            Items.Remove(item);

            Context.SaveChanges();
        }
    }
}
