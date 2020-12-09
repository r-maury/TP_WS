using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using TP_WebService.Models;

namespace TP_WebService.Repositories {
    public interface IGenericRepository<T> where T : BaseEntity {
        IQueryable<T> Collection();
        void Save();
        int Count(Expression<Func<T, bool>> predicateWhere);
        void Delete(int id);
        List<T> FindAll(int start, int maxByPage, Expression<Func<T, int?>> keyOrderBy, Expression<Func<T, bool>> predicateWhere);
        T FindById(int id);
        void Insert(T t);
        void Update(T t);
        List<T> FindBy(Expression<Func<T, bool>> predicateWhere);
    }
}