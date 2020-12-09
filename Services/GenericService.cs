using FastMapper;
using TP_WebService.Tools;
using TP_WebService.Models;
using TP_WebService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace TP_WebService.Services
{
    public class GenericService<TEntity, TDto> : IGenericService<TEntity, TDto> where TEntity : BaseEntity
    {
        protected IGenericRepository<TEntity> genericRepository;

        public GenericService(IGenericRepository<TEntity> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public IList<TDto> Collection()
        {
            List<TEntity> res = genericRepository.Collection().ToList();
            //on convertit le code
            var resDto = DtoTools.Convert<List<TEntity>, List<TDto>>(res);
            return resDto;
        }

        public void Save()
        {
            genericRepository.Save();
        }

        public int Count(Expression<Func<TEntity, bool>> predicateWhere)
        {
            return genericRepository.Count(predicateWhere);
        }

        public void Delete(int id)
        {
            genericRepository.Delete(id);
        }

        public List<TDto> FindAll(int start, int maxByPage, Expression<Func<TEntity, int?>> keyOrderBy, Expression<Func<TEntity, bool>> predicateWhere)
        {
            var res = genericRepository.FindAll(start, maxByPage, keyOrderBy, predicateWhere);
            var resDto = DtoTools.Convert<List<TEntity>, List<TDto>>(res);
            return resDto;
        }

        public TDto FindById(int id)
        {
            var res = genericRepository.FindById(id);
            var resDto = DtoTools.Convert<TEntity, TDto>(res);
            return resDto;
        }

        public virtual void Insert(TDto t)
        {
            //Dto ===> Entity
            var tObj = DtoTools.Convert<TDto,TEntity>(t);
            //on insère l'Entity
            genericRepository.Insert(tObj);
        }

        public void Update(TDto t)
        {
            var tObj = DtoTools.Convert<TDto, TEntity>(t);
            genericRepository.Update(tObj);
        }
    }
}