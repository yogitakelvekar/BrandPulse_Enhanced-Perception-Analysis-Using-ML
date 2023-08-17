using BrandPulse.Application.Contracts.Infrastructure.Persistence;
using BrandPulse.Domain.Entities.Common;
using BrandPulse.SocialMediaData.TransformWorker.Data;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace BrandPulse.Persistence.Repositories
{
    public class BaseSqlRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly BrandPulseSqlDbContext _dbContext;

        public BaseSqlRepository(BrandPulseSqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            T? t = await _dbContext.Set<T>().FindAsync(id);
            return t;
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async virtual Task<IReadOnlyList<T>> GetPagedReponseAsync(int page, int size)
        {
            return await _dbContext.Set<T>().Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task BulkInsertAsync(IEnumerable<T> entities)
        {
            if (entities == null || !entities.Any()) return;

            // Set CreatedDate and LastModifiedDate for AuditableEntity before bulk insert
            foreach (var entity in entities.OfType<AuditableEntity>())
            {
                if (entity.CreatedDate == default)
                {
                    entity.CreatedDate = DateTime.Now;
                }

                entity.LastModifiedDate = DateTime.Now;
            }

            await _dbContext.BulkInsertAsync(entities);
        }

        public async Task<bool> SaveChangesAsync()
        {
            int nRowsAffected = await _dbContext.SaveChangesAsync();
            return nRowsAffected > 0;
        }
    }
}
