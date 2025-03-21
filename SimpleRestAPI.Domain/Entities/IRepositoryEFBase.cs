using System;
namespace SimpleRestAPI.Domain.Entities
{
    public interface IRepositoryEFBase<TEntity> : IDisposable where TEntity : EntityBase
    {
        Task<Guid?> Add(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Remove(Guid guid);
    }
}
