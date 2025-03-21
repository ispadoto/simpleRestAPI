using System;
using System.Linq;

namespace SimpleRestAPI.Domain.Entities
{
    public interface IRepositoryDapperBase<TEntity> : IDisposable where TEntity : EntityBase
    {
        Task<TEntity>  GetById(Guid guid);
        Task<IQueryable<TEntity>> GetAll();
        Task<IQueryable<TEntity>> GetByWhere(string condition);
    }
}
