using TP_WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TP_WebService.Services
{
    public interface IGenericService<TEntity,TDto> where TEntity : BaseEntity
    {
        IList<TDto> Collection();
        void Save();
        int Count(Expression<Func<TEntity, bool>> predicateWhere);
        void Delete(int id);
        List<TDto> FindAll(int start, int maxByPage, Expression<Func<TEntity, int?>> keyOrderBy, Expression<Func<TEntity, bool>> predicateWhere);
        TDto FindById(int id);
        void Insert(TDto t);
        void Update(TDto t);
    }
}
