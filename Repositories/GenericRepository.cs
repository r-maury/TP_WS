using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using TP_WebService.Models;

namespace TP_WebService.Repositories {
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity {
        protected MyDbContext DataContext;
        protected DbSet<T> dbSet;

        public GenericRepository(MyDbContext DataContext) {
            this.DataContext = DataContext;
            this.dbSet = DataContext.Set<T>();

        }

        public List<T> FindAll(int start, int maxByPage,
                                Expression<Func<T, int?>> keyOrderBy,
                                Expression<Func<T, bool>> predicateWhere) {
            IQueryable<T> req = dbSet.AsNoTracking().OrderBy(keyOrderBy);

            if(predicateWhere != null)
                req = req.Where(predicateWhere);

            req = req.Skip(start).Take(maxByPage);
            return req.ToList();
        }

        public int Count(Expression<Func<T, bool>> predicateWhere) {
            IQueryable<T> req = dbSet.AsNoTracking();

            if(predicateWhere != null)
                req = req.Where(predicateWhere);

            return req.Count();
        }


        public IQueryable<T> Collection() {
            return dbSet;
        }

        public void Save() {
            DataContext.SaveChanges();
        }

        public void Delete(int id) {
            var t = FindById(id);
            if(DataContext.Entry(t).State == EntityState.Detached) {
                dbSet.Attach(t);
            }
            dbSet.Remove(t);
        }


        public T FindById(int id) {
            return dbSet.Find(id);
        }

        public void Insert(T t) {
            dbSet.Add(t);
            Save();
        }

        public void Update(T t) {
            dbSet.Attach(t);
            DataContext.Entry(t).State = EntityState.Modified;
        }

        public List<T> FindBy(Expression<Func<T, bool>> predicateWhere) {
            IQueryable<T> req = dbSet.AsNoTracking();

            if(predicateWhere != null)
                req = req.Where(predicateWhere);

            return req.ToList();
        }

    }
}