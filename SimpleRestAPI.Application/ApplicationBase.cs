using SimpleRestAPI.Domain.Entities;
using System;
using System.Linq;

namespace SimpleRestAPI.Application
{
    public class ApplicationBase<TEntity> : IDisposable, IApplicationBase<TEntity> where TEntity : EntityBase
    {
        private readonly IServiceBase<TEntity> _IServiceBase;
        public ApplicationBase(IServiceBase<TEntity> serviceBase)
        {
            _IServiceBase = serviceBase;
        }

        public async Task<Guid?> Add(TEntity entity)
        {
            return await _IServiceBase.Add(entity);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<IQueryable<TEntity>> GetAll()
        {
            return await _IServiceBase.GetAll();
        }

        public async Task<TEntity> GetById(Guid guid)
        {
            return await _IServiceBase.GetById(guid);
        }

        public async Task<IQueryable<TEntity>> GetByWhere(string condition)
        {
            return await _IServiceBase.GetByWhere(condition);
        }

        public async Task<bool> Remove(Guid guid)
        {
            return await _IServiceBase.Remove(guid);
        }

        public async Task<bool> Update(TEntity entity)
        {
            return await _IServiceBase.Update(entity);
        }
    }
}
