
using System;
using System.Linq;
using Dapper;
using Microsoft.EntityFrameworkCore;
using SimpleRestAPI.Domain.Entities;
using SimpleRestAPI.Infra.Database.Context;
using static Dapper.SqlMapper;

namespace SimpleRestAPI.Infra.Database.Repositories
{ 
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected DbSet<TEntity> DbSet;
        protected readonly SimpleRestDB _context;
        protected string _connectionString;

        public RepositoryBase(SimpleRestDB dbContext)
        {
            _context = dbContext;
            _connectionString = _context.Database.GetDbConnection().ConnectionString;
            DbSet = _context.Set<TEntity>();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<Guid?> Add(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            int result = await _context.SaveChangesAsync();

            if (result > 0)
                return entity.Id;
            else
                return null;
        }

        public async Task<TEntity> GetById(Guid guid)
        {
            using (var conn = new SqlConnectionHelper(_connectionString))
            {
                return await conn.GetAsync<TEntity>(guid);
            }
        }

        public async Task<IQueryable<TEntity>> GetAll()
        {
            using (var conn = new SqlConnectionHelper(_connectionString))
            {
                var result =  await conn.GetListAsync<TEntity>(" WHERE Deleted = 0 ");
                return result.AsQueryable();
            }
        }

        public async Task<bool> Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Remove(Guid guid)
        {
            var entity = await GetById(guid);
            entity.Deleted = true;
            return await Update(entity);
        }

        public async Task<IQueryable<TEntity>> GetByWhere(string condition)
        {
            using (var conn = new SqlConnectionHelper(_connectionString))
            {
                var result = await conn.GetListAsync<TEntity>(condition);
                return result.AsQueryable();
            }
        }

        public async Task<IQueryable<dynamic>> GetByQuery(string query)
        {
            using (var conn = new SqlConnectionHelper(_connectionString))
            {
                var result = await conn.QueryAsync<dynamic>(query);
                return result.AsQueryable();
            }
        }

        public async Task<bool> DataExists(Dictionary<string, string> dataComparation)
        {
            using (var conn = new SqlConnectionHelper(_connectionString))
            {
                string comparations = string.Empty;

                foreach (var item in dataComparation)
                    comparations += string.Format(" AND {0} = {1} ", item.Key, item.Value);

                var result = await conn.GetListAsync<TEntity>(string.Format(" WHERE Deleted = 0 {0} ", comparations));

                return result.Any();
            }
        }
    }
}