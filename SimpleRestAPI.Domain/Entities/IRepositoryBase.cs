namespace SimpleRestAPI.Domain.Entities
{
    public interface IRepositoryBase<TEntity> : IRepositoryEFBase<TEntity>, IRepositoryDapperBase<TEntity>
        where TEntity : EntityBase
    {

    }
}
