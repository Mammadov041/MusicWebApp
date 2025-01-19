using Microsoft.EntityFrameworkCore;
using MusicWebApp.Core.Abstract;
using MusicWebApp.Core.DataAccess.Abstract;
using System.Linq.Expressions;

namespace MusicWebApp.Core.DataAccess.EntityFramework
{
    public class EFEntityRepositoryBase<TEntity,TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext
    {
        private readonly TContext _context;

        public EFEntityRepositoryBase(TContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            var addedEntity = _context.Entry(entity);
            addedEntity.State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            var deletedEntity = _context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            var item = await _context.Set<TEntity>().SingleOrDefaultAsync(filter);
            return item;
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            var items = filter == null ? await _context.Set<TEntity>().ToListAsync() : await _context.Set<TEntity>().Where(filter).ToListAsync();
            return items;
        }

    }
}
