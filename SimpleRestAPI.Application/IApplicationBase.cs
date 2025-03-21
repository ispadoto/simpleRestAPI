using SimpleRestAPI.Domain.Entities;
using System;
using System.Linq;


namespace SimpleRestAPI.Application
{
    public interface IApplicationBase<TEntity> where TEntity : EntityBase
    {
        Task<Guid?> Add(TEntity entity);
        Task<TEntity> GetById(Guid guid);
        Task<IQueryable<TEntity>> GetAll();
        Task<bool> Update(TEntity entity);
        Task<bool> Remove(Guid guid);
        Task<IQueryable<TEntity>> GetByWhere(string condition);
    }
}
