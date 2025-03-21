using System;
using System.Linq;

namespace SimpleRestAPI.Domain.Entities
{
    public class ServiceBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        private readonly IRepositoryBase<TEntity> repositoryBase;

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            repositoryBase = repository;
        }

        public async Task<Guid?> Add(TEntity entity)
        {
            return await repositoryBase.Add(entity);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<IQueryable<TEntity>> GetAll()
        {
            return repositoryBase.GetAll().Result.Where(x => x.Deleted == false);
        }

        public async Task<TEntity> GetById(Guid guid)
        {
            return await repositoryBase.GetById(guid);
        }

        public async Task<IQueryable<TEntity>> GetByWhere(string condition)
        {
            return repositoryBase.GetByWhere(condition).Result.Where(x => x.Deleted == false);
        }

        public async Task<bool> Remove(Guid guid)
        {
            return await repositoryBase.Remove(guid);
        }

        public async Task<bool> Update(TEntity entity)
        {
            return await repositoryBase.Update(entity);
        }
    }
}
